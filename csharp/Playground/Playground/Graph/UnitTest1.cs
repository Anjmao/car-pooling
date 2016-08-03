using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace Playground.Graph
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var edges = new SEdge<int>[] {
                new SEdge<int>(1, 2),
                new SEdge<int>(0, 1)
            };

            var graph = edges.ToAdjacencyGraph<int, SEdge<int>>();
        }
    }
}
