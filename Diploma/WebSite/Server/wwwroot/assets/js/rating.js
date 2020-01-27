var rating = document.querySelector('.rating')
var ratingItem = document.querySelectorAll('.rating-item');

rating.addEventListener('click', SetRating); 
function SetRating(e){
  let target = e.target;
  if(target.classList.contains('rating-item')){
    removeClass(ratingItem,'current-active')
    target.classList.add('active','current-active');
  }
}
ratingItem.forEach(item =>{
    item.addEventListener('click', setLocal)
})
function setLocal(){
  let value = this.getAttribute('data-rating');
  localStorage.setItem('Maksim Ivanov rating', value);
}
rating.addEventListener('mouseover', OnMouseOverRating)
 function OnMouseOverRating(e) {
  let target = e.target;
  if(target.classList.contains('rating-item')){
    removeClass(ratingItem,'active')
    target.classList.add('active');
    mouseOverActiveClass(ratingItem)
  }
}
rating.addEventListener('mouseout', OnMouseOutRating)
function OnMouseOutRating(){
  addClass(ratingItem,'active');
  mouseOutActiveClas(ratingItem);
}

function removeClass(arr) {
  for(let i = 0, iLen = arr.length; i <iLen; i ++) {
    for(let j = 1; j < arguments.length; j ++) {
      ratingItem[i].classList.remove(arguments[j]);
    }
  }
}
function addClass(arr) {
  for(let i = 0, iLen = arr.length; i <iLen; i ++) {
    for(let j = 1; j < arguments.length; j ++) {
      ratingItem[i].classList.add(arguments[j]);
    }
  }
}

function mouseOverActiveClass(arr){
  for(let i = 0, iLen = arr.length; i < iLen; i++) {
    if(arr[i].classList.contains('active')){
      break;
    }else {
      arr[i].classList.add('active');
    }
  }
}

function mouseOutActiveClas(arr){
  for(let i = arr.length-1; i >=1; i--) {
    if(arr[i].classList.contains('current-active')){
      break;
    }else {
      arr[i].classList.remove('active');
    }
  }
}
