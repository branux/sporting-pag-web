using System;
using System.Configuration;
using System.Data.Odbc;
using System.Data;

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
}
