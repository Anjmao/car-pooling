using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Interfaces;

namespace CarPooling.Models
{
    public abstract class Rider : IRider
    {
        public string Id { get; set; }
        public Coordinate Pickup { get; set; }
        public Coordinate Dropoff { get; set; }
        public double FlyingDistance { get; set; }

        protected Rider(string id, Coordinate pickup, Coordinate dropoff)
        {
            this.Id = id;
            this.Pickup = pickup;
            this.Dropoff = dropoff;
        }
    }
}
