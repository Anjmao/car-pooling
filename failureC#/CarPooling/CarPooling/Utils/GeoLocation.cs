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

        public static string GetDirection(Coordinate pickup, Coordinate dropoff)
        {
            double bearing = GetBearing(pickup.Latitude, pickup.Longitude, dropoff.Latitude, dropoff.Longitude);

            if (bearing >= 337.5 || bearing <= 22.5) return "N";
            if (bearing > 22.5 && bearing <= 67.5) return "NE";
            if (bearing > 67.5 && bearing <= 112.5) return "E";
            if (bearing > 112.5 && bearing <= 157.5) return "SE";
            if (bearing > 157.5 && bearing <= 202.5) return "S";
            if (bearing > 202.5 && bearing <= 247.5) return "SW";
            if (bearing > 247.5 && bearing <= 292.5) return "W";
            if (bearing > 292.5 && bearing < 337.5) return "NW";

            return String.Empty;
        }

        public static double GetBearing(double originLatitude, double originLongitude, double destinationLatitude, double destinationLongitude)
        {
            var destinationRadian = ToRadians(destinationLongitude - originLongitude);
            var destinationPhi = Math.Log(Math.Tan(ToRadians(destinationLatitude) / 2 + Math.PI / 4) / Math.Tan(ToRadians(originLatitude) / 2 + Math.PI / 4));

            if (Math.Abs(destinationRadian) > Math.PI)
                destinationRadian = destinationRadian > 0
                                        ? -(2 * Math.PI - destinationRadian)
                                        : (2 * Math.PI + destinationRadian);

            return Math.Atan2(destinationRadian, destinationPhi).ToBearing();
        }

        public static double ToBearing(this double r)
        {
            double degrees = ToDegrees(r);
            return (degrees + 360) % 360;
        }

        public static double ToDegrees(this double r)
        {
            return r * 180 / Math.PI;
        }

        public static Boundary GetBoundary(Coordinate point, double distance)
        {
            const double oneDegreeInKm = 110.574;

            var latitudeConversionFactor = distance / oneDegreeInKm;
            var longitudeConversionFactor = distance / oneDegreeInKm / Math.Abs(Math.Cos(GeoLocation.ToRadians(point.Latitude)));


            var minLatitude = point.Latitude - latitudeConversionFactor;
            var minLongitude = point.Longitude - longitudeConversionFactor;

            var maxLatitude = point.Latitude + latitudeConversionFactor;
            var maxLongitude = point.Longitude + longitudeConversionFactor;

            var boundary = new Boundary
            {
                MinCoordinate = new Coordinate(minLatitude, minLongitude),
                MaxCoordinate = new Coordinate(maxLatitude, maxLongitude)
            };

            return boundary;
        }
    }
}
