$(document).ready(function() {
        $("#ctl00_CPHBody_gridTablaPosiciones").addClass("tablaPosiciones");
        $("#tituloCampeonato").append('<span></span> Campeonato: '+$("#ctl00_CPHBody_nombreCampeonato").val());
        cargarCampeonato();	
});

function cargarCampeonato() {
        $.ajax({
              type: "POST",
              url: "WebServiceNoticias.asmx/GetFixtureCampeonato",
              data: "{}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function(response) {
                    var fechas = response.d;
                    $.each(fechas, function(index, fecha) {
                        $('#fixture').append('<div class="fechaCampeonato">' +
                                                        '<h3> Fecha ' + fecha.Numero + '</h3>' 
                                                        +
                                                            getResultadosDiv(fecha).toString()
                                                        +
                                                    '</div>');
                    }); 
              },
              failure: function(msg) {
                alert(msg);
                $('#fixture').text(msg);
              }
        });
  };
  
  function getResultadosDiv(fecha)
  {
        var output ='';
        var resultados = fecha.Resultados;
        var count = fecha.Resultados.length;
        for (var i=0; i<fecha.Resultados.length; i++)
        { 
            var resultado = fecha.Resultados[i];
            output += '<div class="fixtEquipo">'+resultado.EquipoLocal.Nombre + ' <a class="fixtPtsLocal">'+
            resultado.EquipoLocalPuntos +'</a> </div> '+
            ' <div class="fixtCenter">|</div><div class="fixtEquipo"><a class="fixtPtsVisitante">'+resultado.EquipoVisitantePuntos+
            '</a> '+resultado.EquipoVisitante.Nombre+'</div>';
        }
        return output;
  };