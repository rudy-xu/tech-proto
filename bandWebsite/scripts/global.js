/**
 * Create one function queue. Execute this queue when finishing page loaded
 * param: 
 *      function name
 */
function addLoadEvent(func)
{
    var oldOnload = window.onload;
    if(typeof window.onload != 'function')
    {
        window.onload = func;
    }
    else
    {
        window.onload = function() {
            oldOnload();
            func();
        }
    }
}

/**
 * DOM just provide 'insertBefore(newElement, targetElement)' function
 * Create the insertAfter function which insert a node after another node
 * param: 
 *      new element node, 
 *      target element node
 */
function insertAfter(newElement, targetElement)
{
    var parent = targetElement.parentNode;
    if(parent.lastChild == targetElement)
    {
        parent.appendChild(newElement);
    }
    else
    {
        parent.insertBefore(newElement, targetElement.nextSibling);
    }
}

/**
 * Function: Add new class for element
 * @param {*} element 
 * @param {*} value --- new className
 */
function addClass(element, value) 
{
    if(!element.className)
    {
        element.className = value
    }
    else
    {
        newClassName = element.className;
        newClassName += ' ';
        newClassName += value;
        element.className = newClassName;
    }
}


////////////////////////////////////////////////////
// getElementsByTagName -- return Object Array
function hightlightPage() 
{
    if(!document.getElementsByTagName) return false;
    if(!document.getElementById) return false;

    var headers = document.getElementsByTagName('header');
    console.log(headers.length)
    if(headers.length == 0) return false;
    var navs = headers[0].getElementsByTagName('nav');
    console.log(navs)
}

addLoadEvent(hightlightPage);