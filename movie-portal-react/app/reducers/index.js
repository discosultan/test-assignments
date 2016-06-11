import { combineReducers } from 'redux';

function numLoading(state = 0, action) {
  switch (action.type) {
    case 'REQUEST_MOVIES':
    case 'REQUEST_CATEGORIES':
      return state + 1;
    case 'RECEIVE_MOVIES':
    case 'RECEIVE_CATEGORIES':
      return state - 1;
    default:
      return state;
  }
}

function movies(state = [], action) {
  switch (action.type) {
    case 'RECEIVE_MOVIES':
      console.log(action);
      return action.movies;
    default:
      return state;
  }
};

export default {
    numLoading,
    movies
};
