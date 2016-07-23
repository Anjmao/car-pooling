import { Point } from './point';

export class Driver {
	
	private UID: string;
	private currentLocation: Point;
	
	constructor(name: string, location?: Point) {
        this.setUID(name);
        this.setCurrentLocation(location);
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
}