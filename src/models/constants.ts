export class Constants {
   
    private static LAT: number[] = [47.48172, 47.734145];
	private static LONG: number[] = [-122.151602, -122.419866];

    private static key : string = "INSERT YOUR GOOGLE DIRECTIONS API KEY";

    private static mode : string = "driving";

    private static DEBUG : boolean = true;

    public static getMinimumLatitude() : number {
        return Constants.LAT[0];
    }

    public static getMinimumLongitude() : number {
        return Constants.LONG[0];
    }

    public static getMaximumLatitude() : number {
        return Constants.LAT[1];
    }

    public static getMaximumLongitude() : number {
        return Constants.LONG[1];
    }

    public static getMode() : string {
        return Constants.mode;
    }

    public static getKey() : string {
        return "";
    }

    public static isDebugMode() : boolean {
        return Constants.DEBUG;
    }
}