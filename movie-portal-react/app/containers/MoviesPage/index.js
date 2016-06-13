import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import TextFilter from '../../components/TextFilter';
import CategoryFilter from '../../components/CategoryFilter';
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
        const { movies, categories, selectedMovieId, movieDetailsMap } = this.props;
        const selectedMovieDetail = movieDetailsMap[selectedMovieId];
        return (
            <section>
                <section className="row columns">
                    <TextFilter ref="textFilter" onChange={this.handleFilterMovies.bind(this)} />
                    <CategoryFilter ref="categoryFilter" categories={categories} onChange={this.handleFilterMovies.bind(this)} />
                </section>

                <section className="row">
                  <section className="medium-6 columns">
                      <MovieList movies={movies} onSelect={this.handleSelectMovie.bind(this)} />
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

    // handleFilterByCategory(categoryIds) {
    //     const { setCategoryFilter } = this.props;
    //     setCategoryFilter(categoryIds);
    // }
    //
    // handleFilterBySearchString(searchString) {
    //     const { setSearchFilter } = this.props;
    //     setSearchFilter(searchString);
    // }

    handleFilterMovies() {
        const { movies, filterMovies } = this.props;
        filterMovies(movies);
    }

    handleSelectMovie(id) {
        const { selectMovie, requestMovieDetails, movieDetailsMap } = this.props;
        selectMovie(id);
        if (!movieDetailsMap[id]) requestMovieDetails(id);
    }
}

// function filterMovies() {
//     let filteredMovies = this.refs.textFilter.apply(this.state.movies, movie => movie.title);
//     filteredMovies = this.refs.categoryFilter.apply(filteredMovies, movie => movie.category);
//     this.setState(Object.assign(this.state, { filteredMovies: filteredMovies }))
// }

const mapStateToProps = ({movies, categories, selectedMovieId, movieDetailsMap}) =>
    ({ movies, categories, selectedMovieId: selectedMovieId.present, movieDetailsMap });

const mapDispatchToProps = (dispatch) => bindActionCreators(actions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(MoviesPage);
