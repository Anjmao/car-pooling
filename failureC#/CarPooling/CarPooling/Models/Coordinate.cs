namespace CarPooling.Models
{
    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinate(double latitude, double longtitude)
        {
            this.Latitude = latitude;
            this.Longitude = longtitude;
        }
    }
}