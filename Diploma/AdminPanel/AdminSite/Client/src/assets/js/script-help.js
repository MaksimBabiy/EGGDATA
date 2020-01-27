var sideNav = document.querySelector('.sidenav');
M.Sidenav.init(sideNav, {});

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

