<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TemplateNoticia.aspx.cs" Inherits="TemplateNoticia" Title="Noticia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">

    <div id="templatemo_content">
        <div id="side_column">
            <div class="side_column_box">
            
            </div>
        </div>
        
        <div id="main_column">
        
           <!-- <form id="frmNoticiaPrincipal1" runat="server">-->
				<div>
					<div class="main_column_section">
					
					<!--Titulo Noticia-->
						<h2><%= this.tituloNoticia.ToString()%></h2>
					      
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
						
						<div class="main_column_section_content">					  
							<!--Descripcion Noticia -->					
							<div class="descripcionTemplateNoticia"><%= this.descripcionNoticia.ToString()%></div>
					    </div>   
					  <div class="cleaner"></div>
					</div>
				</div>
            <!--</form>-->
        </div>
    </div>
</asp:Content>
