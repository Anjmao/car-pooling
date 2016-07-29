using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Models;

namespace CarPooling.Interfaces
{
    public interface IRider
    {
        string Id { get; set; }

        Coordinate Pickup { get; set; }
        Coordinate Dropoff { get; set; }

        decimal FlyingDistance { get; set; }
    }
}
