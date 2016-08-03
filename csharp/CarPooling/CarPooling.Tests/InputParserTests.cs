using System;
using CarPooling.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class InputParserTests
    {
        [TestMethod]
        public void Should_read_input()
        {
            var parser = new InputParser();

            var result = parser.ReadInput("dd-input.json");
            var first = result[0];

            Assert.AreEqual(first.Id, "GG");
            Assert.AreEqual(first.Pickup.Latitude, 54.797079);
            Assert.AreEqual(first.Pickup.Longitude, 25.292601);
            Assert.AreEqual(first.Dropoff.Latitude, 54.711295);
            Assert.AreEqual(first.Dropoff.Longitude, 25.293195);
            Assert.AreEqual(first.IsDriver, true);
        }
    }
}
