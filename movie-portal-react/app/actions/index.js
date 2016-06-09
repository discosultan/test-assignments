export function requestMovies() {
  return (dispatch, getState) => {
      dispatch({ type: 'REQUEST_MOVIES', });
      let state = getState();
      state.api.get('movies')
               .then(movies => dispatch(receiveMovies(movies)));
  }
};

export const receiveMovies = (movies) => ({
  type: 'RECEIVE_MOVIES',
  movies
});

export const setSearchFilter = (filter) => ({
  type: 'SET_SEARCH_FITLER',
  filter
});

export const setCategoryFilter = (filter) => ({
  type: 'SET_CATEGORY_FILTER',
  filter
});
