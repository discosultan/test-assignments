import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import MultiSelect from '../../components/MultiSelect';
import MovieList from '../../components/MovieList';
import MovieDetails from '../../components/MovieDetails';

import * as actions from './actions';
import { filterMovies } from './reducer';

class MoviesPage extends React.Component {
    componentDidMount() {
        const { requestMovies, requestCategories } = this.props;
        requestMovies();
        requestCategories();
    }

    render() {
        const {
            filteredMovies, categories, selectedMovieDetails, requestMovieDetails,
            searchFilter, categoryFilter, setSearchFilter, setCategoryFilter
        } = this.props;

        return (
            <section>
                // == TOP FILTERING SECTION ==
                // This section is a good candidate for another component.
                <section className="row columns">
                    <label><b>Search:</b>
                        <input type="text" value={searchFilter} onChange={event => setSearchFilter(event.target.value)} />
                    </label>
                    
                    <label><b>Categories:</b>
                        <select multiple value={categoryFilter} onChange={event => setCategoryFilter(
                            [...event.target.options].filter(option => option.selected).map(option => option.value)
                        )}>
                            {categories.map(category =>
                                // React requires a custom 'key' attribute to be defined for repeated elements
                                // to be able to correctly rerender DOM content ('value' may not be unique).
                                <option key={category.id} value={category.id}>{category.name}</option>
                            )}
                        </select>
                    </label>
                </section>

                // == BOTTOM MOVIE LIST AND DETAILS SECTION ==
                <section className="row">
                    <MovieList className="medium-6 columns" value={filteredMovies} onSelect={requestMovieDetails} />
                    <MovieDetails className="medium-6 columns" value={selectedMovieDetails} />
                </section>
            </section>
        );
    }
}

function mapStateToProps(state) {
    state = state.moviesPage.present || state.moviesPage;
    const { categories, selectedMovieDetails, searchFilter, categoryFilter } = state;
    return {
        categories, searchFilter, categoryFilter,
        selectedMovieDetails: selectedMovieDetails,
        filteredMovies: filterMovies(state)
    };
}

const mapDispatchToProps = dispatch => bindActionCreators(actions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(MoviesPage);