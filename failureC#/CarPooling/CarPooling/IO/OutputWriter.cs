//using System.Collections.Generic;

//namespace stem.IO
//{
//    using System.IO;

//    using Newtonsoft.Json;

//    using stem.Core;

//    internal class OutputStructure
//    {
//        public List<Trip> Trips { get; set; }
//    }

//    internal class Trip
//    {
//        public List<Location> Locations { get; set; }
//    }

//    internal class Location
//    {
//        public string Id { get; set; }
//    }

//    public class OutputWriter
//    {
//        public void WriteOutput(string fileName, Dictionary<Booking, CarPoolOption> solution)
//        {
//            var output = this.PrepareOutputStructure(solution);

//            var outputJson = JsonConvert.SerializeObject(output);

//            File.WriteAllText(fileName, outputJson);
//        }

//        private OutputStructure PrepareOutputStructure(Dictionary<Booking, CarPoolOption> solution)
//        {
//            var result = new OutputStructure();
//            result.Trips = new List<Trip>();

//            foreach (var solutionItem in solution)
//            {
//                var trip = new Trip();
//                trip.Locations = new List<Location>();

//                // driver pickup, passenger pickup, passenger dropoff, driver dropoff
//                trip.Locations.Add(new Location() {Id = solutionItem.Value.Driver.Pickup.Id});
//                trip.Locations.Add(new Location() {Id = solutionItem.Key.Pickup.Id});
//                trip.Locations.Add(new Location() {Id = solutionItem.Key.Dropoff.Id});
//                trip.Locations.Add(new Location() {Id = solutionItem.Value.Driver.Pickup.Id });

//                result.Trips.Add(trip);
//            }

//            return result;
//        }
//    }
//}
