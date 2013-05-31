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

public partial class ListaNoticias : System.Web.UI.Page
{
    public List<Noticia> listaNoticia;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            currentPage.Value = "Noticias";
            cargarNoticias();
        }
    }

    private void cargarNoticias()
    {
        listaNoticia = NoticiaDAL.getNoticias();
        this.listaNoticias.Value = listaNoticias.ToString();
    }

    [System.Web.Services.WebMethod]
    public static string GetDate(string when)
    {
        return DateTime.Now.ToString();
    }
}

