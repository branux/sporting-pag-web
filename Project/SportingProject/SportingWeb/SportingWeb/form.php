<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<?php 
	$message = $HTTP_POST_VARS["mensaje"];
	$cuerpo = "Mensaje Web\n";
    $cuerpo .= "Nombre: " . $HTTP_POST_VARS["nom"] . "\n";
    $cuerpo .= "Email: " . $HTTP_POST_VARS["mail"] . "\n";
    $cuerpo .= "Mensaje: " . $HTTP_POST_VARS["mensaje"] . "\n"; 
	
	mail("nicofetter@gmail.com", "Mensaje WEB", $cuerpo,"From: Contacto web <web@sporting.com.ar>");
	echo "respuesta= Mensaje enviado";
?>
</body>
</html>
