using System.Collections.Generic;
using System.Linq;

namespace CarPooling.IO
{
    using System.IO;
    using System.Reflection;
    using Models;
    using Newtonsoft.Json;

    public class InputParser
    {
        class JsonCoordinate
        {
            public string Id { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
            public string Zone { get; set; }
        }

        class JsonBooking
        {
            public string Id { get; set; }
            public JsonCoordinate Pickup { get; set; }
            public JsonCoordinate Dropoff { get; set; }
            public bool IsDriver { get; set; }
        }

        class Input
        {
            public List<JsonBooking> Bookings { get; set; }
        }

        public List<Booking> ReadInput(string fileName)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string location = Path.Combine(executableLocation, fileName);

            var fileContent = File.ReadAllText(location);

            var inputSet = JsonConvert.DeserializeObject<Input>(fileContent);

            return this.ConvertToDataSet(inputSet);
        }

        private List<Booking> ConvertToDataSet(Input input)
        {

            var bookings = input.Bookings.Select(x => new Booking
            {
                Id = x.Id,
                Pickup = new Coordinate(x.Pickup.Lat, x.Pickup.Lon) { Id = x.Pickup.Id, RiderId = x.Id },
                Dropoff = new Coordinate(x.Dropoff.Lat, x.Dropoff.Lon) { Id = x.Dropoff.Id, RiderId = x.Id },
                IsDriver = x.IsDriver
            });

            return bookings.ToList();
        }
    }
}
