export const setSearchFilter = filter => ({ type: 'SET_SEARCH_FILTER', filter });
export const setCategoryFilter = filter => ({ type: 'SET_CATEGORY_FILTER', filter });

export const requestCategories = () => (api, dispatch) => {
    dispatch({ type: 'REQUEST_CATEGORIES' });
    api.get('categories').then(result => dispatch({ type: 'RECEIVE_CATEGORIES', result }));
};

export const requestMovieDetails = id => (api, dispatch) => {
    dispatch({ type: 'REQUEST_MOVIE_DETAILS', id });
    api.get(`movies/${id}`).then(result => dispatch({ type: 'RECEIVE_MOVIE_DETAILS', result }));
};

export const requestMovies = () => (api, dispatch, getState) => {
    dispatch({ type: 'REQUEST_MOVIES' });
    api.get('movies').then(result => {
        dispatch({ type: 'RECEIVE_MOVIES', result });

        // If we received any movies and no movie is selected, select the first one!
        let state = getState();
        state = state.moviesPage.present || state.moviesPage;
        if (result.length && !state.selectedMovieDetails)
            dispatch(requestMovieDetails(result[0].id));
    });
};
