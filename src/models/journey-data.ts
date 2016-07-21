import { Point } from './point';
import { Passenger } from './passenger';
import { Driver } from './driver';

export class JourneyData {
    
    constructor() {
        this.time = 0;
        this.distance = 0;
        this.orderingCharacterBuffer = 0;
    }

    private time : number;

    private distance : number;

    private startLocation : Point;

    private endLocation : Point;

    private driver : Driver;

    private passengerList : Passenger[];

    private waypoints : Set<Point>;

    private ordering : string;

    private URL : string;

    private orderingCharacterBuffer : number;

    public getStartLocation() : Point {
        return this.startLocation;
    }

    public setStartLocation(startLocation : Point) {
        this.startLocation = startLocation;
    }

    public getEndLocation() : Point {
        return this.endLocation;
    }

    public setEndLocation(endLocation : Point) {
        this.endLocation = endLocation;
    }

    public getTime() : number {
        return this.time;
    }

    public setTime(time : number) {
        this.time = time;
    }

    public getDistance() : number {
        return this.distance;
    }

    public setDistance(distance : number) {
        this.distance = distance;
    }

    public getDriver() : Driver {
        return this.driver;
    }

    public setDriver(driver : Driver) {
        this.driver = driver;
    }

    public getPassengerList() : Passenger[] {
        return this.passengerList;
    }

    public setPassengerList(passengerList : Passenger[]) {
        this.passengerList = passengerList;
    }

    public getWaypoints() : Set<Point> {
        return this.waypoints;
    }

    public setWaypoints(waypoints : Set<Point>) {
        this.waypoints = waypoints;
    }

    public getOrdering() : string {
        return this.ordering;
    }

    public setOrdering(ordering : string) {
        this.ordering = ordering;
    }

    public getURL() : string {
        return this.URL;
    }

    public setURL(URL : string) {
        this.URL = URL;
    }

    public compareTo(o : JourneyData) : number {
        if(this.time < o.time) return -1; else if(this.time > o.time) return 1;
        if(this.distance < o.distance) return -1;
        return 1;
    }

    public toString() : string {
        return "";
    }

    public setOrderingCharacterBuffer(buffer : number) {
        this.orderingCharacterBuffer = buffer;
    }

    public getOrderingCharacterBuffer() : number {
        return this.orderingCharacterBuffer;
    }
}