<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Fixture_consola.aspx.cs" Inherits="SportingWeb.Admin.Fixture_consola" Title="Fixture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead_consola" runat="server">
<!-- Adding Scripts for date picker in Fixtures-->
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/redmond/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
        bindPicker();
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function() {
        bindPicker();
    });
    function bindPicker() {
        $("input[type=text][id*=txtFechaPartido]").datepicker({ "dateFormat":"dd/mm/yy"});
    }
    
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody_consola" runat="server">

    <form id="form1" runat="server">

    <div style="padding:10px">
        <h2>Fixture de Primera División</h2>
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
<asp:ScriptManager ID="ScriptManager" runat="server" />

<!-Administrar Fixture->
<div id="dvGridFixture" style="padding:10px; width:100%">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grillaCampeonato" runat="server"
            AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11pt" 
            RowStyle-BackColor="#000000" HeaderStyle-BackColor="green" HeaderStyle-HorizontalAlign="center"
            AllowPaging="True"  ShowFooter="True" OnPageIndexChanging = "OnPagingFixture" 
            onrowediting="EditFixture" onrowupdating="UpdateFixture" 
                onrowcancelingedit="CancelEditFixture" 
                onrowdatabound="grillaCampeonato_RowDataBound" >
                <RowStyle BackColor="Black" />
                <Columns>
                    <asp:TemplateField HeaderText = "Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdResultadoPartido" runat="server" Text='<%# Eval("idResultadoPartido")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIdResultadoPartido" MaxLength="10" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "Fecha Nro">
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("fecha")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" id="ddlFechaGrilla_edit" AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:Label ID="lblIdFechaGrilla" runat="server" Text='<%# Eval("idFechaGrilla")%>' Visible="false"></asp:Label>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:DropDownList runat="server" id="ddlFechaGrilla" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "Fecha partido">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaPartido" runat="server" Text='<%# Eval("fechaPartido")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFechaPartido" runat="server" Text='<%# Bind("fechaPartido") %>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtFechaPartidoFooter" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "Local">
                        <ItemTemplate>
                            <asp:Label ID="lblLocal" runat="server" Text='<%# Eval("equipoLocal")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" id="ddlLocalGrilla_edit" AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:Label ID="lblIdLocalGrilla" runat="server" Text='<%# Eval("idLocalGrilla")%>' Visible="false"></asp:Label>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:DropDownList runat="server" id="ddlLocalGrilla" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Puntos Local" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblPuntosLocal" runat="server" Text='<%# Eval("puntosLocal")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPuntosLocal" runat="server" Text='<%# Eval("puntosLocal")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtPuntosLocal" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "Visitante">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitante" runat="server" Text='<%# Eval("equipoVisitante")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" id="ddlVisitanteGrilla_edit" AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:Label ID="lblIdVisitanteGrilla" runat="server" Text='<%# Eval("idVisitanteGrilla")%>' Visible="false"></asp:Label>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:DropDownList runat="server" id="ddlVisitanteGrilla" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "Puntos Visitante" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblPuntosVisitante" runat="server" Text='<%# Eval("puntosVisitante")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPuntosVisitante" runat="server" Text='<%# Eval("puntosVisitante")%>'></asp:TextBox>
                        </EditItemTemplate> 
                        <FooterTemplate>
                            <asp:TextBox ID="txtPuntosVisitante" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument = '<%# Eval("idResultadoPartido")%>'
                             OnClientClick = "return confirm('Esta seguro que desea borrar este partido del fixture?')"
                            Text = "Eliminar" OnClick = "BorrarPartido"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                OnClick = "AgregarPartido" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:CommandField  ShowEditButton="True" />
                </Columns>
                <HeaderStyle BackColor="Green" HorizontalAlign="Center" />
            </asp:GridView>
            
            <asp:Label ID="lblOutputFixture" runat="server" ForeColor="Red"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID = "grillaCampeonato" />
        </Triggers>
    </asp:UpdatePanel>
</div>
    </form>
</asp:Content>
