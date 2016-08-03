using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public Coordinate Pickup { get; set; }
        public Coordinate Dropoff { get; set; }
        public bool IsDriver { get; set; }
    }
}
