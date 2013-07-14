using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class MultimediaVideo
{
    private int id;
    private String titulo;
    private String urlVideo;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public String Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public String UrlVideo
    {
        get { return urlVideo; }
        set { urlVideo = value; }
    }

    public override String ToString()
    {
        return this.Titulo + ", " + this.UrlVideo;
    }
}