$(document).ready(function() { 
        highlightCurrent();
});

function highlightCurrent(){
    $('.current').removeClass('current');
    
    var current = $("#ctl00_CPHBody_currentPage").val();
    if( current == 'Inicio')
    {
        $(".btnInicio a:first").addClass('current');
    }
    else if(current == 'Equipo')
    {
        $(".btnEquipo a:first").addClass('current');
    }
    else if(current == 'Noticias')
    {
        $(".btnNoticias a:first").addClass('current');
    }
    else if(current == 'Campeonato')
    {
        $(".btnCampeonato a:first").addClass('current');
    }
    else if(current == 'Historia')
    {
        $(".btnHistoria a:first").addClass('current');
    }
    else if(current == 'Contacto')
    {
        $(".btnContacto a:first").addClass('current');
    } 
};