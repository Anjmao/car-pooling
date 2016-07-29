import { Booking } from './import';
import { Driver } from '../models/driver';

export function group(data: Booking[]) {
    
    let sortedData = sortByLongtitude(data);
    let set = new Set(sortedData); 

    let drivers = new Set<Driver>();

    set.forEach(x => {
        if(x.isDriver){
            drivers.add(new Driver(x.id, x.pickup, x.dropoff))
        }
    })
}

export function sortByLongtitude(data: Booking[]) {
    return data.sort((a, b) => {
        return a.pickup.lon - b.pickup.lon;
    });
}