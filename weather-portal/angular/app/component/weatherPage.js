require('./weatherCard.js');
require('../service/weatherApi.js');
require('../service/mockWeatherApi.js');

angular
    .module('app.component.weatherPage', [
        // The mock service below can be used instead of the real weather API to
        // prevent making external API calls.
        'app.service.weatherApi',
        // 'app.service.mockWeatherApi',
        'app.component.weatherCard'
    ])
    .component('weatherPage', weatherPage());

function weatherPage() {
    return {
        template: require('./weatherPage.html'),

        controller: ['weatherApi', function(weatherApi) {
            var cities = ['Amsterdam', 'Tallinn', 'Helsinki', 'Paris', 'Berlin'];

            // Fetch initial current weather data for cities.
            weatherApi.listCurrentForCities(cities).then(function(weatherList) {
                this.weatherList = weatherList;
            }.bind(this));

            this.listForecastForCity = function(city) {
                return weatherApi.listForecastForCity(city);
            }
        }]
    };
}