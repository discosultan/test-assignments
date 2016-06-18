export default api => store => next => action =>
    typeof action === 'function' ?
        action(api, store.dispatch, store.getState) :
        next(action);
