import RestApi from '../utils/RestApi';
const moviePortalApi = new RestApi('http://40.113.15.185:3000/'); // TODO: DI

export function requestMovies() {
  return dispatch => {
      dispatch({ type: 'REQUEST_MOVIES' });
      moviePortalApi.get('movies')
                    .then(movies => dispatch(receiveMovies(movies)));
  }
}

export const receiveMovies = movies => ({
  type: 'RECEIVE_MOVIES',
  movies
});

export function requestCategories() {
  return dispatch => {
    dispatch({ type: 'REQUEST_CATEGORIES' });
    moviePortalApi.get('categories')
                  .then(categories => dispatch(receiveCategories(categories)));
  }
}

export const receiveCategories = categories => ({
  type: 'RECEIVE_CATEGORIES',
  categories
});

export const setSearchFilter = filter => ({
  type: 'SET_SEARCH_FITLER',
  filter
});

export const setCategoryFilter = filter => ({
  type: 'SET_CATEGORY_FILTER',
  filter
});
