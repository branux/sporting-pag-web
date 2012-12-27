$(document).ready(function() {
        getNoticias();
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
                        $('#grillaNoticias').append('<div class="itemNoticia">' +
                                                        '<img src="'+getPortada(not.Imagenes)+'"/>' +
                                                        '<h3>' + not.Titulo + '</h3>' +
                                                        '<p>' + not.Descripcion + '</p>' +
                                                    '</div>');
                    });
              },
              failure: function(msg) {
                $('#output').text(msg);
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

