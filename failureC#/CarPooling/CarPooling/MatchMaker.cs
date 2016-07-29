using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Models;

namespace CarPooling
{

    
    public class MatchMaker
    {
        private List<Driver> drivers = new List<Driver>();
        private List<Passenger> passengers = new List<Passenger>();

        public void Process()
        {

        }

        private Journey ComputeJourneys(string[][] orderings)
        {
            foreach (var driver in this.drivers)
            {
                foreach(var ordering in orderings)
                {

                }
            }

            throw new NotFiniteNumberException();
        }
    }

}
