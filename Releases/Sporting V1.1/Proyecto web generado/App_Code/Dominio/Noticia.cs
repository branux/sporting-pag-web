﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public class Noticia:IComparable
{
    private int idNoticia;
    private String titulo;
    private String descripcion;
    private List<Imagen> imagenes;
    private Boolean principal;

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

    public Boolean Principal
    {
        get { return principal; }
        set { principal = value; }
    }

    public Imagen GetPortada()
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

    public int CompareTo(object obj)
    {
        if (obj != null)
        {
            Noticia not = (Noticia)obj;
            return not.idNoticia - this.idNoticia;
        }
        else
        {
            return -1;
        }      
    }
}


    
