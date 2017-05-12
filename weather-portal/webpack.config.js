var webpack = require('webpack');

module.exports = {
    context: __dirname + '/app',

    entry: {
        app: './app.js'
    },

    output: {
        path: __dirname + '/dist',
        filename: 'app.bundle.js'
    },

    module: {
        rules: [
            { test: /\.html$/, use: [{ loader: 'html-loader' }]}
        ]
    }
};