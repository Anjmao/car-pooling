using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class MatchMakerTests
    {
        Random rand = new Random();

        [TestMethod]
        public void Should_process_and_get_journeys()
        {
            var a = GetRandomPoint();
            var b = GetRandomPoint();
            var c = GetRandomPoint();
            var drivers = new List<Driver>();
            drivers.Add(new Driver("d1", GetRandomPoint(), GetRandomPoint()));

            var passengers = new List<Passenger>();
            passengers.Add(new Passenger("p1", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p2", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p3", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p4", GetRandomPoint(), GetRandomPoint()));

            var buckets = new List<RiderBucket>();
            buckets.Add(new RiderBucket { Driver = drivers[0], Passengers = passengers });

            var maker = new MatchMaker();
            maker.SetBuckets(buckets.AsEnumerable());
            var journeys = maker.Process();

            Assert.AreNotEqual(journeys.Count, 0);
        }

        //TODO: this test should return all drivers
        [TestMethod]
        public void Should_get_one_journey_for_three_passengers_and_match_two_passengers_only()
        {
            // Arrange
            var a = GetRandomPoint();
            var b = GetRandomPoint();
            var c = GetRandomPoint();
            var drivers = new List<Driver>();
            drivers.Add(new Driver("d1", new Coordinate(54.711096, 25.294731), new Coordinate(54.732556, 25.365460)));
            drivers.Add(new Driver("d2", new Coordinate(54.707961, 25.320016), new Coordinate(54.723409, 25.352168)));
            drivers.Add(new Driver("d3", new Coordinate(54.771149, 25.204108), new Coordinate(54.812915, 25.255607)));

            var passengers = new List<Passenger>();
            passengers.Add(new Passenger("p1", new Coordinate(54.715752, 25.321600), new Coordinate(54.719768, 25.345032)));
            passengers.Add(new Passenger("p2", new Coordinate(54.738497, 25.311547), new Coordinate(54.745434, 25.340557)));
            passengers.Add(new Passenger("p3", new Coordinate(54.697131, 25.359615), new Coordinate(54.708218, 25.389534)));

            var buckets = new List<RiderBucket>();
            buckets.Add(new RiderBucket { Driver = drivers[0], Passengers = passengers });
            buckets.Add(new RiderBucket { Driver = drivers[1], Passengers = passengers });
            buckets.Add(new RiderBucket { Driver = drivers[2], Passengers = passengers });

            // Act
            var maker = new MatchMaker();
            maker.SetBuckets(buckets.AsEnumerable());
            var journeys = maker.Process();

            journeys.ForEach(x => Console.WriteLine(x.ToString()));

            // Assert
            Assert.AreEqual(journeys.Count, 1);
        }

        private Coordinate GetRandomPoint()
        {
            var randomLat = this.GetRandomDoubleInRange(Constrains.MaxLatitudes[0], Constrains.MaxLatitudes[1]);
            var randomLong = this.GetRandomDoubleInRange(Constrains.MaxLongitudes[0], Constrains.MaxLongitudes[1]);
            return new Coordinate(randomLat, randomLong);
        }

        private double GetRandomDoubleInRange(double minimum, double maximum)
        {
            double randomVal = minimum + (maximum - minimum) * this.rand.NextDouble();
            return randomVal;
        }
    }


}
