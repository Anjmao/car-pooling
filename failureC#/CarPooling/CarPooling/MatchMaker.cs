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
        private IEnumerable<RiderBucket> bucket;
        
        public void SetBuckets(IEnumerable<RiderBucket> bucket)
        {
            this.bucket = bucket;
        }

        public List<Journey> Process()
        {
            var journeys = this.ComputeJourneys();
            return journeys;
        }

        private List<Journey> ComputeJourneys()
        {
            var journeys = new List<Journey>();

            foreach (var item in this.bucket)
            {
                var orderings = new Combinations<char>(this.CreateLetters(item.Passengers.Count), 4, GenerateOption.WithoutRepetition);

                foreach (var ordering in orderings)
                {
                    var waypoints = this.CreateWaypoins(ordering, item.Passengers);
                    var journey = new Journey();
                    journey.SetDriver(item.Driver);
                    journey.SetPassengers(item.Passengers);
                    journey.SetWaypoints(waypoints);
                    journey.ComputeRoute();

                    journeys.Add(journey);
                }
            }

            journeys = journeys.OrderBy(x => x.TotalDistance).ToList();

            return journeys;
        }

        private HashSet<Coordinate> CreateWaypoins(IList<char> ordering, IList<Passenger> passengers)
        {
            var waypoints = new HashSet<Coordinate>();

            foreach (var letter in ordering)
            {
                var curPassengerIndex = (int)letter - Constrains.Buffer;
                var currentPassenger = passengers[curPassengerIndex];
                var addOrigin = waypoints.Add(currentPassenger.Pickup);
                // if addOrigin is false, it was already added to the set
                if (addOrigin)
                {
                    waypoints.Add(currentPassenger.Dropoff);
                }
            }

            return waypoints;
        }

        private List<char> CreateLetters(int length)
        {
            var passengerChars = new List<char>();

            for (var i = Constrains.Buffer; i < Constrains.Buffer + length; i++)
            {
                var letter = (char)i;
                passengerChars.Add(letter);
                passengerChars.Add(letter);
            }

            return passengerChars;
        }
    }

}
