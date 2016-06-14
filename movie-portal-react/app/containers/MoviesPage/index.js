import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { createSelector } from 'reselect';

import TextBox from '../../components/TextBox';
import MultiSelect from '../../components/MultiSelect';
import MovieList from '../../components/MovieList';
import MovieDetails from '../../components/MovieDetails';

import * as actions from './actions';

class MoviesPage extends React.Component {
    componentDidMount() {
        const { movies, categories, requestMovies, requestCategories } = this.props;
        if (!movies.length) requestMovies();
        if (!categories.length) requestCategories();
    }

    render() {
        const { filteredMovies, categories, selectedMovieId, movieDetailsMap, setSearchFilter, setCategoryFilter, searchFilter, categoryFilter } = this.props;
        const selectedMovieDetail = movieDetailsMap[selectedMovieId];
        return (
            <section>
                <section className="row columns">
                    <TextBox label="Search" value={searchFilter} onInput={setSearchFilter} />
                    <MultiSelect label="Categories" items={categories} value={categoryFilter} onChange={setCategoryFilter}
                                 keySelector={item => item.id} valueSelector={item => item.name} />
                </section>

                <section className="row">
                  <section className="medium-6 columns">
                      <MovieList movies={filteredMovies} onSelect={this.handleSelectMovie.bind(this)} />
                  </section>

                  <section className="medium-6 columns">
                      {!!selectedMovieDetail &&
                          <MovieDetails movieDetails={selectedMovieDetail} />
                      }
                  </section>
                </section>
            </section>
        );
    }

    handleSelectMovie(id) {
        const { selectMovie, requestMovieDetails, movieDetailsMap } = this.props;
        selectMovie(id);
        if (!movieDetailsMap[id]) requestMovieDetails(id);
    }
}

const moviesSelector = state => state.moviesPage.movies;
const categoryFilterSelector = state => state.moviesPage.categoryFilter;
const searchFilterSelector = state => state.moviesPage.searchFilter.toUpperCase();
const categoryFilteredMoviesSelector = createSelector(
    moviesSelector,
    categoryFilterSelector,
    (movies, categoryFilter) => categoryFilter.length
        ? movies.filter(movie => categoryFilter.indexOf(movie.category) >= 0)
        // We use spread operator to copy the array.
        // An alternative would be to use array.slice without args.
        : [...movies]
);
const filteredMoviesSelector = createSelector(
    categoryFilteredMoviesSelector,
    searchFilterSelector,
    (partlyFilteredMovies, searchFilter) => partlyFilteredMovies
        .filter(movie => movie.title.toUpperCase().indexOf(searchFilter) >= 0)
);

function mapStateToProps(state) {
    const { movies, categories, selectedMovieId, movieDetailsMap, searchFilter, categoryFilter } = state.moviesPage;
    return {
        movies, categories, movieDetailsMap,
        searchFilter, categoryFilter,
        selectedMovieId: selectedMovieId.present,
        filteredMovies: filteredMoviesSelector(state)
    };
}

const mapDispatchToProps = (dispatch) => bindActionCreators(actions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(MoviesPage);
