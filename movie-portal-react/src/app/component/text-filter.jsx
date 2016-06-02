import React, { Component } from 'react';

export default class TextFilter extends Component {        
    apply(input, selector) {
        let valueUpper = this.refs.textFilter.value.toUpperCase();
        return input.filter(item => selector(item).toUpperCase().indexOf(valueUpper) >= 0);
    }

    render() {
        return (
            <label><b>Search:</b>
                <input ref="textFilter" type="text" onInput={this.props.onChange} />
            </label>
        );
    }
}