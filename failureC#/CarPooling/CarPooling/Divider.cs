using CarPooling.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using CarPooling.Utils;

namespace CarPooling
{
    public class Divider
    {
        private HashSet<Driver> drivers = new HashSet<Driver>();
        private HashSet<Passenger> passengers = new HashSet<Passenger>();

        private void Group(IEnumerable<Booking> orderings)
        {
            var ordered = orderings.OrderBy(x => x.Pickup.Longitude);
            var riders = new HashSet<Booking>(ordered);

            DistinctDrivers(riders);

            foreach (var driver in drivers)
            {
                driver.FlyingDistance = GeoLocation.GetDistance(driver.Pickup, driver.Dropoff);
            }

        }

        private void DistinctDrivers(HashSet<Booking> riders)
        {
            foreach (var rider in riders)
            {
                if (rider.IsDriver)
                {
                    drivers.Add(new Driver(rider.Id, rider.Pickup, rider.Dropoff));
                }
                else
                {
                    passengers.Add(new Passenger(rider.Id, rider.Pickup, rider.Dropoff));
                }
            }
        }
    }
}
