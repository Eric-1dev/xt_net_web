'use strict';

let str = 'У попа была собака';

let wordsArr = Array();

let charsToDelete = Array();

wordsArr = splitMulti(str, ['?', '!', ':', ';', ',', '.', '\n', '\t', ' ']);

for (let i = 0; i < wordsArr.length; i++) { // Каждое слово
    for (let j = 0; j < wordsArr[i].length; j++) { // Каждая буква в слове
        if (wordsArr[i].toLowerCase().includes(wordsArr[i][j], j + 1))
            charsToDelete.push(wordsArr[i][j]);
    }
}

for (let i = 0; i < charsToDelete.length; i++) {
    str = str.split(charsToDelete[i]).join("");
}

console.log(str);

function splitMulti(str, separators) {
    let tempChar = separators[0];
    for (let i = 1; i < separators.length; i++) {
        str = str.split(separators[i]).join(tempChar);
    }
    str = str.split(tempChar);
    return str;
}
