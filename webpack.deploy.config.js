var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var config = require('./webpack.config.js');
config.output.path = './build/content';
config.output.filename = 'bundle.[hash].js';
config.plugins = [
    new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }),
    new ExtractTextPlugin('[name].[hash].css')
  ]
module.exports = config