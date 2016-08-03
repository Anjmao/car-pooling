using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class OsrmClientTests
    {
        OsrmClient client = new OsrmClient();

        [TestMethod]
        public void Should_query_routes()
        {
            var routes = new List<Coordinate>()
            {
                new Coordinate(54.638912, 25.189396),
                new Coordinate(54.667473, 25.237753),
                new Coordinate(54.671646, 25.219565),
                new Coordinate(54.687837, 25.211626),
            };

            var result = client.GetRoutes(routes.AsEnumerable()).Result;

            Assert.AreEqual(result.Routes.Count(), 1);
        }
    }
}
