using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Utils;

namespace CarPooling.Models
{
    public class Journey
    {
        public Driver Driver { get; set; }
        public List<Passenger> Passengers { get; set; }

        private HashSet<Coordinate> waypoints;

        public double TotalDistance { get; set; }
        private double TotalTime { get; set; }
        
        public void SetWaypoints(HashSet<Coordinate> waypoints)
        {
            this.waypoints = waypoints;
        }

        public HashSet<Coordinate> GetWaypoints()
        {
            return this.waypoints;
        }

        //TODO: calculate from OSRM
        public void ComputeRoute()
        {
            var cur = Driver.Pickup;
            this.TotalDistance = 0;
            foreach (var waypoint in this.waypoints)
            {
                this.TotalDistance += GeoLocation.GetDistance(cur, waypoint);
                cur = waypoint;
            }

            var end = this.Driver.Dropoff;
            this.TotalDistance += GeoLocation.GetDistance(cur, end);
            this.TotalTime = 50 * this.TotalDistance;
        }

        public async Task ComputeOsrmRoute()
        {
            using(var client = new OsrmClient())
            {
                var result = await client.GetRoutes(this.waypoints);
                var route = result.Routes[0];
                this.TotalDistance = route.Distance;
                this.TotalTime = route.Duration;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            int index = 0;
            int length = this.waypoints.Count;

            foreach (var item in this.waypoints)
            {
                var coordinate = item;
                if (index == 0)
                {
                    builder.AppendLine(string.Format("{0}, {1} <green-dot>", coordinate.Latitude.ToString(CultureInfo.InvariantCulture), coordinate.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                else if (index == length - 1)
                {
                    builder.AppendLine(string.Format("{0}, {1} <green-dot>", coordinate.Latitude.ToString(CultureInfo.InvariantCulture), coordinate.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                else
                {
                    builder.AppendLine(string.Format("{0}, {1}", coordinate.Latitude.ToString(CultureInfo.InvariantCulture), coordinate.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                index++;
            }
            

            return builder.ToString();
        }
    }
}
