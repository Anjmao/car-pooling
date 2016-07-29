var expect: Chai.ExpectStatic = require('chai').expect;
import { permutation } from '../models/permutator';
var BUFFER = 65;
describe.only('Permutator.', () => {

    it('should create permutations for total 2 passengers', () => {
        
        var result = permutation(createChars(2), 2 * 2);
        expect(result.length).to.be.eq(24);
    });

    it('should create permutations for total 100 passengers', () => {
        var orderings = [];
        var result = permutation(createChars(100), 2 * 2);
        expect(result.length).to.be.eq(1680);
    });

});

function createChars(passengersLength: number) {
    var passengerChars = [];
    for (var i = BUFFER; i < BUFFER + passengersLength; i++) {
        var letter = String.fromCharCode(i);
        passengerChars.push(letter, letter);
    }
    return passengerChars;
}