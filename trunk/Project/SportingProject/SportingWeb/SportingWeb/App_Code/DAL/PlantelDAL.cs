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
            cmd = new OdbcCommand("SELECT j.id, j.nombreApellido, j.posicion, j.idPlantel, j.idFoto " +
                "FROM jugador j WHERE j.idPlantel = "+plantel.IdPlantel, con);

            cmd.CommandType = CommandType.Text;
            OdbcDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Jugador jugador = new Jugador();
                jugador.IdJugador = dr.GetInt32(0);
                jugador.NombreApellido = dr.GetString(1);
                jugador.Posicion = dr.GetString(2);
                jugador.Foto = ImagenDAL.getImagen(con,dr.GetInt32(4));
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
}