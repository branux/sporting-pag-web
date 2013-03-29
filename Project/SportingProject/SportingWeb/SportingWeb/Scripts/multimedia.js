$(document).ready(function(){
      getAuspiciantes();
      
});

function getAuspiciantes() {
        $.ajax({
              type: "POST",
              url: "WebServiceNoticias.asmx/GetAllAuspiciantes",
              data: "{}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function(response) {
                    var auspiciantes = response.d;
                    $('#auspiciantes').empty();
                    $.each(auspiciantes, function(index, a) {
                       $('#auspiciantes').append('<li><img src="'+a.ImagenAuspiciante.PathMedium+'"/></li>');
                    });
                    
                    $(".rslides").responsiveSlides({
                        pause: true 
                    });
              },
              failure: function(msg) {
                $('#auspiciantes').text(msg);
              }
        });
  };