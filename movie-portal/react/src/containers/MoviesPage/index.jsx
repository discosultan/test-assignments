import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

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
                {/* == TOP FILTERING SECTION == */}
                {/* TODO: This section is a good candidate for another component. */}
                <section className="row column">
                    <label><b>Search:</b>
                        <input type="text" value={searchFilter} onChange={event => setSearchFilter(event.target.value)} />
                    </label>
                    
                    <label><b>Categories:</b>
                        <select multiple value={categoryFilter} onChange={event => setCategoryFilter(
                            // Since `event.target.options` returns an HTMLCollection, we first convert it
                            // to an Array using ES2015 spreading operator in order to use Array.prototype
                            // methods such as `filter` and `map`. 
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

                {/* == BOTTOM MOVIE LIST AND DETAILS SECTION == */}
                <section className="row">
                    <MovieList className="medium-6 column" value={filteredMovies} onSelect={requestMovieDetails} />
                    <MovieDetails className="medium-6 column" value={selectedMovieDetails} />
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
