import React from 'react';

export default class MovieList extends React.Component {
    render() {
        const { movies, onSelect } = this.props;
        return (
            <ul>
                {movies.map(movie =>
                    <li key={movie.id}>
                        <a onClick={onSelect.bind(this, movie.id)}><h1>{movie.title}</h1></a>
                        <p><b>Category</b>: {movie.category}</p>
                    </li>
                )}
            </ul>
        );
    }
}
