using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class Journey
    {
        private Driver driver;
        private List<Passenger> passengers;
        private string[] ordering;
        private List<Coordinate> waypoints;

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

        public void SetOrdering(string[] ordering)
        {
            this.ordering = ordering;
        }

        public void SetWaypoints(List<Coordinate> waypoints)
        {
            this.waypoints = waypoints;
        }

        public void ComputeRoute()
        {
            var cur = driver.Pickup;
            this.TotalDistance = 0;
            foreach (var waypoint in this.waypoints)
            {

            }
        }
    }
}
