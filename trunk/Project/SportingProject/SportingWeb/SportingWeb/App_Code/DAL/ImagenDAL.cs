using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class ImagenDAL
{
    /// <summary>
    /// Setea la lista de imagenes de una noticia
    /// </summary>
    /// <returns></returns>
    public static List<Imagen> getImagenes(Noticia noticia)
    {
        DataSet ds = new DataSet();
        List<Imagen> listaImagenes = new List<Imagen>();
        OdbcDataReader dr = null;

        String query = "SELECT i.id, i.pathBig, i.pathSmall, i.portada, i.pathMedium FROM imagen i, imagen_x_noticia n WHERE i.id=n.idImagen AND n.idNoticia=" + noticia.IdNoticia;
        
        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            using (OdbcCommand cmd = new OdbcCommand(query, con))
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
            }
        }

        if (dr != null)
        {
            try
            {
                while (dr.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.IdImagen = dr.GetInt32(0);
                    imagen.PathBig = dr.GetString(1);
                    imagen.PathSmall = dr.GetString(2);
                    imagen.Portada = dr.GetBoolean(3);
                    imagen.PathMedium = dr.GetString(4);
                    listaImagenes.Add(imagen);
                }
            }
            catch (Exception e)
            {
                throw new SportingException("Ocurrio un problema al intentar obtener las imagenes de las noticias. " + e.Message);
            }
        }
        
        return listaImagenes;
    }

    public static DataTable getDataTableImagenes(int id)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        DataTable dataTable = null;
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.pathBig, i.pathSmall, i.pathMedium FROM imagen_x_noticia ixn, imagen i" +
            " WHERE ixn.idNoticia = " + id + " AND i.id = ixn.idImagen", con);
            cmd.CommandType = CommandType.Text;

            dataTable = new DataTable();
            OdbcDataAdapter adapter = new OdbcDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las imagenes de las noticias. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return dataTable;
    }

    /// <summary>
    /// retorna una imagen
    /// </summary>
    /// <returns></returns>
    public static Imagen getImagen(OdbcConnection con, int idImagen)
    {
        DataSet ds = new DataSet();
        Imagen imagen = new Imagen();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.id, i.pathBig, i.pathSmall, i.portada, i.pathMedium FROM imagen i WHERE i.id=" + idImagen, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imagen.IdImagen = dr.GetInt32(0);
                imagen.PathBig = dr.GetString(1);
                imagen.PathSmall = dr.GetString(2);
                imagen.Portada = dr.GetBoolean(3);
                imagen.PathMedium = dr.GetString(4);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener la imagen. " + e.Message);
        }
        return imagen;
    }

    public static Imagen getImagenJugador(OdbcConnection con, int idJugador)
    {
        DataSet ds = new DataSet();
        Imagen imagen = new Imagen();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.id, i.pathBig, i.pathSmall, i.portada, i.pathMedium FROM imagen i WHERE i.idJugador=" + idJugador, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imagen.IdImagen = dr.GetInt32(0);
                imagen.PathBig = dr.GetString(1);
                imagen.PathSmall = dr.GetString(2);
                imagen.Portada = dr.GetBoolean(3);
                imagen.PathMedium = dr.GetString(4);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener la imagen. " + e.Message);
        }
        return imagen;
    }
}