export class Point {
    
    public lat: number;
    public lon: number;

    constructor(lat: number, lon: number) {
        this.lat = lat;
        this.lon = lon;
    }

    getLat(): number {
        return this.lat;
    }

    getLon(): number {
        return this.lon;
    }
}