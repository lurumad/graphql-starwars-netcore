var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var output = __dirname + '/wwwroot';

module.exports = {
    entry: {
        'bundle': './Views/GraphQL/app.js'
    },

    output: {
        path: output,
        filename: '[name].js'
    },

    resolve: {
        extensions: ['.js', '.json']
    },

    module: {
        rules: [
            {
                test: /\.js/,
                loader: 'babel-loader',
                exclude: /node_modules/,
                query: {
                    presets: ['react']
                }
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: "css-loader"
                })
            }
        ]
    },

    plugins: [
      new ExtractTextPlugin('style.css', { allChunks: true })
    ]
};