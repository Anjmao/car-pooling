using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class CarPoolingTests
    {
        [TestMethod]
        public void Should_process()
        {
            var service = new CarPoolingService(new IO.InputParser(), new MatchMaker(), new Divider());

            var journeys = service.Process();
        }
    }
}
