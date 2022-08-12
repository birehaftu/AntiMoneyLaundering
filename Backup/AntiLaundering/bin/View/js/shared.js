$(document).ready(function() {
  
  var missing_translations = $('span.translation_missing').size();
  if(missing_translations > 0) {
    $('#footer').before('<div style="color:#fff;text-align:center;padding:5px 0;background:#e03e26;font-weight:bold;font-size:120%;">There are ' + missing_translations + ' translation(s) missing on this page!</div>');
  };
  
  // Focus fields
  $('input, textarea').live('focus', function() { $(this).addClass('focused'); });
  $('input, textarea').live('blur', function() { $(this).removeClass('focused'); });
  $('input.focus, textarea.focus, select.focus').focus();  

  $('.sessions form dd.remember span.info a').click(function(){
    $('.sessions form dd.remember div.cookie_information').toggle();
    return false;
  });
  
});
