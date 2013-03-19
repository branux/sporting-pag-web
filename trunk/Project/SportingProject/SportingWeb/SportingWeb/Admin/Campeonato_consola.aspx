<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Campeonato_consola.aspx.cs" Inherits="SportingWeb.Admin.Campeonato_consola" Title="Campeonato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead_consola" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody_consola" runat="server">
<form id="form1" runat="server">

    <h2>Campeonato - Fechas - Equipos</h2>
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    
    <!-Administrar Campeonatos->
    <div id="divGridABMCamp" style="padding:10px;width:100%">
        <h3>Administrar Campeonatos</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grillaCampeonatos" runat="server"  Width="300px"
                AutoGenerateColumns="false" Font-Names="Arial" Font-Size="11pt" 
                RowStyle-BackColor="#000000" HeaderStyle-BackColor="green" 
                AllowPaging="true"  ShowFooter="true" OnPageIndexChanging = "OnPagingCampeonato" 
                onrowediting="EditCampeonato" onrowupdating="UpdateCampeonato" onrowcancelingedit="CancelarCampeonato"
                PageSize = "10" >
                    <Columns>
                        <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCampeonato" runat="server" Text='<%# Eval("idCamp")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtIdCampeonato" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "Campeonato">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("nombre")%>'></asp:TextBox>
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width = "120px"  HeaderText = "Año">
                            <ItemTemplate>
                                <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("anio")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAnio" runat="server" Text='<%# Eval("anio")%>'></asp:TextBox>
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:TextBox ID="txtAnio" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRemove" runat="server"
                                    CommandArgument = '<%# Eval("idCamp")%>'
                                 OnClientClick = "return confirm('Esta seguro que desea borrar el campeonato?')"
                                Text = "Eliminar" OnClick = "BorrarCampeonato"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Agregar"
                                    OnClick = "AddCampeonato" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:CommandField  ShowEditButton="True" />
                    </Columns>
                </asp:GridView>                
                
                <asp:Label ID="lblOutputCamp" runat="server" ForeColor="Red"></asp:Label>
            </ContentTemplate>
            
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "grillaCampeonatos" />
                
            </Triggers>
            
        </asp:UpdatePanel>
        
    </div>
    
    <!-Administrar Fechas->
    <div id="divGridABMFechas" style="padding:10px;width:100%">
        <h3>Administrar Fechas</h3>
        <asp:UpdatePanel ID="UpdatePanelFechas" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grillaFechas" runat="server"  Width="300px"
                AutoGenerateColumns="false" Font-Names="Arial" Font-Size="11pt" 
                RowStyle-BackColor="#000000" HeaderStyle-BackColor="green" 
                AllowPaging="true"  ShowFooter="true" OnPageIndexChanging = "OnPagingFechas" 
                onrowediting="EditFecha" onrowupdating="UpdateFecha" onrowcancelingedit="CancelarFecha"
                PageSize = "10" onrowdatabound="grillaFechas_RowDataBound" >
                    <Columns>
                        <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdFecha" runat="server" Text='<%# Eval("idFecha")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtIdFecha" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "Campeonato">
                            <ItemTemplate>
                                <asp:Label ID="lblCampeonato" runat="server" Text='<%# Eval("campeonato")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" id="ddlCampeonato_edit" AutoPostBack="false">
                                </asp:DropDownList>
                                <asp:Label ID="lblIdCamp" runat="server" Text='<%# Eval("idCamp")%>' Visible="false"></asp:Label>
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:DropDownList runat="server" id="ddlCampeonato" AutoPostBack="false"
                                    OnSelectedIndexChanged="ddlCampeonato_SelectedIndexChanged">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width = "120px"  HeaderText = "Fecha Nro:">
                            <ItemTemplate>
                                <asp:Label ID="lblNumeroFecha" runat="server" Text='<%# Eval("numeroFecha")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNumeroFecha" runat="server" Text='<%# Eval("numeroFecha")%>'></asp:TextBox>
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:TextBox ID="txtNumeroFecha" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width = "120px"  HeaderText = "Descripción">
                            <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("descripcion")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDesc" runat="server" Text='<%# Eval("descripcion")%>'></asp:TextBox>
                            </EditItemTemplate> 
                            <FooterTemplate>
                                <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRemoveFecha" runat="server"
                                    CommandArgument = '<%# Eval("idFecha")%>'
                                 OnClientClick = "return confirm('Esta seguro que desea borrar la fecha?')"
                                Text = "Eliminar" OnClick = "BorrarFecha"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Agregar"
                                    OnClick = "AddFecha" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:CommandField  ShowEditButton="True" />
                    </Columns>
                </asp:GridView>                
                
                <asp:Label ID="lblOutputFecha" runat="server" ForeColor="Red"></asp:Label>
            </ContentTemplate>
            
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "grillaFechas" />
                
            </Triggers>
            
        </asp:UpdatePanel>
    </div>
    
</form>
</asp:Content>
