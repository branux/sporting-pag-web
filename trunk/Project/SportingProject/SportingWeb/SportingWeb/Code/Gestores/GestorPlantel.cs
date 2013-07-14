using System;
using System.Data;
using System.Collections.Generic;

public class GestorPlantel
{
    public static Plantel getPlantelActual()
    {
        Plantel plantel = new Plantel();
        try
        {
            plantel = PlantelDAL.getPlantelActual();
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al obtener plantel actual." + e.Message);
        }
        return plantel;
    }

    public static List<Jugador> getJugadoresPlantelActual()
    {
        List<Jugador> jugadores = new List<Jugador>();
        jugadores = PlantelDAL.getPlantelActual().Jugadores;
        return jugadores;
    }

    public static void registrarJugador_plantelActual(Jugador jugador)
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

    public static Jugador getJugador_plantelActual(int id)
    {
        Jugador jugador = null;
        try
        {
            jugador = PlantelDAL.getJugador_plantelActual(id);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al buscar los datos de un jugador." + e.Message);
        }
        return jugador;
    }

    public static void deleteJugador_plantelActual(int id)
    {
        try
        {
            PlantelDAL.deleteJugador_plantelActual(id);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al buscar los datos de un jugador." + e.Message);
        }
    }

    public static void updateJugador_plantelActual(Jugador jugador)
    {
        try
        {
            PlantelDAL.updateJugador_plantelActual(jugador);
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
            throw new SportingException("Error al modificar un jugador." + e.Message);
        }
    }
}