const scrollTop = () => {
    let navbar = document.querySelector('nav');
    let navLink = document.querySelectorAll('.nav-wrapper a');

    const fun1 = () => {
      if (document.body.scrollTop > 5 || document.documentElement.scrollTop > 5) {
        navbar.classList.remove("transparent");
        navbar.classList.add("nav-color");
        for(let i = 0;i<navLink.length;i++) {
            navLink[i].style.color = "black";
        } 
      } else {
        navbar.classList.remove("nav-color");
        navbar.classList.add("transparent");
        for(let i = 0;i < navLink.length;i++) {
            navLink[i].style.color = "white";
        } 
      }
    }
    navbar && fun1()
}

window.onscroll = function() { scrollTop()};