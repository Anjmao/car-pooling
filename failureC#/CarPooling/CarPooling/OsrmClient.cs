using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Models;
using CarPooling.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarPooling
{
    public class Routes
    {
        public double Distance;
        
        public double Duration;
        
        public Legs[] Legs;
        
        public string Geometry;
    }

    public class Legs
    {
        public double Distance;
        
        public double Duration;
        
        public string Summary;
        
        public object[] Steps;
    }

    public class Waypoints
    {
        public double[] Location;
        
        public string Name;
        
        public string Hint;
    }

    public class RouteResponse
    {
        public Waypoints[] Waypoints;

        public Routes[] Routes;
    }

    public class OsrmClient: ClientBase
    {

        public OsrmClient(): base(new Uri("http://localhost:5000"))
        {
        }

        public Task<RouteResponse> GetRoutes(IEnumerable<Coordinate> coordinates)
        {
            var str = new StringBuilder();
            foreach (var item in coordinates)
            {
                str.Append(string.Format("{0},{1};", 
                    item.Longitude.ToString(CultureInfo.InvariantCulture), 
                    item.Latitude.ToString(CultureInfo.InvariantCulture)));
                
            }

            // remove last brace
            str.Remove(str.Length - 1, 1);

            return this.GetAsync<RouteResponse>("/route/v1/car/" + str.ToString());
        }

        protected override JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
