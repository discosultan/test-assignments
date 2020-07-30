/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// identity function for calling harmony imports with the correct context
/******/ 	__webpack_require__.i = function(value) { return value; };
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 1);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(2);
__webpack_require__(7);
__webpack_require__(6);

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
        template: __webpack_require__(9),

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

/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

// We are using common.js require syntax to tell WebPack of dependencies.
__webpack_require__(0);

// Define root module and its dependencies. All the app's components
// reside in their own modules for now. For a larger application, where
// module boundaries are better defined, it might make sense to group
// multiple components under a single module.
angular.module('app', ['app.component.weatherPage']);

// Additional config would go here or in its dedicated config file.

/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(5);
__webpack_require__(4);
__webpack_require__(3);

angular
    .module('app.component.weatherCard', [
        'app.filter.kelvinToCelsius',
        'app.filter.dateToHourMinute',
        'app.component.windWidget'])
    .component('weatherCard', weatherCard());

function weatherCard() {
    return {
        template: __webpack_require__(8),

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

/***/ }),
/* 3 */
/***/ (function(module, exports, __webpack_require__) {

angular
    .module('app.component.windWidget', [])
    .component('windWidget', windWidget());

function windWidget() {
    return {
        template: __webpack_require__(10),

        bindings: {
            value: '<'
        }
    };
}

/***/ }),
/* 4 */
/***/ (function(module, exports) {

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

/***/ }),
/* 5 */
/***/ (function(module, exports) {

angular
    .module('app.filter.kelvinToCelsius', [])
    .filter('kelvinToCelsius', kelvinToCelsius);

function kelvinToCelsius() {
    return function(kelvin) {
        return kelvin - 273.15;
    }
}

/***/ }),
/* 6 */
/***/ (function(module, exports) {

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

/***/ }),
/* 7 */
/***/ (function(module, exports) {

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

/***/ }),
/* 8 */
/***/ (function(module, exports) {

module.exports = "<style>\r\n    .weather-card.mdl-card {\r\n        width: 360px;\r\n    }\r\n    .weather-card__forecast {\r\n        max-height: 0;\r\n        transition: max-height 1s ease;\r\n        overflow: hidden;\r\n    }\r\n    .weather-card__forecast--expanded {\r\n        max-height: 425px;\r\n    }\r\n</style>\r\n\r\n<div class=\"weather-card mdl-card mdl-shadow--2dp\">\r\n    <!-- City name -->\r\n    <div class=\"mdl-card__title\">\r\n        <h2 class=\"mdl-color-text--accent\">{{::$ctrl.value.name}}</h2>\r\n    </div>\r\n\r\n    <!-- Current weather data -->\r\n    <div class=\"mdl-card__supporting-text\">\r\n        Temperature: {{::$ctrl.value.temperature | kelvinToCelsius | number:2 }} C\r\n        Wind: <wind-widget value=\"$ctrl.value.wind\"></wind-widget>\r\n    </div>\r\n\r\n    <!-- Expandable forecast section -->\r\n    <div class=\"weather-card__forecast\"\r\n         ng-class=\"{'weather-card__forecast--expanded': $ctrl.expanded}\">\r\n        <table class=\"mdl-data-table\">\r\n            <thead>\r\n                <tr>\r\n                    <th>Time</th>\r\n                    <th>Forecast</th>\r\n                    <th>Temp</th>\r\n                    <th>Wind</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n                <tr ng-repeat=\"value in $ctrl.forecast\">\r\n                    <td class=\"mdl-data-table__cell\">{{::value.date | dateToHourMinute}}</td>\r\n                    <td><img ng-src=\"{{::$ctrl.value.iconUrl}}\" /></td>\r\n                    <td>{{::value.temperature | kelvinToCelsius | number:2 }} C</td>\r\n                    <td><wind-widget value=\"value.wind\"></wind-widget></td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n\r\n    <!-- Forecast expansion button -->\r\n    <div class=\"mdl-card__actions mdl-card--border\">\r\n        <a class=\"mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect\"\r\n           ng-click=\"$ctrl.expand()\">\r\n            Forecast\r\n            <span ng-if=\"!$ctrl.expanded\">&#x25bc;</span>\r\n            <span ng-if=\"$ctrl.expanded\">&#x25b2;</span>\r\n        </a>\r\n    </div>\r\n\r\n    <!-- Top-right weather icon -->\r\n    <div class=\"mdl-card__menu\">\r\n        <img ng-src=\"{{::$ctrl.value.iconUrl}}\" />\r\n    </div>\r\n</div>";

/***/ }),
/* 9 */
/***/ (function(module, exports) {

module.exports = "<!-- \r\n    We should probably add a more sophisticated layout here.\r\n    Currently all the cards are simply stacked horizontally.\r\n\r\n    Styles are defined together with their corresponding components. Since\r\n    these are going to be global, they are named based on BEM guidelines\r\n    to scope them.\r\n-->\r\n\r\n<style>\r\n    .weather-page__element {\r\n        padding: 4px;\r\n    }\r\n</style>\r\n\r\n<section class=\"mdl-grid\">\r\n    <div ng-repeat=\"value in $ctrl.weatherList\" class=\"weather-page__element\">\r\n        <weather-card value=\"value\" on-request-forecast=\"$ctrl.listForecastForCity\">\r\n    </div>\r\n</section>";

/***/ }),
/* 10 */
/***/ (function(module, exports) {

module.exports = "<style>\r\n    .wind-widget__arrow {\r\n        width: 16px;\r\n        height: 16px;\r\n        display: inline-block;\r\n        background: url('./img/arrow.svg') center / cover;\r\n    }\r\n</style>\r\n\r\n<span>\r\n    {{::$ctrl.value.speed}} m/s\r\n    &nbsp;\r\n    <span class=\"wind-widget__arrow\" style=\"transform: rotate({{$ctrl.value.direction}}deg)\"></span>\r\n</span>";

/***/ })
/******/ ]);