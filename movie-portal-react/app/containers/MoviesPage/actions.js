import RestApi from '../../utils/RestApi';
const moviePortalApi = new RestApi('http://40.113.15.185:3000/'); // TODO: DI?

export const requestMovies = () => dispatch => {
    dispatch({ type: 'REQUEST_MOVIES' });
    moviePortalApi.get('movies')
                  .then(movies => {
                      dispatch(receiveMovies(movies));
                      if (movies.length) dispatch(requestMovieDetails(movies[0].id));
                  });
}

export const receiveMovies = movies => ({
    type: 'RECEIVE_MOVIES',
    movies
});

export const selectMovie = id => ({
    type: 'SELECT_MOVIE',
    id
});

export const requestMovieDetails = id => dispatch => {
    dispatch({ type: 'REQUEST_MOVIE_DETAILS', id: id });
    moviePortalApi.get(`movies/${id}`)
                  .then(movieDetails => dispatch(receiveMovieDetails(movieDetails)));
}

export const receiveMovieDetails = movieDetails => ({
    type: 'RECEIVE_MOVIE_DETAILS',
    movieDetails
});


export const requestCategories = () => dispatch => {
    dispatch({ type: 'REQUEST_CATEGORIES' });
    moviePortalApi.get('categories')
                  .then(categories => dispatch(receiveCategories(categories)));
}

export const receiveCategories = categories => ({
    type: 'RECEIVE_CATEGORIES',
    categories
});

export const setSearchFilter = filter => ({
    type: 'SET_SEARCH_FILTER',
    filter
});

export const setCategoryFilter = filter => ({
    type: 'SET_CATEGORY_FILTER',
    filter
});
