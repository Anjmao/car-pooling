using System.Collections.Generic;

namespace CarPooling.Models
{
    public class Coordinate : IEqualityComparer<Coordinate>
    {
        // Needed only for test team
        public string Id { get; set; }
        public string RiderId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinate(double latitude, double longtitude)
        {
            this.Latitude = latitude;
            this.Longitude = longtitude;
        }

        public bool Equals(Coordinate one, Coordinate two)
        {
            return one.Latitude == two.Latitude && one.Longitude == two.Longitude && one.RiderId == two.RiderId;
        }

        public int GetHashCode(Coordinate item)
        {
            return this.Latitude.GetHashCode() ^ this.Longitude.GetHashCode() ^ this.RiderId.GetHashCode();
        }
    }
}