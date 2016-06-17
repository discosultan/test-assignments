export const setSearchFilter = filter => ({ type: 'SET_SEARCH_FILTER', filter });
export const setCategoryFilter = filter => ({ type: 'SET_CATEGORY_FILTER', filter });

// The actions below are preprocessed by our restApiMiddleware that handles the
// async nature of these requests. Resource uri defaults to type suffix and method to GET.
export const requestMovies = () => ({
    type: 'REQUEST_MOVIES',
    // onReceived: (result, dispatch) => {
    //     if (result.length) dispatch(requestMovieDetails(result[0].id));
    // }
}); // uri defaults to 'movies'
export const requestCategories = () => ({ type: 'REQUEST_CATEGORIES' }); // uri defaults to 'categories'
export const requestMovieDetails = id => ({ type: 'REQUEST_MOVIE_DETAILS', uri: `movies/${id}` });
