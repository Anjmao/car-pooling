import { Booking } from './import';

export function group(data: Booking[]) {
    
    let sortedData = sortByLongtitude(data);
    let set = new Set(sortedData); 



    
}

export function sortByLongtitude(data: Booking[]) {
    return data.sort((a, b) => {
        return a.pickup.lon - b.pickup.lon;
    });
}

function name() {
    
}