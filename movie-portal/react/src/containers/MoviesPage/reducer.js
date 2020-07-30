import { byArray, bySearchString } from '../../utils/filters';

const initialState = {
    searchFilter: '',
    categoryFilter: [],
    movies: [],
    categories: [],
    selectedMovieDetails: null
};

export default function moviesPage(state = initialState, action) {
    switch (action.type) {
        case 'SET_SEARCH_FILTER':
            return { ...state, searchFilter: action.filter };
        case 'SET_CATEGORY_FILTER':
            return { ...state, categoryFilter: action.filter };
        case 'RECEIVE_MOVIES':
            return { ...state, movies: action.result };
        case 'RECEIVE_CATEGORIES':
            return { ...state, categories: action.result };
        case 'RECEIVE_MOVIE_DETAILS':
            return { ...state, selectedMovieDetails: action.result };
        default:
            return state;
    }
}

export function filterMovies(state) {
    const { movies, categoryFilter, searchFilter } = state;
    return movies.filter(byArray(movie => movie.category, categoryFilter))
                 .filter(bySearchString(movie => movie.title, searchFilter));
}
