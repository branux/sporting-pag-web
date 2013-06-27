<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Equipo_consola.aspx.cs" Inherits="SportingWeb.Admin.Equipo_consola" Title="Equipo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHHead_consola" runat="server">
    <script language="javascript" type="text/javascript">
        function validarImagenNotEmpty()
        {
            if (document.getElementById("<%=fileUpload.ClientID%>").value=="")
            {
                alert("No hay ninguna imagen para cargar");
                document.getElementById("<%=fileUpload.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody_consola" runat="server">
    <input id="currentPage" type="hidden" runat="Server"/>
    <div style="padding:10px">
        <h2>Equipo de Primera División</h2>
        <br/>
        <h3>Agregar nuevo jugador</h3>
        <table style="border-right: 1px solid #C0C0C0; border-bottom: 1px solid #C0C0C0; padding: 1px 4px; width: 350px; border-left-style: solid; border-left-width: 1px; border-top-style: solid; border-top-width: 1px;">
            <tr>
                <td>
                    <p><asp:Label ID="lblNomApe" runat="server" ForeColor="#C0C0C0" Text="Nombre y Apellido" Width="100px"></asp:Label></p>
                    <asp:TextBox ID="txtNomApe" runat="server" Width="222px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValidNomApe" runat="server" 
                        ControlToValidate="txtNomApe" Display="Dynamic" 
                        ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p><asp:Label ID="lblPosicion" runat="server" ForeColor="#C0C0C0" Text="Posición" Width="100px"></asp:Label></p>
                    <asp:TextBox ID="txtPosicion" runat="server" Width="222px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValidPosicion" runat="server" 
                        ControlToValidate="txtPosicion" Display="Dynamic" 
                        ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <p><asp:Label ID="lblImagen" runat="server" ForeColor="#C0C0C0" Text="Imagen" Width="100px"></asp:Label></p>
                    <p><asp:FileUpload ID="fileUpload" runat="server" Text="Cargar imagen..." Width="230px" onchange="cargarImagenJugador()"/>
                        <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar imagen" Width="100px" 
                            onclick="btnCargarImagen_Click" OnClientClick="return validarImagenNotEmpty()"/>
                    </p>
                    <div style="width:120px; height:100px; padding-top:5px;">
                        <asp:Image ID="imgJugador" runat="server" Height="100px" Width="120px"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOutput" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                        onclick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CausesValidation="False" onclick="btnCancelar_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="padding:10px; margin-top:10px;">
        <h3>Jugadores</h3>
        <asp:GridView ID="grillaJugadores" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="idJugador" 
            OnRowCommand="GrillaJugadores_RowCommand" CssClass="mGrid">
            <RowStyle/>
            <Columns>
                <asp:BoundField DataField="idJugador" HeaderText="Id Jugador" Visible="False" />
                <asp:ImageField DataImageUrlField="imagen" HeaderText="Imagen" 
                ItemStyle-HorizontalAlign="Center"></asp:ImageField>
                
                <asp:BoundField DataField="nombreApellido" HeaderText="Nombre y Apellido" />
                <asp:BoundField DataField="posicion" HeaderText="Posición" />
                
                <asp:TemplateField ShowHeader="true" HeaderText="Opciones">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEditar" runat="server" CommandArgument='<%# Eval("idJugador") %>'
                            CausesValidation="False" CommandName="Editar"
                            ImageUrl="../../Images/icono_edit1.png" ToolTip="Editar" />
                        <asp:ImageButton ID="imgEliminar" runat="server" CommandArgument='<%# Eval("idJugador") %>'
                            CausesValidation="False" CommandName="Eliminar"
                            ImageUrl="../../Images/icono_delete1.png" ToolTip="Eliminar"
                            OnClientClick="javascript:return confirm('Esta seguro que desea borrar el jugador?');" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <HeaderStyle/>
        </asp:GridView>
    </div>
</asp:Content>
