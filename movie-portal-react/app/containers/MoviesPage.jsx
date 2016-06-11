import React from 'react';
import { connect } from 'react-redux';

// import TextFilter from '../components/text-filter.jsx';
// import CategoryFilter from '../components/category-filter.jsx';
import MovieList from '../components/MovieList.jsx';

import { requestMovies, requestCategories } from '../actions/index.js';

class MoviesPage extends React.Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {
    //         categories: [],
    //         filteredMovies: []
    //     }
    // }

    componentDidMount() {
        const { dispatch } = this.props;
        dispatch(requestMovies());
        // dispatch(requestCategories());

        // this.api.get('movies').then(movies => {
        //     this.setState(Object.assign(this.state, { movies: movies }));
        //     filterMovies.call(this);
        // });
        // this.api.get('categories').then(categories => this.setState(Object.assign(this.state, { categories: categories })));
    }

    render() {
        const { movies } = this.props;
        return (
            <section className="row columns">
                {/*<TextFilter ref="textFilter" onChange={filterMovies.bind(this)} />
                <CategoryFilter ref="categoryFilter" onChange={filterMovies.bind(this)} categories={this.state.categories} />*/}
                <MovieList movies={movies} />
            </section>
        );
    }
}

function filterMovies() {
    let filteredMovies = this.refs.textFilter.apply(this.state.movies, movie => movie.title);
    filteredMovies = this.refs.categoryFilter.apply(filteredMovies, movie => movie.category);
    this.setState(Object.assign(this.state, { filteredMovies: filteredMovies }))
}

function mapStateToProps(state) {
  const { movies } = state;
  return { movies };
}

export default connect(mapStateToProps)(MoviesPage);
