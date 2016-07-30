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
        private List<RiderBucket> buckets;
        private HashSet<string> matchedPassengers = new HashSet<string>();


        public void SetBuckets(IEnumerable<RiderBucket> buckets)
        {
            this.buckets = buckets.ToList();
        }

        public List<Journey> Process()
        {
            var matchedJourneys = new List<Journey>();

            var journeys = this.ComputeJourneys();
            var i = journeys.Count;
            while (i > 0)
            {
                var bestJourney = journeys[0];

                // if no more passengers break this loop;
                if (bestJourney.Passengers == null)
                {
                    matchedJourneys.Add(bestJourney);
                    break;
                }
                
                matchedJourneys.Add(bestJourney);
                AddMatchedPassengers(bestJourney);
                RemoveMatchedPassengersFromBuckets(bestJourney.Driver.Id);
                
                journeys = this.ComputeJourneys();
                i = journeys.Count;
            }
            
            
            return matchedJourneys;
        }

        private void AddMatchedPassengers(Journey journey)
        {
            foreach (var item in journey.GetWaypoints())
            {
                this.matchedPassengers.Add(item.RiderId);
            }
        }

        private void RemoveMatchedPassengersFromBuckets(string driverId)
        {
            this.buckets.RemoveAll(x => x.Driver.Id == driverId);
            foreach (var item in this.buckets)  
            {
                item.Passengers.RemoveAll(x => this.matchedPassengers.Contains(x.Id));
            }
        }
        
        private List<Journey> ComputeJourneys()
        {
            var journeys = new List<Journey>();

            foreach (var item in this.buckets)
            {
                // TODO: OMG this soooo so bad
                item.Passengers = item.Passengers.Where(x => x.IsMatched == false).ToList();

                // if no passengers return only driver journey
                if (item.Passengers.Count == 0)
                {
                    var waypoints = this.CreateWaypoins(null, null, item.Driver);
                    var journey = new Journey();
                    journey.Driver = item.Driver;
                    journey.Passengers = null;
                    journey.SetWaypoints(waypoints);
                    journey.ComputeRoute();
                    journeys.Add(journey);

                    return journeys;
                }

                // TODO: cache to dictionary by passengers count
                var orderings = new Combinations<char>(this.CreateLetters(item.Passengers.Count), 4, GenerateOption.WithoutRepetition);

                foreach (var ordering in orderings)
                {
                    var waypoints = this.CreateWaypoins(ordering, item.Passengers, item.Driver);
                    var journey = new Journey();
                    journey.Driver = item.Driver;
                    journey.Passengers = item.Passengers;
                    journey.SetWaypoints(waypoints);
                    journey.ComputeRoute();

                    journeys.Add(journey);
                }
            }

            // TODO: implement trip score
            journeys = journeys.OrderBy(x => x.TotalDistance).ToList();
            
            return journeys;
        }

        private HashSet<Coordinate> CreateWaypoins(IList<char> ordering, IList<Passenger> passengers, Driver driver)
        {
            var waypoints = new HashSet<Coordinate>();

            waypoints.Add(driver.Pickup);

            if (ordering != null && passengers != null)
            {
                foreach (var letter in ordering)
                {
                    var currentPassenger = passengers[(int)letter - Constrains.Buffer];

                    var addOrigin = waypoints.Add(currentPassenger.Pickup);
                    // if addOrigin is false, it was already added to the set
                    if (addOrigin)
                    {
                        waypoints.Add(currentPassenger.Dropoff);
                    }
                }
            }
            
            waypoints.Add(driver.Dropoff);

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
