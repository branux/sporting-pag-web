using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public class Noticia
{
    private int idNoticia;
    private String titulo;
    private String descripcion;
    private List<Imagen> imagenes;

    public Noticia()
    {
    }

    public int IdNoticia
    {
        get { return idNoticia; }
        set { idNoticia = value; }
    }

    public string Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }

    public List<Imagen> Imagenes
    {
        get { return imagenes; }
        set { imagenes = value; }
    }

    public Imagen getPortada()
    {
        Imagen portada = null;
        if (this.Imagenes != null)
        {
            foreach (Imagen imagen in this.Imagenes)
            {
                if (imagen.Portada)
                {
                    portada = imagen;
                }
            }
        }
        return portada;
    } 
}


    
