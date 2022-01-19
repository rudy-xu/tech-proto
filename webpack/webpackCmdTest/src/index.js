/**
 * the entry file of webpack: index.js
 * 
 * 1.running commands:
 *      - development: webpack xx/index.js -o xx/ --mode=development
 *      webpack uses index.js as entry file for packaging, output file defaults to main.js, and it will create floder automatically
 * 
 *      - production commands: webpack xx/index.js -o xx/ --mode=production
 *      Compare development, it will encapsulate code or give final result code
 * 
 * 2. summary
 *      - put all packing file into entry file
 *      - webpack will complie ES6 module to module which browser can recognize
 *      - deal js/json
 *      - can not css/img and so on, so we should use webpack.config.js to invoke loader and plugins
 */

 import './index.css';
 import './index.less';

 import data from "./data.json";

 console.log(data);

 function add(x, y)
 {
     return x+y;
 }

 console.log(add(1,2));