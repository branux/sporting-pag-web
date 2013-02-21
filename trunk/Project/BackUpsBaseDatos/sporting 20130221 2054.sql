-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.77-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema sporting
--

CREATE DATABASE IF NOT EXISTS sporting;
USE sporting;

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
 (1,'Torneo Inicial',2013);
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipo`
--

/*!40000 ALTER TABLE `equipo` DISABLE KEYS */;
INSERT INTO `equipo` (`id`,`nombre`,`localidad`) VALUES 
 (1,'Sporting','Sampacho'),
 (2,'Lambert','Monte Maiz'),
 (3,'Argentino','Monte Maiz'),
 (4,'Corralense','Corral de Bustos'),
 (5,'Renny','Escalante'),
 (6,'Olimpo','Laborde');
/*!40000 ALTER TABLE `equipo` ENABLE KEYS */;


--
-- Definition of table `fecha_campeonato`
--

DROP TABLE IF EXISTS `fecha_campeonato`;
CREATE TABLE `fecha_campeonato` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `numero` int(10) unsigned NOT NULL,
  `fecha` datetime NOT NULL,
  `idCampeonato` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `fecha_campeonato`
--

/*!40000 ALTER TABLE `fecha_campeonato` DISABLE KEYS */;
INSERT INTO `fecha_campeonato` (`id`,`numero`,`fecha`,`idCampeonato`) VALUES 
 (1,1,'2013-01-02 00:00:00',1);
/*!40000 ALTER TABLE `fecha_campeonato` ENABLE KEYS */;


--
-- Definition of table `imagen`
--

DROP TABLE IF EXISTS `imagen`;
CREATE TABLE `imagen` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `pathBig` varchar(45) NOT NULL,
  `pathSmall` varchar(45) NOT NULL,
  `portada` smallint(5) unsigned NOT NULL default '1',
  `pathMedium` varchar(45) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `imagen`
--

/*!40000 ALTER TABLE `imagen` DISABLE KEYS */;
INSERT INTO `imagen` (`id`,`pathBig`,`pathSmall`,`portada`,`pathMedium`) VALUES 
 (1,'../Images/noticias/noticia1.jpg','../Images/noticias/noticia1small.jpg',1,'../Images/noticias/noticia1Medium.jpg'),
 (2,'../Images/noticias/noticia2.jpg','../Images/noticias/noticia2.jpg',1,'../Images/noticias/noticia2Medium.jpg'),
 (3,'../Images/noticias/lateral1.jpg','../Images/noticias/lateral1Small.jpg',1,'../Images/noticias/lateral1.jpg'),
 (4,'../Images/noticias/lateral2.jpg','../Images/noticias/lateral2Small.jpg',1,'../Images/noticias/lateral2.jpg'),
 (5,'../Images/noticias/NoticiaExp2Big.jpg','../Images/noticias/NoticiaExp2Small.jpg',0,'../Images/noticias/NoticiaExp2Small.jpg'),
 (6,'../Images/noticias/NoticiaExp3Big.jpg','../Images/noticias/NoticiaExp3Small.jpg',0,'../Images/noticias/NoticiaExp3Small.jpg'),
 (7,'../Images/plantel/plantel.jpg','../Images/plantel/plantel.jpg',0,'../Images/plantel/plantel.jpg'),
 (8,'../Images/plantel/jugador1.jpg','../Images/plantel/jugador1.jpg',0,'../Images/plantel/jugador1.jpg'),
 (9,'../Images/plantel/jugador2.jpg','../Images/plantel/jugador2.jpg',0,'../Images/plantel/jugador2.jpg'),
 (10,'../Images/plantel/jugador3.jpg','../Images/plantel/jugador3.jpg',0,'../Images/plantel/jugador3.jpg'),
 (11,'../Images/plantel/jugador4.jpg','../Images/plantel/jugador4.jpg',0,'../Images/plantel/jugador4.jpg'),
 (12,'../Images/plantel/jugador5.jpg','../Images/plantel/jugador5.jpg',0,'../Images/plantel/jugador5.jpg'),
 (13,'../Images/plantel/jugador6.jpg','../Images/plantel/jugador6.jpg',0,'../Images/plantel/jugador6.jpg'),
 (14,'../Images/plantel/jugador7.jpg','../Images/plantel/jugador7.jpg',0,'../Images/plantel/jugador7.jpg'),
 (15,'../Images/plantel/jugador8.jpg','../Images/plantel/jugador8.jpg',0,'../Images/plantel/jugador8.jpg');
/*!40000 ALTER TABLE `imagen` ENABLE KEYS */;


--
-- Definition of table `imagen_x_noticia`
--

DROP TABLE IF EXISTS `imagen_x_noticia`;
CREATE TABLE `imagen_x_noticia` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `idImagen` int(10) unsigned NOT NULL,
  `idNoticia` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `imagen_x_noticia`
--

/*!40000 ALTER TABLE `imagen_x_noticia` DISABLE KEYS */;
INSERT INTO `imagen_x_noticia` (`id`,`idImagen`,`idNoticia`) VALUES 
 (1,1,1),
 (2,2,2),
 (3,3,3),
 (4,4,4),
 (5,5,1),
 (6,6,1);
/*!40000 ALTER TABLE `imagen_x_noticia` ENABLE KEYS */;


--
-- Definition of table `jugador`
--

DROP TABLE IF EXISTS `jugador`;
CREATE TABLE `jugador` (
  `nombreApellido` varchar(150) NOT NULL,
  `posicion` varchar(20) NOT NULL,
  `idPlantel` int(10) unsigned NOT NULL default '0',
  `idFoto` int(10) unsigned NOT NULL default '0',
  `id` int(10) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `jugador`
--

/*!40000 ALTER TABLE `jugador` DISABLE KEYS */;
INSERT INTO `jugador` (`nombreApellido`,`posicion`,`idPlantel`,`idFoto`,`id`) VALUES 
 ('Bruno Labaque','Base',1,8,1),
 ('Matias Lescano','Alero',1,9,2),
 ('Roman Gonzalez','Pivot',1,10,3),
 ('Javier Bulfoni','Escolta',1,11,4),
 ('Diego Guaita','Ala Pivot',1,12,5),
 ('Mariano Fierro','Ala Pivot',1,13,6),
 ('Alexis Elsener','Alero',1,14,7),
 ('Julian Aprea','Pivot',1,15,8);
/*!40000 ALTER TABLE `jugador` ENABLE KEYS */;


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
-- Definition of table `noticia`
--

DROP TABLE IF EXISTS `noticia`;
CREATE TABLE `noticia` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `titulo` varchar(50) NOT NULL,
  `descripcion` varchar(300) NOT NULL,
  `principal` bit(1) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `noticia`
--

/*!40000 ALTER TABLE `noticia` DISABLE KEYS */;
INSERT INTO `noticia` (`id`,`titulo`,`descripcion`,`principal`) VALUES 
 (1,'Sporting estrena web','Sporting tiene su nueva web, pasa a conocerla, tenemos todas las secciones que vos queres.',0x00),
 (2,'Nueva Noticia en Sporting','Una tendinitis no le permitió cerrar el año de la mejor manera al uruguayense Elnes Bolling Jr que jugó apenas 4 minutos en la derrota de su equipo en el clásico de San Luis por el Torneo Federal. Todo lo contrario para Facu Mendoza que con Estudiantes de Olavarría sigue ganando y anotando puntos.',0x01),
 (3,'Noticia lateral 1','Vamos que sale la primera noticia lateral del dia.',0x00),
 (4,'Noticia lateral 2','Vamos que sale la segunda noticia lateral del dia.',0x00);
/*!40000 ALTER TABLE `noticia` ENABLE KEYS */;


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
 (1,'2012/2013',7,'Plantel de la temporada 2012/2013',1);
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
  PRIMARY KEY  USING BTREE (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `resultado_partido`
--

/*!40000 ALTER TABLE `resultado_partido` DISABLE KEYS */;
INSERT INTO `resultado_partido` (`id`,`idFecha`,`idEquipoLocal`,`localPuntos`,`idEquipoVisitante`,`visitantePuntos`,`jugado`) VALUES 
 (1,1,'1',100,'2',95,1),
 (2,1,'3',102,'4',98,1),
 (3,1,'5',55,'6',78,1);
/*!40000 ALTER TABLE `resultado_partido` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
