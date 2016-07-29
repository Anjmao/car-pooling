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

        public List<Journey> Process()
        {
            var letters = this.CreateLetters();
            var orderings = Permutator.GetPermutations<char>(letters, 4).Select(x => x.ToList()).ToList();

            throw new NotImplementedException();
        }

        private Journey ComputeJourneys(char[][] orderings)
        {
            foreach (var driver in this.drivers)
            {
                foreach(var ordering in orderings)
                {
                    var waypoints = this.CreateWaypoins(ordering);
                }
            }

            throw new NotImplementedException();
        }

        private HashSet<Coordinate> CreateWaypoins(char[] ordering)
        {
            var waypoints = new HashSet<Coordinate>();

            foreach (var letter in ordering)
            {
                var curPassengerIndex = (int)letter - this.buffer;
                var currentPassenger = this.passengers[curPassengerIndex];
                var addOrigin = waypoints.Add(currentPassenger.Pickup);
                // if addOrigin is false, it was already added to the set
                if (!addOrigin)
                {
                    waypoints.Add(currentPassenger.Dropoff);
                }
            }

            return waypoints;
        }

        private List<char> CreateLetters()
        {
            var passengerChars = new List<char>();

            for (var i = this.buffer; i < this.buffer + this.passengers.Count; i++)
            {
                var letter = (char)i;
                passengerChars.Add(letter);
                passengerChars.Add(letter);
            }

            return passengerChars;
        }
    }

}
