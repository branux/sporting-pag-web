$(window).load(function(){ 

    // Cargar plantel
    var plantel=$("#ctl00_CPHBody_jugadoresPlantel").val();
    var arrayPlantel=plantel.split(";");
                
	for (var i=0; i<arrayPlantel.length - 1; i++) {
        var arrayJugador=arrayPlantel[i].split(",");
        $("#plantelActual").append( '<div class="jugador"><p class="caption"><img  width="150px" height="250px" src ='+
        arrayJugador[2]+'/><span>'+arrayJugador[0]+'<br/>'+arrayJugador[1]+'</span></p></jugador>' ) ;
    }    
	// For each instance of p.caption
	$("p.caption").each(function(){
		$(this)
			// Add the following CSS properties and values
			.css({
				 // Height equal to the height of the image
				"height" : $(this).children("img").height() + "px",
				// Width equal to the width of the image
				"width" : $(this).children("img").width() + "px"
			})
			// Select the child "span" of this p.caption
			// Add the following CSS properties and values
			.children("span").css(

				// Width equal to p.caption
				// But subtract 20px to callibrate for the padding
				"width", $(this).width() - 20 + "px")

			// find the <big> tag if it exists
			// And then add the following div to break the line
			.find("big").after('<div class="clear"></div>');
		
			// When you hover over p.caption
			$("p.caption").hover(function(){

				// Fade in the child "span"
				$(this).children("span").stop().fadeTo(400, 1);
				}, function(){
				// Once you mouse off, fade it out
				$(this).children("span").stop().delay(600).fadeOut(400);
			});
	// End $(this)	 
	});

});