﻿'use strict';

class Service {
    constructor() {
        this.arr = [];
    }
    add(newObj = null) {
        if (newObj === null)
            return false;
        newObj.id = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c, r) => ('x' == c ? (r = Math.random() * 16 | 0) : (r & 0x3 | 0x8)).toString(16));
        this.arr.push(newObj);
        return true;
    }
    getById(id = '') {
        for (let i = 0; i < this.arr.length; i++) {
            if (this.arr[i].id == id)
                return this.arr;
        }
        return null;
    }
    getAll() {
        return this.arr;
    }
    deleteById(id = '') {
        for (let i = 0; i < this.arr.length; i++) {
            if (this.arr[i].id == id) {
                let deletedObj = JSON.parse(JSON.stringify(this.arr[i]));
                this.arr.splice(i, 1);
                return deletedObj;
            }
        }
        return null;
    }
    replaceById(id, newObj = null) {
        if (newObj === null)
            return false;
        for (let i = 0; i < this.arr.length; i++) {
            if (this.arr[i].id == id) {
                this.arr[i] = newObj;
                return true;
            }
        }
        return false;
    }
    updateById(id = '', newObj = null) {
        if (newObj === null)
            return false;
        for (let i = 0; i < this.arr.length; i++) {
            if (this.arr[i].id == id) {
                for (let prop in newObj) {
                    this.arr[i][prop] = newObj[prop];
                    return true;
                }
            }
        }
        return false;
    }
}

let user1 = {
    name: 'Alex',
    age: 30,
}

let user2 = {
    name: 'Eric',
    age: 32,
}

let storage = new Service();

// Testing Add
storage.add(user1);
storage.add(user2);

console.log("Storage state 1:");
console.log(storage.getAll());

// Testing Update
storage.updateById(user1.id, user2);

console.log("Storage state 2:");
console.log(storage.getAll());

// Testing Delete
let delUser = storage.deleteById(user1.id);

console.log("Storage state 3:");
console.log(storage.getAll());

console.log(delUser);