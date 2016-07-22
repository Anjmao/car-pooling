
import * as express from "express";
var app = express();

import { MatchMaker } from './match-maker';
import { Passenger } from './models/passenger';
import { Driver } from './models/driver';
import { Point } from './models/point';
import { Constants } from './models/constants';


app.get('/', function (req, res) {
    
    var maker = new MatchMaker();
    var p1 = new Passenger('Vovka', getRandomPoint(), getRandomPoint());
    var p2 = new Passenger('Borkia', getRandomPoint(), getRandomPoint());

    var d1 = new Driver('Buratinas', getRandomPoint());

    maker.setPassengers(p1, p2);
    maker.setDrivers(d1);
    var journeys = maker.process();

    console.log(journeys, 'ddd');
});

console.log('Listening on port: ' + 8888);
app.listen(8888);

function getRandomPoint() {
    var randomLat = getRandomDoubleInRange(Constants.getMinimumLatitude(), Constants.getMaximumLatitude());
    var randomLong = getRandomDoubleInRange(Constants.getMinimumLongitude(), Constants.getMaximumLongitude());
    return new Point(randomLat, randomLong);
}

function getRandomDoubleInRange(minimum: number, maximum: number) {
    var randomVal = minimum + (maximum - minimum) * Math.random();
    return randomVal;
}