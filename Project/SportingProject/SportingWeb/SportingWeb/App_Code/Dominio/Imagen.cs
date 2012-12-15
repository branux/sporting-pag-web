using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Odbc;

[Serializable]
public class Imagen //: ConexionBD
{
    private String pathBig;
    private String pathSmall;
    private int idImagen;
    private Boolean portada;

    public int IdImagen
    {
        get { return idImagen; }
        set { idImagen = value; }
    }

    public Imagen()
    {
    }

    public String PathSmall
    {
        get { return pathSmall; }
        set { pathSmall = value; }
    }

    public String PathBig
    {
        get { return pathBig; }
        set { pathBig = value; }
    }

    public Boolean Portada
    {
        get { return portada; }
        set { portada = value; }
    }
   /* public static void deleteImagen(int idImg)
    {
        try
        {
            OdbcCommand cmd = new OdbcCommand("DELETE FROM imagen " +
                                              "WHERE idImagen=" + idImg.ToString(), ObtenerConexion());

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //Obtengo una imagen de base de datos segun su id
    public static Imagen getImagen(int idImg)
    {
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT pathGrande, pathChica, idImagen " +
                                              "FROM imagen " +
                                              "WHERE idImagen=" + idImg.ToString(), ObtenerConexion());

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmd.Connection.Close();
            Imagen imagenRet = new Imagen();

            imagenRet.PathBig = dt.Rows[0]["pathGrande"].ToString();
            imagenRet.PathSmall = dt.Rows[0]["pathChica"].ToString();
            imagenRet.IdImagen = Convert.ToInt32(dt.Rows[0]["idImagen"].ToString());

            return imagenRet;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //Chequear que no haya misma imagen en uso por otro producto (calzado o accesorio)
    public static bool imagenEnUsoPorOtroProducto(string path)
    {
        bool enUso = false;
        try
        {
            OdbcCommand cmd = new OdbcCommand("SELECT count(pathGrande) FROM imagen " +
                                              "WHERE pathGrande = '" + path + "'", ObtenerConexion());

            int resultado = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();

            if (resultado > 0)
            {
                enUso = true;
            }
            return enUso;
        }
        catch (Exception e)
        {
            throw e;
        }
    }*/
}
