import * as React from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";

import WeatherCard from "../../components/WeatherCard";
import { Weather } from "../../models";
import { fetchWeatherForCities, fetchForecastForCity } from "./epics";

interface WeatherPageProps {
    weathers: Weather[],
    forecasts: { [city: string]: Weather[] },
    fetchWeatherForCities: (cities: string[]) => void,
    fetchForecastForCity: (city: string) => void
}

class WeatherPage extends React.Component<WeatherPageProps> {
    componentWillMount() {
        this.props.fetchWeatherForCities(["Amsterdam", "Tallinn", "Helsinki", "Paris", "Berlin"]);
    }

    render() {
        const { weathers, forecasts, fetchForecastForCity } = this.props;
        return (
            <section className="mdl-grid">
                { weathers.map(weather => (
                    <div key={weather.name} className="weather-page__element">
                        <WeatherCard
                            currentWeather={weather}
                            forecast={forecasts[weather.name]}
                            onExpand={fetchForecastForCity}
                        /> 
                    </div>
                ))}
            </section>
        );
    }
}

// CONNECT

const mapStateToProps = state => ({
    weathers: state.weatherPage.weathers,
    forecasts: state.weatherPage.forecasts
});
const mapDispatchToProps = (dispatch: Dispatch<any>) =>
    bindActionCreators({ fetchWeatherForCities, fetchForecastForCity }, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(WeatherPage as any); // TODO