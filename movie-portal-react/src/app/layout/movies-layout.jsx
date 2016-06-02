import React, { Component } from 'react';

import TextFilter from '../component/text-filter.jsx';
import CategoryFilter from '../component/category-filter.jsx';
import MovieList from '../component/movie-list.jsx';

export default class MoviesLayout extends Component {
    constructor(props, context) {
        super(props);
        this.api = context.api;
        this.state = {
            movies: [],
            categories: [],
            filteredMovies: []
        }
    }

    componentDidMount() {
        this.api.get('movies').then(movies => {
            this.setState(Object.assign(this.state, { movies: movies }));
            filterMovies.call(this);
        });
        this.api.get('categories').then(categories => this.setState(Object.assign(this.state, { categories: categories })));
    }        
    
    render() {
        return (
            <section className="row columns">
                <TextFilter ref="textFilter" onChange={filterMovies.bind(this)} />                
                <CategoryFilter ref="categoryFilter" onChange={filterMovies.bind(this)} categories={this.state.categories} />                
                <MovieList movies={this.state.filteredMovies} />
            </section>
        );
    }
}

function filterMovies() {
    let filteredMovies = this.refs.textFilter.apply(this.state.movies, movie => movie.title);    
    filteredMovies = this.refs.categoryFilter.apply(filteredMovies, movie => movie.category);                
    this.setState(Object.assign(this.state, { filteredMovies: filteredMovies }))
}

MoviesLayout.contextTypes = {
    api: React.PropTypes.object.isRequired
}