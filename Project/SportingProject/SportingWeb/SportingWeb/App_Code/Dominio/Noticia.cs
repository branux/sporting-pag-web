using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.Odbc;


public class Noticia
{
    private int idNoticia;
    private String titulo;
    private String descripcion;
    private List<Imagen> imagenes;

    public Noticia()
    {
    }

    public int IdNoticia
    {
        get { return idNoticia; }
        set { idNoticia = value; }
    }

    public string Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }

    public List<Imagen> Imagenes
    {
        get { return imagenes; }
        set { imagenes = value; }
    }

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
}


    
