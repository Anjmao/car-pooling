using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PermutatorTests
    {
        [TestMethod]
        public void Create_permutatios_for_two_passengers()
        {
            var passengers = CreatePassengersChars(2);

            var p = Permutator.Combinations(passengers, 2);

            Assert.AreEqual(p.Count(), 2);
        }

        private List<string> CreatePassengersChars(int length)
        {
            var result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                var letter = (char)i;
                result.Add(letter.ToString());
            }
            return result;
        }
    }
}
