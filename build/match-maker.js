"use strict";
var constants_1 = require('./models/constants');
var journey_1 = require('./models/journey');
var journey_data_1 = require('./models/journey-data');
var MatchMaker = (function () {
    function MatchMaker() {
        this.PassengerList = [];
        this.DriverList = [];
        // offset for converting passengers to character
        //	  to find best possible combination for journey
        // first 32 chars are for controlling peripherals
        this.buffer = 65;
        this.DEBUG = constants_1.Constants.isDebugMode();
    }
    MatchMaker.prototype.setPassengers = function () {
        var passengers = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            passengers[_i - 0] = arguments[_i];
        }
        for (var _a = 0, passengers_1 = passengers; _a < passengers_1.length; _a++) {
            var passenger = passengers_1[_a];
            this.PassengerList.push(passenger);
        }
    };
    MatchMaker.prototype.setDrivers = function () {
        var drivers = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            drivers[_i - 0] = arguments[_i];
        }
        for (var _a = 0, drivers_1 = drivers; _a < drivers_1.length; _a++) {
            var driver = drivers_1[_a];
            this.DriverList.push(driver);
        }
    };
    MatchMaker.prototype.process = function () {
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
        var passengerChars = [];
        for (var i = this.buffer; i < this.buffer + this.PassengerList.length; i++) {
            var letter = String.fromCharCode(i);
            passengerChars.push(letter, letter);
        }
        // all possible orderings for passengers
        // TODO: convert to set
        var orderings = [];
        // get permutations
        this.permute("", passengerChars.join(''), orderings);
        if (this.DEBUG) {
        }
        // remove cases of no overlap [pick up, drop off, pick up, ...]
        // first [pick, drop] is useless
        this.removeNoOverlaps(orderings);
        if (this.DEBUG) {
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
        var journeysHaversine = [];
        this.computeJourneyDetailsHaversine(journeysHaversine, orderings);
        // sort Journey to get the best route
        // TODO: check if sorting correct
        journeysHaversine = journeysHaversine.sort();
        return journeysHaversine;
    };
    MatchMaker.prototype.computeJourneyDetailsHaversine = function (journeys, orderings) {
        // get best path for each of the available drivers
        for (var _i = 0, _a = this.DriverList; _i < _a.length; _i++) {
            var driver = _a[_i];
            // corresponding passenger orderings
            for (var _b = 0, orderings_1 = orderings; _b < orderings_1.length; _b++) {
                var ordering = orderings_1[_b];
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
                var waypoints = [];
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
                var thisJourney = new journey_data_1.JourneyData();
                thisJourney.setDriver(driver);
                thisJourney.setPassengerList(this.PassengerList);
                thisJourney.setOrdering(ordering);
                thisJourney.setWaypoints(waypoints);
                thisJourney.setStartLocation(driver.getCurrentLocation());
                thisJourney.setEndLocation(last);
                thisJourney.setOrderingCharacterBuffer(this.buffer);
                var j = new journey_1.Journey(thisJourney);
                // get haversine distance
                j.computeHaversines();
                // add updated journey to the list
                journeys.push(j);
            }
        }
    };
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
    MatchMaker.prototype.removeNoOverlaps = function (orderings) {
        for (var i = 0; i < orderings.length; i++) {
            var ordering = orderings[i];
            if (ordering.charAt(0) == ordering.charAt(1)) {
                orderings.splice(i, 1);
            }
        }
    };
    MatchMaker.prototype.permute = function (prefix, str, orderings) {
        var n = str.length;
        if (n == 0) {
            orderings.push(prefix);
            return;
        }
        // permute characters after start
        for (var i = 0; i < n; i++) {
            this.permute(prefix + str.charAt(i), str.substring(0, i) + str.substring(i + 1), orderings);
        }
    };
    return MatchMaker;
}());
exports.MatchMaker = MatchMaker;
//# sourceMappingURL=match-maker.js.map