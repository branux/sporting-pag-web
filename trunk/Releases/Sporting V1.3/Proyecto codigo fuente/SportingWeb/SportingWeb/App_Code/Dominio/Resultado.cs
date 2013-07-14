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

public class Resultado
{
    private int idResultado;
    private EquipoCampeonato equipoLocal;
    private EquipoCampeonato equipoVisitante;
    private int equipoLocalPuntos;
    private int equipoVisitantePuntos;
    private Boolean jugado;
    private DateTime fechaPartido;

    public DateTime FechaPartido
    {
        get { return fechaPartido; }
        set { fechaPartido = value; }
    }

    public Resultado()
    {
    }

    public int IdResultado
    {
        get { return idResultado; }
        set { idResultado = value; }
    }

    public EquipoCampeonato EquipoLocal
    {
        get { return equipoLocal; }
        set { equipoLocal = value; }
    }

    public EquipoCampeonato EquipoVisitante
    {
        get { return equipoVisitante; }
        set { equipoVisitante = value; }
    }

    public int EquipoLocalPuntos
    {
        get { return equipoLocalPuntos; }
        set { equipoLocalPuntos = value; }
    }

    public int EquipoVisitantePuntos
    {
        get { return equipoVisitantePuntos; }
        set { equipoVisitantePuntos = value; }
    }

    public Boolean Jugado
    {
        get { return jugado; }
        set { jugado = value; }
    }
}