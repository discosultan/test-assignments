const initialState = {
    numLoading: 0
};

export default function app(state = initialState, action) {
    if (action.type.startsWith('REQUEST'))
        return { ...state, numLoading: state.numLoading + 1 }
    if (action.type.startsWith('RECEIVE'))
        return { ...state, numLoading: state.numLoading - 1 }
    return state;
}
