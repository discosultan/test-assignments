import React from 'react';

export default class MovieDetails extends React.Component {
    render() {
        const { movieDetails } = this.props;
        return (
            <section class="row columns">
                <h1>{movieDetails.title}</h1>
                <p><b>Category</b>: {movieDetails.category}</p>
                <p><b>Rating</b>: {movieDetails.rating}</p>
                <p><b>Description</b>: {movieDetails.description}</p>
            </section>
        );
    }
}
