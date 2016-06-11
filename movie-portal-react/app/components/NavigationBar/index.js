import React from 'react';
import { Link } from 'react-router';

import loading from './loading.gif';

export default class NavigationBar extends React.Component {
    render() {
        return (
            <nav className="top-bar">
                <div className="top-bar-left">
                    <ul className="dropdown menu" data-dropdown-menu>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/movies">Movies</Link></li>
                        {this.props.numLoading > 0 &&
                            <li>
                              <img width="38" height="38" src={loading} />
                            </li>
                        }
                    </ul>
                </div>
            </nav>
        );
    }
}
