<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Fixture_consola.aspx.cs" Inherits="SportingWeb.Admin.Fixture_consola" Title="Fixture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead_consola" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody_consola" runat="server">

    <form id="form1" runat="server">

    <div style="padding:10px">
        <h2>Equipo de Primera División</h2>
        <br/>
        <h3>Seleccionar campeonato y/o la fecha</h3>
        <table style="border-right: 1px solid #C0C0C0; border-bottom: 1px solid #C0C0C0; padding: 1px 4px; width: 350px; border-left-style: solid; border-left-width: 1px; border-top-style: solid; border-top-width: 1px;">
            <tr>
                <td>
                    <p><asp:Label ID="lblCampeonato" runat="server" ForeColor="#C0C0C0" Text="Campeonato" Width="100px"></asp:Label></p>
                        <asp:DropDownList ID="ddlCampeonato" runat="server" Width="222px" 
                        onselectedindexchanged="ddlCampeonato_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p><asp:Label ID="lblFecha" runat="server" ForeColor="#C0C0C0" Text="Fecha" Width="100px"></asp:Label></p>
                    <asp:DropDownList ID="ddlFecha" runat="server" Width="222px" 
                    onselectedindexchanged="ddlFecha_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOutput" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

<div id="dvGrid" style="padding:10px;width:100%">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grillaCampeonato" runat="server"  Width="100%"
            AutoGenerateColumns="false" Font-Names="Arial" Font-Size="11pt" 
            AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="green" 
            AllowPaging="true"  ShowFooter="true" OnPageIndexChanging = "OnPaging" 
            onrowediting="EditPartido" onrowupdating="UpdatePartido" onrowcancelingedit="CancelarModificacion"
            PageSize = "10" >
                <Columns>
                    <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "Id">
                        <ItemTemplate>
                            <asp:Label ID="lblIdResultadoPartido" runat="server" Text='<%# Eval("idResultadoPartido")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIdResultadoPartido" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Fecha">
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("fecha")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("fecha")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Local">
                        <ItemTemplate>
                            <asp:Label ID="lblLocal" runat="server" Text='<%# Eval("equipoLocal")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLocal" runat="server" Text='<%# Eval("equipoLocal")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtLocal" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Puntos Local">
                        <ItemTemplate>
                            <asp:Label ID="lblPuntosLocal" runat="server" Text='<%# Eval("puntosLocal")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPuntosLocal" runat="server" Text='<%# Eval("puntosLocal")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtPuntosLocal" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Visitante">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitante" runat="server" Text='<%# Eval("equipoVisitante")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVisitante" runat="server" Text='<%# Eval("equipoVisitante")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtVisitante" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Puntos Visitante">
                        <ItemTemplate>
                            <asp:Label ID="lblPuntosVisitante" runat="server" Text='<%# Eval("puntosVisitante")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPuntosVisitante" runat="server" Text='<%# Eval("puntosVisitante")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtPuntosVisitante" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument = '<%# Eval("idResultadoPartido")%>'
                             OnClientClick = "return confirm('Esta seguro que desea borrar el partido?')"
                            Text = "Eliminar" OnClick = "BorrarPartido"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                OnClick = "AgregarNuevoPartido" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:CommandField  ShowEditButton="True" />
                </Columns>
                <AlternatingRowStyle BackColor="#C2D69B"  />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID = "grillaCampeonato" />
        </Triggers>
    </asp:UpdatePanel>
</div>
    </form>
</asp:Content>
