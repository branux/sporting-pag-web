<%@ Page Language="C#" MasterPageFile="~/Admin/Consola.Master" AutoEventWireup="true" CodeBehind="Noticias_consola.aspx.cs" Inherits="SportingWeb.Admin.Noticias_consola" Title="Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead_consola" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody_consola" runat="server">
    <form id="form1" runat="server">
        <div style="padding:10px">
            <h2>Noticias</h2>
            <br/>
            <h3>Agregar nueva noticia</h3>
            <table style="border-right: 1px solid #C0C0C0; border-bottom: 1px solid #C0C0C0; padding: 1px 4px; width: 350px; border-left-style: solid; border-left-width: 1px; border-top-style: solid; border-top-width: 1px;">
                <tr>
                    <td>
                        <p><asp:Label ID="lblTitulo" runat="server" ForeColor="#C0C0C0" Text="Título" Width="100px"></asp:Label></p>
                        <asp:TextBox ID="txtTitulo" runat="server" Width="222px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValidTitulo" runat="server" 
                            ControlToValidate="txtTitulo" Display="Dynamic" 
                            ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtIdNoticia" runat="server" Visible="False" Width="10px"></asp:TextBox>
                    </td>
                    <td rowspan="3" valign="top">
                        <p>
                            <asp:Label ID="lblImagenes" runat="server" ForeColor="#C0C0C0" Text="Imágenes" Width="100px"></asp:Label></p>
                            <!--Grilla imagenes-->
                            <asp:GridView ID="grillaImagenes" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="idImagen">
                                <Columns>
                                    <asp:BoundField DataField="idImagen" Visible="False" />
                                    <asp:ImageField DataImageUrlField="image" HeaderText="Imagen" 
                                        ItemStyle-HorizontalAlign="Center"></asp:ImageField>
                                    <asp:TemplateField ShowHeader="true" HeaderText="Opciones">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="deleteImg_grillaImagenes" runat="server" CommandArgument='<%# Eval("idImagen") %>'
                                                CausesValidation="False" CommandName="Eliminar" OnClick="deleteImg_grillaImagenes_Click"
                                                ImageUrl="../../Images/icono_delete1.png" ToolTip="Eliminar"
                                                OnClientClick="javascript:return confirm('Esta seguro que desea borrar la imagen?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lblDesc" runat="server" ForeColor="#C0C0C0" Text="Descripción" Width="100px"></asp:Label></p>
                        <asp:TextBox ID="txtDesc" runat="server" Width="222px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValidDesc" runat="server" 
                            ControlToValidate="txtDesc" Display="Dynamic" 
                            ErrorMessage="Campo obligatorio"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lblImagen" runat="server" ForeColor="#C0C0C0" Text="Imagen" Width="100px"></asp:Label></p>
                        <p><asp:FileUpload ID="fileUpload" runat="server" Text="Cargar imagen..." Width="230px" onchange="cargarImagenNoticia()"/>
                            <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar imagen" Width="100px" 
                                onclick="btnCargarImagen_Click" OnClientClick="return validarImagenNotEmpty()"/>
                        </p>
                        <div style="width:120px; height:100px; padding-top:5px;">
                            <asp:Image ID="imgNoticia" runat="server" Height="100px" Width="120px"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblOutput" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                            onclick="btnGuardar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            CausesValidation="False" onclick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        
        <!--Grilla Noticias-->
        <div style="padding:10px; margin-top:10px;">
        <h3>Noticias</h3>
        <asp:GridView ID="grillaNoticias" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="idNoticia" 
            OnRowCommand="GrillaNoticias_RowCommand" CssClass="mGrid">
            <RowStyle/>
            <Columns>
                <asp:BoundField DataField="idNoticia" HeaderText="Id Noticia" Visible="False" />
                <asp:ImageField DataImageUrlField="imagenes" HeaderText="Imagenes" 
                ItemStyle-HorizontalAlign="Center"></asp:ImageField>
                
                <asp:BoundField DataField="titulo" HeaderText="Título" />
                <asp:BoundField DataField="desc" HeaderText="Descripción" />
                <asp:TemplateField ShowHeader="true" HeaderText="Principal">
                    <ItemTemplate>
                     <%# (Boolean.Parse(Eval("principal").ToString()))? "Si": "No"%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true" HeaderText="Opciones">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEditar" runat="server" CommandArgument='<%# Eval("idNoticia") %>'
                            CausesValidation="False" CommandName="Editar"
                            ImageUrl="../../Images/icono_edit1.png" ToolTip="Editar" />
                        <asp:ImageButton ID="imgEliminar" runat="server" CommandArgument='<%# Eval("idNoticia") %>'
                            CausesValidation="False" CommandName="Eliminar"
                            ImageUrl="../../Images/icono_delete1.png" ToolTip="Eliminar"
                            OnClientClick="javascript:return confirm('Esta seguro que desea borrar la noticia?');" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <HeaderStyle/>
        </asp:GridView>
    </div>
    </form>
</asp:Content>
