import { MatchMaker } from '../match-maker';
import { Passenger } from '../models/passenger';
import { Driver } from '../models/driver';
import { Point } from '../models/point';
import { Constants } from '../models/constants';

var expect: Chai.ExpectStatic = require('chai').expect;

describe('Match maker', function () {
    var maker: MatchMaker;
    beforeEach(() => {
        maker = new MatchMaker();
    });

    xit('should calculate routes', () => {
        var p1 = new Passenger('Vovka', getRandomPoint(), getRandomPoint());
        var p2 = new Passenger('Borkia', getRandomPoint(), getRandomPoint());

        var d1 = new Driver('Buratinas', getRandomPoint());

        maker.setPassengers(p1, p2);
        maker.setDrivers(d1);
        var journeys = maker.process();

        console.log(journeys.length, 'total journeys');
    });

    it('performance test', () => {
        let passengers: Passenger[] = [];
        for (let index = 0; index < 3; index++) {
            passengers.push(new Passenger('Vovka' + index, getRandomPoint(), getRandomPoint()))
        }

        let drivers: Driver[] = [];
        for (let index = 0; index < 10; index++) {
            drivers.push(new Driver('Buratinas', getRandomPoint()))
        }

        maker.setPassengers(...passengers);
        maker.setDrivers(...drivers);

        let journeys = maker.process();
        console.log(journeys[0], 'total journeys');
    });
});

function getRandomPoint() {
    var randomLat = getRandomDoubleInRange(Constants.getMinimumLatitude(), Constants.getMaximumLatitude());
    var randomLong = getRandomDoubleInRange(Constants.getMinimumLongitude(), Constants.getMaximumLongitude());
    return new Point(randomLat, randomLong);
}

function getRandomDoubleInRange(minimum: number, maximum: number) {
    var randomVal = minimum + (maximum - minimum) * Math.random();
    return randomVal;
}