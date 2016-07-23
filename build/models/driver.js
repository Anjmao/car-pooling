"use strict";
var Driver = (function () {
    function Driver(name, location) {
        this.setUID(name);
        this.setCurrentLocation(location);
    }
    Driver.prototype.getUID = function () {
        return this.UID;
    };
    Driver.prototype.setUID = function (uID) {
        this.UID = uID;
    };
    Driver.prototype.getCurrentLocation = function () {
        return this.currentLocation;
    };
    Driver.prototype.setCurrentLocation = function (currentLocation) {
        this.currentLocation = currentLocation;
    };
    return Driver;
}());
exports.Driver = Driver;
//# sourceMappingURL=driver.js.map