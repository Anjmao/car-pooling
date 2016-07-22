"use strict";
var Haversine = (function () {
    function Haversine() {
    }
    Haversine.getHaversine = function (lat1, lat2, lon1, lon2) {
        var latDistance = Haversine.toRadian(lat2 - lat1);
        var lonDistance = Haversine.toRadian(lon2 - lon1);
        var a = Math.sin(latDistance / 2) * Math.sin(latDistance / 2) + Math.cos(Haversine.toRadian(lat1)) * Math.cos(Haversine.toRadian(lat2)) * Math.sin(lonDistance / 2) * Math.sin(lonDistance / 2);
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var distance = Haversine.RADIUS * c;
        return distance;
    };
    Haversine.toRadian = function (value) {
        return value * Math.PI / 180;
    };
    Haversine.RADIUS = 6371;
    return Haversine;
}());
exports.Haversine = Haversine;
//# sourceMappingURL=haversine.js.map