﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class Constrains
    {
        public const int Buffer = 65;
        public static double[] MaxLatitudes = new double[] { 56.27, 53.53 };
        public static double[] MaxLongitudes = new double[] { 20.56, 26.50 };
        public static int MaxPassengersInCar = 2;
    }
}
