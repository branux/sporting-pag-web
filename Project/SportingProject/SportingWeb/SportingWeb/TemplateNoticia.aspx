<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TemplateNoticia.aspx.cs" Inherits="TemplateNoticia" Title="Noticia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <div id="templatemo_content">
        <div id="side_column">
            <div class="side_column_box">
            <!--Imagenes de la noticia-->
                <form id="frmGallery">
                    <div id="gallery">
                        <asp:DataList ID="GaleriaNoticia" runat="server" RepeatLayout="Table">
                          <ItemTemplate>
                                <a href='<%#getHREF(Container.DataItem)%>' rel=''>
                                    <img src='<%# getSRC(Container.DataItem) %>' class="imagenNoticia"/>
                                </a>
                          </ItemTemplate>
                        </asp:DataList>
                    </div>
                 </form>
            </div>
        </div>
        
        <div id="main_column">
            <form id="frmNoticiaPrincipal1" runat="server">
				<div>
					<div class="main_column_section">       				
						<!--Titulo Noticia-->
						<h2>
						    <span></span>
						    <asp:Label ID="lblTituloNoticia" runat="server" class="tituloNoticia"></asp:Label>
						</h2>
						<div class="main_column_section_content">					  
							<!--Descripcion Noticia -->					
							<asp:Label ID="lblDescripcionNoticia" runat="server"></asp:Label>
					    </div>   
					  <div class="cleaner"></div>
					  <div class="bottom"></div>
					</div>
				</div>
            </form>
        </div>
    </div>
</asp:Content>
