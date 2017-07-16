import * as React from "react";
import { Wind } from "../models";

export default class WindWidget extends React.Component<{ value: Wind }> {
    render() {
        const value = this.props.value;
        const style = {
            width: "16px",
            height: "16px",
            display: "inline-block",
            background: "url('./arrow.svg') center / cover",
            transform: `rotate(${value.deg}deg)`
        };
        return (
            <span>
                {value.speed} m/s
                &nbsp;
                <span style={style}></span>
            </span>
        );
    }
}