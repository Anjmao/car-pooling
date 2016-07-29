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
        private Driver driver;
        private List<Passenger> passengers;
        private HashSet<Coordinate> waypoints;

        public double TotalDistance { get; set; }
        private double TotalTime { get; set; }

        public void SetDriver(Driver driver)
        {
            this.driver = driver;
        }

        public void SetPassengers(List<Passenger> passengers)
        {
            this.passengers = passengers;
        }

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
            var cur = driver.Pickup;
            this.TotalDistance = 0;
            foreach (var waypoint in this.waypoints)
            {
                this.TotalDistance += GeoLocation.GetDistance(cur, waypoint);
                cur = waypoint;
            }

            var end = this.driver.Dropoff;
            this.TotalDistance += GeoLocation.GetDistance(cur, end);
            this.TotalTime = 50 * this.TotalDistance;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            int index = 0;
            int length = this.waypoints.Count;

            foreach (var item in this.waypoints)
            {
                if (index == 0)
                {
                    builder.AppendLine(string.Format("{0}, {1} <green-dot>", item.Latitude.ToString(CultureInfo.InvariantCulture), item.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                else if (index == length - 1)
                {
                    builder.AppendLine(string.Format("{0}, {1} <green-dot>", item.Latitude.ToString(CultureInfo.InvariantCulture), item.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                else
                {
                    builder.AppendLine(string.Format("{0}, {1}", item.Latitude.ToString(CultureInfo.InvariantCulture), item.Longitude.ToString(CultureInfo.InvariantCulture)));
                }
                index++;
            }
            

            return builder.ToString();
        }
    }
}
