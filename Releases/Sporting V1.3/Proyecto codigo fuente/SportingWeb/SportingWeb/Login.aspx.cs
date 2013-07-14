using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace SportingWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                String password = txtPass.Text;
                String usr = txtUsuario.Text;

                //Validate if the usr or password are strong!
                if (!IsTextStrong(usr) || !IsTextStrong(password))
                {
                    lblMensaje.Text = "Nombre de usuario o password incorrecto";
                    return;
                }

                //usuario está autenticado?
                int idUsuario = Seguridad.validarUsuario(usr, password);
                if (idUsuario != 0)
                {
                    //Obtiene un string con el rol del usuario
                    string rol = Seguridad.ObtenerRol(idUsuario);
                    //Crear un ticket de autenticación
                    FormsAuthenticationTicket autTicket = new FormsAuthenticationTicket(1, txtUsuario.Text, DateTime.Now, DateTime.Now.AddMinutes(60), false, rol);
                    //Encriptar el ticket
                    string encrTicket = FormsAuthentication.Encrypt(autTicket);
                    // Crea una cookie con el ticket encriptado
                    HttpCookie autCookie = new HttpCookie(".sportingsampacho", encrTicket);
                    // Agrega la cookie a la Response
                    Response.Cookies.Add(autCookie);
                    // Redirecciona al usuario a la página que solicitó originalmente
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsuario.Text, false));
                }
                else
                {
                    lblMensaje.Text = "Nombre de usuario o password incorrecto";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrio un error: " + "internal data base error";
            }
        }

        //Validate if the usr or password are strong!
        public static bool IsTextStrong(string text)
        {
            return Regex.IsMatch(text, @"^[0-9a-zA-Z'.\s]{1,10}$");
        }
    }
}
