using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de WebServiceNoticias
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
[System.Web.Script.Services.ScriptService]
public class WebServiceNoticias : System.Web.Services.WebService
{
    public List<Noticia> listaNoticias;

    private void cargarNoticias()
    {
        listaNoticias = NoticiaDAL.getNoticias();
    }

    [WebMethod]
    public List<Noticia> GetAllNoticias()
    {
        cargarNoticias();
        return listaNoticias;
    }
}

