--
-- Definition of table `auspiciante`
--

DROP TABLE IF EXISTS `auspiciante`;
CREATE TABLE `auspiciante` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `imagen` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `auspiciante`
--

/*!40000 ALTER TABLE `auspiciante` DISABLE KEYS */;
INSERT INTO `auspiciante` (`id`,`imagen`) VALUES 
 (1,22),
 (2,23),
 (3,24);
/*!40000 ALTER TABLE `auspiciante` ENABLE KEYS */;


--
-- Definition of table `campeonato`
--

DROP TABLE IF EXISTS `campeonato`;
CREATE TABLE `campeonato` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `nombre` varchar(150) NOT NULL,
  `anio` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`id`,`anio`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `campeonato`
--

/*!40000 ALTER TABLE `campeonato` DISABLE KEYS */;
INSERT INTO `campeonato` (`id`,`nombre`,`anio`) VALUES 
 (1,'Torneo Francisco \"Pancho\" Rossi',2013);
/*!40000 ALTER TABLE `campeonato` ENABLE KEYS */;


--
-- Definition of table `equipo`
--

DROP TABLE IF EXISTS `equipo`;
CREATE TABLE `equipo` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `nombre` varchar(45) NOT NULL,
  `localidad` varchar(45) NOT NULL,
  PRIMARY KEY  USING BTREE (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipo`
--

/*!40000 ALTER TABLE `equipo` DISABLE KEYS */;
INSERT INTO `equipo` (`id`,`nombre`,`localidad`) VALUES 
 (1,'Sporting','Sampacho'),
 (2,'Banda Norte','Rio Cuarto'),
 (3,'U.N.R.C.','Rio Cuarto'),
 (4,'Central Argentino','Rio Cuarto'),
 (5,'Alberdi (VM)','Villa Mercedes'),
 (6,'Acción Juvenil','Rio Cuarto'),
 (7,'C.C. Alberdi','Rio Cuarto'),
 (8,'La Merced','San Luis'),
 (9,'Gorriones RC','Rio Cuarto'),
 (12,'Central Argentino \"B\"','Rio Cuarto');
/*!40000 ALTER TABLE `equipo` ENABLE KEYS */;


--
-- Definition of table `fecha_campeonato`
--

DROP TABLE IF EXISTS `fecha_campeonato`;
CREATE TABLE `fecha_campeonato` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `numero` int(10) unsigned NOT NULL,
  `descripcion` varchar(255) NOT NULL,
  `idCampeonato` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`id`),
  KEY `FK_fecha_campeonato_campeonato` (`idCampeonato`),
  CONSTRAINT `FK_fecha_campeonato_campeonato` FOREIGN KEY (`idCampeonato`) REFERENCES `campeonato` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `fecha_campeonato`
--

/*!40000 ALTER TABLE `fecha_campeonato` DISABLE KEYS */;
INSERT INTO `fecha_campeonato` (`id`,`numero`,`descripcion`,`idCampeonato`) VALUES 
 (6,4,'',1),
 (7,5,'',1),
 (8,6,'',1),
 (9,7,'',1),
 (10,8,'',1),
 (11,9,'',1),
 (12,10,'',1),
 (13,11,'',1),
 (14,1,'',1),
 (15,2,'',1),
 (16,3,'',1);
/*!40000 ALTER TABLE `fecha_campeonato` ENABLE KEYS */;


--
-- Definition of table `jugador`
--

DROP TABLE IF EXISTS `jugador`;
CREATE TABLE `jugador` (
  `nombreApellido` varchar(150) NOT NULL,
  `posicion` varchar(20) NOT NULL,
  `idPlantel` int(10) unsigned NOT NULL default '0',
  `id` int(10) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `jugador`
--

/*!40000 ALTER TABLE `jugador` DISABLE KEYS */;
INSERT INTO `jugador` (`nombreApellido`,`posicion`,`idPlantel`,`id`) VALUES 
 ('Agustin Brunelli','Alero',1,28),
 ('Alan Sanson','Escolta',1,29),
 ('Bustos','Ala-pivot',1,30),
 ('Gonzalo Delfino','Pivot',1,31),
 ('Ignacio Aran','Alero',1,32),
 ('Juan Pablo Bordese','Base',1,33),
 ('Juan Pablo Miazzo','Pivot',1,34),
 ('Leonardo Brol','Base',1,35),
 ('Luis Acha','Escolta',1,36),
 ('Mariano Avanzini','Pivot',1,37),
 ('Renzo Cappellari','Alero',1,38),
 ('Rodrigo Casteletta','Ala-pivot',1,39),
 ('Walter Steinbach','Director Técnico',1,40);
/*!40000 ALTER TABLE `jugador` ENABLE KEYS */;


--
-- Definition of table `imagen`
--

DROP TABLE IF EXISTS `imagen`;
CREATE TABLE `imagen` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `pathBig` varchar(250) NOT NULL,
  `pathSmall` varchar(250) NOT NULL,
  `portada` smallint(5) unsigned NOT NULL default '1',
  `pathMedium` varchar(250) NOT NULL,
  `idJugador` int(10) unsigned default NULL,
  PRIMARY KEY  (`id`),
  KEY `FK_imagen_jugador` (`idJugador`),
  CONSTRAINT `FK_imagen_jugador` FOREIGN KEY (`idJugador`) REFERENCES `jugador` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `imagen`
--

/*!40000 ALTER TABLE `imagen` DISABLE KEYS */;
INSERT INTO `imagen` (`id`,`pathBig`,`pathSmall`,`portada`,`pathMedium`,`idJugador`) VALUES 
 (7,'../Images/plantel/plantel.jpg','../Images/plantel/plantel.jpg',0,'../Images/plantel/plantel.jpg',NULL),
 (22,'../Images/publiciteAquiAzul.jpg','../Images/publiciteAquiAzul.jpg',0,'../Images/publiciteAquiAzul.jpg',NULL),
 (23,'../Images/publiciteAquiNegro.jpg','../Images/publiciteAquiNegro.jpg',0,'../Images/publiciteAquiNegro.jpg',NULL),
 (24,'../Images/publiciteAquiNaranja.jpg','../Images/publiciteAquiNaranja.jpg',0,'../Images/publiciteAquiNaranja.jpg',NULL),
 (39,'../images/plantel/actual/AgustinBrunelli.JPG','../images/plantel/actual/AgustinBrunelli1_thumb.jpg',0,'',28),
 (40,'../images/plantel/actual/AlanSanson.JPG','../images/plantel/actual/AlanSanson_thumb.jpg',0,'',29),
 (41,'../images/plantel/actual/Bustos.JPG','../images/plantel/actual/Bustos_thumb.jpg',0,'',30),
 (42,'../images/plantel/actual/GonzaloDelfino.JPG','../images/plantel/actual/GonzaloDelfino_thumb.jpg',0,'',31),
 (43,'../images/plantel/actual/IgnacioAran.JPG','../images/plantel/actual/IgnacioAran_thumb.jpg',0,'',32),
 (44,'../images/plantel/actual/JuanPabloBordese.JPG','../images/plantel/actual/JuanPabloBordese_thumb.jpg',0,'',33),
 (45,'../images/plantel/actual/JuanPabloMiazzo.JPG','../images/plantel/actual/JuanPabloMiazzo_thumb.jpg',0,'',34),
 (46,'../images/plantel/actual/LeonardoBrol.JPG','../images/plantel/actual/LeonardoBrol_thumb.jpg',0,'',35),
 (47,'../images/plantel/actual/LuisAcha.JPG','../images/plantel/actual/LuisAcha_thumb.jpg',0,'',36),
 (48,'../images/plantel/actual/MarianoAvanzini.JPG','../images/plantel/actual/MarianoAvanzini_thumb.jpg',0,'',37),
 (49,'../images/plantel/actual/RenzoCappellari.JPG','../images/plantel/actual/RenzoCappellari_thumb.jpg',0,'',38),
 (50,'../images/plantel/actual/RodrigoCasteletta.JPG','../images/plantel/actual/RodrigoCasteletta_thumb.jpg',0,'',39),
 (51,'../images/plantel/actual/WalterSteinbach.JPG','../images/plantel/actual/WalterSteinbach_thumb.jpg',0,'',40),
 (52,'../images/noticias/181461_462884457122711_320823316_n.jpg','../images/noticias/181461_462884457122711_320823316_n_thumb.jpg',1,'',NULL),
 (53,'../images/noticias/182789_462884787122678_1483834677_n.jpg','../images/noticias/182789_462884787122678_1483834677_n_thumb.jpg',0,'',NULL),
 (54,'../images/noticias/197704_462884593789364_2082730977_n.jpg','../images/noticias/197704_462884593789364_2082730977_n_thumb.jpg',0,'',NULL),
 (55,'../images/noticias/198971_462884487122708_1388199592_n.jpg','../images/noticias/198971_462884487122708_1388199592_n_thumb.jpg',0,'',NULL),
 (56,'../images/noticias/941175_462884463789377_2034868531_n.jpg','../images/noticias/941175_462884463789377_2034868531_n_thumb.jpg',0,'',NULL),
 (58,'../images/noticias/261679_468978206513336_1675760952_n.jpg','../images/noticias/261679_468978206513336_1675760952_n_thumb.jpg',1,'',NULL),
 (59,'../images/noticias/576672_468977806513376_1221752310_n.jpg','../images/noticias/576672_468977806513376_1221752310_n_thumb.jpg',0,'',NULL),
 (60,'../images/noticias/923266_468978019846688_127957340_n.jpg','../images/noticias/923266_468978019846688_127957340_n_thumb.jpg',0,'',NULL),
 (61,'../images/noticias/969275_468977783180045_1448019316_n.jpg','../images/noticias/969275_468977783180045_1448019316_n_thumb.jpg',0,'',NULL),
 (62,'../images/noticias/969550_468979189846571_12589190_n.jpg','../images/noticias/969550_468979189846571_12589190_n_thumb.jpg',0,'',NULL),
 (63,'../images/noticias/969683_468978916513265_1168594348_n.jpg','../images/noticias/969683_468978916513265_1168594348_n_thumb.jpg',0,'',NULL),
 (67,'../images/noticias/922937_463252357085921_718308284_n.jpg','../images/noticias/922937_463252357085921_718308284_n_thumb.jpg',1,'',NULL),
 (68,'../images/noticias/943756_463252353752588_1828817729_n.jpg','../images/noticias/943756_463252353752588_1828817729_n_thumb.jpg',0,'',NULL),
 (69,'../images/noticias/390573_463252433752580_1390923748_n.jpg','../images/noticias/390573_463252433752580_1390923748_n_thumb.jpg',0,'',NULL);
/*!40000 ALTER TABLE `imagen` ENABLE KEYS */;


--
-- Definition of table `noticia`
--

DROP TABLE IF EXISTS `noticia`;
CREATE TABLE `noticia` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `titulo` varchar(250) NOT NULL,
  `descripcion` mediumtext NOT NULL,
  `principal` bit(1) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `noticia`
--

/*!40000 ALTER TABLE `noticia` DISABLE KEYS */;
INSERT INTO `noticia` (`id`,`titulo`,`descripcion`,`principal`) VALUES 
 (1,'Sporting venció a Acción Juvenil','En juego válido por la 5º Fecha del certamen denominado Francisco \"Pancho\" Rossi organizado por la Asociación Riocuartense de Básquetbol, Sporting Club le ganó, como local, a Acción Juvenil de Río Cuarto por 63 a 54. \r\n\r\nEl partido no tuvo grandes pasajes de buen básquet pero que dejó, como es habitual, la actitud de los \"albos\". Los goleadores de la noche fueron Miazzo de Sporting y Silvestri de Acción, ambos con 15 puntos.\r\n\r\nLos parciales fueron los siguientes: 13-13/24-24/45-35/63-54.Estos fueron los marcadores particulares :SPORTING: Bordese (11), Avanzini (9), Casteletta (14), Brol (5), Acha (9)x y Miazzo (15). DT: Walter Steinbach. ACCIÓN JUVENIL: Engeman (14), Santinelli (12), Silvestri (15), Juárez (4)x, Carrizo (6) y Gonzalo (3). DT: Pablo Gautero. En Sub-17, también ganó Sporting ante Acción Juvenil por 78 a 38',0x00),
 (2,'Sporting venció a A.A Banda Norte','Por una nueva fecha del Torneo Francisco \"Pancho\" Rossi, Sporting, en su estadio, le ganó 51 a 46 a Banda Norte que era uno de los punteros del certamen de Mayores de la Asociación Riocuartense de Básquetbol. \r\n\r\nTrabajoso triunfo, con vaivenes y sin defición hasta el fianl del juego. Los \"albos\" no pudieron despegarse de su rival para conseguir una diferencia tranquilizadora. La progresión en el marcador fue la siguiente, siempre a favor de los locales: 15-12/27-19/41-33/51-46. Buena actuación del joven Alan Sanson quien sumó minutos y aportó para la victoria. Éstos fueron los registros particulares: SPORTING (51): Bordese (6), Casteletta (10), Avanzini (4), Acha (5), Miazzo (16), Sanson (10). DT: Walter Steinbach. BANDA NORTE (46): Argüello (4), Grasseller (7), Ferrero (11), Bautista (3), Posse (6), Ciravegna (11), Rinaudo (4). DT: Gustavo Aguilera. ARBITROS: RIERA-RETAMOZO. Estos equipos jugaron en Sub 15 y Sub 17, con dos victorias \"verdes\", 58 a 80 y 64 a 45, respectivamente.',0x00),
 (3,'\"San Fernando Mayor 2012\"','El equipo de voley masculino del Instituto Pizzurno recibió el galardón mayor en la 18º entrega de los Premios San Fernando. Fue por consagrarse campeón provincial en la categoría Sub-18 y su participación en el nacional de Mar del Plata.  Hubo premiaciones en cada una de las disciplinas, menciones especiales y deportitas y dirigentes en el recuerdo. Asistieron más 700 personas al gimnasio del Instituto La Consolata.\r\n\r\nEl voley del Instituto Pizzurno fue el gran ganador de la noche. Se llevó el \"San Fernando Mayor 2012\" en su 18 edición. A continuación, todos los ganadores.',0x00);
/*!40000 ALTER TABLE `noticia` ENABLE KEYS */;


--
-- Definition of table `imagen_x_noticia`
--

DROP TABLE IF EXISTS `imagen_x_noticia`;
CREATE TABLE `imagen_x_noticia` (
  `idImagen` int(10) unsigned NOT NULL,
  `idNoticia` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`idImagen`,`idNoticia`),
  KEY `FK_imagen_x_noticia_noticia` (`idNoticia`),
  CONSTRAINT `FK_imagen_x_noticia_imagen` FOREIGN KEY (`idImagen`) REFERENCES `imagen` (`id`) ON DELETE CASCADE,
  CONSTRAINT `FK_imagen_x_noticia_noticia` FOREIGN KEY (`idNoticia`) REFERENCES `noticia` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `imagen_x_noticia`
--

/*!40000 ALTER TABLE `imagen_x_noticia` DISABLE KEYS */;
INSERT INTO `imagen_x_noticia` (`idImagen`,`idNoticia`) VALUES 
 (52,1),
 (53,1),
 (54,1),
 (55,1),
 (56,1),
 (58,2),
 (59,2),
 (60,2),
 (61,2),
 (62,2),
 (63,2),
 (67,3),
 (68,3),
 (69,3);
/*!40000 ALTER TABLE `imagen_x_noticia` ENABLE KEYS */;


--
-- Definition of table `multimedia`
--

DROP TABLE IF EXISTS `multimedia`;
CREATE TABLE `multimedia` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `titulo` varchar(100) NOT NULL,
  `urlVideo` varchar(250) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `multimedia`
--

/*!40000 ALTER TABLE `multimedia` DISABLE KEYS */;
INSERT INTO `multimedia` (`id`,`titulo`,`urlVideo`) VALUES 
 (1,'Primer Video de Sporting','<iframe width=\"600\" height=\"360\" src=\"http://www.youtube.com/embed/ee0bvmTXaqw?feature=player_detailpage\" frameborder=\"0\" allowfullscreen></iframe>'),
 (2,'Segundo Video de Sporting','<iframe width=\"600\" height=\"360\" src=\"http://www.youtube.com/embed/621EiVbAUA8?feature=player_detailpage\" frameborder=\"0\" allowfullscreen></iframe>');
/*!40000 ALTER TABLE `multimedia` ENABLE KEYS */;


--
-- Definition of table `plantel`
--

DROP TABLE IF EXISTS `plantel`;
CREATE TABLE `plantel` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `temporada` varchar(150) NOT NULL,
  `idFoto` int(10) unsigned NOT NULL,
  `info` varchar(250) NOT NULL,
  `actual` tinyint(1) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `plantel`
--

/*!40000 ALTER TABLE `plantel` DISABLE KEYS */;
INSERT INTO `plantel` (`id`,`temporada`,`idFoto`,`info`,`actual`) VALUES 
 (1,'2013',7,'Plantel de la temporada 2013',1);
/*!40000 ALTER TABLE `plantel` ENABLE KEYS */;


--
-- Definition of table `resultado_partido`
--

DROP TABLE IF EXISTS `resultado_partido`;
CREATE TABLE `resultado_partido` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `idFecha` int(10) unsigned NOT NULL,
  `idEquipoLocal` varchar(45) NOT NULL,
  `localPuntos` int(10) unsigned NOT NULL,
  `idEquipoVisitante` varchar(45) NOT NULL,
  `visitantePuntos` int(10) unsigned NOT NULL,
  `jugado` smallint(5) unsigned NOT NULL,
  `fechaPartido` datetime NOT NULL,
  PRIMARY KEY  USING BTREE (`id`),
  KEY `FK_resultado_partido_fecha_campeonato` (`idFecha`),
  CONSTRAINT `FK_resultado_partido_fecha_campeonato` FOREIGN KEY (`idFecha`) REFERENCES `fecha_campeonato` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
