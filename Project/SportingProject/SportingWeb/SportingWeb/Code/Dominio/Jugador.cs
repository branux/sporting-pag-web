using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class Jugador
{
    private int idJugador;
    private String nombreApellido;
    private String posicion;
    private Imagen foto;

    public Jugador()
    {
    }

    public int IdJugador
    {
        get { return idJugador; }
        set { idJugador = value; }
    }

    public String NombreApellido
    {
        get { return nombreApellido; }
        set { nombreApellido = value; }
    }

    public String Posicion
    {
        get { return posicion; }
        set { posicion = value; }
    }

    public Imagen Foto
    {
        get { return foto; }
        set { foto = value; }
    }

    public override String ToString()
    {
        return this.NombreApellido + ", " + this.posicion + ", " + this.Foto.PathSmall;
    }
}