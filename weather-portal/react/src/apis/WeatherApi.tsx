import { Observable } from "rxjs";
import "rxjs/add/operator/map";

import { Weather } from "../models";

const appId = "d6a5a71867647fd3062b104541bfb68f";
const baseUrl = "http://api.openweathermap.org/data/2.5";
const iconUrl = "http://openweathermap.org/img/w";
const cityToIdMap = {
    Amsterdam: 2759794,
    Tallinn: 588409,
    Paris: 2968815,
    Berlin: 2950159,
    Helsinki: 658224
};

export interface IWeatherApi {
    listWeatherForCities(cities: string[]): Observable<Weather[]>,
    listForecastForCity(city: string): Observable<Weather[]>
}

export default class WeatherApi implements IWeatherApi {
    readonly cache = {};

    listWeatherForCities(cities) {
        const formattedIds = cities.map(city => cityToIdMap[city]).join(",");
        const url = `${baseUrl}/group?appid=${appId}&id=${formattedIds}`;
        let result = this.cache[url];
        if (!result) {
            result = Observable
                .from(fetch(url).then(res => res.json() as Promise<OpenWeatherMapResponse>))
                .map(res => res.list.map(openWeatherMapModelToAppModel));
            this.cache[url] = result;
        }
        return result;
    }

    listForecastForCity(city) {
        const url = `${baseUrl}/forecast?appid=${appId}&id=${cityToIdMap[city]}&cnt=5`;
        let result = this.cache[url];
        if (!result) {
            result = Observable
                .from(fetch(url).then(res => res.json() as Promise<OpenWeatherMapResponse>))
                .map(res => res.list.map(openWeatherMapModelToAppModel));
            this.cache[url] = result;
        }
        return result;
    }
}

function openWeatherMapModelToAppModel(model: OpenWeatherMapModel): Weather {
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
        temp: model.main.temp,
        wind: {
            speed: model.wind.speed,
            deg: model.wind.deg
        },
        iconUrl: `${iconUrl}/${model.weather[0].icon}.png`,
        date: new Date(model.dt * 1000) // model.dt is a UNIX timestamp.
    };
}

interface OpenWeatherMapResponse {
    list: OpenWeatherMapModel[]
}

interface OpenWeatherMapModel {
    name: string,
    weather: { icon: string }[]
    wind: { speed: number, deg: number },
    main: { temp: number },
    dt: number
}