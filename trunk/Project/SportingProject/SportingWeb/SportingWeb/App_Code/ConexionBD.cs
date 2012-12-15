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
