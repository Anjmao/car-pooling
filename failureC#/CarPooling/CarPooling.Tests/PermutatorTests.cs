using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class PermutatorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var passengers = new List<char>() { 'A', 'A', 'B', 'B'};
            var combinations = Permutator.GetPermutations<char>(passengers, 2 * 2).Select(x => x.ToList()).ToList();

            Assert.AreEqual(combinations.Count, 24);
        }
    }
}
