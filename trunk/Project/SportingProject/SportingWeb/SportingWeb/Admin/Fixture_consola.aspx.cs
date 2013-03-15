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
using System.Drawing;
using System.Collections.Generic;

namespace SportingWeb.Admin
{
    public partial class Fixture_consola : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarCampeonatos();
                cargarFechas(Convert.ToInt32(ddlCampeonato.SelectedValue));
                limpiarCampos();
            }
            else
            {
                setSuccessColorOutput(false);
            }
        }

        private void cargarCampeonatos()
        {
            try
            {
                DataTable dtCampeonatos = new DataTable();
                dtCampeonatos.Columns.Add("id");
                dtCampeonatos.Columns.Add("nombre");

                foreach (CampeonatoLiga camp in GestorCampeonato.getCampeonatos())
                {
                    DataRow row = dtCampeonatos.NewRow();
                    row["id"] = camp.IdCampeonato;
                    row["nombre"] = camp.Anio.ToString() + " - " + camp.Nombre;
                    dtCampeonatos.Rows.Add(row);
                }

                ddlCampeonato.DataSource = dtCampeonatos;
                ddlCampeonato.DataTextField = dtCampeonatos.Columns["nombre"].ToString();
                ddlCampeonato.DataValueField = dtCampeonatos.Columns["id"].ToString();
                ddlCampeonato.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = er.Message;
            }
        }

        private void cargarFechas(int idCamp)
        {
            try
            {
                DataTable dtFechas = new DataTable();
                dtFechas.Columns.Add("id");
                dtFechas.Columns.Add("nombre");

                DataRow row = dtFechas.NewRow();
                row["id"] = -1;
                row["nombre"] = "Todas...";
                dtFechas.Rows.Add(row);

                foreach (FechaCampeonato fechaCamp in GestorCampeonato.getFechasCampeonato(idCamp))
                {
                    row = dtFechas.NewRow();
                    row["id"] = fechaCamp.IdFecha;
                    row["nombre"] = "Fecha " + fechaCamp.Numero.ToString() + " - " + fechaCamp.Descripcion.ToString();
                    dtFechas.Rows.Add(row);
                }

                ddlFecha.DataSource = dtFechas;
                ddlFecha.DataTextField = dtFechas.Columns["nombre"].ToString();
                ddlFecha.DataValueField = dtFechas.Columns["id"].ToString();
                ddlFecha.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = er.Message;
            }
        }

        private void limpiarCampos()
        {
            //TODO: seleccionar los combos a sus valores iniciales (index "Todas" para cmb fechas).
        }

        private void setSuccessColorOutput(bool isSuccess)
        {
            if (isSuccess)
            {
                lblOutput.ForeColor = Color.Green;
            }
            else
            {
                lblOutput.ForeColor = Color.Red;
            }
        }

        protected void AgregarNuevoPartido(object sender, EventArgs e)
        {
            //Obtengo los valores del nuevo partido
            //string CustomerID = ((TextBox)GridView1.FooterRow.FindControl("txtCustomerID")).Text;
            //string Name = ((TextBox)GridView1.FooterRow.FindControl("txtContactName")).Text;
            //string Company = ((TextBox)GridView1.FooterRow.FindControl("txtCompany")).Text;

            //Guardo el nuevo partido en BD
            //TODO

            //Recargo la grilla
            //TODO
        }

        protected void EditPartido(object sender, GridViewEditEventArgs e)
        {
            grillaCampeonato.EditIndex = e.NewEditIndex;
            ddlFecha_SelectedIndexChanged(null, null);
        }

        protected void CancelarModificacion(object sender, GridViewCancelEditEventArgs e)
        {
            //GridView1.EditIndex = -1;
            //BindData();
            grillaCampeonato.EditIndex = -1;
            ddlFecha_SelectedIndexChanged(null, null);
        }

        protected void UpdatePartido(object sender, GridViewUpdateEventArgs e)
        {
            //Obtengo los valores del partido a modificar
            //string CustomerID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblCustomerID")).Text;
            //string Name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContactName")).Text;
            //string Company = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCompany")).Text;

            //Guardo el partido en BD
            //TODO

            //Recargo la grilla
            //TODO
        }

        protected void BorrarPartido(object sender, EventArgs e)
        {
            //Obtengo el id del partido a borrar
            LinkButton lnkRemove = (LinkButton)sender;
            String idPartido = lnkRemove.CommandArgument;

            //Borro el partido en BD
            //TODO

            //Recargo la grilla
            //TODO
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            //Pagination
            ////BindData();
            ////GridView1.PageIndex = e.NewPageIndex;
            ////GridView1.DataBind();
        }

        protected void ddlCampeonato_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
            cargarFechas(idCamp);
        }

        protected void ddlFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtengo los datos del campeonato y la fecha del mismo
            int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
            int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);

            //Traigo el fixture
            List<FechaCampeonato> fixture = GestorCampeonato.getFixtureCampeonato(idCamp, idFecha);
            
            //Defino las cabeceras de la grilla
            DataTable dtFixture = new DataTable();
            dtFixture.Columns.Add("idResultadoPartido");
            dtFixture.Columns.Add("fecha");
            dtFixture.Columns.Add("equipoLocal");
            dtFixture.Columns.Add("puntosLocal");
            dtFixture.Columns.Add("equipoVisitante");
            dtFixture.Columns.Add("puntosVisitante");

            //Cargo el fixture con los datos recuperados
            DataRow row;
            foreach (FechaCampeonato fecha in fixture)
            {
                foreach (Resultado resultado in fecha.Resultados)
	            {
                    row = dtFixture.NewRow();
                    row["idResultadoPartido"] = resultado.IdResultado;
                    row["fecha"] = "Fecha " + fecha.Numero + " " + fecha.Descripcion.ToString();
                    row["equipoLocal"] = resultado.EquipoLocal.Nombre+" ("+resultado.EquipoLocal.Localidad+")";
                    row["puntosLocal"] = resultado.EquipoLocalPuntos.ToString();
                    row["equipoVisitante"] = resultado.EquipoVisitante.Nombre + " (" + resultado.EquipoVisitante.Localidad + ")";
                    row["puntosVisitante"] = resultado.EquipoVisitantePuntos.ToString();
                    dtFixture.Rows.Add(row);
	            }
            }

            grillaCampeonato.DataSource = dtFixture;
            grillaCampeonato.DataBind();
        }

    }
}
