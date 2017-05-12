angular
    .module('app.filter.dateToHourMinute', [])
    .filter('dateToHourMinute', dateToHourMinute);

function dateToHourMinute() {
    return function(date) {
        return zeroPadSingleDigit(date.getHours()) + ':' + zeroPadSingleDigit(date.getMinutes());
    }
}

function zeroPadSingleDigit(value) {
    return ('0' + value).slice(-2);
}