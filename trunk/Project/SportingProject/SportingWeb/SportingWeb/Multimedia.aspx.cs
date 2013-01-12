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

public partial class Multimedia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarMultimedia();
        }
    }

    private void cargarMultimedia()
    {
        List<MultimediaVideo> mList = GestorMultimedia.getAllMultimedia();
        String mult = "";
        foreach (MultimediaVideo m in mList)
        {
            mult += m.ToString() + ";";
        }
        this.multimedia.Value = mult;
    }
}

