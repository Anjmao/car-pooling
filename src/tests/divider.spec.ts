import { group, sortByLongtitude } from '../utils/divider';
import { Booking } from '../utils/import';
import { Point } from '../models/point';

var expect: Chai.ExpectStatic = require('chai').expect;

describe('divider', () => {

    it.only('should sort by longtitude', () => {
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
});