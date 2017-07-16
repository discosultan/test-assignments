import { Observable } from "rxjs";
import { IWeatherApi } from "./WeatherApi";

export default class MockWeatherApi implements IWeatherApi {
    listWeatherForCities() {
        return Observable.of([
            {
                'name': 'Amsterdam',
                'temp': 281.64,
                'wind': {
                    'speed': 4.1,
                    'deg': 240
                },
                'iconUrl': 'http://openweathermap.org/img/w/04n.png',
                'date': new Date()
            },
            {
                'name': 'Tallinn',
                'temp': 274.15,
                'wind': {
                    'speed': 7.2,
                    'deg': 200
                },
                'iconUrl': 'http://openweathermap.org/img/w/13n.png',
                'date': new Date()
            },
            {
                'name': 'Helsinki',
                'temp': 284.95,
                'wind': {
                    'speed': 2.1,
                    'deg': 250
                },
                'iconUrl': 'http://openweathermap.org/img/w/10n.png',
                'date': new Date()
            },
            {
                'name': 'Paris',
                'temp': 281.14,
                'wind': {
                    'speed': 2.6,
                    'deg': 60
                },
                'iconUrl': 'http://openweathermap.org/img/w/01n.png',
                'date': new Date()
            },
            {
                'name': 'Berlin',
                'temp': 278.13,
                'wind': {
                    'speed': 2.1,
                    'deg': 230
                },
                'iconUrl': 'http://openweathermap.org/img/w/01n.png',
                'date': new Date()
            }
        ]);
    }

    listForecastForCity(city) {
        return this.listWeatherForCities().map(obs => obs.map(weather => {
            weather.name = city;
            return weather;
        }));
    }
}