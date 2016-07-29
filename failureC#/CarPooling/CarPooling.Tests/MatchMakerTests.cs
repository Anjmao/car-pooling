using System;
using System.Collections.Generic;
using CarPooling.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class MatchMakerTests
    {
        [TestMethod]
        public void Should_process()
        {
            var drivers = new List<Driver>();
            drivers.Add(new Driver("d1", GetRandomPoint(), GetRandomPoint()));

            var passengers = new List<Passenger>();
            passengers.Add(new Passenger("p1", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p2", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p3", GetRandomPoint(), GetRandomPoint()));
            passengers.Add(new Passenger("p4", GetRandomPoint(), GetRandomPoint()));

            var maker = new MatchMaker(drivers, passengers);
            var journeys = maker.Process();
        }

        private static Coordinate GetRandomPoint()
        {
            var randomLat = getRandomDoubleInRange(Constrains.MaxLatitudes[0], Constrains.MaxLatitudes[1]);
            var randomLong = getRandomDoubleInRange(Constrains.MaxLongitudes[0], Constrains.MaxLongitudes[1]);
            return new Coordinate(randomLat, randomLong);
        }

        private static double getRandomDoubleInRange(double minimum, double maximum)
        {
            Random rand = new Random();
            double randomVal = minimum + (maximum - minimum) * rand.NextDouble();
            return randomVal;
        }
    }


}
