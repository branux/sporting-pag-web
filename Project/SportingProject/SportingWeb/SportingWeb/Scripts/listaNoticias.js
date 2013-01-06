$(document).ready(function() {
        getNoticias();	
        showScroll();
});

function getNoticias() {
        $.ajax({
              type: "POST",
              url: "WebServiceNoticias.asmx/GetAllNoticias",
              data: "{}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function(response) {
                    var noticias = response.d;
                    $('#grillaNoticias').empty();
                    $.each(noticias, function(index, not) {
                        $('#grillaNoticias').append('<p><div class="itemNoticia">' +
                                                        '<img src="'+getPortada(not.Imagenes)+'"/>' +
                                                        '<h3>' + not.Titulo + '</h3>' +
                                                        '<p>' + not.Descripcion + '</p>' +
                                                    '</div></p>');
                    });
              },
              failure: function(msg) {
                $('#grillaNoticias').text(msg);
              }
        });
  };
  
  function getPortada(imagenes) {
        var portada = null;
        if (imagenes != null)
        {
            for (var i=0;i<imagenes.length;i++)
            { 
                if (imagenes[i].Portada)
                {
                    portada = imagenes[i];
                }
            }
        }
        return portada.PathSmall;
  };
  
  function showScroll() {
        //$('#scrollbar1').tinyscrollbar();	
  };

