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
                //Cargo el fixture
                int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
                int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
                cargarFixture(idCamp, idFecha);
            }
            else
            {
                setSuccessColorOutput(false);
            }
        }

        protected void cargarCampeonatos()
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

        protected void cargarFechas(int idCamp)
        {
            try
            {
                DataTable dtFechas = getFechas_DataTable(idCamp);
                
                DataRow row = dtFechas.NewRow();
                row["id"] = -1;
                row["nombre"] = "Todas...";
                dtFechas.Rows.Add(row);

                ddlFecha.DataSource = dtFechas;
                ddlFecha.DataTextField = dtFechas.Columns["nombre"].ToString();
                ddlFecha.DataValueField = dtFechas.Columns["id"].ToString();
                ddlFecha.DataBind();

                //Selecciono por defecto la fecha "Todas..."
                ddlFecha.SelectedIndex = ddlFecha.Items.Count - 1;
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = er.Message;
            }
        }

        private DataTable getFechas_DataTable(int idCamp)
        {
            DataTable dtFechas = new DataTable();
            dtFechas.Columns.Add("id");
            dtFechas.Columns.Add("nombre");

            DataRow row;
            foreach (FechaCampeonato fechaCamp in GestorCampeonato.getFechasCampeonato(idCamp))
            {
                row = dtFechas.NewRow();
                row["id"] = fechaCamp.IdFecha;
                row["nombre"] = "Fecha " + fechaCamp.Numero.ToString();
                dtFechas.Rows.Add(row);
            }

            return dtFechas;
        }

        protected void cargarFixture(int idCamp, int idFecha)
        {
            //Traigo el fixture
            List<FechaCampeonato> fixture = GestorCampeonato.getFixtureCampeonato(idCamp, idFecha);

            //Defino las cabeceras de la grilla
            DataTable dtFixture = new DataTable();
            dtFixture.Columns.Add("idResultadoPartido");
            dtFixture.Columns.Add("fecha");
            dtFixture.Columns.Add("fechaPartido");
            dtFixture.Columns.Add("equipoLocal");
            dtFixture.Columns.Add("puntosLocal");
            dtFixture.Columns.Add("equipoVisitante");
            dtFixture.Columns.Add("puntosVisitante");
            dtFixture.Columns.Add("idFechaGrilla");
            dtFixture.Columns.Add("idLocalGrilla");
            dtFixture.Columns.Add("idVisitanteGrilla");

            //Cargo el fixture con los datos recuperados
            DataRow row;
            foreach (FechaCampeonato fecha in fixture)
            {
                foreach (Resultado resultado in fecha.Resultados)
                {
                    row = dtFixture.NewRow();
                    row["idResultadoPartido"] = resultado.IdResultado;
                    row["fecha"] = "Fecha " + fecha.Numero;
                    row["fechaPartido"] = resultado.FechaPartido.ToString("dd/MM/yyyy");
                    row["equipoLocal"] = resultado.EquipoLocal.Nombre + " (" + resultado.EquipoLocal.Localidad + ")";
                    row["puntosLocal"] = resultado.EquipoLocalPuntos.ToString();
                    row["equipoVisitante"] = resultado.EquipoVisitante.Nombre + " (" + resultado.EquipoVisitante.Localidad + ")";
                    row["puntosVisitante"] = resultado.EquipoVisitantePuntos.ToString();
                    row["idFechaGrilla"] = fecha.IdFecha;
                    row["idLocalGrilla"] = resultado.EquipoLocal.IdEquipo;
                    row["idVisitanteGrilla"] = resultado.EquipoVisitante.IdEquipo;

                    dtFixture.Rows.Add(row);
                }
            }

            grillaCampeonato.DataSource = dtFixture;
            grillaCampeonato.DataBind();
        }

        protected void limpiarCampos()
        {
            //TODO: seleccionar los combos a sus valores iniciales (index "Todas" para cmb fechas).
        }

        protected void setSuccessColorOutput(bool isSuccess)
        {
            if (isSuccess)
            {
                lblOutput.ForeColor = Color.Green;
                lblOutputFixture.ForeColor = Color.Green;
            }
            else
            {
                lblOutput.ForeColor = Color.Red;
                lblOutputFixture.ForeColor = Color.Red;
            }
        }

        protected void AgregarPartido(object sender, EventArgs e)
        {
            FechaCampeonato fechaFixture = new FechaCampeonato();
            Resultado resultadoPartido = new Resultado();
            try
            {
                //Obtengo los valores de la nueva fecha del fixture
                if (((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlFechaGrilla")).SelectedValue == "")
                {
                    throw new SportingException("El campo Fecha Nro es requerido. Seleccione una fecha.");
                }
                fechaFixture.IdFecha = Convert.ToInt32(((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlFechaGrilla")).SelectedValue);
                if (((TextBox)grillaCampeonato.FooterRow.FindControl("txtFechaPartidoFooter")).Text == "")
                {
                    throw new SportingException("El campo Fecha partido es requerido. Seleccione una fecha");
                }
                try
                {
                    resultadoPartido.FechaPartido = Convert.ToDateTime(((TextBox)grillaCampeonato.FooterRow.FindControl("txtFechaPartidoFooter")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Fecha incorrecta. Ingrese una fecha válida.");
                }
                if (((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlLocalGrilla")).SelectedValue == "")
                {
                    throw new SportingException("El campo Local es requerido. Seleccione un equipo Local.");
                }
                resultadoPartido.EquipoLocal = new EquipoCampeonato();
                resultadoPartido.EquipoLocal.IdEquipo = Convert.ToInt32(((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlLocalGrilla")).SelectedValue);
                if (((TextBox)grillaCampeonato.FooterRow.FindControl("txtPuntosLocal")).Text == "")
                {
                    throw new SportingException("El campo Puntos Local es requerido. Si el partido no ha sido jugado ingrese cero (0)");
                }
                try
                {
                    resultadoPartido.EquipoLocalPuntos = Convert.ToInt32(((TextBox)grillaCampeonato.FooterRow.FindControl("txtPuntosLocal")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Puntos Local incorrecto. Ingrese solo números.");
                }
                if (((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlVisitanteGrilla")).SelectedValue == "")
                {
                    throw new SportingException("El campo Visitante es requerido. Seleccione un equipo Visitante.");
                }
                resultadoPartido.EquipoVisitante = new EquipoCampeonato();
                resultadoPartido.EquipoVisitante.IdEquipo = Convert.ToInt32(((DropDownList)grillaCampeonato.FooterRow.FindControl("ddlVisitanteGrilla")).SelectedValue);
                if (((TextBox)grillaCampeonato.FooterRow.FindControl("txtPuntosVisitante")).Text == "")
                {
                    throw new SportingException("El campo Puntos Visitante es requerido. Si el partido no ha sido jugado ingrese cero (0)");
                }
                try
                {
                    resultadoPartido.EquipoVisitantePuntos = Convert.ToInt32(((TextBox)grillaCampeonato.FooterRow.FindControl("txtPuntosVisitante")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Puntos Visitante incorrecto. Ingrese solo números.");
                }

                fechaFixture.Resultados = new List<Resultado>();
                fechaFixture.Resultados.Add(resultadoPartido);

                //Guardo el nuevo partido del fixture
                GestorCampeonato.registrarPartidoFixture(fechaFixture);
                setSuccessColorOutput(true);
                lblOutputFixture.Text = "Partido registrado con éxito!";

                //Recargo la grilla
                //Cargo el fixture
                int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
                int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
                cargarFixture(idCamp, idFecha);
                grillaCampeonato.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = ex.Message;
            }
        }

        protected void EditFixture(object sender, GridViewEditEventArgs e)
        {
            grillaCampeonato.EditIndex = e.NewEditIndex;
            //Cargo el fixture
            int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
            int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
            cargarFixture(idCamp, idFecha);

            //Pongo foco en la fecha del partido que se esta editando
            ((DropDownList)grillaCampeonato.Rows[e.NewEditIndex].FindControl("ddlFechaGrilla_edit")).Focus();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFixture.Text = "";
        }

        protected void CancelEditFixture(object sender, GridViewCancelEditEventArgs e)
        {
            grillaCampeonato.EditIndex = -1;
            //Cargo el fixture
            int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
            int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
            cargarFixture(idCamp, idFecha);
        }

        protected void UpdateFixture(object sender, GridViewUpdateEventArgs e)
        {
            FechaCampeonato fechaFixture = new FechaCampeonato();
            Resultado resultadoPartido = new Resultado();
            try
            {
                //Obtengo los valores de la fecha del fixture a modificar
                if (((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlFechaGrilla_edit")).SelectedValue == "")
                {
                    throw new SportingException("El campo Fecha Nro es requerido. Seleccione una fecha.");
                }
                fechaFixture.IdFecha = Convert.ToInt32(((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlFechaGrilla_edit")).SelectedValue);
                if (((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtFechaPartido")).Text == "")
                {
                    throw new SportingException("El campo Fecha partido es requerido. Seleccione una fecha");
                }
                try
                {
                    resultadoPartido.FechaPartido = Convert.ToDateTime(((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtFechaPartido")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Fecha incorrecta. Ingrese una fecha válida.");
                }
                if (((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlLocalGrilla_edit")).SelectedValue == "")
                {
                    throw new SportingException("El campo Local es requerido. Seleccione un equipo Local.");
                }
                resultadoPartido.EquipoLocal = new EquipoCampeonato();
                resultadoPartido.EquipoLocal.IdEquipo = Convert.ToInt32(((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlLocalGrilla_edit")).SelectedValue);
                if (((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtPuntosLocal")).Text == "")
                {
                    throw new SportingException("El campo Puntos Local es requerido. Si el partido no ha sido jugado ingrese cero (0)");
                }
                try
                {
                    resultadoPartido.EquipoLocalPuntos = Convert.ToInt32(((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtPuntosLocal")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Puntos Local incorrecto. Ingrese solo números.");
                }
                if (((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlVisitanteGrilla_edit")).SelectedValue == "")
                {
                    throw new SportingException("El campo Visitante es requerido. Seleccione un equipo Visitante.");
                }
                resultadoPartido.EquipoVisitante = new EquipoCampeonato();
                resultadoPartido.EquipoVisitante.IdEquipo = Convert.ToInt32(((DropDownList)grillaCampeonato.Rows[e.RowIndex].FindControl("ddlVisitanteGrilla_edit")).SelectedValue);
                if (((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtPuntosVisitante")).Text == "")
                {
                    throw new SportingException("El campo Puntos Visitante es requerido. Si el partido no ha sido jugado ingrese cero (0)");
                }
                try
                {
                    resultadoPartido.EquipoVisitantePuntos = Convert.ToInt32(((TextBox)grillaCampeonato.Rows[e.RowIndex].FindControl("txtPuntosVisitante")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Puntos Visitante incorrecto. Ingrese solo números.");
                }
                resultadoPartido.IdResultado = Convert.ToInt32(((Label)grillaCampeonato.Rows[e.RowIndex].FindControl("lblIdResultadoPartido")).Text);
                fechaFixture.Resultados = new List<Resultado>();
                fechaFixture.Resultados.Add(resultadoPartido);

                //Modifico el partido del fixture existente
                GestorCampeonato.updatePartidoFixture(fechaFixture);
                setSuccessColorOutput(true);
                lblOutputFixture.Text = "Partido actualizado con éxito!";

                //Recargo la grilla de fixture
                grillaCampeonato.EditIndex = -1;
                int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
                int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
                cargarFixture(idCamp, idFecha);
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = ex.Message;
            }
        }

        protected void BorrarPartido(object sender, EventArgs e)
        {
            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFixture.Text = "";

            try
            {
                //Obtengo el id del partido a borrar
                LinkButton lnkRemove = (LinkButton)sender;
                String idResultadoPartido = lnkRemove.CommandArgument;

                //Borro el partido del fixture
                GestorCampeonato.deletePartidoFixture(idResultadoPartido);

                setSuccessColorOutput(true);
                lblOutputFixture.Text = "El partido fue eliminado con éxito";

                //Recargo la grilla de fixture
                int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
                int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
                cargarFixture(idCamp, idFecha);
                grillaCampeonato.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = ex.Message;
            }
        }

        protected void OnPagingFixture(object sender, GridViewPageEventArgs e)
        {
            //Pagination
            //Cargo el fixture
            int idCamp = Convert.ToInt32(ddlCampeonato.SelectedValue);
            int idFecha = Convert.ToInt32(ddlFecha.SelectedValue);
            cargarFixture(idCamp, idFecha);
            grillaCampeonato.PageIndex = e.NewPageIndex;
            grillaCampeonato.DataBind();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFixture.Text = "";
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

            cargarFixture(idCamp, idFecha);
        }

        protected void grillaCampeonato_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FECHAS
                DropDownList ddlFechaGrillaEdit = (DropDownList)e.Row.FindControl("ddlFechaGrilla_edit");
                if (ddlFechaGrillaEdit != null)
                {
                    DataTable dtFechas = getFechas_DataTable(Convert.ToInt32(ddlCampeonato.SelectedValue));
                    ddlFechaGrillaEdit.DataSource = dtFechas;
                    ddlFechaGrillaEdit.DataTextField = dtFechas.Columns["nombre"].ToString();
                    ddlFechaGrillaEdit.DataValueField = dtFechas.Columns["id"].ToString();
                    ddlFechaGrillaEdit.DataBind();

                    //el label lblIdFechaGrilla se agrega solo para guardar el id de la fecha y poder seleccionarla cuando se edita la fila
                    Label lblIdFechaGrilla = ((Label)e.Row.FindControl("lblIdFechaGrilla"));
                    ddlFechaGrillaEdit.Items.FindByValue(lblIdFechaGrilla.Text).Selected = true;
                }

                DataTable dtEquipos = getEquipos_DataTable();
                //EQUIPO LOCAL
                DropDownList ddlLocalGrillaEdit = (DropDownList)e.Row.FindControl("ddlLocalGrilla_edit");
                if (ddlLocalGrillaEdit != null)
                {
                    ddlLocalGrillaEdit.DataSource = dtEquipos;
                    ddlLocalGrillaEdit.DataTextField = dtEquipos.Columns["nombre"].ToString();
                    ddlLocalGrillaEdit.DataValueField = dtEquipos.Columns["idEquipo"].ToString();
                    ddlLocalGrillaEdit.DataBind();

                    //el label lblIdLocalGrilla se agrega solo para guardar el id del equipo Local y poder seleccionarlo cuando se edita la fila
                    Label lblIdLocalGrilla = ((Label)e.Row.FindControl("lblIdLocalGrilla"));
                    ddlLocalGrillaEdit.Items.FindByValue(lblIdLocalGrilla.Text).Selected = true;
                }

                //EQUIPO VISITANTE
                DropDownList ddlVisitanteGrillaEdit = (DropDownList)e.Row.FindControl("ddlVisitanteGrilla_edit");
                if (ddlVisitanteGrillaEdit != null)
                {
                    ddlVisitanteGrillaEdit.DataSource = dtEquipos;
                    ddlVisitanteGrillaEdit.DataTextField = dtEquipos.Columns["nombre"].ToString();
                    ddlVisitanteGrillaEdit.DataValueField = dtEquipos.Columns["idEquipo"].ToString();
                    ddlVisitanteGrillaEdit.DataBind();

                    //el label lblIdVisitanteGrilla se agrega solo para guardar el id del equipo Visitante y poder seleccionarlo cuando se edita la fila
                    Label lblIdVisitanteGrilla = ((Label)e.Row.FindControl("lblIdVisitanteGrilla"));
                    ddlVisitanteGrillaEdit.Items.FindByValue(lblIdVisitanteGrilla.Text).Selected = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //FECHAS
                DropDownList ddlFechaGrilla = (DropDownList)e.Row.FindControl("ddlFechaGrilla");
                DataTable dtFechas = getFechas_DataTable(Convert.ToInt32(ddlCampeonato.SelectedValue));
                ddlFechaGrilla.DataSource = dtFechas;
                ddlFechaGrilla.DataTextField = dtFechas.Columns["nombre"].ToString();
                ddlFechaGrilla.DataValueField = dtFechas.Columns["id"].ToString();
                ddlFechaGrilla.DataBind();

                DataTable dtEquipos = getEquipos_DataTable();
                //EQUIPO LOCAL
                DropDownList ddlLocalGrilla = (DropDownList)e.Row.FindControl("ddlLocalGrilla");
                ddlLocalGrilla.DataSource = dtEquipos;
                ddlLocalGrilla.DataTextField = dtEquipos.Columns["nombre"].ToString();
                ddlLocalGrilla.DataValueField = dtEquipos.Columns["idEquipo"].ToString();
                ddlLocalGrilla.DataBind();

                //EQUIPO VISITANTE
                DropDownList ddlVisitanteGrilla = (DropDownList)e.Row.FindControl("ddlVisitanteGrilla");
                ddlVisitanteGrilla.DataSource = dtEquipos;
                ddlVisitanteGrilla.DataTextField = dtEquipos.Columns["nombre"].ToString();
                ddlVisitanteGrilla.DataValueField = dtEquipos.Columns["idEquipo"].ToString();
                ddlVisitanteGrilla.DataBind();

            }
        }

        private DataTable getEquipos_DataTable()
        {
            DataTable dtEquipos = null;
            try
            {
                dtEquipos = new DataTable();
                dtEquipos.Columns.Add("idEquipo");
                dtEquipos.Columns.Add("nombre");
                dtEquipos.Columns.Add("localidad");

                foreach (EquipoCampeonato equipo in GestorCampeonato.getEquipos())
                {
                    DataRow row = dtEquipos.NewRow();
                    row["idEquipo"] = equipo.IdEquipo;
                    row["nombre"] = equipo.Nombre + " (" + equipo.Localidad + ")";
                    dtEquipos.Rows.Add(row);
                }
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutputFixture.Text = er.Message;
            }
            return dtEquipos;
        }
    }
}
