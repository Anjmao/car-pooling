export interface Booking {
    id: any;
    pickup: Coordinate;
    dropoff: Coordinate;
    isDriver: boolean;
}

interface Coordinate {
    id: any;
    lat: any;
    lon: any;
}
