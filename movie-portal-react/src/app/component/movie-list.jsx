import React, { Component } from 'react';

export default class MovieList extends Component {
    render() {
        return (
            <ul>
                {this.props.movies.map(movie => 
                    <li key={movie.id}>
                        <a><h1>{movie.title}</h1></a>
                        <p><b>Category</b>: {movie.category}</p>
                    </li>
                )}
            </ul>
        );
    }
}