using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling.Models;
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

            var journeys = service.Process("input-2016-01-29-evening.json").ToList();

            var outputWriter = new OutputWriter();
            outputWriter.WriteOutput("dd-out.json", journeys);
        }
    }
}
