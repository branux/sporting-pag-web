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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarNoticiasPrincipales();
            cargarNoticiasLaterales();
        }
    }

    private void cargarNoticiasPrincipales()
    {
        gridNoticias.DataSource = GestorNoticias.getTableNoticias();
        gridNoticias.DataBind();
    }

    private void cargarNoticiasLaterales()
    {
        List<Noticia> noticias = GestorNoticias.getNoticiasLaterales();
        if (noticias[0] != null)
        {
            cargarNoticiaLateral1(noticias[0]);
        }
        if (noticias[1] != null)
        {
            cargarNoticiaLateral2(noticias[1]);
        }
    }

   /* private void cargarNoticiasPrincipales()
    {
        List<Noticia> noticias = GestorNoticias.getNoticiasPrincipales();
        foreach (Noticia not in noticias)
        {
            if (not.Principal1)
            {
                cargarNoticiaPrincipal1(not);
            }
            else if (not.Principal2)
            {
                cargarNoticiaPrincipal2(not);
            }
        }
    }*/

   /* private void cargarNoticiaPrincipal1(Noticia not)
    {
        lblTituloNoticia1.Text = not.Titulo;
        lblDescripcionNoticia1.Text = not.Descripcion;
        imgNoticia1.ImageUrl = not.getPortada().PathSmall;
        idNoticia1 = not.IdNoticia.ToString();
    }

    private void cargarNoticiaPrincipal2(Noticia not)
    {
        lblTituloNoticia2.Text = not.Titulo;
        lblDescripcionNoticia2.Text = not.Descripcion;
        imgNoticia2.ImageUrl = not.getPortada().PathSmall;
        idNoticia2 = not.IdNoticia.ToString();
    }*/

    private void cargarNoticiaLateral1(Noticia not)
    {
        lblTituloLateral1.Text = not.Titulo;
        lblDescripcionLateral1.Text = not.Descripcion;
        imgLateral1.ImageUrl = not.getPortada().PathSmall;
        idNoticiaLateral1 = not.IdNoticia.ToString();
    }

    private void cargarNoticiaLateral2(Noticia not)
    {
        lblTituloLateral2.Text = not.Titulo;
        lblDescripcionLateral2.Text = not.Descripcion;
        imgLateral2.ImageUrl = not.getPortada().PathSmall;
        idNoticiaLateral2 = not.IdNoticia.ToString();
    }
}

