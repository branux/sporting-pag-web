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
                    $('#pageNoticias').empty();
                    $.each(noticias, function(index, not) {
                       $('#pageNoticias').append('<li><p><div class="itemNoticia">' +
                                                        '<img src="'+getPortada(not.Imagenes)+'"/>' +
                                                        '<h3><a href="TemplateNoticia.aspx?id='+not.IdNoticia+'">' + not.Titulo + '</a></h3>' +
                                                        '<p>' + not.Descripcion + '</p>' +
                                                    '</div></p></li>');
                    });
              },
              failure: function(msg) {
                $('#pageNoticias').text(msg);
              }
        });
        paginar();
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
  
function paginar(){
    $('#pageNoticias').addClass('content'); 
    $.ajax({
        target:'#paging_container',
        success: function() {
            $('#paging_container').pajinate({
                items_per_page : 3,
                num_page_links_to_display : 3,
                nav_label_first : '&laquo;',
                nav_label_prev : '<',
                nav_label_next : '>',
                nav_label_last : '&raquo;'
            });
        }
    });  
};