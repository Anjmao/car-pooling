import { Point } from './point';

export class Passenger {
    private UID : string;

    private origin : Point;

    private destination : Point;

    public constructor(name? : any, origin? : Point, destination? : Point) {
        this.setUID(name);
        this.setOrigin(origin);
        this.setDestination(destination);
    }

    public getOrigin() : Point {
        return this.origin;
    }

    public setOrigin(origin : Point) {
        this.origin = origin;
    }

    public getDestination() : Point {
        return this.destination;
    }

    public setDestination(destination : Point) {
        this.destination = destination;
    }

    public getUID() : string {
        return this.UID;
    }

    public setUID(uID : string) {
        this.UID = uID;
    }
}