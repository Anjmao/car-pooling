﻿using CarPooling.Models;
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

        private IEnumerable<RiderBucket> Group(IEnumerable<Booking> orderings)
        {
            var riderBucket = new HashSet<RiderBucket>();

            var ordered = orderings.OrderBy(x => x.Pickup.Longitude);
            var riders = new HashSet<Booking>(ordered);

            DistinctDrivers(riders);

            foreach (var driver in drivers)
            {
                var bucket = new RiderBucket { Driver = driver };

                driver.FlyingDistance = GeoLocation.GetDistance(driver.Pickup, driver.Dropoff);
                driver.ComputeBoundaries();

                foreach (var passenger in passengers)
                {
                    if (driver.Bounds(passenger))
                    {
                        if (driver.DrivesInSameDirection(passenger))
                        {
                            bucket.Passengers.Add(passenger);
                        }
                    }
                }

                riderBucket.Add(bucket);
            }

            return riderBucket;
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
