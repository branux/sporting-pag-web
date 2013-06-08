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

public partial class Default : System.Web.UI.Page
{
    protected String idNoticia1;
    protected String idNoticia2;
    protected String idNoticiaLateral1;
    protected String idNoticiaLateral2;
    protected String pathBigLateral1;
    protected String pathBigLateral2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            currentPage.Value = "Inicio";
            cargarNoticiasPrincipales();
            cargarNoticiasLaterales();
        }
    }

    private void cargarNoticiasPrincipales()
    {
        try
        {
            gridNoticias.DataSource = GestorNoticias.getTableNoticias();
            gridNoticias.DataBind();
        }
        catch (Exception ex)
        {
            if (ex.InnerException is SportingException)
            {
                SportingException spex = (SportingException)ex.InnerException;
                lblOutput.Text = "Error al cargar las noticias. Detalles: " + spex.Message;
            }
            lblOutput.Text = "Error al cargar las noticias. Detalles: " + ex.Message;
        }
        
    }

    protected string evalWithMaxLength(string fieldName, int maxLength, string sufix)
    {
        object value = this.Eval(fieldName);
        if (value == null)
            return null;
        
        string str = value.ToString();
        if (str.Length > maxLength)
        {
            str = str.Substring(0, maxLength + 1);
            str = str = str.Substring(0, Math.Min(str.Length, str.LastIndexOf(" ") == -1 ? 0 : str.LastIndexOf(" ")));
            str = str + sufix;
            return str;
        }
        else
            return str;
    }

    private void cargarNoticiasLaterales()
    {
       /* List<Noticia> noticias = GestorNoticias.getNoticiasLaterales();
        if (noticias[0] != null)
        {
            cargarNoticiaLateral1(noticias[0]);
        }
        if (noticias[1] != null)
        {
            cargarNoticiaLateral2(noticias[1]);
        }*/
    }

    /*private void cargarNoticiaLateral1(Noticia not)
    {
        lblTituloLateral1.Text = not.Titulo;
        lblDescripcionLateral1.Text = not.Descripcion;
        imgLateral1.ImageUrl = not.GetPortada().PathSmall;
        idNoticiaLateral1 = not.IdNoticia.ToString();
        pathBigLateral1 = not.GetPortada().PathBig;
    }

    private void cargarNoticiaLateral2(Noticia not)
    {
        lblTituloLateral2.Text = not.Titulo;
        lblDescripcionLateral2.Text = not.Descripcion;
        imgLateral2.ImageUrl = not.GetPortada().PathSmall;
        idNoticiaLateral2 = not.IdNoticia.ToString();
        pathBigLateral2 = not.GetPortada().PathBig;
    }*/
}

