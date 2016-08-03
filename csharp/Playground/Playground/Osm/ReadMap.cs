using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsmSharp.Geo.Geometries;
using OsmSharp.Math.Geo;
using OsmSharp.Osm.PBF.Streams;
using OsmSharp.Osm.Streams.Filters;

namespace Playground.Osm
{
    [TestClass]
    public class ReadMap : TestBase
    {
        [TestMethod]
        public void Should_read_elements()
        {
            using (var fileStream = new FileInfo(Path.Combine(TestBase.BinPath, "lithuania.osm.pbf")).OpenRead())
            {
                var source = new PBFOsmStreamSource(fileStream);
                foreach (var element in source.Take(10))
                {
                    Debug.WriteLine(element.ToString());
                }

                //Debug.WriteLine(source.Count() + " total");

                //Assert.IsTrue(source.Count() > 0);

                var filter = new OsmStreamFilterPoly(
                    new LineairRing(
                    new GeoCoordinate(54.715086, 25.286772),
                    new GeoCoordinate(54.720391, 25.302308)));
                filter.RegisterSource(source);

                Debug.WriteLine(filter.Count() + " filtered");
            }
        }
    }
}
