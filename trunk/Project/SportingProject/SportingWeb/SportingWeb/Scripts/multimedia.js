$(window).load(function(){ 

    // Cargar multimedia
    var multimediaList=$("#ctl00_CPHBody_multimedia").val();
    var arrayMultimedia=multimediaList.split(";");
                
	for (var i=0; i<arrayMultimedia.length - 1; i++) {
        var arrayM=arrayMultimedia[i].split(",");
        $("#grillaMultimedia").append( '<div class="multimedia"><h1>'+arrayM[0]+'</h1>'+arrayM[1]+'</div>' ) ;
    } 
});