const initialState = {
    searchFilter: '',
    categoryFilter: [],
    movies: [],
    categories: [],
    selectedMovieDetails: null,
    // This map's only purpose is to cache already fetched details at
    // client side so we don't do multiple requests for the same movie.
    movieDetailsMap: {}
};

function moviesPage(state = initialState, action) {
    switch (action.type) {
        case 'SET_SEARCH_FILTER':
            return { ...state, searchFilter: action.filter };
        case 'SET_CATEGORY_FILTER':
            return { ...state, categoryFilter: action.filter };
        case 'RECEIVE_MOVIES':
            return { ...state, movies: action.movies };
        case 'RECEIVE_CATEGORIES':
            return { ...state, categories: action.categories };
        case 'RECEIVE_MOVIE_DETAILS':
            const details = action.movieDetails;
            return {
                ...state,
                selectedMovieDetails: details,
                movieDetailsMap: { ...state.movieDetailsMap, [details.id]: details }
            };
        default:
            return state;
    }
}

export default moviesPage;
