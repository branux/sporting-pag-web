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
using System.Collections.Generic;

public partial class Campeonato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            currentPage.Value = "Campeonato";
            cargarTablaPosiciones();
            formatearTablaPosiciones();
        }
    }

    private void formatearTablaPosiciones()
    {
        gridTablaPosiciones.CellPadding = 10;
        gridTablaPosiciones.BorderWidth = 0;
    }

    protected void cargarTablaPosiciones()
    {
        try
        {
            DataTable dtTablaPosicion = new DataTable();
            dtTablaPosicion.Columns.Add("equipo");
            dtTablaPosicion.Columns[0].ColumnName = "Equipo";
            dtTablaPosicion.Columns.Add("pj");
            dtTablaPosicion.Columns[1].ColumnName = "PJ";
            dtTablaPosicion.Columns.Add("pg");
            dtTablaPosicion.Columns[2].ColumnName = "PG";
            dtTablaPosicion.Columns.Add("pp");
            dtTablaPosicion.Columns[3].ColumnName = "PP";
            dtTablaPosicion.Columns.Add("pts");
            dtTablaPosicion.Columns[4].ColumnName = "PTS";

            CampeonatoLiga campeonatoactual = GestorCampeonato.getCampeonatoActual();
            nombreCampeonato.Value = campeonatoactual.Nombre;
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

