using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class Auspiciante
{
    private int idAuspiciante;
    private Imagen imagenAuspiciante;

    public Auspiciante()
    {
    }

    public int IdAuspiciante
    {
        get { return idAuspiciante; }
        set { idAuspiciante = value; }
    }

    public Imagen ImagenAuspiciante
    {
        get { return imagenAuspiciante; }
        set { imagenAuspiciante = value; }
    }
}