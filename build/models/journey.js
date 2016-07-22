"use strict";
var constants_1 = require('./constants');
var haversine_1 = require('./haversine');
var Journey = (function () {
    function Journey(thisJourney) {
        this.setThisJourney(thisJourney);
    }
    Journey.prototype.setThisJourney = function (thisJourney) {
        this.thisJourney = thisJourney;
    };
    Journey.prototype.getOriginString = function () {
        var st = [];
        st.push("origin=");
        var start = this.thisJourney.getStartLocation();
        st.push(start.getX() + "," + start.getY());
        return st.join('');
    };
    Journey.prototype.getDestinationString = function () {
        var st = [];
        st.push("&destination=");
        var end = this.thisJourney.getEndLocation();
        st.push(end.getX() + "," + end.getY());
        return st.join('');
    };
    Journey.prototype.getWaypointsString = function () {
        var waypoints = this.thisJourney.getWaypoints(); //TODO: types
        if (waypoints == null)
            return "";
        var st = [];
        st.push("&waypoints=");
        for (var _i = 0, waypoints_1 = waypoints; _i < waypoints_1.length; _i++) {
            var wayoint = waypoints_1[_i];
            var point = wayoint;
            st.push(point.getX() + "," + point.getY() + "|");
        }
        // remove extra | at the end
        // sorry for the bad style
        st.splice(st.length - 1, 1);
        return st.join('');
    };
    Journey.prototype.validateAndSetURL = function () {
        if (this.thisJourney.getStartLocation() == null || this.thisJourney.getEndLocation() == null)
            throw new Error("Coordinates are not set");
        // ensure coordinates are within bounds of Seattle
        if (!this.validBounds())
            throw new Error("Only trips inside Seattle are supported now.");
        // URL for Google Maps
        var url = "https://maps.googleapis.com/maps/api/directions/json?"
            + this.getOriginString()
            + this.getDestinationString()
            + this.getWaypointsString()
            + "&mode="
            + constants_1.Constants.getMode()
            + "&language=en-EN&sensor=false"
            + "&key="
            + constants_1.Constants.getKey();
        this.thisJourney.setURL(url);
        if (Journey.DEBUG)
            console.log("DEBUG URL: " + url);
    };
    Journey.prototype.obtainData = function () {
        this.validateAndSetURL();
        try {
            //TODO: call OSRM
            // parse data
            this.parseData(null);
        }
        catch (err) {
        }
    };
    //TODO: implement this
    Journey.prototype.parseData = function (data) {
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
    };
    Journey.prototype.validBounds = function () {
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
        for (var _i = 0, waypoints_2 = waypoints; _i < waypoints_2.length; _i++) {
            var waypoint = waypoints_2[_i];
            if (!this.validateCoordinate(waypoint))
                return false;
        }
        return true;
    };
    Journey.prototype.validateCoordinate = function (point) {
        //		if(DEBUG) {
        //			System.out.println("DEBUG LAT/LONG");
        //			System.out.println("(minLong, maxLong), (minLat, maxLat)\n[" + Constants.getMinimumLongitude() + " " + 
        //					Constants.getMaximumLongitude() 
        //					+"]  [" + Constants.getMinimumLatitude() + 
        //					" " + Constants.getMaximumLatitude() + "]") ;
        //			
        //			System.out.println("(pointLat, pointLong)\n[" + point.getY() + " " + point.getX() + "]\n\n");
        //		}
        if (Math.abs(point.getY()) < Math.abs(constants_1.Constants.getMinimumLongitude())
            || Math.abs(point.getY()) > Math.abs(constants_1.Constants.getMaximumLongitude())
            || Math.abs(point.getX()) < Math.abs(constants_1.Constants.getMinimumLatitude())
            || Math.abs(point.getX()) > Math.abs(constants_1.Constants.getMaximumLatitude()))
            return false;
        return true;
    };
    Journey.prototype.toString = function () {
        return this.thisJourney.toString();
    };
    Journey.prototype.computeHaversines = function () {
        if (this.thisJourney.getStartLocation() == null || this.thisJourney.getEndLocation() == null)
            throw new Error("Coordinates are not set");
        // ensure coordinates are within bounds of Seattle
        if (!this.validBounds())
            throw new Error("Only trips inside Seattle are supported now.");
        var cur = this.thisJourney.getStartLocation();
        var totalDistance = 0;
        for (var _i = 0, _a = this.thisJourney.getWaypoints(); _i < _a.length; _i++) {
            var waypoint = _a[_i];
            totalDistance += haversine_1.Haversine.getHaversine(cur.getX(), waypoint.getX(), cur.getY(), waypoint.getY());
            cur = waypoint;
        }
        var end = this.thisJourney.getEndLocation();
        totalDistance += haversine_1.Haversine.getHaversine(cur.getX(), end.getX(), cur.getY(), end.getY());
        this.thisJourney.setDistance(totalDistance);
        this.thisJourney.setTime(0);
    };
    Journey.DEBUG = constants_1.Constants.isDebugMode();
    return Journey;
}());
exports.Journey = Journey;
//# sourceMappingURL=journey.js.map