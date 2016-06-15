import RestApi from '../../utils/RestApi';

const moviePortalApi = new RestApi('http://40.113.15.185:3000/'); // TODO: DI?
// const getMoviesPageState = getState => getState().moviesPage;
const getMoviesPageState = getState => getState().moviesPage.present;

export const requestMovies = () => (dispatch, getState) => {
    dispatch({ type: 'REQUEST_MOVIES' });
    const existingMovies = getMoviesPageState(getState).movies;
    if (existingMovies.length) {
        dispatch(receiveMovies(existingMovies));
    } else {
        moviePortalApi.get('movies')
                      .then(movies => {
                          dispatch(receiveMovies(movies));
                          if (movies.length) dispatch(requestMovieDetails(movies[0].id));
                      });
    }
}

export const receiveMovies = movies => ({
    type: 'RECEIVE_MOVIES',
    movies
});

export const requestMovieDetails = id => (dispatch, getState) => {
    dispatch({ type: 'REQUEST_MOVIE_DETAILS', id: id });
    const existingMovieDetails = getMoviesPageState(getState).movieDetailsMap[id];
    if (existingMovieDetails) {
        dispatch(receiveMovieDetails(existingMovieDetails));
    } else {
        moviePortalApi.get(`movies/${id}`)
                      .then(movieDetails => dispatch(receiveMovieDetails(movieDetails)));
    }
}

export const receiveMovieDetails = movieDetails => ({
    type: 'RECEIVE_MOVIE_DETAILS',
    movieDetails
});


export const requestCategories = () => (dispatch, getState) => {
    dispatch({ type: 'REQUEST_CATEGORIES' });
    const existingCategories = getMoviesPageState(getState).categories;
    if (existingCategories.length) {
        dispatch(receiveCategories(existingCategories));
    } else {
        moviePortalApi.get('categories')
                      .then(categories => dispatch(receiveCategories(categories)));
    }
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
