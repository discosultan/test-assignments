import angular from 'angular';

const inArray = $filter => {
    return (list, arrayFilter, element) => {
        if (arrayFilter && arrayFilter.length > 0) {
            return $filter("filter")(list, listItem => arrayFilter.indexOf(listItem[element]) > -1);
        }
        return list;
    }
}

export default angular.module('filter.in-array', []).filter('inArray', inArray).name;
