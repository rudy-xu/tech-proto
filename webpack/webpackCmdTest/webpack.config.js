/**
 * All build tools are based on nodeJs, so that, default to use common.js
 */

 //resolve is uesd to splice path
 const { resolve } = require('path');
 const HtmlWebpackPlugin = require('html-webpack-plugin');    //this plugin defaults to create empty html and automatically invoke all resource

 module.exports = { 
     //entry point
     entry: './src/index.js',

     //output
     output: {
         filename: 'built.js',
         path: resolve(__dirname, 'build/config') 
     },

     //loader:  1.download 2.use
     module: {
         rules: [
             //different file needs different loader
             {
                 //match files
                 test: /\.css$/,

                 //use loader to deal 
                 use: [
                     //using array executing order: down -> up
                     //add style into <head>
                     'style-loader',
                     //complie css to commonjs module
                     'css-loader'
                 ]
             },
             {
                 test: /\.less$/,
                 use: [
                     'style-loader',
                     'css-loader',   //css -> commonJs
                     'less-loader'  //less -> css
                 ]
             }
         ]
     },

     //plugins: 1.download 2.invoke 3.use
     plugins: [
         //config
         new HtmlWebpackPlugin({
             //copy html and invoke all resource(script -> packing file)
             template: './src/index.html'
         })
     ],

     //mode
     mode: 'development'
 };