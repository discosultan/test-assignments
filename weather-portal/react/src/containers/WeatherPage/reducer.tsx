import { Weather } from "../../models";
import { ActionType } from "./epics";

// REDUCER

type FetchWeatherForCitiesAction = {
    type: ActionType.FetchWeatherForCities,
    cities: string[]
};
type FetchWeatherForCitiesFulfilled = {
    type: ActionType.FetchWeatherForCitiesFulfilled,
    weathers: Weather[]
};
type FetchForecastForCity = {
    type: ActionType.FetchForecastForCity,
    city: string
};
type FetchForecastForCityFulfilled = {
    type: ActionType.FetchForecastForCityFulfilled,
    city: string,
    forecast: Weather[]
};

type Action =
    FetchWeatherForCitiesAction |
    FetchWeatherForCitiesFulfilled |
    FetchForecastForCity |
    FetchForecastForCityFulfilled;

interface State {
    weathers: Weather[];
    forecasts: { [city: string]: Weather[] }
}

const initialState: State = {
    weathers: [],
    forecasts: {}
};
export default function reducer(state = initialState, action: Action) {
    switch (action.type) {
        case ActionType.FetchWeatherForCitiesFulfilled:
            return { ...state, weathers: action.weathers };
        case ActionType.FetchForecastForCityFulfilled:
            const newForecasts = { ...state.forecasts };
            newForecasts[action.city] = action.forecast;
            return { ...state, forecasts: newForecasts };
        default:
            return state;
    }
}