import React from 'react';
import { Link } from 'react-router-dom';

import loading from './loading.gif';

import UndoRedo from '../../containers/UndoRedo';

export default class NavigationBar extends React.Component {
    render() {
        const { numLoading } = this.props;
        return (
            <nav className="top-bar">
                <div className="top-bar-left">
                    <ul className="dropdown menu" data-dropdown-menu>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/movies">Movies</Link></li>
                        {numLoading > 0 &&
                            <li><img width="38" height="38" src={loading} /></li>
                        }
                    </ul>
                </div>
                <div className="top-bar-right">
                    <UndoRedo />
                </div>
            </nav>
        );
    }
}
