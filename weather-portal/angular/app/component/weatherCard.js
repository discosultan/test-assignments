require('../filter/kelvinToCelsius');
require('../filter/dateToHourMinute');
require('./windWidget');

angular
    .module('app.component.weatherCard', [
        'app.filter.kelvinToCelsius',
        'app.filter.dateToHourMinute',
        'app.component.windWidget'])
    .component('weatherCard', weatherCard());

function weatherCard() {
    return {
        template: require('./weatherCard.html'),

        controller: function() {
            this.expanded = false;

            this.expand = function() {
                this.expanded = !this.expanded;
                if (!this.forecast) {
                    this.onRequestForecast(this.value.name).then(function(forecast) {
                        this.forecast = forecast;
                    }.bind(this));
                }
            }.bind(this);
        },

        bindings: {
            value: '<', // one-way binding
            onRequestForecast: '<'
        }
    };
}