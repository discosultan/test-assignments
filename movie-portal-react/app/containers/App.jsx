import React from 'react';
import { connect } from 'react-redux'

import NavigationBar from '../components/NavigationBar';

class App extends React.Component {
    render() {
        const { numLoading } = this.props;
        return (
            <div>
                <NavigationBar numLoading={numLoading} />
                <main>
                    {this.props.children}
                </main>
            </div>
        );
    }
}

function mapStateToProps(state) {
  const { numLoading } = state;
  return { numLoading };
}

export default connect(mapStateToProps)(App);
