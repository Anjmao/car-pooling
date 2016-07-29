import { JourneyData  } from './journey-data';
import { Constants  } from './constants';
import { Haversine } from './haversine';
import { Point  } from './point';

export class Journey {

    private static DEBUG = Constants.isDebugMode();
    
    thisJourney: JourneyData;

    constructor(thisJourney: JourneyData) {
        this.setThisJourney(thisJourney);
    }

    private setThisJourney(thisJourney: JourneyData) {
        this.thisJourney = thisJourney;
    }

    private getOriginString(): string {
        var st: string[] = [];
        st.push("origin=");
        var start = this.thisJourney.getStartLocation();
        st.push(start.getLat() + "," + start.getLon());
        return st.join('');
    }

    private getDestinationString(): string {
        var st: string[] = [];
        st.push("&destination=");
        var end = this.thisJourney.getEndLocation();
        st.push(end.getLat() + "," + end.getLon());
        return st.join('');
    }

    private getWaypointsString(): string {
        var waypoints = this.thisJourney.getWaypoints(); //TODO: types
        if (waypoints == null)
            return "";

        var st: string[] = [];
        st.push("&waypoints=");
        for (let wayoint of waypoints) {
            var point = wayoint;
            st.push(point.getLat() + "," + point.getLon() + "|");
        }

        // remove extra | at the end
        // sorry for the bad style
        st.splice(st.length - 1, 1);
        return st.join('');
    }

    private validateAndSetURL(): void {
        if (this.thisJourney.getStartLocation() == null || this.thisJourney.getEndLocation() == null)
            throw new Error("Coordinates are not set");

        // ensure coordinates are within bounds of Seattle
        if (!this.validBounds())
            throw new Error("Only trips inside Seattle are supported now.");

        // URL for Google Maps
        var url =
            "https://maps.googleapis.com/maps/api/directions/json?"
            + this.getOriginString()
            + this.getDestinationString()
            + this.getWaypointsString()
            + "&mode="
            + Constants.getMode()
            + "&language=en-EN&sensor=false"
            + "&key="
            + Constants.getKey();
        this.thisJourney.setURL(url);

        if (Journey.DEBUG)
            console.log("DEBUG URL: " + url);
    }

    public obtainData(): void {
        this.validateAndSetURL();
        try {
            //TODO: call OSRM

            // parse data
            this.parseData(null);

        } catch (err) {

        }
    }

    //TODO: implement this
    private parseData(data: string) {
        /*JSONObject container = new JSONObject(data);

        // routes -> legs -> leg0, leg1, leg2, ....
        JSONArray routesObj = container.getJSONArray("routes");
        JSONObject routesBody = routesObj.getJSONObject(0);
        JSONArray legs = routesBody.getJSONArray("legs");

        // without waypoints, legs contains 1 object, start -> end
        // with waypoints, legs contains 1 + length(waypoints) objects.
        int length = 1;
        if (thisJourney.getWaypoints() != null)
            length += thisJourney.getWaypoints().size();

        assert length == legs.length();

        long totalDistance = 0;		// in meters
        long totalTime = 0;			// in seconds
        for (int i = 0; i < legs.length() ; i++) {
            JSONObject legContents = legs.getJSONObject(i);

            // distance sub-array
            JSONObject distance = legContents.getJSONObject("distance");
            totalDistance += distance.getLong("value");

            // duration sub-array
            JSONObject duration = legContents.getJSONObject("duration");
            totalTime += duration.getLong("value");
        }

        // debug console
        if (DEBUG) {
            System.out.println("DEBUG Total distance: " + totalDistance);
            System.out.println("DEBUG Total duration: " + totalTime + "\n");
        }

        thisJourney.setDistance(totalDistance);
        thisJourney.setTime(totalTime); */
    }

    private validBounds(): boolean {
        // validate start
        if (!this.validateCoordinate(this.thisJourney.getStartLocation()))
            return false;

        // validate end
        if (!this.validateCoordinate(this.thisJourney.getEndLocation()))
            return false;

        // validate waypoints
        var waypoints = this.thisJourney.getWaypoints();
        if (waypoints == null)
            return true;

        for (let waypoint of waypoints) {
            if (!this.validateCoordinate(waypoint))
                return false;
        }
        return true;
    }

    private validateCoordinate(point: Point): boolean {
        //		if(DEBUG) {
        //			System.out.println("DEBUG LAT/LONG");
        //			System.out.println("(minLong, maxLong), (minLat, maxLat)\n[" + Constants.getMinimumLongitude() + " " + 
        //					Constants.getMaximumLongitude() 
        //					+"]  [" + Constants.getMinimumLatitude() + 
        //					" " + Constants.getMaximumLatitude() + "]") ;
        //			
        //			System.out.println("(pointLat, pointLong)\n[" + point.getY() + " " + point.getX() + "]\n\n");
        //		}
        if (Math.abs(point.getLon()) < Math.abs(Constants.getMinimumLongitude())
            || Math.abs(point.getLon()) > Math.abs(Constants.getMaximumLongitude())
            || Math.abs(point.getLat()) < Math.abs(Constants.getMinimumLatitude())
            || Math.abs(point.getLat()) > Math.abs(Constants.getMaximumLatitude()))
            return false;

        return true;
    }

    public toString(): string {
        return this.thisJourney.toString();
    }

    public computeHaversines(): void {
        if (this.thisJourney.getStartLocation() == null || this.thisJourney.getEndLocation() == null)
            throw new Error("Coordinates are not set");

        // ensure coordinates are within bounds of Seattle
        if (!this.validBounds())
            throw new Error("Only trips inside Seattle are supported now.");

        var cur = this.thisJourney.getStartLocation();
        var totalDistance = 0;
        for (let waypoint of this.thisJourney.getWaypoints()) {
            totalDistance += Haversine.getHaversine(cur.getLat(), waypoint.getLat(), cur.getLon(), waypoint.getLon());
            cur = waypoint;
        }

        var end = this.thisJourney.getEndLocation();
        totalDistance += Haversine.getHaversine(cur.getLat(), end.getLat(),
            cur.getLon(), end.getLon());

        this.thisJourney.setDistance(totalDistance);
        this.thisJourney.setTime(0);

    }
}
