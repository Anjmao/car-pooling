﻿using System;
using System.Collections.Generic;
using System.Linq;
using CarPooling.Models;
using Combinatorics.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPooling.Tests
{
    [TestClass]
    public class PermutatorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var combinations = new Variations<string>(this.CreateLetters(2), 4, GenerateOption.WithoutRepetition);

            //Assert.AreEqual(24, combinations.Count);
            Assert.IsTrue(combinations.FirstOrDefault(x => x[0] == "A" && x[1] == "B" && x[2] == "A" && x[3] == "B") != null);
        }


        private List<string> CreateLetters(int length)
        {
            var passengerChars = new List<string>();

            for (var i = Constrains.Buffer; i < Constrains.Buffer + length; i++)
            {
                var letter = (char)i;
                passengerChars.Add(letter.ToString());
                passengerChars.Add(letter.ToString());
            }

            return passengerChars;
        }
    }
}
