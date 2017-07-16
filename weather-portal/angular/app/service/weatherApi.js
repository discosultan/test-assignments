angular
    .module('app.service.weatherApi', [])
    .service('weatherApi', ['$http', weatherApi]);

function weatherApi($http) {
    // We could use some caching here (localStorage, for example) because there
    // is no point in refetching the data from external API multiple times in
    // a small time period.

    // We could also publish an event when an external request is made so that
    // other components could subscribe and show a loading indicator if desired.

    // The app id should be configured in app.js or perhaps a dedicated
    // config file and injected through angular.value, for example.
    var appId = 'd6a5a71867647fd3062b104541bfb68f';

    var listCurrentForCitiesUrl = 'http://api.openweathermap.org/data/2.5/group';
    listCurrentForCitiesUrl += '?appid=' + appId;

    var getForecastForCityLimit = 5;
    var getForecastForCityUrl = 'http://api.openweathermap.org/data/2.5/forecast';
    getForecastForCityUrl += '?appid=' + appId;
    getForecastForCityUrl += '&cnt=' + getForecastForCityLimit;

    var iconUrl = 'http://openweathermap.org/img/w/';

    // A sample map to map requested city name to open weather data city id.
    // We wouldn't normally keep such a map at the client side as it would get
    // huge considering the amount of cities in the world.
    var cityToIdMap = {
        Amsterdam: 2759794,
        Tallinn: 588409,
        Paris: 2968815,
        Berlin: 2950159,
        Helsinki: 658224
    };

    var service = {};

    service.listCurrentForCities = function(cities) {
        var ids = cities
            .map(function(city) { return cityToIdMap[city]; })
            .join(',');
        var fullUrl = listCurrentForCitiesUrl + '&id=' + ids;

        return $http.get(fullUrl).then(function(response) {
            var models = response.data.list;
            return models.map(function(model) {
                return openWeatherModelToAppModel(model);
            })
        });
    }

    service.listForecastForCity = function(city) {
        var id = cityToIdMap[city];
        var fullUrl = getForecastForCityUrl + '&id=' + id;

        return $http.get(fullUrl).then(function(response) {
            var models = response.data.list;
            return models.map(function(model) {
                return openWeatherModelToAppModel(model);
            })
        });
    }

    function openWeatherModelToAppModel(model) {
        // Open Weather Map weather model.
        // Ref: http://openweathermap.org/current

        // Our app's weather model.
        // {
        //     name: string;
        //     temperature: number;   // kelvin
        //     wind: {
        //         speed: number;     // m/sec
        //         direction: number; // degrees from N
        //     };
        //     iconUrl: string;
        //     date: Date;
        // }

        return {
            name: model.name,
            temperature: model.main.temp,
            wind: {
                speed: model.wind.speed,
                direction: model.wind.deg
            },
            iconUrl: iconUrl + model.weather[0].icon + '.png',
            date: new Date(model.dt * 1000) // model.dt is a UNIX timestamp.
        };
    }

    return service;
}