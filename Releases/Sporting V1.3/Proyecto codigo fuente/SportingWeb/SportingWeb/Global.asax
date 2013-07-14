<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Security.Principal" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {  
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando se cierra la aplicación
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Código que se ejecuta al producirse un error no controlado
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando se inicia una nueva sesión
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: El evento Session_End se desencadena sólo con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        // o SQLServer, el evento no se genera.

    }
    
    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        // Recupera la cookie
        HttpCookie authCookie = Context.Request.Cookies[".sportingsampacho"];
        if (authCookie == null)
        {
            // Si no existe termina
            return;
        }
        FormsAuthenticationTicket autTicket = null;
        try
        {
            autTicket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch (Exception ex)
        {
            return;
        }
        if (autTicket == null)
        {
            // No se pudo desencriptar
            return;
        }
        //Recupero el Rol
        String[] rol = autTicket.UserData.Split(new char[] { '|' });
        // Creo un objeto Identity y lo vinculo al Context
        GenericIdentity id = new GenericIdentity(autTicket.Name, ".sportingsampacho");
        GenericPrincipal principal = new GenericPrincipal(id, rol);
        Context.User = principal;
    }

       
</script>
