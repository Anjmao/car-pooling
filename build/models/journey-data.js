"use strict";
var JourneyData = (function () {
    function JourneyData() {
        this.time = 0;
        this.distance = 0;
        this.orderingCharacterBuffer = 0;
    }
    JourneyData.prototype.getStartLocation = function () {
        return this.startLocation;
    };
    JourneyData.prototype.setStartLocation = function (startLocation) {
        this.startLocation = startLocation;
    };
    JourneyData.prototype.getEndLocation = function () {
        return this.endLocation;
    };
    JourneyData.prototype.setEndLocation = function (endLocation) {
        this.endLocation = endLocation;
    };
    JourneyData.prototype.getTime = function () {
        return this.time;
    };
    JourneyData.prototype.setTime = function (time) {
        this.time = time;
    };
    JourneyData.prototype.getDistance = function () {
        return this.distance;
    };
    JourneyData.prototype.setDistance = function (distance) {
        this.distance = distance;
    };
    JourneyData.prototype.getDriver = function () {
        return this.driver;
    };
    JourneyData.prototype.setDriver = function (driver) {
        this.driver = driver;
    };
    JourneyData.prototype.getPassengerList = function () {
        return this.passengerList;
    };
    JourneyData.prototype.setPassengerList = function (passengerList) {
        this.passengerList = passengerList;
    };
    JourneyData.prototype.getWaypoints = function () {
        return this.waypoints;
    };
    JourneyData.prototype.setWaypoints = function (waypoints) {
        this.waypoints = waypoints;
    };
    JourneyData.prototype.getOrdering = function () {
        return this.ordering;
    };
    JourneyData.prototype.setOrdering = function (ordering) {
        this.ordering = ordering;
    };
    JourneyData.prototype.getURL = function () {
        return this.URL;
    };
    JourneyData.prototype.setURL = function (URL) {
        this.URL = URL;
    };
    JourneyData.prototype.compareTo = function (o) {
        if (this.time < o.time)
            return -1;
        else if (this.time > o.time)
            return 1;
        if (this.distance < o.distance)
            return -1;
        return 1;
    };
    JourneyData.prototype.toString = function () {
        return "";
    };
    JourneyData.prototype.setOrderingCharacterBuffer = function (buffer) {
        this.orderingCharacterBuffer = buffer;
    };
    JourneyData.prototype.getOrderingCharacterBuffer = function () {
        return this.orderingCharacterBuffer;
    };
    return JourneyData;
}());
exports.JourneyData = JourneyData;
//# sourceMappingURL=journey-data.js.map