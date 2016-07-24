import { Point } from './point';

export class Driver {
	
	private UID: string;
	private currentLocation: Point; //TODO remove this
	private origin : Point;
    private destination : Point;
	
	constructor(name: string, location?: Point, origin? : Point, destination? : Point) {
        this.setUID(name);
        this.setCurrentLocation(location);
		this.setOrigin(origin);
        this.setDestination(destination);
	}

	getUID(): string {
		return this.UID;
	}

	setUID(uID: string) {
		this.UID = uID;
	}

	getCurrentLocation(): Point {
		return this.currentLocation;
	}

	setCurrentLocation(currentLocation: Point) {
		this.currentLocation = currentLocation;
	}

	getOrigin() : Point {
        return this.origin;
    }

    setOrigin(origin : Point) {
        this.origin = origin;
    }

    getDestination() : Point {
        return this.destination;
    }

    setDestination(destination : Point) {
        this.destination = destination;
    }
}