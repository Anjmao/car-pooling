﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class RiderBucket
    {
        public RiderBucket()
        {
            this.Passengers = new List<Passenger>();
        }

        public Driver Driver { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
