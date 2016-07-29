// var Combinatorics = require('js-combinatorics');
function array_fill(startIndex, num, mixedVal) {
    var key;
    var tmpArr = {};

    if (!isNaN(startIndex) && !isNaN(num)) {
        for (key = 0; key < num; key++) {
            tmpArr[(key + startIndex)] = mixedVal
        }
    }

    return tmpArr
}

function assoc_apply(assoc, keys) {
    var array = [];
    for (var i = 0; i < Object.keys(keys).length; i++) {
        array[i] = assoc[keys[i]];
    }
    return array;
}

function permutate_vector(perm_vector, items_count, exp, i = 0) {
    if (i >= exp) return false;
    perm_vector[i]++;
    if (perm_vector[i] >= items_count) {
        perm_vector[i] = 0;
        perm_vector = permutate_vector(
            perm_vector,
            items_count,
            exp,
            i + 1
        );
    }
    return perm_vector;
};

export function permutation(array: any[], exp = -1) {
    exp = (exp < 0) ? array.length : exp;
    if (exp < 1) {
        return [[]];
    }

    var items_count = array.length;
    var perm_vector = array_fill(0, exp, 0);

    var permutations = [];
    while (perm_vector) {
        permutations.push(assoc_apply(array, perm_vector));
        perm_vector = permutate_vector(perm_vector, items_count, exp);
    }
    return permutations;
}


function* Permut(characters: string, length: number) {
    var result = [];

    function Combinations(characters: string, length: number): string[] {
        for (var i = 0; i < characters.length; i++) {
            // only want 1 character, just return this one
            if (length == 1)
                yield characters[i];

            // want more than one character, return this one plus all combinations one shorter
            // only use characters after the current one for the rest of the combinations
            else {
                var comb: string[] = Combinations(characters.substr(i + 1, characters.length - (i + 1)), length - 1);
                comb.forEach(s => result.push(characters[i] + s));
            }
        }
    }

}