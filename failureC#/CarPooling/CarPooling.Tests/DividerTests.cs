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
                //new Booking { Id = "vaidotai -> darbas", Pickup = new Coordinate(54.601842, 25.176438), Dropoff = new Coordinate(54.711199, 25.293655),  IsDriver = true },
                //new Booking { Id = "top", Pickup = new Coordinate(54.511415, 25.303790), Dropoff = new Coordinate(54.601842, 25.176438),  IsDriver = true },
                //new Booking { Id = "left", Pickup = new Coordinate(54.717071, 25.221068), Dropoff = new Coordinate(54.716813, 25.100807),  IsDriver = true },
                //new Booking { Id = "right", Pickup = new Coordinate(54.702995, 25.217565), Dropoff = new Coordinate(54.695785, 25.299121),  IsDriver = true },

                new Booking { Id = "driver",  Pickup = new Coordinate(54.735558, 25.22621115), Dropoff = new Coordinate(54.707950, 25.297634), IsDriver = true },

                new Booking { Id = "pickup-outside-top->bottom-inside",  Pickup = new Coordinate(54.741341, 25.225346), Dropoff = new Coordinate(54.711378, 25.262083), IsDriver = false },
                new Booking { Id = "pickup-outside-left->bottom",  Pickup = new Coordinate(54.727646, 25.190909), Dropoff = new Coordinate(54.714786, 25.213182), IsDriver = false },
                new Booking { Id = "pickup-outside-right->left",  Pickup = new Coordinate(54.739237, 25.309430), Dropoff = new Coordinate(54.713442, 25.246796), IsDriver = false },
                new Booking { Id = "pickup-outside-bottom->top",  Pickup = new Coordinate(54.700294, 25.275542), Dropoff = new Coordinate(54.732859, 25.242382), IsDriver = false },
                new Booking { Id = "pickup-inside-bottom->top",  Pickup = new Coordinate(54.731550, 25.231383), Dropoff = new Coordinate(54.710903, 25.264943), IsDriver = false },
                new Booking { Id = "pickup-inside-right->left",  Pickup = new Coordinate(54.733216, 25.262626), Dropoff = new Coordinate(54.717084, 25.221233), IsDriver = false },
                new Booking { Id = "pickup-inside-bottom->top-wrong-direction",  Pickup = new Coordinate(54.716362, 25.270920), Dropoff = new Coordinate(54.729535, 25.239405), IsDriver = false },
                new Booking { Id = "pickup-inside-bottom->right-wrong-direction",  Pickup = new Coordinate(54.713969, 25.235941), Dropoff = new Coordinate(54.727066, 25.276119), IsDriver = false },
            };

            var riderBuckets = divider.Group(orderings);

            foreach (var riderBucket in riderBuckets)
            {
                var pickupBoundary = riderBucket.Driver.PickupBoundary;
                var dropoffBoundary = riderBucket.Driver.DropoffBoundary;

                Console.WriteLine(riderBucket.Driver.Pickup.Latitude + " , " + riderBucket.Driver.Pickup.Longitude + " <green-dot>");
                Console.WriteLine(riderBucket.Driver.Dropoff.Latitude + ", " + riderBucket.Driver.Dropoff.Longitude + " <green-dot>");

                Console.WriteLine(pickupBoundary.MinCoordinate.Latitude + ", " + pickupBoundary.MinCoordinate.Longitude);
                Console.WriteLine(pickupBoundary.MaxCoordinate.Latitude + ", " + pickupBoundary.MaxCoordinate.Longitude);
                Console.WriteLine(dropoffBoundary.MinCoordinate.Latitude + ", " + dropoffBoundary.MinCoordinate.Longitude);
                Console.WriteLine(dropoffBoundary.MaxCoordinate.Latitude + ", " + dropoffBoundary.MaxCoordinate.Longitude);
                Console.WriteLine(Environment.NewLine);

                foreach (var passenger in riderBucket.Passengers)
                {
                    Console.WriteLine(passenger.Pickup.Latitude + " , " + passenger.Pickup.Longitude + " <tan-dot>");
                    Console.WriteLine(passenger.Dropoff.Latitude + ", " + passenger.Dropoff.Longitude + " <yellow-dot>");
                }

                Assert.AreEqual(3, riderBucket.Passengers.Count);
            }
        }
    }
}
