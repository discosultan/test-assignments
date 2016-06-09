import React from 'react';
import { connect } from 'react-redux'

// import TextFilter from '../components/text-filter.jsx';
// import CategoryFilter from '../components/category-filter.jsx';
// import MovieList from '../components/movie-list.jsx';

import { requestMovies } from '../actions/index.js';

class MoviesPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            movies: [],
            categories: [],
            filteredMovies: []
        }
    }

    componentDidMount() {
        const { dispatch } = this.props;
        dispatch(requestMovies());

        // this.api.get('movies').then(movies => {
        //     this.setState(Object.assign(this.state, { movies: movies }));
        //     filterMovies.call(this);
        // });
        // this.api.get('categories').then(categories => this.setState(Object.assign(this.state, { categories: categories })));
    }

    render() {
        return (
            <section className="row columns">tere
                {/*<TextFilter ref="textFilter" onChange={filterMovies.bind(this)} />
                <CategoryFilter ref="categoryFilter" onChange={filterMovies.bind(this)} categories={this.state.categories} />
                <MovieList movies={this.state.filteredMovies} />*/}
            </section>
        );
    }
}

function filterMovies() {
    let filteredMovies = this.refs.textFilter.apply(this.state.movies, movie => movie.title);
    filteredMovies = this.refs.categoryFilter.apply(filteredMovies, movie => movie.category);
    this.setState(Object.assign(this.state, { filteredMovies: filteredMovies }))
}

export default connect()(MoviesPage);
