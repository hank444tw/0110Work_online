/* 
Inspired from: https://tympanus.net/codrops/2013/08/28/transitions-for-off-canvas-navigations/

there are still some quirks for animation 3, 6, 7, 8 & 14 
as they do not animate back gracefully  
(those are the navs in the div with class st-pusher)
*/

var click = document.querySelectorAll('#btnSidebar');
var menu = document.querySelector('#st-container');
var pusher = document.querySelector('.st-pusher');
// to store the corresponding effect
var effect;

// adding a click event to all the buttons
for (var i = 0; i < click.length; i++) {
    click[i].addEventListener('click', addClass)
}


    pusher.addEventListener('click', closeMenu);



function addClass(e) {
    // to get the correct effect
    effect = e.target.getAttribute('data-effect');
    // adding the effects
    menu.classList.toggle(effect);
    menu.classList.toggle('st-menu-open');

    // console.log(e.target.getAttribute('data-effect'));
}

function closeMenu(el) {
    // if the click target has this class then we close the menu by removing all the classes
    if (el.target.classList.contains('st-pusher')) {
        menu.classList.toggle(effect);
        menu.classList.toggle('st-menu-open');
        // console.log(el.target);
    }
}

//-------------Sidebar下拉式選單------------------
$(".st-menu-dropdown").click(function () {
    $(this)
        .siblings("div")
        .slideToggle();
});
//-------------Sidebar下拉式選單 End---------------
