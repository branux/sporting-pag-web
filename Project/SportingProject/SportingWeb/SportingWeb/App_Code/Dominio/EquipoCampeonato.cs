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

public class EquipoCampeonato : IComparable
{
    private int idEquipo;
    private String nombre;
    private String localidad;

    public EquipoCampeonato()
    {
    }

    public int IdEquipo
    {
        get { return idEquipo; }
        set { idEquipo = value; }
    }

    public String Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public String Localidad
    {
        get { return localidad; }
        set { localidad = value; }
    }

    public int CompareTo(object obj)
    {
        if (obj != null)
        {
            EquipoCampeonato equipo = (EquipoCampeonato)obj;
            return equipo.IdEquipo - this.IdEquipo;
        }
        else
        {
            return -1;
        }
    }
}