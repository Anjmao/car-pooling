using System;
using System.Linq;
using CarPooling.Utils;

namespace CarPooling.Models
{
    public class Driver : Rider
    {
        public Boundary PickupBoundary;
        public Boundary DropoffBoundary;
        public string Direction;
        public double RightMostLongitude;

        private double[] Latitudes;
        private double[] Longitudes;

        public Driver(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        public void ComputeBoundaries()
        {
            var distance = this.FlyingDistance / 2;

            this.PickupBoundary = GeoLocation.GetBoundary(this.Pickup, distance);
            this.DropoffBoundary = GeoLocation.GetBoundary(this.Dropoff, distance);

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

            this.RightMostLongitude = Math.Max(this.PickupBoundary.MaxCoordinate.Longitude, this.DropoffBoundary.MaxCoordinate.Longitude);

            Latitudes = new[]
            {
                PickupBoundary.MinCoordinate.Latitude, PickupBoundary.MinCoordinate.Latitude,
                DropoffBoundary.MinCoordinate.Latitude, DropoffBoundary.MinCoordinate.Latitude
            };

            Longitudes = new[]
            {
                PickupBoundary.MinCoordinate.Longitude, PickupBoundary.MinCoordinate.Longitude,
                DropoffBoundary.MinCoordinate.Longitude, DropoffBoundary.MinCoordinate.Longitude
            };

            Array.Sort(Latitudes);
            Array.Sort(Longitudes);
        }

        public bool Bounds(Passenger passenger)
        {
            return true;

            if (passenger.Pickup.Latitude < this.Latitudes[2] && passenger.Pickup.Latitude < this.Latitudes[3]
                && passenger.Dropoff.Latitude < this.Latitudes[2] && passenger.Dropoff.Latitude < this.Latitudes[3]
                && passenger.Pickup.Latitude > this.Latitudes[0] && passenger.Pickup.Latitude > this.Latitudes[1]
                && passenger.Dropoff.Latitude > this.Latitudes[0] && passenger.Dropoff.Latitude > this.Latitudes[1]

                && passenger.Pickup.Longitude < this.Longitudes[2] && passenger.Pickup.Longitude < this.Longitudes[3]
                && passenger.Dropoff.Longitude < this.Longitudes[2] && passenger.Dropoff.Longitude < this.Longitudes[3]
                && passenger.Pickup.Longitude > this.Longitudes[0] && passenger.Pickup.Longitude > this.Longitudes[1]
                && passenger.Dropoff.Longitude > this.Longitudes[0] && passenger.Dropoff.Longitude > this.Longitudes[1])
            {
                return true;
            }

            return false;
        }

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
