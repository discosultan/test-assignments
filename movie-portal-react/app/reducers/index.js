import { combineReducers } from 'redux';

const initialState = {
  numLoading: 0
};

const movies = (state = initialState, action) => {
  switch (action.type) {
    case 'REQUEST_MOVIES':
      return Object.assign({}, state, {
        numLoading: state.numLoading + 1
      });
    case 'RECEIVE_MOVIES':
      return Object.assign({}, state, {
        numLoading: state.numLoading - 1,
        movies: action.movies
      });
    default:
      return state;
  }
};

export default combineReducers({
    movies
});
