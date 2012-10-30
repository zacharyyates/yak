Object.prototype.isDate = function (obj) {
    if(Object.prototype.toString.call(obj) === "[object Date]") {
        return isNaN((obj).getTime());
    }
    return false;
};
