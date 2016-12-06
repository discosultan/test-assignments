import React from 'react';

export default class HomePage extends React.Component {
    render() {
        return (
            // React uses 'className' instead of 'class' mainly to be compatible
            // with older browsers. Accessing `this.props.class` would error on 
            // older versions of IE because 'class' keyword has a special meaning.
            // https://www.quora.com/Why-do-I-have-to-use-className-instead-of-class-in-ReactJs-components-done-in-JSX
            <section className="row columns">
                <div className="callout primary text-center">
                    <h1>Welcome to React Movie Portal</h1>
                </div>
            </section>
        );
    }
}
