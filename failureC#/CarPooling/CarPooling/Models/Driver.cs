using System;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private readonly string[] up = { "NE", "NW", "N" };
        private readonly string[] down = { "SE", "SW", "S" };
        private readonly string[] left = { "WN", "WS", "W" };
        private readonly string[] right = { "EN", "ES", "E" };

        private readonly string[][] directions =
        {
            new[] { "NE", "NW", "N" },
            new[] { "SE", "SW", "S" },
            new[] { "WN", "WS", "W" },
            new[] { "EN", "ES", "E" }
        };

        public Driver(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        public void ComputeBoundaries()
        {
            var distance = this.FlyingDistance / 2;

            this.PickupBoundary = GeoLocation.GetBoundary(this.Pickup, distance);
            this.DropoffBoundary = GeoLocation.GetBoundary(this.Dropoff, distance);

            var up = this.up.Contains(Direction);
            var down = this.down.Contains(Direction);
            var left = this.left.Contains(Direction);
            var right = this.right.Contains(Direction);

            if (up || down)
            {
                this.PickupBoundary.MinCoordinate.Latitude = this.Pickup.Latitude;
                this.PickupBoundary.MaxCoordinate.Latitude = this.Pickup.Latitude;

                this.DropoffBoundary.MinCoordinate.Latitude = this.Dropoff.Latitude;
                this.DropoffBoundary.MaxCoordinate.Latitude = this.Dropoff.Latitude;

                if (down)
                {
                    this.DropoffBoundary.MaxCoordinate.Longitude = this.Dropoff.Longitude;
                }
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
                PickupBoundary.MinCoordinate.Latitude, PickupBoundary.MaxCoordinate.Latitude,
                DropoffBoundary.MinCoordinate.Latitude, DropoffBoundary.MaxCoordinate.Latitude
            };

            Longitudes = new[]
            {
                PickupBoundary.MinCoordinate.Longitude, PickupBoundary.MaxCoordinate.Longitude,
                DropoffBoundary.MinCoordinate.Longitude, DropoffBoundary.MaxCoordinate.Longitude
            };

            Array.Sort(Latitudes);
            Array.Sort(Longitudes);
        }

        public bool Bounds(Passenger passenger)
        {
            var latitudeTop = (passenger.Pickup.Latitude < this.Latitudes[2] || passenger.Pickup.Latitude < this.Latitudes[3])
                    && (passenger.Dropoff.Latitude < this.Latitudes[2] || passenger.Dropoff.Latitude < this.Latitudes[3]);

            var latitudeBottom = passenger.Pickup.Latitude > this.Latitudes[0] && passenger.Pickup.Latitude > this.Latitudes[1]
                    && passenger.Dropoff.Latitude > this.Latitudes[0] && passenger.Dropoff.Latitude > this.Latitudes[1];

            var latitude = latitudeTop && latitudeBottom;

            var longitudeRight = (passenger.Pickup.Longitude < this.Longitudes[2] || passenger.Pickup.Longitude < this.Longitudes[3])
                    && (passenger.Dropoff.Longitude < this.Longitudes[2] || passenger.Dropoff.Longitude < this.Longitudes[3]);

            var longitudeLeft = (passenger.Pickup.Longitude > this.Longitudes[0] || passenger.Pickup.Longitude > this.Longitudes[1])
                && (passenger.Dropoff.Longitude > this.Longitudes[0] || passenger.Dropoff.Longitude > this.Longitudes[1]);

            var longitude = longitudeRight && longitudeLeft;

            if (latitude && longitude)
            {
                return true;
            }

            return false;
        }

        public bool DrivesInSameDirection(Passenger passenger)
        {
            var passengerDirection = GeoLocation.GetDirection(passenger.Pickup, passenger.Dropoff);
            if (passengerDirection == this.Direction)
            {
                return true;
            }

            return this.directions.Any(x => x.Contains(passengerDirection) && x.Contains(this.Direction));
        }

        public void SetDirection()
        {
            this.Direction = GeoLocation.GetDirection(this.Pickup, this.Dropoff);
        }
    }
}
