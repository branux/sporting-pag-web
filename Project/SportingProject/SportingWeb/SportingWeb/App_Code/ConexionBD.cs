using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Odbc;
using System.Data;
using System.Collections.Generic;

public class ConexionBD
{
    private static OdbcConnection con;

    public ConexionBD()
    {

    }

    public static OdbcConnection ObtenerConexion()
    {
        try
        {
            if (con == null)
            {
                con = new OdbcConnection(ConfigurationManager.ConnectionStrings["sportingCn"].ConnectionString.ToString());
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema con la conexión a la base de datos" + e.Message);
        }

    }

    public static void cerrarConexion()
    {
        try
        {
            con.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un problema al cerrar la conexión a la base de datos" + e.Message);
        }
    }

    /// <summary>
    /// Retorna la lista de todos las noticias
    /// </summary>
    /// <returns></returns>
    public List<Noticia> getNoticias()
    {
        con = ObtenerConexion();
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
    public void setImagenes(Noticia noticia)
    {
        con = ObtenerConexion();
        DataSet ds = new DataSet();
        List<Imagen> listaImagenes = new List<Imagen>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT i.id, i.pathBig, i.pathSmall FROM imagen i, imagen_x_noticia n WHERE i.id=n.idImagen AND n.idNoticia=" + noticia.IdNoticia, con);
            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Imagen imagen = new Imagen();
                imagen.IdImagen = dr.GetInt32(0);
                imagen.PathBig = dr.GetString(1);
                imagen.PathSmall = dr.GetString(2);

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

    /*public DataSet getDatasetCalzados()
    {
        con = ObtenerConexion();
        DataSet ds = new DataSet();
        List<Calzado> listaCalzados = new List<Calzado>();
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT c.idCalzado, c.codigo, c.nombre, c.descripcion, c.idColeccion ,i.pathGrande, i.pathChica FROM calzado c, imagen i WHERE c.idCalzado=i.idCalzado", con);
            cmd.CommandType = CommandType.Text;

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            da.Fill(ds, "Conferencias");
        }
        catch (Exception e)
        {
            throw new CardellaException("Ocurrio un problema al intentar obtener todos los zapatos de la base de datos. " + e.Message);
        }
        finally
        {
            con.Close();
        }
        return ds;
    }*/
}
