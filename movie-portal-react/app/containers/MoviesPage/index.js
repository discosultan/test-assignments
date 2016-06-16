import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import TextBox from '../../components/TextBox';
import MultiSelect from '../../components/MultiSelect';
import MovieList from '../../components/MovieList';
import MovieDetails from '../../components/MovieDetails';

import * as actions from './actions';
import { byArray, bySearchString } from '../../utils/filters';

class MoviesPage extends React.Component {
    componentDidMount() {
        const { requestMovies, requestCategories } = this.props;
        requestMovies();
        requestCategories();
    }

    render() {
        const {
            filteredMovies, categories, selectedMovieDetails, requestMovieDetails,
            searchFilter, categoryFilter, setSearchFilter, setCategoryFilter
        } = this.props;
        return (
            <section>
                <section className="row columns">
                    <TextBox label="Search" value={searchFilter} onChange={setSearchFilter} />
                    <MultiSelect label="Categories" items={categories} value={categoryFilter} onChange={setCategoryFilter}
                                 keySelector={item => item.id} valueSelector={item => item.name} />
                </section>

                <section className="row">
                  <section className="medium-6 columns">
                      <MovieList value={filteredMovies} onSelect={requestMovieDetails} />
                  </section>

                  <section className="medium-6 columns">
                      {!!selectedMovieDetails &&
                          <MovieDetails value={selectedMovieDetails} />
                      }
                  </section>
                </section>
            </section>
        );
    }
}

function mapStateToProps(state) {
    state = state.moviesPage.present || state.moviesPage;
    const { movies, categories, selectedMovieDetails, searchFilter, categoryFilter } = state;
    return {
        movies, categories, searchFilter, categoryFilter, selectedMovieDetails,
        filteredMovies: movies.filter(byArray(movie => movie.category, categoryFilter))
                              .filter(bySearchString(movie => movie.title, searchFilter))
    };
}

const mapDispatchToProps = dispatch => bindActionCreators(actions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(MoviesPage);
