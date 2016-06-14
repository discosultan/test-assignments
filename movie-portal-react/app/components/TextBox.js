import React from 'react';

export default class TextBox extends React.Component {
    render() {
        const { label, value } = this.props;
        return (
            <label><b>{label}:</b>
                <input type="text" onInput={this.handleInput.bind(this)} defaultValue={value} />
            </label>
        );
    }

    handleInput(event) {
        const { onInput } = this.props;
        onInput(event.target.value);
    }
}
