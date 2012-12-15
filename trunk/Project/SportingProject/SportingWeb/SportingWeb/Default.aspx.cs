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

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarNoticias();
        }
    }

    public void cargarNoticias()
    {
        // Noticia 1
        Noticia not1 = NoticiaDAL.getNoticiaById(1);
        lblTituloNoticia1.Text = not1.Titulo;
        lblDescripcionNoticia1.Text = not1.Descripcion;
        imgNoticia1.ImageUrl = not1.getPortada().PathSmall;

        // Noticia 2
        Noticia not2 = NoticiaDAL.getNoticiaById(2);
        lblTituloNoticia2.Text = not2.Titulo;
        lblDescripcionNoticia2.Text = not2.Descripcion;
        imgNoticia2.ImageUrl = not2.getPortada().PathSmall;
    }
}

