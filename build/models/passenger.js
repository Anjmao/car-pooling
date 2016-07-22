"use strict";
var Passenger = (function () {
    function Passenger(name, origin, destination) {
        this.setUID(name);
        this.setOrigin(origin);
        this.setDestination(destination);
    }
    Passenger.prototype.getOrigin = function () {
        return this.origin;
    };
    Passenger.prototype.setOrigin = function (origin) {
        this.origin = origin;
    };
    Passenger.prototype.getDestination = function () {
        return this.destination;
    };
    Passenger.prototype.setDestination = function (destination) {
        this.destination = destination;
    };
    Passenger.prototype.getUID = function () {
        return this.UID;
    };
    Passenger.prototype.setUID = function (uID) {
        this.UID = uID;
    };
    return Passenger;
}());
exports.Passenger = Passenger;
//# sourceMappingURL=passenger.js.map