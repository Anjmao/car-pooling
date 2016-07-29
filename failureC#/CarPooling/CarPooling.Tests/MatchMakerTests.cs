using System;
using System.Collections.Generic;
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

            var maker = new MatchMaker(drivers, passengers);
            var journeys = maker.Process();
            Assert.AreNotEqual(journeys.Count, 0);
        }

        [TestMethod]
        public void Should_process_and_get_journeys_2()
        {
            var a = GetRandomPoint();
            var b = GetRandomPoint();
            var c = GetRandomPoint();
            var drivers = new List<Driver>();
            drivers.Add(new Driver("d1", new Coordinate(54.711096, 25.294731), new Coordinate(54.732556, 25.365460)));

            var passengers = new List<Passenger>();
            passengers.Add(new Passenger("p1", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p2", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p3", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p4", GetRandomPoint(), GetRandomPoint()));

            var maker = new MatchMaker(drivers, passengers);
            var journeys = maker.Process();
            Assert.AreNotEqual(journeys.Count, 0);
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
