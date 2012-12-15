<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" Inherits="Default" Title="Página sin título" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
 <div id="templatemo_content">
    	
        <div id="side_column">
        
        	<div class="side_column_box">
				
            	<h2><span></span>Noticias</h2>
                
                <div class="side_column_box_content">
                	<div class="news_section">
                        <h3><a href="#">Etiam tempus tellus eget</a></h3>
                        <a href="#"><img class="image_wrapper" src="images/templatemo_image_04.jpg" alt="flower" /></a>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas et ipsum sem, ut lobortis dui.</p>
                  </div>
                        
                    <div class="news_section">
                        <h3><a href="#">Nam quis aliquet quam</a></h3>
                        <a href="#"><img class="image_wrapper" src="images/templatemo_image_03.jpg" alt="tiger" /></a>                        
                      <p>Sed pharetra neque vel mauris auctor ornare. Maecenas urna lorem, consectetur eget consectetur id.</p>
                  </div>
					
					<div class="botonVerMas"><a href="#">Ver mas</a></div>
                </div>
                
                <div class="bottom"></div>
            </div>
            
            <div class="side_column_box">
            
            	<h2><span></span>Newsletter</h2>
                
                <div class="side_column_box_content">

                    <form action="#" method="get">
                        <input type="text" value="Enter your email address..." name="q" size="10" class="inputfield" title="searchfield" onfocus="clearText(this)" onblur="clearText(this)" />
                        
                        <input type="submit" name="Search" value="Subscribe" alt="Search" class="submit_button" title="Search" />
                    </form>
                    
                    <div class="cleaner"></div>
                </div>
                
                <div class="bottom"></div>
            </div> 
        </div> <!-- end of side column -->
        <div id="main_column">
			<form id="frmNoticiaPrincipal1" runat="server">
				<div>
					<div class="main_column_section">       				
						<!--Titulo Noticia 1-->
						<h2>
						    <span></span>
						    <asp:Label ID="lblTituloNoticia1" runat="server" class="tituloNoticia"></asp:Label>
						</h2>
						<div class="main_column_section_content">
							<!--Imagen Noticia 1-->
							<a href="#"><asp:Image ID="imgNoticia1" class="image_wrapper fl_image" runat="server" ImageUrl=""/></a>						  
							<!--Descripcion Noticia 1-->					
							<asp:Label ID="lblDescripcionNoticia1" runat="server"></asp:Label>
							<div class="botonVerMas"><a href="#">Ver Mas</a></div>
					    </div>   
					  <div class="cleaner"></div>
					  <div class="bottom"></div>
					</div>
				</div>
            </form>
			<form id="frmNoticiaPrincipal2">
				<div>
					<div class="main_column_section">       				
						<!--Titulo Noticia 2-->
						<h2>
						    <span></span>
						    <asp:Label ID="lblTituloNoticia2" runat="server" class="tituloNoticia"></asp:Label>
						</h2>					
						<div class="main_column_section_content">	
							<!--Imagen Noticia 2-->
							<a href="#"><asp:Image ID="imgNoticia2" class="image_wrapper fl_image" runat="server" ImageUrl=""/></a>						  
							<!--Descripcion Noticia 2-->					
							<asp:Label ID="lblDescripcionNoticia2" runat="server"></asp:Label>
							<div class="botonVerMas"><a href="#">Ver Mas</a></div>
					    </div>
					  <div class="cleaner"></div>
					  <div class="bottom"></div>
					</div>
				</div>
            </form>
            <div class="cleaner"></div>
        </div> <!-- end of main column -->
    
    	<div class="cleaner"></div>
    </div> <!-- end of content -->
</asp:Content>
