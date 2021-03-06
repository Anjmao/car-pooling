export class Haversine {
    private static RADIUS: number = 6371;

    public static getHaversine(lat1: number, lat2: number, lon1: number, lon2: number): number {
        var latDistance: number = Haversine.toRadian(lat2 - lat1);
        var lonDistance: number = Haversine.toRadian(lon2 - lon1);
        var a: number = Math.sin(latDistance / 2) * Math.sin(latDistance / 2) + Math.cos(Haversine.toRadian(lat1)) * Math.cos(Haversine.toRadian(lat2)) * Math.sin(lonDistance / 2) * Math.sin(lonDistance / 2);
        var c: number = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var distance: number = Haversine.RADIUS * c;
        return distance;
    }

    public static getDistance(point1, point2): number {
        return Haversine.getHaversine(point1.lat, point2.lat, point1.lon, point2.lon);
    }

    public static toRadian(value: number): number {
        return value * Math.PI / 180;
    }
}