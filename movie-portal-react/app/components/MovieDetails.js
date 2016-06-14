import React from 'react';

export default class MovieDetails extends React.Component {
    render() {
        const { value } = this.props;
        return (
            <section class="row columns">
                <h1>{value.title}</h1>
                <p><b>Category</b>: {value.category}</p>
                <p><b>Rating</b>: {value.rating}</p>
                <p><b>Description</b>: {value.description}</p>
            </section>
        );
    }
}
