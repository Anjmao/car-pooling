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
        private int buffer = 65;

        public MatchMaker(List<Driver> drivers, List<Passenger> passengers)
        {
            this.drivers = drivers;
            this.passengers = passengers;
        }

        public void Process()
        {
            var letters = this.CreateLetters();
            var orderings = Permutator.GetPermutations<string>(letters, 4).Select(x => x.ToList()).ToList();
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

        private List<string> CreateLetters()
        {
            var passengerChars = new List<string>();

            for (var i = this.buffer; i < this.buffer + this.passengers.Count; i++)
            {
                var letter = (char)i;
                passengerChars.Add(i.ToString());
                passengerChars.Add(i.ToString());
            }

            return passengerChars;
        }
    }

}
