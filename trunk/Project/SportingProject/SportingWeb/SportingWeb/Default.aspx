<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" Inherits="Default" Title="Sporting Club" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
 <div id="templatemo_content">
    	
        <div id="side_column">
        
        	<div class="side_column_box">
				
            	<h2><span></span>Noticias</h2>
                     <div class="side_column_box_content">
                	    <div class="news_section">
                            <h3><a href="TemplateNoticia.aspx?id=<%= this.idNoticiaLateral1 %>"> <asp:Label ID="lblTituloLateral1" runat="server"></asp:Label></a></h3>
                            <a><asp:Image ID="imgLateral1" class="image_wrapper" runat="server" ImageUrl=""/></a>
                            <p><asp:Label ID="lblDescripcionLateral1" runat="server"></asp:Label></p>
                       </div>
                            
                        <div class="news_section">
                            <h3><a href="TemplateNoticia.aspx?id=<%= this.idNoticiaLateral2 %>"> <asp:Label ID="lblTituloLateral2" runat="server"></asp:Label></a></h3>
                            <a><asp:Image ID="imgLateral2" class="image_wrapper" runat="server" ImageUrl=""/></a>
                            <p><asp:Label ID="lblDescripcionLateral2" runat="server"></asp:Label></p>
                       </div>
					    <div class="botonVerMas"><a href="ListaNoticias.aspx">Ver mas</a></div>
                </div>
                <div class="bottom"></div>
            </div>
            
        </div> <!-- end of side column -->
        <div id="main_column">
            <form id="frmListaNoticias" runat="server">
            <div id="slider">
                                <ul id="sliderContent">
				<asp:GridView ID="gridNoticias" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Width="100%" AllowPaging="True" CellPadding="0" 
                        CellSpacing="0" HorizontalAlign="Center" PageSize="10" ShowHeader="False">
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            
                                    <Columns>
                                          <asp:TemplateField>         
                                                  <ItemTemplate>
                                                        <li class="sliderImage">
                                                            <a href="TemplateNoticia.aspx?id=<%# DataBinder.Eval(Container.DataItem, "id")%>"><img src="<%# DataBinder.Eval(Container.DataItem, "pathmedium")%>" alt="1" /></a>
                                                            <span class="top"><h3 id="tituloNoticiaPortada"><%# DataBinder.Eval(Container.DataItem, "titulo")%></h3><%# DataBinder.Eval(Container.DataItem, "descripcion")%></span>
                                                        </li>
                                                  </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    
                        </asp:GridView>
                        <div class="clear sliderImage"></div>
                                </ul>
                           </div>
            </form>
        </div>
    
    	<div class="cleaner"></div>
    </div> <!-- end of content -->
</asp:Content>
