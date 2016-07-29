import { Point } from '../models/point';

export interface Booking {
    id?: any;
    pickup?: Coordinate;
    dropoff?: Coordinate;
    isDriver?: boolean;
}

interface Coordinate extends Point {
    id?: any;
}
