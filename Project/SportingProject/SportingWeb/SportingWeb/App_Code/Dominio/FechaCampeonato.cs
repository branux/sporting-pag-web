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

public class FechaCampeonato
{
    private int idFecha;
    private int numero;
    private DateTime fecha;
    private CampeonatoLiga campeonato;
    private List<Resultado> resultados;

    public FechaCampeonato()
    {
    }

    public int IdFecha
    {
        get { return idFecha; }
        set { idFecha = value; }
    }

    public int Numero
    {
        get { return numero; }
        set { numero = value; }
    }

    public DateTime Fecha
    {
        get { return fecha; }
        set { fecha = value; }
    }

    public CampeonatoLiga Campeonato
    {
        get { return campeonato; }
        set { campeonato = value; }
    }

    public List<Resultado> Resultados
    {
        get { return resultados; }
        set { resultados = value; }
    }   
}