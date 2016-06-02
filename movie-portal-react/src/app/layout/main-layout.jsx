import React, { Component } from 'react';

import { ApiLoadingChangedEvent } from '../service/api.js';
import NavigationBar from '../component/navigation-bar.jsx';

export default class MainLayout extends Component {
    constructor(props, context) {
        super(props);
        this.eventAggregator = context.eventAggregator;
        this.state = {
            isLoading: false
        }
        
        this.onApiLoadingChanged = eventArgs => this.setState({
            isLoading: eventArgs.numLoading > 0 
        });
    }
    
    componentDidMount() {
        this.eventAggregator.subscribe(ApiLoadingChangedEvent, this.onApiLoadingChanged);
    }
    
    componentWillUnmount() {
        this.eventAggregator.unsubscribe(ApiLoadingChangedEvent, this.onApiLoadingChanged);
    }
    
    render() {
        return (            
            <div>
                <NavigationBar isLoading={this.state.isLoading} />
                <main>
                    {this.props.children}
                </main>                    
            </div>            
        );
    }
}

MainLayout.contextTypes = {
    eventAggregator: React.PropTypes.object.isRequired
}