interface Object {
    isDate(obj: Object): bool;
}

Object.prototype.isDate = function (obj: Object) {
    if (Object.prototype.toString.call(obj) === "[object Date]") {
        return isNaN((<Date>obj).getTime());
    }
    return false;
};