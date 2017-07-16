import * as React from "react";
import * as ReactDOM from "react-dom";
import { createStore, combineReducers, applyMiddleware } from "redux";
import { Provider } from "react-redux";
import { createEpicMiddleware, combineEpics } from "redux-observable";

import WeatherApi from "./apis/WeatherApi";
import MockWeatherApi from "./apis/MockWeatherApi";
import WeatherPage from "./containers/WeatherPage";
import weatherPageReducer from "./containers/WeatherPage/reducer";
import { fetchWeatherForCitiesEpic, fetchForecastForCityEpic } from "./containers/WeatherPage/epics";

const rootReducer = combineReducers({ weatherPage: weatherPageReducer });
const initialState = undefined;
const epicMiddleware = createEpicMiddleware(
    combineEpics(
        fetchWeatherForCitiesEpic,
        fetchForecastForCityEpic
    ), {
        dependencies: new WeatherApi()
    }
);
const middleware = applyMiddleware(epicMiddleware);

const store = createStore(
    rootReducer,
    initialState,
    middleware
);

ReactDOM.render(
    <Provider store={store}>
        <WeatherPage />
    </Provider>,
    document.getElementById("app")
);