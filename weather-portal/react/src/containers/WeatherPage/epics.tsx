import "rxjs/add/operator/mergeMap";

import { Weather } from "../../models";

// ACTIONS

export enum ActionType {
    FetchWeatherForCities = "FetchWeatherForCities",
    FetchWeatherForCitiesFulfilled = "FetchWeatherForCitiesFulfilled",
    FetchForecastForCity = "FetchForecastForCity",
    FetchForecastForCityFulfilled = "FetchForecastForCityFulfilled"
}

export const fetchWeatherForCities = (cities: string[]) => ({ type: ActionType.FetchWeatherForCities, cities });
export const fetchWeatherForCitiesFulfilled = (weathers: Weather[]) => ({ type: ActionType.FetchWeatherForCitiesFulfilled, weathers });
export const fetchForecastForCity = (city: string) => ({ type: ActionType.FetchForecastForCity, city });
export const fetchForecastForCityFulfilled = (forecast: Weather[], city: string) => ({ type: ActionType.FetchForecastForCityFulfilled, forecast, city });

// EPICS

// TODO: Add strong typings once the following PR gets resolved
// TODO: https://github.com/redux-observable/redux-observable/pull/250
export const fetchWeatherForCitiesEpic: any = (action$, store, api) => action$
    .ofType(ActionType.FetchWeatherForCities)
    .mergeMap(action => api
        .listWeatherForCities(action.cities)
        .map(fetchWeatherForCitiesFulfilled)
    );

export const fetchForecastForCityEpic: any = (action$, state, api) => action$
    .ofType(ActionType.FetchForecastForCity)
    .mergeMap(action => api
        .listForecastForCity(action.city)
        .map(forecast => fetchForecastForCityFulfilled(forecast, action.city))
    );