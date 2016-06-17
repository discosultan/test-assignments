import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import TextBox from '../../components/TextBox';
import MultiSelect from '../../components/MultiSelect';
import MovieList from '../../components/MovieList';
import MovieDetails from '../../components/MovieDetails';

import * as actions from './actions';
import { filterMovies } from './reducer';

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
                    <MovieList className="medium-6 columns" value={filteredMovies} onSelect={requestMovieDetails} />
                    <MovieDetails className="medium-6 columns" value={selectedMovieDetails} />
                </section>
            </section>
        );
    }
}

function mapStateToProps(state) {
    state = state.moviesPage.present || state.moviesPage;
    const { categories, selectedMovieDetails, searchFilter, categoryFilter } = state;
    return {
        categories, searchFilter, categoryFilter,
        selectedMovieDetails: selectedMovieDetails,
        filteredMovies: filterMovies(state)
    };
}

const mapDispatchToProps = dispatch => bindActionCreators(actions, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(MoviesPage);
