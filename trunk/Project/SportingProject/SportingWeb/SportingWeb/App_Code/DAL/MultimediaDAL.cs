using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class MultimediaDAL
{
    public static List<MultimediaVideo> getAllMultimedia()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<MultimediaVideo> listaMultimedia = new List<MultimediaVideo>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT m.id, m.titulo, m.urlVideo FROM multimedia m", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                MultimediaVideo m = new MultimediaVideo();
                m.Id = dr.GetInt32(0);
                m.Titulo = dr.GetString(1);
                m.UrlVideo = dr.GetString(2);
                listaMultimedia.Add(m);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener los videos. " + e.Message);
        }
        return listaMultimedia;
    }
}