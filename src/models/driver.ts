import { Point } from './point';

export class Driver {
	
	private UID: string;
	private currentLocation: Point; //TODO remove this
	private origin : Point;
    private destination : Point;
	
	constructor(id: string, origin? : Point, destination? : Point) {
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

	getCurrentLocation(): Point {
		return this.currentLocation;
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