angular
    .module('app.filter.kelvinToCelsius', [])
    .filter('kelvinToCelsius', kelvinToCelsius);

function kelvinToCelsius() {
    return function(kelvin) {
        return kelvin - 273.15;
    }
}