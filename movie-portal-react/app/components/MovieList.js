import React from 'react';

export default class MovieList extends React.Component {
    render() {
        const { value, onSelect } = this.props;
        return (
            <ul>
                {value.map(movie =>
                    <li key={movie.id}>
                        <a onClick={onSelect.bind(this, movie.id)}>
                            <h1>{movie.title}</h1>
                        </a>

                        <p><b>Category:</b> {movie.category}</p>
                    </li>
                )}
            </ul>
        );
    }
}
