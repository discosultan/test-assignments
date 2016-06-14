import React from 'react';

export default class TextBox extends React.Component {
    render() {
        const { label, value } = this.props;
        return (
            <label><b>{label}:</b>
                <input type="text" onChange={this.handleInput.bind(this)} value={value} />
            </label>
        );
    }

    handleInput(event) {
        const { onChange } = this.props;
        onChange(event.target.value);
    }
}
