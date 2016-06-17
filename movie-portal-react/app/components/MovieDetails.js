import React from 'react';

export default class MovieDetails extends React.Component {
    render() {
        const { value, className } = this.props;
        return (value &&
            <section className={className}>
                <h1>{value.title}</h1>
                <p><b>Category</b>: {value.category}</p>
                <p><b>Rating</b>: {value.rating}</p>
                <p><b>Description</b>: {value.description}</p>
            </section>
        );
    }
}
