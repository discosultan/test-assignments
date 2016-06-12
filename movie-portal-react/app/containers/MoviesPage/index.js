import React from 'react';
import { connect } from 'react-redux';

// import TextFilter from '../components/text-filter';
// import CategoryFilter from '../components/category-filter';
import MovieList from '../../components/MovieList';
import MovieDetails from '../../components/MovieDetails';

import { requestMovies, selectMovie, requestMovieDetails, requestCategories } from './actions';

class MoviesPage extends React.Component {
    componentDidMount() {
        const { movies, categories, dispatch } = this.props;
        if (!movies.length) dispatch(requestMovies());
        if (!categories.length) dispatch(requestCategories());
    }

    render() {
        const { movies, selectedMovieId, movieDetailsMap } = this.props;
        const selectedMovieDetail = movieDetailsMap[selectedMovieId];
        return (
            <section className="row">
              <section className="medium-6 columns">
                  {/*<TextFilter ref="textFilter" onChange={filterMovies.bind(this)} />
                  <CategoryFilter ref="categoryFilter" onChange={filterMovies.bind(this)} categories={this.state.categories} />*/}
                  <MovieList movies={movies} onSelect={this.handleSelectMovie.bind(this)} />
              </section>
              <section className="medium-6 columns">
                  {!!selectedMovieDetail &&
                      <MovieDetails movieDetails={selectedMovieDetail} />
                  }
              </section>
            </section>
        );
    }

    handleSelectMovie(id) {
        const { dispatch, movieDetailsMap } = this.props;
        dispatch(selectMovie(id));
        if (!movieDetailsMap[id]) dispatch(requestMovieDetails(id));
    }
}

// function filterMovies() {
//     let filteredMovies = this.refs.textFilter.apply(this.state.movies, movie => movie.title);
//     filteredMovies = this.refs.categoryFilter.apply(filteredMovies, movie => movie.category);
//     this.setState(Object.assign(this.state, { filteredMovies: filteredMovies }))
// }

function mapStateToProps(state) {
    const { movies, categories, selectedMovieId, movieDetailsMap } = state;
    return { movies, categories, selectedMovieId, movieDetailsMap };
}

export default connect(mapStateToProps)(MoviesPage);
