$(document).ready(function() { 
        highlightCurrent();
});

function highlightCurrent(){
    $('.current').removeClass('current');
    
    var current = $("#ctl00_CPHBody_consola_currentPage").val();
    if(current == 'Equipo')
    {
        $(".btnEquipo a:first").addClass('current');
    }
    else if(current == 'Fixture')
    {
        $(".btnFixture a:first").addClass('current');
    }
    else if(current == 'Noticias')
    {
        $(".btnNoticias a:first").addClass('current');
    }
    else if(current == 'Campeonato')
    {
        $(".btnCampeonato a:first").addClass('current');
    }
};