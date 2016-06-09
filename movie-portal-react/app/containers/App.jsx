import React from 'react';
import { connect } from 'react-redux'

import NavigationBar from '../components/NavigationBar.jsx';

class App extends React.Component {
    render() {
        const { numLoading } = this.props;
        return (
            <div>
                <NavigationBar isLoading={numLoading} />
                <main>
                    {this.props.children}
                </main>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { numLoading } = state;
    return { isLoading: numLoading > 0 };
}

export default connect(mapStateToProps)(App);
