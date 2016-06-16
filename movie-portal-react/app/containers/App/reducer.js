const initialState = {
    numLoading: 0
};

function app(state = initialState, action) {
    switch (action.type) {
        case 'REQUEST_MOVIES':
        case 'REQUEST_MOVIE_DETAILS':
        case 'REQUEST_CATEGORIES':
            return { ...state, numLoading: state.numLoading + 1 }
        case 'RECEIVE_MOVIES':
        case 'RECEIVE_MOVIE_DETAILS':
        case 'RECEIVE_CATEGORIES':
            return { ...state, numLoading: state.numLoading - 1 }
        default:
            return state;
    }
}

export default app;
