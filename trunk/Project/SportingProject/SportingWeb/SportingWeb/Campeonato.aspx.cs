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

public partial class Campeonato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            cargarTablaPosiciones();
        }
    }

    protected void cargarTablaPosiciones()
    {
        try
        {
            DataTable dtTablaPosicion = new DataTable();
            dtTablaPosicion.Columns.Add("equipo");
            dtTablaPosicion.Columns.Add("pj");
            dtTablaPosicion.Columns.Add("pg");
            dtTablaPosicion.Columns.Add("pp");
            dtTablaPosicion.Columns.Add("pts");

            CampeonatoLiga campeonatoactual = GestorCampeonato.getCampeonatoActual();
            TablaPosiciones tablaActual = GestorCampeonato.getTablaPosiciones(campeonatoactual);
            foreach (PosicionTabla pos in tablaActual.Posiciones)
            {
                DataRow row = dtTablaPosicion.NewRow();
                row["equipo"] = pos.Equipo.Nombre;
                row["pj"] = pos.PartidosJugados;
                row["pg"] = pos.PartidosGanados;
                row["pp"] = pos.PartidosPerdidos;
                row["pts"] = pos.Puntos;
                dtTablaPosicion.Rows.Add(row);
            }
            gridTablaPosiciones.DataSource = dtTablaPosicion;
            gridTablaPosiciones.DataBind();                    
        }
        catch (Exception er)
        {
            //lblOutput.Text = "Error al cargar tabla de posiciones " + er.Message;
            throw er;
        }
    }
}

