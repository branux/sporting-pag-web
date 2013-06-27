--
-- Definition of table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `idUsuario` int(10) unsigned NOT NULL auto_increment,
  `user` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `rol` varchar(45) NOT NULL,
  PRIMARY KEY  (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usuario`
--

/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` (`idUsuario`,`user`,`password`,`rol`) VALUES 
 (1,'admin','SCDCS2013','Admin');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;