import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

import NavigationBar from '../../components/NavigationBar';

class App extends React.Component {
    render() {
        const { numLoading } = this.props;
        return (
            <div>
                <NavigationBar numLoading={numLoading} />
                <br />
                <main>
                    {this.props.children}
                </main>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { numLoading } = state.app;
    return { numLoading };
}

export default withRouter(connect(mapStateToProps)(App));
