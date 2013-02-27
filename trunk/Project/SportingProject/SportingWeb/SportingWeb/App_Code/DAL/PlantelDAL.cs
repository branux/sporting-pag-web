using System.Data.Odbc;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

public class PlantelDAL
{
    public static Plantel getPlantelActual()
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        OdbcCommand cmd = null;
        Plantel plantel = null;
        try
        {
            cmd = new OdbcCommand("SELECT p.id, p.temporada, p.idFoto, p.info, p.actual "+
                "FROM plantel p WHERE p.actual = 1", con);

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                plantel = new Plantel();
                plantel.IdPlantel = dr.GetInt32(0);
                plantel.Temporada = dr.GetString(1);
                plantel.Foto = ImagenDAL.getImagen(con,dr.GetInt32(2));
                plantel.Info = dr.GetString(3);
                plantel.Jugadores = getJugadores(plantel);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener el plantel actual. "+ e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return plantel;
    }

    public static List<Jugador> getJugadores(Plantel plantel)
    {
        OdbcConnection con = ConexionBD.ObtenerConexion();
        OdbcCommand cmd = null;
        List<Jugador> jugadores = new List<Jugador>();
        try
        {
            cmd = new OdbcCommand("SELECT j.id, j.nombreApellido, j.posicion, j.idPlantel " +
                "FROM jugador j WHERE j.idPlantel = "+plantel.IdPlantel, con);

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Jugador jugador = new Jugador();
                jugador.IdJugador = dr.GetInt32(0);
                jugador.NombreApellido = dr.GetString(1);
                jugador.Posicion = dr.GetString(2);
                jugador.Foto = ImagenDAL.getImagenJugador(con, dr.GetInt32(0));
                jugadores.Add(jugador);
            }
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener los jugadores del plantel. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return jugadores ;
    }

    public static void insertarJugador_plantelActual(Jugador jugador)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            if (jugador == null)
            {
                throw new SportingException("Error al registrar nuevo jugador. Jugador sin información.");
            }
            if (jugador.Foto == null || jugador.Foto.PathSmall == "")
            {
                throw new PathImgEmptyException("Error al registrar nuevo jugador. El jugador no posee foto.");
            }
            conexion = ConexionBD.ObtenerConexion();

            //Guardo los datos del jugador
            String insertarJugador;
            insertarJugador = " insert into jugador (nombreApellido, posicion, idPlantel)" +
                              " values ('" + jugador.NombreApellido + "', '" + jugador.Posicion + "', " +
                              1 + ")";
            cmd = new OdbcCommand(insertarJugador, conexion);
            cmd.ExecuteNonQuery();

            //Obtengo el id del jugador que acabo de insertar
            String lastIdJugador = "Select  LAST_INSERT_ID()";
            cmd = new OdbcCommand(lastIdJugador, conexion);
            int idJugador = Convert.ToInt32(cmd.ExecuteScalar());

            //Guardo los datos de la foto del jugador
            String insertarImagen = " insert into imagen (pathBig, pathSmall, pathMedium, portada, idJugador)" +
                           " values ('" + jugador.Foto.PathBig + "', '" + jugador.Foto.PathSmall + "','" +
                            jugador.Foto.PathMedium + "'," + jugador.Foto.Portada + ", " + idJugador + ")";
            cmd = new OdbcCommand(insertarImagen, conexion);
            cmd.ExecuteNonQuery();
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

    public static Jugador getJugador_plantelActual(int id)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        Jugador jugador = new Jugador();
        try
        {
            conexion = ConexionBD.ObtenerConexion();
            String getJugador = "select j.id, j.nombreApellido, j.posicion, j.idPlantel " +
                                "from jugador j where j.idPlantel = 1 and j.id = " + id;

            cmd = new OdbcCommand(getJugador, conexion);

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            jugador.IdJugador = Convert.ToInt32(dt.Rows[0]["id"].ToString());
            jugador.NombreApellido = dt.Rows[0]["nombreApellido"].ToString();
            jugador.Posicion = dt.Rows[0]["posicion"].ToString();
            jugador.Foto = ImagenDAL.getImagenJugador(conexion, id);
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar obtener los datos de un jugador. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return jugador;
    }

    /* Borro el jugador y su foto */
    public static void deleteJugador_plantelActual(int idJugador)
    {
        OdbcConnection conexion = null;
        OdbcCommand cmd = null;
        try
        {
            conexion = ConexionBD.ObtenerConexion();

            String deleteJugador = "DELETE FROM jugador WHERE id = " + idJugador.ToString();

            cmd = new OdbcCommand(deleteJugador, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            throw new SportingException("Ocurrio un error al intentar borrar un jugador. " + e.Message);
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
}