using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public class NoticiaDAL
{
    private static String selectNoticia = "SELECT n.id, n.titulo, n.descripcion, n.principal FROM noticia n ";

    //Obtengo una noticia de base de datos por su id
    public static Noticia getNoticiaById(int id)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        OdbcCommand cmd = null;
        Noticia noticia = null;
        try
        {
            cmd = new OdbcCommand(selectNoticia + "WHERE n.id=" + id.ToString(), con);

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                noticia.Imagenes = ImagenDAL.getImagenes(noticia, con);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener la noticia "+id+" de la base de datos. "+e.Message);
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
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        OdbcDataReader dr = null;

        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            using (OdbcCommand cmd = new OdbcCommand(selectNoticia, con))
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Noticia noticia = new Noticia();
                        noticia.IdNoticia = dr.GetInt32(0);
                        noticia.Titulo = dr.GetString(1);
                        noticia.Descripcion = dr.GetString(2);
                        noticia.Principal = dr.GetBoolean(3);
                        noticia.Imagenes = ImagenDAL.getImagenes(noticia, con);
                        listaNoticias.Add(noticia);
                    }
                }
                catch (Exception e)
                {
                    throw new SportingException("Ocurrio un problema al intentar obtener todas las noticias. " + e.Message);
                }


            }
        }

        return listaNoticias;
    }

    /// <summary>
    /// Retorna un datatable con todas las noticias.
    /// Cada noticia con el pathmedium de su imagen de portada.
    /// </summary>
    /// <returns></returns>
    public static DataTable getDataTableNoticias()
    {
        DataTable dataTable = new DataTable();
        String query = "SELECT n.id, n.titulo, n.descripcion, n.principal, i.pathBig "+
                "FROM noticia n, imagen_x_noticia ixn, imagen i "+
                "WHERE ixn.idImagen = i.id AND ixn.idNoticia = n.id AND i.portada = 1";

        using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
        {
            using (OdbcCommand cmd = new OdbcCommand(query, con))
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                try
                {
                    dataTable = new DataTable();
                    OdbcDataAdapter adapter = new OdbcDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
                catch (Exception e)
                {
                    //StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~") + "log.txt", true);
                    //sw.WriteLine(e.Message);
                    //sw.Flush();
                    //sw.Close();
                    //throw new SportingException("Ocurrio un problema al intentar obtener todas las noticias. " + e.Message);
                    throw e;
                }
            }
        }

        return dataTable;
    }

    public static List<Noticia> getNoticiasPrincipales()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        DataSet ds = new DataSet();
        List<Noticia> listaNoticias = new List<Noticia>();
        try
        {
            OdbcCommand cmd = new OdbcCommand(selectNoticia + "WHERE principal = 1", con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Noticia noticia = new Noticia();
                noticia.IdNoticia = dr.GetInt32(0);
                noticia.Titulo = dr.GetString(1);
                noticia.Descripcion = dr.GetString(2);
                noticia.Principal = dr.GetBoolean(3);
                noticia.Imagenes = ImagenDAL.getImagenes(noticia, con);
                listaNoticias.Add(noticia);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al intentar obtener las noticias principales. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return listaNoticias;
    }

    public static void insertarNoticia(Noticia noticia)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (noticia == null)
            {
                throw new SportingException("Error al registrar nueva noticia. Noticia sin información.");
            }
            if (noticia.Imagenes == null)
            {
                throw new PathImgEmptyException("Error al registrar nueva noticia. La noticia no posee imagenes.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos de la noticia
            String insertarNoticia;
            insertarNoticia = " insert into noticia (titulo, descripcion, principal)" +
                              " values ('" + noticia.Titulo + "', '" + noticia.Descripcion + "', " +
                              noticia.Principal + ")";
            cmd = new OdbcCommand(insertarNoticia, conexion);
            cmd.ExecuteNonQuery();

            //Obtengo el id de la noticia que acabo de insertar
            String lastIdNoticia = "Select  LAST_INSERT_ID()";
            cmd = new OdbcCommand(lastIdNoticia, conexion);
            int idNoticia = Convert.ToInt32(cmd.ExecuteScalar());
 
            //Guardo las imagenes de la noticia
            String insertarImagen;
            bool portada = true;//la 1er imagen cargada queda como "portada"
            foreach (Imagen img in noticia.Imagenes)
            {
                //inserto la imagen
                insertarImagen = "insert into imagen (pathBig, pathSmall, portada, pathMedium) values ('" +
                    img.PathBig + "', '" + img.PathSmall + "', " + portada + ", '" + img.PathMedium + "')";
                cmd = new OdbcCommand(insertarImagen, conexion);
                cmd.ExecuteNonQuery();
                portada = false;

                //Obtengo el id de la imagen que acabo de insertar
                String lastIdImagen = "Select  LAST_INSERT_ID()";
                cmd = new OdbcCommand(lastIdImagen, conexion);
                int idImagen = Convert.ToInt32(cmd.ExecuteScalar());

                //inserto la imagen y la noticia (tabla imagen_x_noticia)
                String insertImgXNoticia = "insert into imagen_x_noticia (idImagen, idNoticia) values (" + 
                    idImagen + ", " + idNoticia + ")";
                cmd = new OdbcCommand(insertImgXNoticia, conexion);
                cmd.ExecuteNonQuery();
            }
            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void updateNoticia(Noticia noticia)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (noticia == null)
            {
                throw new SportingException("Error al modificar la noticia. Noticia sin información.");
            }
            if (noticia.Imagenes == null)
            {
                throw new PathImgEmptyException("Error al modificar la noticia. la noticia no posee imagenes.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Actualizo los datos de la noticia
            String updateNoticia = "UPDATE noticia set titulo='" + noticia.Titulo +
                                    "', descripcion = '" + noticia.Descripcion + "' WHERE id = " +
                                    noticia.IdNoticia;
            cmd = new OdbcCommand(updateNoticia, conexion);
            cmd.ExecuteNonQuery();

            //Borro todas las imagenes de la noticia
            String borrarImagenes = " delete i from imagen i where i.id in "
                           + " ( select ixn.idImagen from imagen_x_noticia ixn "
                           + " where idNoticia =" + noticia.IdNoticia + ") ";
            cmd = new OdbcCommand(borrarImagenes, conexion);
            cmd.ExecuteNonQuery();

            //Registro las imagenes de la noticia todas de nuevo
            bool portada = true;//la 1er imagen cargada queda como "portada"
            foreach (Imagen img in noticia.Imagenes)
            {
                //inserto la imagen
                String insertarImagen = "insert into imagen (pathBig, pathSmall, portada, pathMedium) values ('" +
                    img.PathBig + "', '" + img.PathSmall + "', " + portada + ", '" + img.PathMedium + "')";
                cmd = new OdbcCommand(insertarImagen, conexion);
                cmd.ExecuteNonQuery();
                portada = false;

                //Obtengo el id de la imagen que acabo de insertar
                String lastIdImagen = "Select  LAST_INSERT_ID()";
                cmd = new OdbcCommand(lastIdImagen, conexion);
                int idImagen = Convert.ToInt32(cmd.ExecuteScalar());

                //inserto la imagen y la noticia (tabla imagen_x_noticia)
                String insertImgXNoticia = "insert into imagen_x_noticia (idImagen, idNoticia) values (" +
                    idImagen + ", " + noticia.IdNoticia + ")";
                cmd = new OdbcCommand(insertImgXNoticia, conexion);
                cmd.ExecuteNonQuery();
            }

            conexion.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    public static void deleteNoticia(int id)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            //Borro todas las imagenes de la noticia
            String borrarImagenes = " delete i from imagen i where i.id in "
                           + " ( select ixn.idImagen from imagen_x_noticia ixn "
                           + " where idNoticia =" + id.ToString() + ") ";
            cmd = new OdbcCommand(borrarImagenes, conexion);
            cmd.ExecuteNonQuery();

            //borro la noticia
            String deleteNoticia = "DELETE FROM noticia WHERE id = " + id.ToString();
            cmd = new OdbcCommand(deleteNoticia, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar una noticia. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
}