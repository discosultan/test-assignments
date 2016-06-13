import React from 'react';
import { bindActionCreators } from 'redux';
import { ActionCreators } from 'redux-undo';
import { connect } from 'react-redux';

class UndoRedo extends React.Component {
    render() {
        const { canUndo, canRedo, undo, redo } = this.props;
        return (
            <section>
                <button type="button" className="button no-margin" onClick={undo} disabled={!canUndo}>Undo</button>
                <button type="button" className="button no-margin" onClick={redo} disabled={!canRedo}>Redo</button>
            </section>
        );
    }
}

const mapStateToProps = (state) => ({
    canUndo: state.selectedMovieId.past.length > 0,
    canRedo: state.selectedMovieId.future.length > 0
});

const mapDispatchToProps = (dispatch) => bindActionCreators(ActionCreators, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(UndoRedo);
