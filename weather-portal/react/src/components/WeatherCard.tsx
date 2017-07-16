import * as React from "react";

import WindWidget from "./WindWidget";
import * as convert from "../utils/convert";
import * as format from "../utils/format";
import { Weather } from "../models";

interface WeatherCardProps {
    currentWeather: Weather,
    forecast?: Weather[],
    onExpand: (city: string) => void
}

interface WeatherCardState {
    expanded: boolean
}

export default class WeatherCard extends React.Component<WeatherCardProps, WeatherCardState> {
    constructor(props) {
        super(props);
        this.state = { expanded: false };
    }

    expand() {
        const { currentWeather, forecast, onExpand } = this.props;
        const { expanded } = this.state;
        this.setState({ expanded: !expanded });
        if (!forecast) onExpand(currentWeather.name);
    }

    render() {
        const { currentWeather, forecast } = this.props;
        const { expanded } = this.state;
        const formattedTemp = convert.kelvinToCelsius(currentWeather.temp).toFixed(2);

        return (
            <div className="weather-card mdl-card mdl-shadow--2dp">
                { /* City name */ }
                <div className="mdl-card__title">
                    <h2 className="mdl-color-text--accent">{currentWeather.name}</h2>
                </div>

                { /* Current weather data */ }
                <div className="mdl-card__supporting-text">
                    Temperature: {formattedTemp} C
                    Wind: <WindWidget value={currentWeather.wind}></WindWidget>
                </div>

                { /* Expandable forecast section */ }
                <div className={"weather-card__forecast" + (expanded ? " weather-card__forecast--expanded" : "")}>
                    <table className="mdl-data-table">
                        <thead>
                            <tr>
                                <th>Time</th>
                                <th>Forecast</th>
                                <th>Temp</th>
                                <th>Wind</th>
                            </tr>
                        </thead>
                        <tbody>
                            { forecast && forecast.map((predictedWeather, i) => (
                                <tr key={i}>
                                    <td className="mdl-data-table__cell">
                                        {format.dateToHourMinuteStr(predictedWeather.date)}
                                    </td>
                                    <td><img src={predictedWeather.iconUrl} /></td>
                                    <td>{formattedTemp} C</td>
                                    <td><WindWidget value={predictedWeather.wind}></WindWidget></td>
                                </tr>
                            )) }
                        </tbody>
                    </table>
                </div>

                { /* Forecast expansion button */ }
                <div className="mdl-card__actions mdl-card--border">
                    <a className="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect"
                       onClick={this.expand.bind(this)}>
                        Forecast
                        { expanded
                            ? <span>&#x25bc;</span>
                            : <span>&#x25b2;</span>
                        }
                    </a>
                </div>

                { /* Top-right weather icon */ }
                <div className="mdl-card__menu">
                    <img src={currentWeather.iconUrl} />
                </div>
            </div>
        );
    }
}