using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class MultimediaDAL
{
    public static List<Auspiciante> getAllAuspiciantes()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Auspiciante> listaAuspiciantes = new List<Auspiciante>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT a.id, a.imagen FROM auspiciante a", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Auspiciante a = new Auspiciante();
                a.IdAuspiciante = dr.GetInt32(dr.GetOrdinal("id"));
                a.ImagenAuspiciante = ImagenDAL.getImagen(con,dr.GetInt32(dr.GetOrdinal("imagen")));

                listaAuspiciantes.Add(a);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener los auspiciantes. " + e.Message);
        }
        return listaAuspiciantes;
    }
}