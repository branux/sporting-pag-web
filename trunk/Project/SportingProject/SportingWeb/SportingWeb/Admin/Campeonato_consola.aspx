<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Campeonato_consola.aspx.cs" Inherits="SportingWeb.Admin.Campeonato_consola" Title="Campeonato" %>
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
                        <asp:DropDownList ID="ddlCampeonato" runat="server" Width="222px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p><asp:Label ID="lblFecha" runat="server" ForeColor="#C0C0C0" Text="Fecha" Width="100px"></asp:Label></p>
                    <asp:DropDownList ID="ddlFecha" runat="server" Width="222px"></asp:DropDownList>
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
                    <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "CustomerID">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("CustomerID")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCustomerID" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Name">
                        <ItemTemplate>
                            <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ContactName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContactName" runat="server" Text='<%# Eval("ContactName")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "Company">
                        <ItemTemplate>
                            <asp:Label ID="lblCompany" runat="server"
                                Text='<%# Eval("CompanyName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompany" runat="server"
                                Text='<%# Eval("CompanyName")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument = '<%# Eval("id")%>'
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
