import React from 'react';
import { bindActionCreators } from 'redux';
import { ActionCreators } from 'redux-undo';
import { connect } from 'react-redux';

class UndoRedo extends React.Component {
    constructor(props) {
        super(props);
        this.handleArrowKeys = event => {
            const { canUndo, canRedo, undo, redo } = this.props;
            if (event.keyCode === 37 && canUndo) undo();
            else if (event.keyCode === 39 && canRedo) redo();
        };
    }

    componentDidMount() {
        window.addEventListener('keydown', this.handleArrowKeys, false);
    }

    componentWillUnmount() {
        window.removeEventListener('keydown', this.handleArrowKeys, false);
    }

    render() {
        const { canUndo, canRedo, undo, redo } = this.props;
        const btnStyle = { margin: 0 };
        return (
            <section>
                <button type="button" className="button" style={btnStyle} onClick={undo} disabled={!canUndo}>
                    Undo
                </button>
                <button type="button" className="button" style={btnStyle} onClick={redo} disabled={!canRedo}>
                    Redo
                </button>
            </section>
        );
    }
}

const mapStateToProps = state => {
    const moviesPage = state.moviesPage;
    return {
        canUndo: moviesPage.past.length > 0,
        canRedo: moviesPage.future.length > 0
    };
};

const mapDispatchToProps = dispatch => bindActionCreators(ActionCreators, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(UndoRedo);
