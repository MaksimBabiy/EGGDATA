$('#hum-menu').click(function() {

  $('.admin-nav').animate({width: 0});
  $('.admin-content').animate({width: 100+'%'});

  if($('.admin-nav').width() === 0) {
    $('.admin-nav').animate({width: 25+'%'});
    $('.admin-content').animate({width: 75+'%'});
  }
});