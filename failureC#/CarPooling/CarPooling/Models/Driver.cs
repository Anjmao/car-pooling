using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.Utils;

namespace CarPooling.Models
{
    public class Driver : Rider
    {
        public Boundary PickupBoundary { get; set; }
        public Boundary DropoffBoundary { get; set; }
        public string Direction { get; set; }

        public Driver(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        public void ComputeBoundaries()
        {
            var distance = this.FlyingDistance / 2;

            this.PickupBoundary = this.GetBoundary(this.Pickup, distance);
            this.DropoffBoundary = this.GetBoundary(this.Dropoff, distance);

            var up = Direction == "NE" || Direction == "NW" || Direction == "N";
            var down = Direction == "SE" || Direction == "SW" || Direction == "S";
            var left = Direction == "WN" || Direction == "WS" || Direction == "W";
            var right = Direction == "EN" || Direction == "ES" || Direction == "E";

            if (up || down)
            {
                this.PickupBoundary.MinCoordinate.Latitude = this.Pickup.Latitude;
                this.PickupBoundary.MaxCoordinate.Latitude = this.Pickup.Latitude;
                this.DropoffBoundary.MinCoordinate.Latitude = this.Dropoff.Latitude;
                this.DropoffBoundary.MaxCoordinate.Latitude = this.Dropoff.Latitude;
            }
            else if (left || right)
            {
                this.PickupBoundary.MinCoordinate.Longitude = this.Pickup.Longitude;
                this.PickupBoundary.MaxCoordinate.Longitude = this.Pickup.Longitude;
                this.DropoffBoundary.MinCoordinate.Longitude = this.Dropoff.Longitude;
                this.DropoffBoundary.MaxCoordinate.Longitude = this.Dropoff.Longitude;
            }
        }

        public bool Bounds(Passenger passenger)
        {
            return true;
        }

        private Boundary GetBoundary(Coordinate point, double distance)
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

        //        public bool isWithin(Coordinate pt, GeoCoordinate sw, GeoCoordinate ne)
        //        {
        //            return pt.Latitude >= sw.Latitude &&
        //                   pt.Latitude <= ne.Latitude &&
        //                   pt.Longitude >= sw.Longitude &&
        //                   pt.Longitude <= ne.Longitude
        //}

        public bool DrivesInSameDirection(Passenger passenger)
        {
            return true;
        }

        public void SetDirection()
        {
            this.Direction = GeoLocation.GetDirection(this.Pickup, this.Dropoff);
        }
    }
}
