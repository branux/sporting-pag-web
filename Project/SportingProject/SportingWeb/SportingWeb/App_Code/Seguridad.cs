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
using System.Data.SqlClient;
using System.Data.Odbc;

public class Seguridad : ConexionBD
{
    public Seguridad()
    {

    }
    public static Int32 validarUsuario(String usr, String pass)
    {
        String query = "SELECT idUsuario FROM usuario " +
            "WHERE user='" + usr.ToString() + "'" +
            "AND   password='" + pass.ToString() + "'";
        try
        {


            using (OdbcConnection con = new OdbcConnection(Constantes.CONNECTION_STRING))
            {
                using (OdbcCommand cmd = new OdbcCommand(query, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    Int32 idUser = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Connection.Close();

                    return idUser;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string ObtenerRol(Int32 idUsuario)
    {
        String query = "SELECT rol FROM usuario " +
            "WHERE idUsuario=" + idUsuario.ToString();
        try
        {
            OdbcCommand cmd = new OdbcCommand(query, ObtenerConexion());

            String rol = cmd.ExecuteScalar().ToString();
            cmd.Connection.Close();

            return rol;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
