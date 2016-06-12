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

export function movies(state = [], action) {
  switch (action.type) {
    case 'RECEIVE_MOVIES':
      return action.movies;
    default:
      return state;
  }
};

export function categories(state = [], action) {
  switch (action.type) {
    case 'RECEIVE_CATEGORIES':
      return action.categories;
    default:
      return state;
  }
};

export function selectedMovieId(state = null, action) {
  switch (action.type) {
    case 'SELECT_MOVIE':
      return action.id;
    default:
      return state;
  }
}

export function movieDetailsMap(state = {}, action) {
  switch (action.type) {
    case 'RECEIVE_MOVIE_DETAILS':
      return Object.assign({}, state, { [action.movieDetails.id]: action.movieDetails });
    default:
      return state;
  }
}
