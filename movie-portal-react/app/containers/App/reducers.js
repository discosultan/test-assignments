export function numLoading(state = 0, action) {
    switch (action.type) {
        case 'REQUEST_MOVIES':
        case 'REQUEST_MOVIE_DETAILS':
        case 'REQUEST_CATEGORIES':
            return state + 1;
        case 'RECEIVE_MOVIES':
        case 'RECEIVE_MOVIE_DETAILS':
        case 'RECEIVE_CATEGORIES':
            return state - 1;
        default:
            return state;
    }
}
