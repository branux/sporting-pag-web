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

public class TablaPosiciones
{
    private List<PosicionTabla> posiciones;

    public List<PosicionTabla> Posiciones
    {
        get { return posiciones; }
        set { posiciones = value; }
    }

    public Comparison<PosicionTabla> ComparadorPosicion
    {
        get { return comparadorPosicion; }
        set { }
    }

    public static Comparison<PosicionTabla> comparadorPosicion = delegate(PosicionTabla p1, PosicionTabla p2)
    {
        int retornoComparador =  p2.Puntos.CompareTo(p1.Puntos);
        if (retornoComparador == 0)
        {
            //comparo por partidos ganados
            retornoComparador = p2.PartidosGanados.CompareTo(p1.PartidosGanados);

            if (retornoComparador == 0)
            {
                //comparo por nombre del equipo
                retornoComparador = p1.Equipo.Nombre.CompareTo(p2.Equipo.Nombre);
            }
        }

        return retornoComparador;
    };
}