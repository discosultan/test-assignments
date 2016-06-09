import React, { Component } from 'react';
import { Link } from 'react-router';

export default class NavigationBar extends Component {
    render() {
        return (
            <nav className="top-bar">
                <div className="top-bar-left">
                    <ul className="dropdown menu" data-dropdown-menu>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/movies">Movies</Link></li>
                        {/*this.props.isLoading &&
                            <li><img src="img/loading.gif" width="38" height="38" /></li>
                        */}
                    </ul>
                </div>
            </nav>
        );
    }
}
