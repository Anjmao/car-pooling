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
            //drivers.Add(new Driver("d1"))
        }

        private static double getRandomDoubleInRange(double minimum, double maximum)
        {
            Random rand = new Random();
            double randomVal = minimum + (maximum - minimum) * rand.NextDouble();
            return randomVal;
        }
    }


}
