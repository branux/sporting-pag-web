using System;
using System.Data;
using System.Collections.Generic;

public class GestorCampeonato
{
    public static TablaPosiciones getTablaPosiciones(CampeonatoLiga camp)
    {
        List<Resultado> resultados = CampeonatoDAL.getResultadosCampeonato(camp);
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
}