import asyncTest from './util/tape-async'
import { MoviePreview, Movie, MoviePortalApi } from './movie-portal-api'

const api = new MoviePortalApi('http://40.113.15.185:3000');

asyncTest('listMoviePreviews', async t => {    
    const moviePreviews = await api.listMoviePreviews();

    t.true(moviePreviews.length > 0);    
    t.deepEqual({
        id: 1,
        title: 'Harry Potter',
        category: 'fantasy',
        rating: 5,
        year: '2014'
    }, moviePreviews[0])    
});

asyncTest('listMovie', async t => {    
    const movie = await api.getMovie(1);

    t.deepEqual({
        id: 1,
        title: 'Harry Potter',
        category: 'fantasy',
        rating: 5,
        year: '2014',
        description: 'This is very cool movie about young wizard.'
    }, movie)    
});

asyncTest('listCategories', async t => {
    const categories = await api.listCategories();

    t.true(categories.length > 0)
    t.deepEqual({
        id: 'fantasy',
        name: 'Fantasy'
    }, categories[0])
});