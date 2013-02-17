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
    private FechaCampeonato fechaCampeonato;
    private EquipoCampeonato equipoLocal;
    private EquipoCampeonato equipoVisitante;
    private int equipoLocalPuntos;
    private int equipoVisitantePuntos;

    public Resultado()
    {
    }

    public int IdResultado
    {
        get { return idResultado; }
        set { idResultado = value; }
    }

    public FechaCampeonato FechaCampeonato
    {
        get { return fechaCampeonato; }
        set { fechaCampeonato = value; }
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
}