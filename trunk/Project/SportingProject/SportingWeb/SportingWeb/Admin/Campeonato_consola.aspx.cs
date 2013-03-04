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
                //TODO: cargar Campeonatos en el this.ddlCampeonato
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
            //GridView1.EditIndex = e.NewEditIndex;
            //BindData();
        }

        protected void CancelarModificacion(object sender, GridViewCancelEditEventArgs e)
        {
            //GridView1.EditIndex = -1;
            //BindData();
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

    }
}
