<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SportingWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h1>Sporting - Consola Administrativa</h1>
    <div id="login_container">
        <h3>Login</h3>
        <hr />
        </br>
        <table id="login_table">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblCampOblig" runat="server" Text="Los campos marcados con un asterisco (*) son obligatorios."></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario (*)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario"
                        ErrorMessage="Ingrese usuario" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtUsuario" ValidationExpression="^[0-9a-zA-Z'.\s]{1,10}$" 
                    ErrorMessage="Ingrese usuario" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPass" runat="server" Text="Password (*)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass"
                        ErrorMessage="Ingrese password" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="passCharValidator" runat="server" 
                        ControlToValidate="txtPass" ValidationExpression="^[0-9a-zA-Z'.\s]{1,10}$" 
                        ErrorMessage="Ingrese password" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                </td>
                <td>
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
