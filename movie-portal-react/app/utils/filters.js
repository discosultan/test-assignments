export function byArray(selector, filter) {
    return item => !filter.length || filter.indexOf(selector(item)) >= 0;
}

export function bySearchString(selector, filter) {
    // We use uppercase for case insensitive search.
    filter = filter.toUpperCase();
    return item => selector(item).toUpperCase().indexOf(filter) >= 0;
}
