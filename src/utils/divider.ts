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

    var iterator = drivers.values();
    var nextDriver = iterator.next();

    while (!nextDriver.done) {
        var driver = nextDriver.value;

        driver.flyingDistance = Haversine.getDistance(driver.getOrigin(), driver.getDestination());
        driver.computeBoundaries();

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
    var iterator = riders.values();
    var next = iterator.next();

    while (!next.done) {
        var rider = next.value;
        if (rider.isDriver) {
            drivers.add(new Driver(rider.id, rider.pickup, rider.dropoff))
        } else {
            passengers.add(new Passenger(rider.id, rider.pickup, rider.dropoff))
        }
        next = iterator.next();
    }
}