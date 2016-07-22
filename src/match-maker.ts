import { Passenger } from './models/passenger';
import { Point } from './models/point';
import { Driver } from './models/driver';
import { Constants } from './models/constants';
import { Journey } from './models/journey';
import { JourneyData } from './models/journey-data';

export class MatchMaker {

	PassengerList: Passenger[] = [];
	DriverList: Driver[] = [];
	
	// offset for converting passengers to character
	//	  to find best possible combination for journey
	// first 32 chars are for controlling peripherals
	private buffer = 65;		
	
	private DEBUG = Constants.isDebugMode();
	
	public setPassengers(...passengers: Passenger[]): void {
		for(let passenger of passengers)
			this.PassengerList.push(passenger);
	}
	
	public setDrivers(...drivers: Driver[]): void {
		for(let driver of drivers)
			this.DriverList.push(driver);
	}
	
	public process(): Journey[] {
		// get all orderings of passengers
		// say list is -> p1, p2, p3, ... pn
		// get permutations of AABBCC... without repeats
		// first instance = pick up
		// second instance = drop off
		// for passenger p, pick up and drop off cannot be back-to-back
		// char in Java is 16 bits
		// so, 2^16 passengers can be supported concurrently in a region
		// not like that is ever going to happen since a car can hold 7
		// people at the most
		
		// assign character for every passenger
		// passenger at index 0 gets (char)0, index 1 gets (char)1, ...
		var passengerChars: string[] = [];
		
		for(var i = this.buffer ; i < this.buffer + this.PassengerList.length ; i++) {
			var letter = String.fromCharCode(i);
			passengerChars.push(letter, letter);
		}
		
		// all possible orderings for passengers
        // TODO: convert to set
		var orderings: string[] = [];
		
		// get permutations
		this.permute("", passengerChars.join(''), orderings);
		
		if (this.DEBUG) {
			//console.log("DEBUG All permutations:               ");
			//var t: string = '';
			//orderings.toArray(t);
			//System.out.println(Arrays.toString(t));
		}
		
		// remove cases of no overlap [pick up, drop off, pick up, ...]
		// first [pick, drop] is useless
		this.removeNoOverlaps(orderings);
		
		if (this.DEBUG) {
			//System.out.print("DEBUG All permutations - no overlaps: ");
			//String[] t = new String[orderings.size()];
			//orderings.toArray(t);
			//System.out.println(Arrays.toString(t));
		}
		
		// As soon as passenger calls for driver, 
		// time starts. We will call this the start of
		// journey. The journey ends when the passenger
		// is dropped off.
		// For each of the orderings, we will start with the
		// driver's current location
		
		// google directions api
		// list of journeys
		//var journeys: Journey[] = [];
		//this.computeJourneyDetailsGoogleDirectionsAPI(journeys, orderings);
		
		// sort Journey to get the best route
        // TODO: check if sorting correct
		//journeys = journeys.sort();
		
		//console.log("Google Directions API\n" + journeys[0]);
		
		// haversine
		var journeysHaversine: Journey[] = [];
		this.computeJourneyDetailsHaversine(journeysHaversine, orderings);
		
		// sort Journey to get the best route
        // TODO: check if sorting correct
		journeysHaversine = journeysHaversine.sort(this.sortJourneys);
		
		return  journeysHaversine;
	}
	
	private computeJourneyDetailsHaversine(journeys: Journey[], orderings: string[]): void {
		// get best path for each of the available drivers
		for(let driver of this.DriverList) {
			
			// corresponding passenger orderings
			for(let ordering of orderings) {
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
				for(var i = 0;  i < ordering.length - 1 ; i++) {
					var curPassenger = ordering.charCodeAt(i);
					curPassenger -= this.buffer;
					var addOrigin = waypoints.push(this.PassengerList[curPassenger].getOrigin());
					// if addOrigin is false, it was already added to the set
					if(!addOrigin) {
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
	
	// takes an empty ArrayList and a set of possible ordering
	// populates the array with JourneyData objects that have 
	// total journey time and distances
	/*private void computeJourneyDetailsGoogleDirectionsAPI(ArrayList<Journey> journeys, HashSet<String> orderings)  {
		// get best path for each of the available drivers
		for(Driver driver: DriverList) {
			
			// corresponding passenger orderings
			for(String ordering: orderings) {
				// start location = driver present location
				
				// end location = last passenger drop off
				char lastPassenger = ordering.charAt(ordering.length() - 1);
				// get passenger drop off from PassengerList
				// lastPassenger - buffer = location of passenger in list
				Point last = PassengerList.get(lastPassenger - buffer).getDestination();
				
				// waypoints are all chars from ordering.charAt(0...n-2)
				// first occurrence of character = pick up
				// second occurrence of character = drop off
				// on adding to set, if add returns false, it must mean 
				// first occurrence has been accounted for.
				Set<Point> waypoints = new LinkedHashSet<Point>();
				// using TreeSet since ordering is key
				for(int i = 0;  i < ordering.length() - 1 ; i++) {
					char curPassenger = ordering.charAt(i);
					curPassenger -= buffer;
					boolean addOrigin = waypoints.add(PassengerList.get(curPassenger).getOrigin());
					// if addOrigin is false, it was already added to the set
					if(!addOrigin)
						waypoints.add(PassengerList.get(curPassenger).getDestination());
				}
				
				// get Journey for this ordering
				JourneyData thisJourney = new JourneyData();
				thisJourney.setDriver(driver);
				thisJourney.setPassengerList(PassengerList);
				thisJourney.setOrdering(ordering);
				thisJourney.setWaypoints(waypoints);
				thisJourney.setStartLocation(driver.getCurrentLocation());
				thisJourney.setEndLocation(last);
				thisJourney.setOrderingCharacterBuffer(buffer);
				Journey j = new Journey(thisJourney);
				
				// talk to the Google Directions API to get trip information
				j.obtainData();
				
				// add updated journey to the list
				journeys.add(j);
			}
		}
	}*/

	private removeNoOverlaps(orderings: string[]): void {
		for (var i = 0; i < orderings.length; i++) {
			var ordering = orderings[i];
		    if (ordering.charAt(0) == ordering.charAt(1)) {
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
		for (var i = 0 ; i < n ; i++) {
			this.permute(prefix + str.charAt(i), str.substring(0,i) + str.substring(i+1), orderings);
		}
	}
}
