using System;
using System.Data;
using System.Collections.Generic;

public class GestorPlantel
{
    public static Plantel getPlantelActual()
    {
        return PlantelDAL.getPlantelActual();
    }

    public static List<Jugador> getJugadoresPlantelActual()
    {
        List<Jugador> jugadores = new List<Jugador>();
        jugadores = PlantelDAL.getPlantelActual().Jugadores;
        return jugadores;
    }

    public static void registrarJugador(Jugador jugador)
    {
        try
        {
            PlantelDAL.insertarJugador_plantelActual(jugador);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (PathImgEmptyException imgEx)
        {
            throw imgEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al registrar un nuevo jugador."+ e.Message);
        }
    }
}