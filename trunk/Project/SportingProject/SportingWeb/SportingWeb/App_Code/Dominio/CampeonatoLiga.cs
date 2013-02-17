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

public class CampeonatoLiga
{
    private int idCampeonato;
    private String nombre;
    private int anio;
    private List<FechaCampeonato> listaFechas;

    public CampeonatoLiga()
    {
    }

    public int IdCampeonato
    {
        get { return idCampeonato; }
        set { idCampeonato = value; }
    }

    public String Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public int Anio
    {
        get { return anio; }
        set { anio = value; }
    }

    public List<FechaCampeonato> ListaFechas
    {
        get { return listaFechas; }
        set { listaFechas = value; }
    }
}