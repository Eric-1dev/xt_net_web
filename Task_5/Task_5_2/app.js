'use strict';

var str = '3.5 + 4 * 10 - 5.3 / 5 =';

var numbers = [];
var operators = [];

var regNums = /\s*[\-\+\/\*=]\s*/;

var regOperators = /[\d\.=\s]+/;

var regCheckInput = /\s*(\d+?(\.{1})?\d*\s*[\+\-\/\*]\s*)+\d+?(\.{1})?\d*\s*=\s*/;

if (str.replace(regCheckInput, '') == "")
    console.log("Correct string");
else
    console.log("Incorrect string");

numbers = str.split(regNums);
operators = str.split(regOperators);

console.log(str);
console.log(numbers);
console.log(operators);
