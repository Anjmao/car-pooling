using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class Passenger : Rider
    {
        public Passenger(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        public bool IsMatched { get; set; }
    }
}
