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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

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
 (6,'../Images/noticias/NoticiaExp3Big.jpg','../Images/noticias/NoticiaExp3Small.jpg',0,'../Images/noticias/NoticiaExp3Small.jpg');
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




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
