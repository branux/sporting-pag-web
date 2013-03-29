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

public partial class TemplateNoticia : System.Web.UI.Page
{
    protected String tituloNoticia;
    protected String descripcionNoticia;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getInfo();
        }
    }


    public string getInfo()
    {
        Noticia not = new Noticia();
        try
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                not = GestorNoticias.getNoticia(id);
                tituloNoticia = not.Titulo;
                descripcionNoticia = not.Descripcion;

                GaleriaNoticia.DataSource = GestorNoticias.getTableImagenes(id).DefaultView;
                GaleriaNoticia.DataBind();
            }           
        }
        catch (SportingException e)
        {
            return "Ocurrio un problema: " + e.Message;
        }
        return "";
    }

    public string getHREF(object sURL)
    {
        DataRowView dRView = (DataRowView)sURL;
        return dRView["pathBig"].ToString();
    }

    public string getSRC(object imgSRC)
    {
        DataRowView dRView = (DataRowView)imgSRC;
        return dRView["pathSmall"].ToString();
    }

}

