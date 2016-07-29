using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling.Models
{
    public class Driver : Rider
    {
        public Driver(string id, Coordinate pickup, Coordinate dropoff) : base(id, pickup, dropoff)
        {

        }

        //    computeBoundaries()
        //    {
        //        let distance = this.flyingDistance / 2;

        //        this.originBoundary = this.getBoundary(this.origin, distance);
        //        this.destinationBoundary = this.getBoundary(this.destination, distance);
        //    }

        //matchesPoints(point: Point)
        //    {
        //        return true;
        //    }

        //    private getBoundary(point: Point, distance)
        //    {
        //        let oneDegreeInKm = 110.574;

        //        let latitudeConversionFactor = distance / oneDegreeInKm;
        //        let longitudeConversionFactor = distance / oneDegreeInKm / Math.abs(Math.cos(Haversine.toRadian(point.lat)));

        //        var boundary: Boundary = {
        //            minLat: point.lat - latitudeConversionFactor,
        //        maxLat: point.lat + latitudeConversionFactor,
        //        minLon: point.lon - longitudeConversionFactor,
        //        maxLon: point.lon + longitudeConversionFactor
        //        };

        //        return boundary;
        //    }

    }
}
