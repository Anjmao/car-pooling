using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Models;
using Combinatorics.Collections;

namespace CarPooling
{
    
    public class MatchMaker
    {
        private List<Driver> drivers = new List<Driver>();
        private List<Passenger> passengers = new List<Passenger>();
        
        public MatchMaker(List<Driver> drivers, List<Passenger> passengers)
        {
            this.drivers = drivers;
            this.passengers = passengers;
        }

        public List<Journey> Process()
        {
            var journeys = this.ComputeJourneys();
            return journeys;
        }

        private List<Journey> ComputeJourneys()
        {
            var journeys = new List<Journey>();
            foreach (var driver in this.drivers)
            {
                var orderings = new Combinations<char>(this.CreateLetters(), 4, GenerateOption.WithoutRepetition);

                foreach (var ordering in orderings)
                {
                    var waypoints = this.CreateWaypoins(ordering);
                    var journey = new Journey();
                    journey.SetDriver(driver);
                    journey.SetPassengers(this.passengers);
                    journey.SetWaypoints(waypoints);
                    journey.ComputeRoute();

                    journeys.Add(journey);
                }
            }

            return journeys;
        }

        private HashSet<Coordinate> CreateWaypoins(IList<char> ordering)
        {
            var waypoints = new HashSet<Coordinate>();

            foreach (var letter in ordering)
            {
                var curPassengerIndex = (int)letter - Constrains.Buffer;
                var currentPassenger = this.passengers[curPassengerIndex];
                var addOrigin = waypoints.Add(currentPassenger.Pickup);
                // if addOrigin is false, it was already added to the set
                if (addOrigin)
                {
                    waypoints.Add(currentPassenger.Dropoff);
                }
            }

            return waypoints;
        }

        private List<char> CreateLetters()
        {
            var passengerChars = new List<char>();

            for (var i = Constrains.Buffer; i < Constrains.Buffer + this.passengers.Count; i++)
            {
                var letter = (char)i;
                passengerChars.Add(letter);
                passengerChars.Add(letter);
            }

            return passengerChars;
        }
    }

}
