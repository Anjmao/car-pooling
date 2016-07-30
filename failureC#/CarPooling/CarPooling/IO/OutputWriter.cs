using System.Collections.Generic;

namespace stem.IO
{
    using System.IO;
    using System.Reflection;
    using CarPooling.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }

    internal class OutputStructure
    {
        public List<Trip> Trips { get; set; }
    }

    internal class Trip
    {
        public List<Location> Locations { get; set; }
    }

    internal class Location
    {
        public string Id { get; set; }
    }

    public class OutputWriter
    {
        public void WriteOutput(string fileName, List<Journey> journeys)
        {
            var output = this.PrepareOutputStructure(journeys);

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            var outputJson = JsonConvert.SerializeObject(output, Formatting.Indented, settings);

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string location = Path.Combine(executableLocation, fileName);

            File.WriteAllText(location, outputJson);
        }

        private OutputStructure PrepareOutputStructure(List<Journey> journeys)
        {
            var result = new OutputStructure();
            result.Trips = new List<Trip>();

            foreach (var solutionItem in journeys)
            {
                var trip = new Trip();
                trip.Locations = new List<Location>();

                foreach (var waypoint in solutionItem.GetWaypoints())
                {
                    trip.Locations.Add(new Location() { Id = waypoint.Id });
                }

                result.Trips.Add(trip);
            }

            return result;
        }
    }
}
