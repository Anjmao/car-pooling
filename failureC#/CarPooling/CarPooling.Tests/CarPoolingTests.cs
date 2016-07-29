using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stem.IO;

namespace CarPooling.Tests
{
    [TestClass]
    public class CarPoolingTests
    {
        [TestMethod]
        public void Should_process_and_output()
        {
            var service = new CarPoolingService(new IO.InputParser(), new MatchMaker(), new Divider());

            var journeys = service.Process().Take(10).ToList();

            var outputWriter = new OutputWriter();
            outputWriter.WriteOutput("dd-output.json", journeys);
        }
    }
}
