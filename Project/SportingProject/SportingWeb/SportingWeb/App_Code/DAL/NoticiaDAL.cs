using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;

public class NoticiaDAL
{
    //Obtengo una noticia de base de datos por su id
    public static Noticia getNoticiaById(int id)
    {
        OdbcCommand cmd = null;
        Noticia noticia = null;
        try
        {
            cmd = new OdbcCommand("SELECT n.id, n.titulo, n.descripcion" +
                                              " FROM noticia n" +
                                              " WHERE n.id=" + id.ToString(), ConexionBD.ObtenerConexion());

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                setImagenes(noticia);
            }
        }
        catch (Exception e)
        {
            // throw new SportingException("Ocurrio un error al intentar obtener la noticia "+id+" de la base de datos. "+e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return noticia;
    }

    /// <summary>
    /// Retorna la lista de todos las noticias
    /// </summary>
    /// <returns></returns>
    public static List<Noticia> getNoticias()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT n.id, n.titulo, n.descripcion FROM noticia n", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Noticia noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                setImagenes(noticia);
                listaNoticias.Add(noticia);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener todos los zapatos de la base de datos. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return listaNoticias;
    }

    /// <summary>
    /// Setea la lista de imagenes de una noticia
    /// </summary>
    /// <returns></returns>
    public static void setImagenes(Noticia noticia)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Imagen> listaImagenes = new List<Imagen>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.id, i.pathBig, i.pathSmall, i.portada FROM imagen i, imagen_x_noticia n WHERE i.id=n.idImagen AND n.idNoticia=" + noticia.IdNoticia, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Imagen imagen = new Imagen();
                imagen.IdImagen = dr.GetInt32(0);
                imagen.PathBig = dr.GetString(1);
                imagen.PathSmall = dr.GetString(2);
                imagen.Portada = dr.GetBoolean(3);
                listaImagenes.Add(imagen);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las imagenes de las noticias. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        noticia.Imagenes = listaImagenes;
    }
}