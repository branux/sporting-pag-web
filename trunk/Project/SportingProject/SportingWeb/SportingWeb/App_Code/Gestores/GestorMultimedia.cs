using System;
using System.Data;
using System.Collections.Generic;

public class GestorMultimedia
{
    public static List<MultimediaVideo> getAllMultimedia()
    {
        return MultimediaDAL.getAllMultimedia();
    }
}