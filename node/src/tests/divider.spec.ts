import { group, sortByLongtitude } from '../utils/divider';
import { Booking } from '../utils/import';
import { Point } from '../models/point';

var expect: Chai.ExpectStatic = require('chai').expect;

describe.only('divider', () => {

    it('should sort by longtitude', () => {
        var data: Booking[] = [
            { pickup: new Point(0, 10), id: 'p1' },
            { pickup: new Point(0, 25), id: 'p2' },
            { pickup: new Point(0, 0), id: 'p3' },
            { pickup: new Point(0, -75), id: 'p4' },
            { pickup: new Point(0, -6), id: 'p5' },
            { pickup: new Point(0, 71), id: 'p6' },
        ]

        var sorted = sortByLongtitude(data);

        expect(sorted[0].id).to.equal('p4');
        expect(sorted[1].id).to.equal('p5');
        expect(sorted[2].id).to.equal('p3');
        expect(sorted[3].id).to.equal('p1');
        expect(sorted[4].id).to.equal('p2');
        expect(sorted[5].id).to.equal('p6');
    });

    it('should group drivers with passengers', ()=> {
        var data: Booking[] = [
            { pickup: new Point(0, 10), dropoff: new Point(0, 10), id: 'p1', isDriver: false },
            { pickup: new Point(54.735558, 25.22621115), dropoff: new Point(54.707950, 25.297634), id: 'p2', isDriver: true },
            { pickup: new Point(0, 10), dropoff: new Point(0, 10), id: 'p3', isDriver: false },
            { pickup: new Point(0, 10), dropoff: new Point(0, 10), id: 'p4', isDriver: false },
            { pickup: new Point(0, 10), dropoff: new Point(0, 10), id: 'p5', isDriver: false },
        ];

        var drivers = group(data);
        console.log(drivers.values().next().value);
        
        expect(drivers.size).to.equal(1);
    });

    it('should share reference', () => {
        var booking = { isDriver: false };
        var orderings = new Set<Booking>();
        orderings.add(booking);
        booking.isDriver = true;

        expect(orderings.values().next().value.isDriver).to.be.true;
    });
});