using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class DividerTests
    {
        [TestMethod]
        public void ShouldGroup()
        {
            var divider = new Divider();

            var orderings = new List<Booking>
            {
                new Booking { Id = "vaidotai -> darbas", Pickup = new Coordinate(54.601842, 25.176438), Dropoff = new Coordinate(54.711199, 25.293655),  IsDriver = true },
                new Booking { Id = "top", Pickup = new Coordinate(54.601842, 25.176438), Dropoff = new Coordinate(54.511415, 25.303790),  IsDriver = true },
                new Booking { Id = "left", Pickup = new Coordinate(54.717071, 25.221068), Dropoff = new Coordinate(54.716813, 25.100807),  IsDriver = true },
                new Booking { Id = "right", Pickup = new Coordinate(54.702995, 25.217565), Dropoff = new Coordinate(54.695785, 25.299121),  IsDriver = true },
                new Booking { Id = "p2",  Pickup = new Coordinate(0,0), Dropoff = new Coordinate(0,0), IsDriver = false },
                new Booking { Id = "p3",  Pickup = new Coordinate(0,0), Dropoff = new Coordinate(0,0), IsDriver = false },
                new Booking { Id = "p4",  Pickup = new Coordinate(54.735558, 25.22621115), Dropoff = new Coordinate(54.707950, 25.297634), IsDriver = true }
            };

            var riderBuckets = divider.Group(orderings);

            foreach (var riderBucket in riderBuckets)
            {
                var pickupBoundary = riderBucket.Driver.PickupBoundary;
                var dropoffBoundary = riderBucket.Driver.DropoffBoundary;

                //Console.WriteLine(riderBucket.Driver.Direction);

                Console.WriteLine(riderBucket.Driver.Pickup.Latitude + " , " + riderBucket.Driver.Pickup.Longitude + " <green-dot>");
                Console.WriteLine(riderBucket.Driver.Dropoff.Latitude + ", " + riderBucket.Driver.Dropoff.Longitude + " <green-dot>");

                Console.WriteLine(pickupBoundary.MinCoordinate.Latitude + ", " + pickupBoundary.MinCoordinate.Longitude);
                Console.WriteLine(pickupBoundary.MaxCoordinate.Latitude + ", " + pickupBoundary.MaxCoordinate.Longitude);
                Console.WriteLine(dropoffBoundary.MinCoordinate.Latitude + ", " + dropoffBoundary.MinCoordinate.Longitude);
                Console.WriteLine(dropoffBoundary.MaxCoordinate.Latitude + ", " + dropoffBoundary.MaxCoordinate.Longitude);
                Console.WriteLine(Environment.NewLine);
            }

            //Assert.AreEqual(2, riderBuckets.Count());
        }
    }
}
