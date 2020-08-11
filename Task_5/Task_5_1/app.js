'use strict';

var str = 'У попа была собака';

var wordsArr = [];

var charsToDelete = [];

wordsArr = splitMulti(str, ['?', '!', ':', ';', ',', '.', '\n', '\t', ' ']);

for (var i = 0; i < wordsArr.length; i++) { // Каждое слово
    for (var j = 0; j < wordsArr[i].length; j++) { // Каждая буква в слове
        if (wordsArr[i].toLowerCase().includes(wordsArr[i][j], j + 1))
            charsToDelete.push(wordsArr[i][j]);
    }
}

for (var i = 0; i < charsToDelete.length; i++) {
    str = str.split(charsToDelete[i]).join("");
}

console.log(str);

function splitMulti(str, separators) {
    var tempChar = separators[0];
    for (var i = 1; i < separators.length; i++) {
        str = str.split(separators[i]).join(tempChar);
    }
    str = str.split(tempChar);
    return str;
}
