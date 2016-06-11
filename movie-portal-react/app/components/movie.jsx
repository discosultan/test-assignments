import React from 'react';

export default class Movie extends React.Component {
    render() {
        const { movie } = this.props;
        return (
            <section class="row columns">
                <h1>{movie.title}</h1>
                <p><b>Category</b>: {movie.category}</p>
                <p><b>Rating</b>: {movie.rating}</p>
                <p><b>Description</b>: {movie.description}</p>
            </section>
        );
    }
}
