using System;
using System.Collections.Generic;
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
    }
}
