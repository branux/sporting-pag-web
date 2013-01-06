using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Equipo : System.Web.UI.Page
{
    protected String temporada;
    protected String fotoPlantel;
    protected String infoPlantel;
    protected String jugadores;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarPlantel();
        }
    }

    private void cargarPlantel()
    {
        Plantel plantel = GestorPlantel.getPlantelActual();
        temporada = "Plantel temporada "+plantel.Temporada;
        fotoPlantel = plantel.Foto.PathMedium;
        infoPlantel = plantel.Info;

        foreach (Jugador jugador in plantel.Jugadores)
        {
            jugadores += jugador.ToString() + ";";
        }
        this.jugadoresPlantel.Value = jugadores;
    } 
}

