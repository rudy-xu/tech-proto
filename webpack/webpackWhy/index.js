import $ from 'jquery';

$('title').click(() => {
    console.log('click');
    $('body').css('backgroundColor','red');
});

// document.getElementById('title').onclick = function () {
//     console.log("click");
//     document.body.style.backgroundColor = 'red';
// };


