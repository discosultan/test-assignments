import React from 'react';

export default class MultiSelect extends React.Component {
    render() {
        const { label, items, keySelector, valueSelector, value } = this.props;
        return (
            <label><b>{label}:</b>
                <select multiple value={value} onChange={handleChange.bind(this)}>
                    {items.map(item =>
                        <option key={keySelector(item)} value={keySelector(item)}>
                            {valueSelector(item)}
                        </option>
                    )}
                </select>
            </label>
        );
    }
}

function handleChange(event) {
    const { onChange } = this.props;

    // We use spreading operator to convert options to an array.
    // An alternative would be to use Array.prototype.filter.call.
    const options = [...event.target.options];
    const selectedValues = options.filter(option => option.selected)
                                  .map(option => option.value);
    this.selectedValues = selectedValues;
    onChange(selectedValues);
}
