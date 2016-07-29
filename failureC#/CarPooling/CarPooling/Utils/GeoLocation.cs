using System;
using CarPooling.Models;

namespace CarPooling.Utils
{
    public static class GeoLocation
    {
        private const double EARTH_RADIUS_KM = 6371;

        public static double GetDistance(Coordinate start, Coordinate end)
        {
            var lat = ToRadians(end.Latitude - start.Latitude);
            var lon = ToRadians(end.Longitude - start.Longitude);

            var d = Math.Pow(Math.Sin(lat / 2), 2)
                    + Math.Cos(ToRadians(start.Latitude)) * Math.Cos(ToRadians(end.Latitude))
                    * Math.Pow(Math.Sin(lon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(d), Math.Sqrt(1 - d));

            double distance = EARTH_RADIUS_KM * c;
            return distance;
        }

        public static double ToRadians(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }
}
