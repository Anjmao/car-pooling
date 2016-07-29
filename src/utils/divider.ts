import { Booking } from './import';
import { Driver } from '../models/driver';
import { Passenger } from '../models/passenger';
import { Haversine } from '../models/haversine';

let drivers = new Set<Driver>();
let passengers = new Set<Passenger>();

export function group(data: Booking[]) {

    let sortedData = sortByLongtitude(data);
    let riders = new Set(sortedData);

    distinctDrivers(riders)

    let iterator = drivers.values();
    let nextDriver = iterator.next();

    while (!nextDriver.done) {
        let driver = nextDriver.value;
        driver.flyingDistance = Haversine.getDistance(driver.getOrigin(), driver.getDestination());
        driver.computeBoundaries();

        let passengerIterator = passengers.values();
        let nextPassenger = passengerIterator.next();

        while (!nextPassenger.done) {
            let passenger = nextPassenger.value;
            nextPassenger = passengerIterator.next(); 
        }

        nextDriver = iterator.next();
    }

    return drivers;
}

export function sortByLongtitude(data: Booking[]) {
    return data.sort((a, b) => {
        return a.pickup.lon - b.pickup.lon;
    });
}

function distinctDrivers(riders: Set<Booking>) {
    let iterator = riders.values();
    let next = iterator.next();

    while (!next.done) {
        let rider = next.value;
        if (rider.isDriver) {
            drivers.add(new Driver(rider.id, rider.pickup, rider.dropoff))
        } else {
            passengers.add(new Passenger(rider.id, rider.pickup, rider.dropoff))
        }
        next = iterator.next();
    }
}