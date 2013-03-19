using System;
using System.Data;
using System.Collections.Generic;

public class GestorCampeonato
{
    public static TablaPosiciones getTablaPosiciones(CampeonatoLiga camp)
    {
        List<Resultado> resultados = CampeonatoDAL.getResultadosCampeonato(camp,true);
        TablaPosiciones tabla = new TablaPosiciones();
        tabla.Posiciones = new List<PosicionTabla>();
        foreach (Resultado res in resultados)
        {
            EquipoCampeonato ganador = getGanador(res);
            
            Boolean ganadorLocal = res.EquipoLocal.CompareTo(ganador) == 0;
            actualizarPuntosEquipo(tabla, res.EquipoLocal, ganadorLocal);

            Boolean ganadorVisitante = res.EquipoVisitante.CompareTo(ganador) == 0;
            actualizarPuntosEquipo(tabla, res.EquipoVisitante, ganadorVisitante);
        }
        tabla.Posiciones.Sort(tabla.ComparadorPosicion);
        return tabla;
    }

    private static EquipoCampeonato getGanador(Resultado res)
    {
        if (res.EquipoLocalPuntos - res.EquipoVisitantePuntos > 0)
        {
            return res.EquipoLocal;
        }
        return res.EquipoVisitante;
    }

    private static void actualizarPuntosEquipo(TablaPosiciones tabla, EquipoCampeonato equipo, Boolean ganador)
    {
        PosicionTabla nuevaPosicion = new PosicionTabla();
        nuevaPosicion.Equipo = equipo;
        nuevaPosicion.PartidosJugados = 1;
        if (ganador)
        {
            nuevaPosicion.PartidosGanados = 1;
            nuevaPosicion.Puntos = 2;
        }
        else 
        {
            nuevaPosicion.PartidosPerdidos = 1;
            nuevaPosicion.Puntos = 1;
        }

        if (tabla.Posiciones.Contains(nuevaPosicion))
        {
            foreach (PosicionTabla pos in tabla.Posiciones)
            {
                if (pos.Equipo.CompareTo(equipo) == 0)
                {
                    pos.PartidosJugados++;
                    pos.PartidosGanados += nuevaPosicion.PartidosGanados;
                    pos.PartidosPerdidos += nuevaPosicion.PartidosPerdidos;
                    pos.Puntos += nuevaPosicion.Puntos;
                }
            }
        }
        else
        {
            tabla.Posiciones.Add(nuevaPosicion);   
        }
    }

    public static CampeonatoLiga getCampeonatoActual()
    {
        return CampeonatoDAL.getCampeonatoActual();
    }

    public static List<Resultado> getFixture(CampeonatoLiga camp)
    {
        List<Resultado> resultados = CampeonatoDAL.getResultadosCampeonato(camp, false);

        return resultados;
    }

    public static List<CampeonatoLiga> getCampeonatos()
    {
        return CampeonatoDAL.getCampeonatos();
    }

    public static List<FechaCampeonato> getFechasCampeonato(int idCampeonato)
    {
        return CampeonatoDAL.getFechasDeCampeonato(new CampeonatoLiga(idCampeonato));
    }

    public static List<FechaCampeonato> getFixtureCampeonato(int idCampeonato, int fecha)
    {
        List<FechaCampeonato> fixture = new List<FechaCampeonato>();
        if (fecha == -1)
        {
            fixture = CampeonatoDAL.getFechasDeCampeonato(new CampeonatoLiga(idCampeonato));
        }
        else
        {
            //fixture = CampeonatoDAL.getFixtureCampeonato_porFecha(new CampeonatoLiga(idCampeonato), fecha);
        }
        return fixture;
    }

    public static void registrarCampeonato(CampeonatoLiga camp)
    {
        try
        {
            CampeonatoDAL.insertarCampeonato(camp);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al registrar un nuevo campeonato." + e.Message);
        }
    }

    public static void updateCampeonato(CampeonatoLiga camp)
    {
        try
        {
            CampeonatoDAL.updateCampeonato(camp);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al actualizar el campeonato." + e.Message);
        }
    }

    public static void deleteCampeonato(string idCamp)
    {
        try
        {
            CampeonatoDAL.deleteCampeonato(idCamp);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al eliminar el campeonato." + e.Message);
        }
    }

    public static void registrarFechaCampeonato(FechaCampeonato fechaCamp)
    {
        try
        {
            CampeonatoDAL.insertarFecha(fechaCamp);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al registrar una fecha de campeonato." + e.Message);
        }
    }

    public static void updateFechaCampeonato(FechaCampeonato fechaCamp)
    {
        try
        {
            CampeonatoDAL.updateFecha(fechaCamp);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al actualizar la fecha de campeonato." + e.Message);
        }
    }

    public static void deleteFechaCampeonato(string idFecha)
    {
        try
        {
            CampeonatoDAL.deleteFecha(idFecha);
        }
        catch (SportingException spEx)
        {
            throw spEx;
        }
        catch (Exception e)
        {
            throw new SportingException("Error al eliminar la fecha de campeonato." + e.Message);
        }
    }
}