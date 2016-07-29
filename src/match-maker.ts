import { Passenger } from './models/passenger';
import { Point } from './models/point';
import { Driver } from './models/driver';
import { Constants } from './models/constants';
import { Journey } from './models/journey';
import { JourneyData } from './models/journey-data';
import { Haversine } from './models/haversine';

class DriverPickup {
	passenger: Passenger;
	distancesToDriver: number;
}

class DriverBucket {
	driver: Driver;
	pickups: DriverPickup[]
}

export class MatchMaker {

	PassengerList: Passenger[] = [];
	DriverList: Driver[] = [];

	// offset for converting passengers to character
	//	  to find best possible combination for journey
	// first 32 chars are for controlling peripherals
	private buffer = 65;

	private DEBUG = Constants.isDebugMode();

	public setPassengers(...passengers: Passenger[]): void {
		for (let passenger of passengers)
			this.PassengerList.push(passenger);
	}

	public setDrivers(...drivers: Driver[]): void {
		for (let driver of drivers)
			this.DriverList.push(driver);
	}

	public calculate(): DriverBucket[] {

		let results:DriverBucket[] = [];
		for (let driver of this.DriverList) {
			let pickups: DriverPickup[] = [];
			for (let passenger of this.PassengerList) {

				var h = Haversine.getHaversine(
					driver.getOrigin().getX(), 
					passenger.getOrigin().getX(), 
					driver.getOrigin().getY(), 
					passenger.getDestination().getY());

				pickups.push({passenger: passenger, distancesToDriver: h});
			}
			results.push({
				driver: driver,
				pickups: pickups.sort((a, b) => b.distancesToDriver - a.distancesToDriver)
			});
		}

		return results;
	}

	public process(): Journey[] {
		var passengerChars: string[] = [];

		for (var i = this.buffer; i < this.buffer + this.PassengerList.length; i++) {
			var letter = String.fromCharCode(i);
			passengerChars.push(letter, letter);
		}

		// all possible orderings for passengers
		var orderings: string[] = [];

		// get permutations
		this.permute("_", passengerChars.join(''), orderings);
		console.log(orderings.length);

		// remove cases of no overlap [pick up, drop off, pick up, ...]
		// first [pick, drop] is useless
		this.removeNoOverlaps(orderings);
		this.removeNoFirstInFirstOut(orderings);

		// haversine
		var journeysHaversine: Journey[] = [];
		this.computeJourneyDetailsHaversine(journeysHaversine, orderings);

		// sort Journey to get the best route
        // TODO: check if sorting correct
		journeysHaversine = journeysHaversine.sort(this.sortJourneys);

		return journeysHaversine;
	}

	private computeJourneyDetailsHaversine(journeys: Journey[], orderings: string[]): void {
		// get best path for each of the available drivers
		for (let driver of this.DriverList) {

			// corresponding passenger orderings
			for (let ordering of orderings) {
				// start location = driver present location

				// end location = last passenger drop off
				var lastPassenger = ordering.charAt(ordering.length - 1);
				// get passenger drop off from PassengerList
				// lastPassenger - buffer = location of passenger in list
                // TODO: check if its correct to convert chart to int
				var last = this.PassengerList[lastPassenger.charCodeAt(0) - this.buffer].getDestination();

				// waypoints are all chars from ordering.charAt(0...n-2)
				// first occurrence of character = pick up
				// second occurrence of character = drop off
				// on adding to set, if add returns false, it must mean 
				// first occurrence has been accounted for.
				var waypoints: Point[] = [];

				// using TreeSet since ordering is key
				for (var i = 0; i < ordering.length - 1; i++) {
					var curPassenger = ordering.charCodeAt(i);
					curPassenger -= this.buffer;
					var addOrigin = waypoints.push(this.PassengerList[curPassenger].getOrigin());
					// if addOrigin is false, it was already added to the set
					if (!addOrigin) {
                        waypoints.push(this.PassengerList[curPassenger].getDestination());
                    }

				}

				// get Journey for this ordering
				var thisJourney = new JourneyData();
				thisJourney.setDriver(driver);
				thisJourney.setPassengerList(this.PassengerList);
				thisJourney.setOrdering(ordering);
				thisJourney.setWaypoints(waypoints);
				thisJourney.setStartLocation(driver.getCurrentLocation());
				thisJourney.setEndLocation(last);
				thisJourney.setOrderingCharacterBuffer(this.buffer);
				var j = new Journey(thisJourney);

				// get haversine distance
				j.computeHaversines();

				// add updated journey to the list
				journeys.push(j);
			}
		}
	}

	private sortJourneys(a: Journey, b: Journey) {
		return a.thisJourney.getDistance() - b.thisJourney.getDistance();
	}
	private removeNoOverlaps(orderings: string[]): void {
		let i = orderings.length
		while (i--) {
    		let ordering = orderings[i];
			if (ordering.charAt(0) === ordering.charAt(1)) {
				orderings.splice(i, 1);
			}
		}
	}

	private removeNoFirstInFirstOut(orderings: string[]): void {
		let i = orderings.length
		while (i--) {
    		let ordering = orderings[i];
			if (ordering.charAt(0) !== ordering.charAt(ordering.length - 1)) {
				orderings.splice(i, 1);
			}
		}
	}

	private permute(prefix: string, str: string, orderings: string[]): void {
		var n = str.length;
		if (n == 0) {
            orderings.push(prefix);
            return;
        }

		// permute characters after start
		for (var i = 0; i < n; i++) {
			this.permute(prefix + str.charAt(i), str.substring(0, i) + str.substring(i + 1), orderings);
		}
	}
}
