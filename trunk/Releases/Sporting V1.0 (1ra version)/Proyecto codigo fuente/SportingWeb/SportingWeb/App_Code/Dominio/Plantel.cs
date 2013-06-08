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

public class Plantel
{
    private int idPlantel;
    private String temporada;
    private Imagen foto;
    private String info;
    private List<Jugador> jugadores;

    public Plantel()
    {
    }

    public int IdPlantel
    {
        get { return idPlantel; }
        set { idPlantel = value; }
    }

    public String Temporada
    {
        get { return temporada; }
        set { temporada = value; }
    }

    public Imagen Foto
    {
        get { return foto; }
        set { foto = value; }
    }

    public String Info
    {
        get { return info; }
        set { info = value; }
    }

    public List<Jugador> Jugadores
    {
        get { return jugadores; }
        set { jugadores = value; }
    }
}