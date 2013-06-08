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

namespace SportingWeb.Admin
{
    public partial class Campeonato_consola : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarCampeonatos();
                cargarFechas();
                cargarEquipos();
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
                dtCampeonatos.Columns.Add("idCamp");
                dtCampeonatos.Columns.Add("nombre");
                dtCampeonatos.Columns.Add("anio");

                foreach (CampeonatoLiga camp in GestorCampeonato.getCampeonatos())
                {
                    DataRow row = dtCampeonatos.NewRow();
                    row["idCamp"] = camp.IdCampeonato;
                    row["nombre"] = camp.Nombre;
                    row["anio"] = camp.Anio.ToString();
                    dtCampeonatos.Rows.Add(row);
                }

                grillaCampeonatos.DataSource = dtCampeonatos;
                grillaCampeonatos.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = er.Message;
            }
        }

        private void cargarFechas()
        {
            try
            {
                DataTable dtFechas = new DataTable();
                dtFechas.Columns.Add("idFecha");
                dtFechas.Columns.Add("campeonato");
                dtFechas.Columns.Add("idCamp");
                dtFechas.Columns.Add("numeroFecha");
                dtFechas.Columns.Add("descripcion");

                foreach (CampeonatoLiga camp in GestorCampeonato.getCampeonatos())
                {
                    foreach (FechaCampeonato fecha in camp.ListaFechas)
                    {
                        DataRow row = dtFechas.NewRow();
                        row["campeonato"] = camp.Anio.ToString() + " - " + camp.Nombre;
                        row["idCamp"] = camp.IdCampeonato.ToString();
                        row["idFecha"] = fecha.IdFecha.ToString();
                        row["numeroFecha"] = fecha.Numero.ToString();
                        row["descripcion"] = fecha.Descripcion;
                        dtFechas.Rows.Add(row);
                    }
                }

                grillaFechas.DataSource = dtFechas;
                grillaFechas.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = er.Message;
            }
        }

        private DataTable getComboCampeonatos()
        {
            DataTable dtCampeonatos = new DataTable();
            try
            {
                dtCampeonatos.Columns.Add("id");
                dtCampeonatos.Columns.Add("nombre");

                foreach (CampeonatoLiga camp in GestorCampeonato.getCampeonatos())
                {
                    DataRow row = dtCampeonatos.NewRow();
                    row["id"] = camp.IdCampeonato;
                    row["nombre"] = camp.Anio.ToString() + " - " + camp.Nombre;
                    dtCampeonatos.Rows.Add(row);
                }
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = er.Message;
            }
            return dtCampeonatos;
        }

        private void cargarEquipos()
        {
            try
            {
                DataTable dtEquipos = new DataTable();
                dtEquipos.Columns.Add("idEquipo");
                dtEquipos.Columns.Add("nombre");
                dtEquipos.Columns.Add("localidad");

                foreach (EquipoCampeonato equipo in GestorCampeonato.getEquipos())
                {
                    DataRow row = dtEquipos.NewRow();
                    row["idEquipo"] = equipo.IdEquipo;
                    row["nombre"] = equipo.Nombre;
                    row["localidad"] = equipo.Localidad;
                    dtEquipos.Rows.Add(row);
                }

                grillaEquipos.DataSource = dtEquipos;
                grillaEquipos.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = er.Message;
            }
        }

        private void setSuccessColorOutput(bool isSuccess)
        {
            if (isSuccess)
            {
                lblOutputCamp.ForeColor = Color.Green;
                lblOutputFecha.ForeColor = Color.Green;
                lblOutputEquipo.ForeColor = Color.Green;
            }
            else
            {
                lblOutputCamp.ForeColor = Color.Red;
                lblOutputFecha.ForeColor = Color.Red;
                lblOutputEquipo.ForeColor = Color.Red;
            }
        }

        protected void AddCampeonato(object sender, EventArgs e)
        {
            CampeonatoLiga campeonato = new CampeonatoLiga();

            try
            {
                //Obtengo los valores del nuevo campeoanto
                if (((TextBox)grillaCampeonatos.FooterRow.FindControl("txtNombre")).Text == "")
                {
                    throw new SportingException("Campeonato requerido.");
                }
                campeonato.Nombre = ((TextBox)grillaCampeonatos.FooterRow.FindControl("txtNombre")).Text;
                if (((TextBox)grillaCampeonatos.FooterRow.FindControl("txtAnio")).Text == "")
                {
                    throw new SportingException("Año requerido.");
                }
                try
                {
                    campeonato.Anio = Convert.ToInt32(((TextBox)grillaCampeonatos.FooterRow.FindControl("txtAnio")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Año incorrecto. Ingrese solo números.");
                }

                //Guardo el nuevo campeonato
                GestorCampeonato.registrarCampeonato(campeonato);
                setSuccessColorOutput(true);
                lblOutputCamp.Text = "Campeonato registrado con éxito!";

                //Recargo la grilla
                cargarCampeonatos();
                cargarFechas();
                grillaCampeonatos.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = ex.Message;
            }
        }

        protected void EditCampeonato(object sender, GridViewEditEventArgs e)
        {
            grillaCampeonatos.EditIndex = e.NewEditIndex;
            cargarCampeonatos();
            
            //Pongo foco en el nombre del Campeonato
            ((TextBox)grillaCampeonatos.Rows[e.NewEditIndex].FindControl("txtNombre")).Focus();
            
            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputCamp.Text = "";
        }

        protected void CancelarCampeonato(object sender, GridViewCancelEditEventArgs e)
        {
            grillaCampeonatos.EditIndex = -1;
            cargarCampeonatos();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputCamp.Text = "";
        }

        protected void UpdateCampeonato(object sender, GridViewUpdateEventArgs e)
        {
            CampeonatoLiga campeonato = new CampeonatoLiga();

            try
            {
                //Obtengo los valores del campeoanto
                if (((TextBox)grillaCampeonatos.Rows[e.RowIndex].FindControl("txtNombre")).Text == "")
                {
                    throw new SportingException("Campeonato requerido.");
                }
                campeonato.Nombre = ((TextBox)grillaCampeonatos.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                if (((TextBox)grillaCampeonatos.Rows[e.RowIndex].FindControl("txtAnio")).Text == "")
                {
                    throw new SportingException("Anio requerido.");
                }
                try
                {
                    campeonato.Anio = Convert.ToInt32(((TextBox)grillaCampeonatos.Rows[e.RowIndex].FindControl("txtAnio")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Año incorrecto. Ingrese solo números.");
                }
                campeonato.IdCampeonato = Convert.ToInt32(((Label)grillaCampeonatos.Rows[e.RowIndex].FindControl("lblIdCampeonato")).Text);

                //Modifico un campeonato existente
                GestorCampeonato.updateCampeonato(campeonato);
                setSuccessColorOutput(true);
                lblOutputCamp.Text = "Campeonato actualizado con éxito!";

                //Recargo la grilla
                grillaCampeonatos.EditIndex = -1;
                cargarCampeonatos();
                cargarFechas();
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = ex.Message;
            }
        }

        protected void BorrarCampeonato(object sender, EventArgs e)
        {
            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputCamp.Text = "";

            try
            {
                //Obtengo el id del campeonato a borrar
                LinkButton lnkRemove = (LinkButton)sender;
                String idCamp = lnkRemove.CommandArgument;

                //Borro el campeonato en BD
                GestorCampeonato.deleteCampeonato(idCamp);
                
                setSuccessColorOutput(true);
                lblOutputCamp.Text = "El campeonato fue eliminado con éxito";

                //Recargo la grilla
                cargarCampeonatos();
                cargarFechas();
                grillaCampeonatos.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputCamp.Text = ex.Message;
            }
        }

        protected void OnPagingCampeonato(object sender, GridViewPageEventArgs e)
        {
            //Pagination
            cargarCampeonatos();
            grillaCampeonatos.PageIndex = e.NewPageIndex;
            grillaCampeonatos.DataBind();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputCamp.Text = "";
        }

        protected void grillaFechas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCampeonatoEdit = (DropDownList)e.Row.FindControl("ddlCampeonato_edit");

                if (ddlCampeonatoEdit != null)
                {
                    DataTable dtCampeonatos = getComboCampeonatos();
                    ddlCampeonatoEdit.DataSource = dtCampeonatos;
                    ddlCampeonatoEdit.DataTextField = dtCampeonatos.Columns["nombre"].ToString();
                    ddlCampeonatoEdit.DataValueField = dtCampeonatos.Columns["id"].ToString();
                    ddlCampeonatoEdit.DataBind();

                    //el label lblIdCamp se agrega solo para guardar el id del camp y poder seleccionarlo cuando se edita la fila
                    Label lblIdCamp = ((Label)e.Row.FindControl("lblIdCamp"));
                    ddlCampeonatoEdit.Items.FindByValue(lblIdCamp.Text).Selected = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlCampeonato = (DropDownList)e.Row.FindControl("ddlCampeonato");
                DataTable dtCampeonatos = getComboCampeonatos();
                ddlCampeonato.DataSource = dtCampeonatos;
                ddlCampeonato.DataTextField = dtCampeonatos.Columns["nombre"].ToString();
                ddlCampeonato.DataValueField = dtCampeonatos.Columns["id"].ToString();
                ddlCampeonato.DataBind();
            }
        } 

        protected void AddFecha(object sender, EventArgs e)
        {
            FechaCampeonato fechaCamp = new FechaCampeonato();
            try
            {
                //Obtengo los valores de la nueva fecha de campeoanto
                if (((DropDownList)grillaFechas.FooterRow.FindControl("ddlCampeonato")).SelectedValue == "")
                {
                    throw new SportingException("Campeonato requerido. Seleccione un campeonato.");
                }
                fechaCamp.IdCampeonato = Convert.ToInt32(((DropDownList)grillaFechas.FooterRow.FindControl("ddlCampeonato")).SelectedValue);
                if (((TextBox)grillaFechas.FooterRow.FindControl("txtNumeroFecha")).Text == "")
                {
                    throw new SportingException("Número de fecha requerido.");
                }
                try
                {
                    fechaCamp.Numero = Convert.ToInt32(((TextBox)grillaFechas.FooterRow.FindControl("txtNumeroFecha")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Número de fecha incorrecto. Ingrese solo números.");
                }
                fechaCamp.Descripcion = ((TextBox)grillaFechas.FooterRow.FindControl("txtDesc")).Text;

                //Guardo la nueva fecha de campeonato
                GestorCampeonato.registrarFechaCampeonato(fechaCamp);
                setSuccessColorOutput(true);
                lblOutputFecha.Text = "Fecha registrada con éxito!";

                //Recargo la grilla
                cargarFechas();
                grillaFechas.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = ex.Message;
            }
        }

        protected void EditFecha(object sender, GridViewEditEventArgs e)
        {
            grillaFechas.EditIndex = e.NewEditIndex;
            cargarFechas();

            //Pongo foco en el numero de la fecha
            ((TextBox)grillaFechas.Rows[e.NewEditIndex].FindControl("txtNumeroFecha")).Focus();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFecha.Text = "";
        }

        protected void CancelarFecha(object sender, GridViewCancelEditEventArgs e)
        {
            grillaFechas.EditIndex = -1;
            cargarFechas();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFecha.Text = "";
        }

        protected void UpdateFecha(object sender, GridViewUpdateEventArgs e)
        {
            FechaCampeonato fechaCamp = new FechaCampeonato();
            try
            {
                //Obtengo los valores de la fecha de campeoanto
                if (((DropDownList)grillaFechas.Rows[e.RowIndex].FindControl("ddlCampeonato_edit")).SelectedValue == "")
                {
                    throw new SportingException("Campeonato requerido. Seleccione un campeonato.");
                }
                fechaCamp.IdCampeonato = Convert.ToInt32(((DropDownList)grillaFechas.Rows[e.RowIndex].FindControl("ddlCampeonato_edit")).SelectedValue);
                if (((TextBox)grillaFechas.Rows[e.RowIndex].FindControl("txtNumeroFecha")).Text == "")
                {
                    throw new SportingException("Número de fecha requerido.");
                }
                try
                {
                    fechaCamp.Numero = Convert.ToInt32(((TextBox)grillaFechas.Rows[e.RowIndex].FindControl("txtNumeroFecha")).Text);
                }
                catch (Exception ex)
                {
                    throw new SportingException("Número de fecha incorrecto. Ingrese solo números.");
                }
                fechaCamp.Descripcion = ((TextBox)grillaFechas.Rows[e.RowIndex].FindControl("txtDesc")).Text;
                fechaCamp.IdFecha = Convert.ToInt32(((Label)grillaFechas.Rows[e.RowIndex].FindControl("lblIdFecha")).Text);

                //Modifico una fecha de campeonato existente
                GestorCampeonato.updateFechaCampeonato(fechaCamp);
                setSuccessColorOutput(true);
                lblOutputFecha.Text = "Fecha actualizada con éxito!";

                //Recargo la grilla
                grillaFechas.EditIndex = -1;
                cargarFechas();
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = ex.Message;
            }
        }

        protected void BorrarFecha(object sender, EventArgs e)
        {
            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFecha.Text = "";

            try
            {
                //Obtengo el id de la fecha a borrar
                LinkButton lnkRemoveFecha = (LinkButton)sender;
                String idFecha = lnkRemoveFecha.CommandArgument;

                //Borro la fecha del campeonato en BD
                GestorCampeonato.deleteFechaCampeonato(idFecha);

                setSuccessColorOutput(true);
                lblOutputFecha.Text = "La fecha fue eliminada con éxito";

                //Recargo la grilla
                cargarFechas();
                grillaFechas.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputFecha.Text = ex.Message;
            }
        }

        protected void OnPagingFechas(object sender, GridViewPageEventArgs e)
        {
            //Pagination
            cargarFechas();
            grillaFechas.PageIndex = e.NewPageIndex;
            grillaFechas.DataBind();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputFecha.Text = "";
        }

        protected void AddEquipo(object sender, EventArgs e)
        {
            EquipoCampeonato equipo = new EquipoCampeonato();

            try
            {
                //Obtengo los valores del nuevo equipo
                if (((TextBox)grillaEquipos.FooterRow.FindControl("txtNombre")).Text == "")
                {
                    throw new SportingException("Equipo requerido.");
                }
                equipo.Nombre = ((TextBox)grillaEquipos.FooterRow.FindControl("txtNombre")).Text;
                if (((TextBox)grillaEquipos.FooterRow.FindControl("txtLocalidad")).Text == "")
                {
                    throw new SportingException("Localidad requerida.");
                }
                equipo.Localidad = ((TextBox)grillaEquipos.FooterRow.FindControl("txtLocalidad")).Text;
                
                //Guardo el nuevo equipo
                GestorCampeonato.registrarEquipo(equipo);
                setSuccessColorOutput(true);
                lblOutputEquipo.Text = "Equipo registrado con éxito!";

                //Recargo la grilla
                cargarEquipos();
                grillaEquipos.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = ex.Message;
            }
        }

        protected void EditEquipo(object sender, GridViewEditEventArgs e)
        {
            grillaEquipos.EditIndex = e.NewEditIndex;
            cargarEquipos();

            //Pongo foco en el nombre del Equipo
            ((TextBox)grillaEquipos.Rows[e.NewEditIndex].FindControl("txtNombre")).Focus();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputEquipo.Text = "";
        }

        protected void CancelarEquipo(object sender, GridViewCancelEditEventArgs e)
        {
            grillaEquipos.EditIndex = -1;
            cargarEquipos();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputEquipo.Text = "";
        }

        protected void UpdateEquipo(object sender, GridViewUpdateEventArgs e)
        {
            EquipoCampeonato equipo = new EquipoCampeonato();

            try
            {
                //Obtengo los valores del equipo
                if (((TextBox)grillaEquipos.Rows[e.RowIndex].FindControl("txtNombre")).Text == "")
                {
                    throw new SportingException("Equipo requerido.");
                }
                equipo.Nombre = ((TextBox)grillaEquipos.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                if (((TextBox)grillaEquipos.Rows[e.RowIndex].FindControl("txtLocalidad")).Text == "")
                {
                    throw new SportingException("Localidad requerida.");
                }
                equipo.Localidad = ((TextBox)grillaEquipos.Rows[e.RowIndex].FindControl("txtLocalidad")).Text;
                equipo.IdEquipo = Convert.ToInt32(((Label)grillaEquipos.Rows[e.RowIndex].FindControl("lblIdEquipo")).Text);

                //Modifico un equipo existente
                GestorCampeonato.updateEquipo(equipo);
                setSuccessColorOutput(true);
                lblOutputEquipo.Text = "Equipo actualizado con éxito!";

                //Recargo la grilla
                grillaEquipos.EditIndex = -1;
                cargarEquipos();
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = ex.Message;
            }
        }

        protected void BorrarEquipo(object sender, EventArgs e)
        {
            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputEquipo.Text = "";

            try
            {
                //Obtengo el id del equipo a borrar
                LinkButton lnkRemoveEquipo = (LinkButton)sender;
                String idEquipo = lnkRemoveEquipo.CommandArgument;

                //Borro el equipo en BD
                GestorCampeonato.deleteEquipo(idEquipo);

                setSuccessColorOutput(true);
                lblOutputEquipo.Text = "El equipo fue eliminado con éxito";

                //Recargo la grilla
                cargarEquipos();
                grillaEquipos.SelectedIndex = -1;
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutputEquipo.Text = ex.Message;
            }
        }

        protected void OnPagingEquipo(object sender, GridViewPageEventArgs e)
        {
            //Pagination
            cargarEquipos();
            grillaEquipos.PageIndex = e.NewPageIndex;
            grillaEquipos.DataBind();

            //Limpio el mensaje de salida para asegurarme que no quede uno viejo.
            lblOutputEquipo.Text = "";
        }
    }
}
