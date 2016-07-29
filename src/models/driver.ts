import { Point } from './point';
import { Haversine } from './haversine'

interface Boundary {
    minLat: number;
    minLon: number;
    maxLat: number;
    maxLon: number;
}

export class Driver {

    private UID: string;
    private origin: Point;
    private destination: Point;

    private originBoundary: Boundary;
    private destinationBoundary: Boundary;

    flyingDistance: number;

    constructor(id: string, origin?: Point, destination?: Point) {
        this.setUID(id);
        this.setOrigin(origin);
        this.setDestination(destination);
    }

    getUID(): string {
        return this.UID;
    }

    setUID(uID: string) {
        this.UID = uID;
    }

    getOrigin(): Point {
        return this.origin;
    }

    setOrigin(origin: Point) {
        this.origin = origin;
    }

    getDestination(): Point {
        return this.destination;
    }

    setDestination(destination: Point) {
        this.destination = destination;
    }

    computeBoundaries() {
        let distance = this.flyingDistance / 2;

        this.originBoundary = this.getBoundary(this.origin, distance);
        this.destinationBoundary = this.getBoundary(this.destination, distance);
    }

    private getBoundary(point: Point, distance) {
        let oneDegreeInKm = 110.574;

        let latitudeConversionFactor = this.flyingDistance / oneDegreeInKm;
        let longitudeConversionFactor = this.flyingDistance / oneDegreeInKm / Math.abs(Math.cos(Haversine.toRadian(point.lat)));

        var boundary: Boundary = {
            minLat: point.lat - latitudeConversionFactor,
            maxLat: point.lat + latitudeConversionFactor,
            minLon: point.lon - longitudeConversionFactor,
            maxLon: point.lon + longitudeConversionFactor
        };

        return boundary;
    }
}