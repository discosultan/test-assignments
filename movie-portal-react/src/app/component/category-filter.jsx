import React, { Component } from 'react';

export default class CategoryFilter extends Component {
    constructor(props) {
        super(props);
        this.selectedValues = [];
    }
            
    apply(input, selector) {        
        return this.selectedValues.length == 0
            ? input
            : input.filter(item => this.selectedValues.indexOf(selector(item)) >= 0);
    }

    render() {
        return (
            <label><b>Categories:</b>
                <select ref="listFilter" multiple onChange={handleChange.bind(this)}>
                    {this.props.categories.map(category =>
                        <option key={category.id} value={category.id}>{category.name}</option>    
                    )}
                </select>
            </label>
        );
    }
}

function handleChange(evt) {
    // We use spreading operator to convert options to an array.
    let options = [...evt.target.options];
    let selectedValues = options.filter(option => option.selected).map(option => option.value);
    this.selectedValues = selectedValues;
    this.props.onChange();
}