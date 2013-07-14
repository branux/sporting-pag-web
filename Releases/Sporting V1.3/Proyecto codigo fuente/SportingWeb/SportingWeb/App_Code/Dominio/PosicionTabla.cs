using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public class PosicionTabla : IComparable
{
    private EquipoCampeonato equipo;
    private int puntos;
    private int partidosJugados;
    private int partidosGanados;
    private int partidosPerdidos;

    public EquipoCampeonato Equipo
    {
        get { return equipo; }
        set { equipo = value; }
    }
    
    public int Puntos
    {
        get { return puntos; }
        set { puntos = value; }
    }
    
    public int PartidosJugados
    {
        get { return partidosJugados; }
        set { partidosJugados = value; }
    }
    
    public int PartidosGanados
    {
        get { return partidosGanados; }
        set { partidosGanados = value; }
    }

    public int PartidosPerdidos
    {
        get { return partidosPerdidos; }
        set { partidosPerdidos = value; }
    }

    /// <summary>
    /// el compare to es por el id del equipo que ocupa esta posicion
    /// ya  que no puede aparecer el mismo equipo mas de una vez en la tabla
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object obj)
    {
        if (obj != null)
        {
            PosicionTabla posicion = (PosicionTabla)obj;
            return posicion.Equipo.IdEquipo - this.Equipo.IdEquipo;
        }
        else
        {
            return -1;
        }
    }

    public override bool Equals(Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        PosicionTabla pos = (PosicionTabla)obj;
        return this.Equipo.IdEquipo == pos.Equipo.IdEquipo;
    }
}