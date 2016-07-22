"use strict";
var match_maker_1 = require('../match-maker');
var passenger_1 = require('../models/passenger');
var driver_1 = require('../models/driver');
var point_1 = require('../models/point');
var constants_1 = require('../models/constants');
var expect = require('chai').expect;
describe('Match maker', function () {
    var maker;
    beforeEach(function () {
        maker = new match_maker_1.MatchMaker();
    });
    it('should calculate routes', function () {
        var p1 = new passenger_1.Passenger('Vovka', getRandomPoint(), getRandomPoint());
        var p2 = new passenger_1.Passenger('Borkia', getRandomPoint(), getRandomPoint());
        var d1 = new driver_1.Driver('Buratinas', getRandomPoint());
        maker.setPassengers(p1, p2);
        maker.setDrivers(d1);
        var journeys = maker.process();
        console.log(journeys, 'ddd');
    });
});
function getRandomPoint() {
    var randomLat = getRandomDoubleInRange(constants_1.Constants.getMinimumLatitude(), constants_1.Constants.getMaximumLatitude());
    var randomLong = getRandomDoubleInRange(constants_1.Constants.getMinimumLongitude(), constants_1.Constants.getMaximumLongitude());
    return new point_1.Point(randomLat, randomLong);
}
function getRandomDoubleInRange(minimum, maximum) {
    var randomVal = minimum + (maximum - minimum) * Math.random();
    return randomVal;
}
//# sourceMappingURL=match-maker.spec.js.map