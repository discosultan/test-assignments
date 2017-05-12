angular
    .module('app.service.mockWeatherApi', [])
    .service('weatherApi', ['$q', mockWeatherApi]);

function mockWeatherApi($q) {
    var service = {};

    service.listCurrentForCities = function() {
        return $q.resolve([
            {
                'name': 'Amsterdam',
                'temperature': 281.64,
                'wind': {
                    'speed': 4.1,
                    'direction': 240
                },
                'iconUrl': 'http://openweathermap.org/img/w/04n.png',
                'date': new Date()
            },
            {
                'name': 'Tallinn',
                'temperature': 274.15,
                'wind': {
                'speed': 7.2,
                'direction': 200
                },
                'iconUrl': 'http://openweathermap.org/img/w/13n.png',
                'date': new Date()
            },
            {
                'name': 'Helsinki',
                'temperature': 284.95,
                'wind': {
                'speed': 2.1,
                'direction': 250
                },
                'iconUrl': 'http://openweathermap.org/img/w/10n.png',
                'date': new Date()
            },
            {
                'name': 'Paris',
                'temperature': 281.14,
                'wind': {
                'speed': 2.6,
                'direction': 60
                },
                'iconUrl': 'http://openweathermap.org/img/w/01n.png',
                'date': new Date()
            },
            {
                'name': 'Berlin',
                'temperature': 278.13,
                'wind': {
                'speed': 2.1,
                'direction': 230
                },
                'iconUrl': 'http://openweathermap.org/img/w/01n.png',
                'date': new Date()
            }
        ]);
    }

    service.listForecastForCity = function() {
        return service.listCurrentForCities();
    }

    return service;
}