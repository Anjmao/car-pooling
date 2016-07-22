"use strict";
var Constants = (function () {
    function Constants() {
    }
    Constants.getMinimumLatitude = function () {
        return Constants.LAT[0];
    };
    Constants.getMinimumLongitude = function () {
        return Constants.LONG[0];
    };
    Constants.getMaximumLatitude = function () {
        return Constants.LAT[1];
    };
    Constants.getMaximumLongitude = function () {
        return Constants.LONG[1];
    };
    Constants.getMode = function () {
        return Constants.mode;
    };
    Constants.getKey = function () {
        return "";
    };
    Constants.isDebugMode = function () {
        return Constants.DEBUG;
    };
    Constants.LAT = [47.48172, 47.734145];
    Constants.LONG = [-122.151602, -122.419866];
    Constants.key = "INSERT YOUR GOOGLE DIRECTIONS API KEY";
    Constants.mode = "driving";
    Constants.DEBUG = true;
    return Constants;
}());
exports.Constants = Constants;
//# sourceMappingURL=constants.js.map