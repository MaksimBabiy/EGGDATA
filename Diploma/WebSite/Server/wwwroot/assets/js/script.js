var sideNav = document.querySelector('.sidenav'); 
M.Sidenav.init(sideNav, {});


var slider = document.querySelector('.slider'); 
M.Slider.init(slider, {
    full_width:true,
    indicators:true,
    height:750,
    transition: 500,
    interval:6000,
    height:660
});  
var instance = M.Slider.getInstance(slider);



var arrowRight = document.querySelector('.nxt_b');
var arrowLeft = document.querySelector('.prv_b');

arrowLeft.addEventListener("click", 
function() {
    instance.prev();
});

arrowRight.addEventListener("click",
function() {
    instance.next();
});


var navbar = document.querySelector('nav');
var navLink = document.querySelectorAll('.nav-wrapper a');

console.log()

window.onscroll = function() { scrollingNav()};

function scrollingNav () {
    if (document.body.scrollTop > 5 || document.documentElement.scrollTop > 5) {
        navbar.classList.remove("transparent");
        navbar.classList.add("nav-color");
        for(i=0;i<navLink.length;i++) {
            navLink[i].style.color = "black";
        } 
      } else {
        navbar.classList.remove("nav-color");
        navbar.classList.add("transparent");
        for(i=0;i<navLink.length;i++) {
            navLink[i].style.color = "white";
        } 
      }
}

