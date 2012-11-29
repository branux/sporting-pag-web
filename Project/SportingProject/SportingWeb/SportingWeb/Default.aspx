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
					
					<div class="button_01"><a href="#">Ver mas</a></div>
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
							<a href="#"><img class="image_wrapper fl_image" src="images/templatemo_image_01.jpg" alt="image two" /></a> 
						  
							<!--Descripcion Noticia 1-->
							<p>This <a href="http://www.templatemo.com/page/2" target="_parent">website template</a> is provided by <a href="http://www.templatemo.com" target="_parent">templatemo.com</a> for free of charge. Validate <a href="http://validator.w3.org/check?uri=referer">XHTML</a> &amp; <a href="http://jigsaw.w3.org/css-validator/check/referer">CSS</a>. Feel free to download, modify and apply this template for your personal or commercial websites.</p>
							<div> 
								<p>Mauris in sapien in massa pellentesque viverra. Quisque venenatis fringilla eros. Etiam in lorem ac nulla imperdiet sodales. Phasellus in justo at lectus pulvinar ultrices. Mauris ultrices sollicitudin turpis.</p>
								<p>Nunc eget urna. Mauris vestibulum felis eget tortor. Praesent mattis varius quam. Vestibulum ullamcorper ipsum nec augue. Vestibulum auctor odio eget ante. Nunc commodo, magna pharetra semper vehicula, dui ligula feugiat elit, et euismod nunc orci ut libero. Etiam sodales massa vel metus.</p>
							</div>
						  <div class="button_01"><a href="#">Ver Mas</a></div>
						  </div>
					  <div class="cleaner"></div>
					  <div class="bottom"></div>
					</div>
				</div>
            </form>
			<form id="frmNoticiaPrincipal2">
				<div>
					<div class="main_column_section">       				
						<!--Titulo Noticia 1-->
						<h2><span></span>Sporting Club Deportivo y Cultural</h2>
						<div class="main_column_section_content">
						
						  <!--Imagen Noticia 1-->
							<a href="#"><img class="image_wrapper fl_image" src="images/templatemo_image_02.jpg" alt="image two" /></a>  
						  
						  <!--Descripcion Noticia 1-->
							<p>This <a href="http://www.templatemo.com/page/2" target="_parent">website template</a> is provided by <a href="http://www.templatemo.com" target="_parent">templatemo.com</a> for free of charge. Validate <a href="http://validator.w3.org/check?uri=referer">XHTML</a> &amp; <a href="http://jigsaw.w3.org/css-validator/check/referer">CSS</a>. Feel free to download, modify and apply this template for your personal or commercial websites.</p>
							<div> 
								<p>Mauris in sapien in massa pellentesque viverra. Quisque venenatis fringilla eros. Etiam in lorem ac nulla imperdiet sodales. Phasellus in justo at lectus pulvinar ultrices. Mauris ultrices sollicitudin turpis.</p>
								<p>Nunc eget urna. Mauris vestibulum felis eget tortor. Praesent mattis varius quam. Vestibulum ullamcorper ipsum nec augue. Vestibulum auctor odio eget ante. Nunc commodo, magna pharetra semper vehicula, dui ligula feugiat elit, et euismod nunc orci ut libero. Etiam sodales massa vel metus.</p>
							</div>
						  <div class="button_01"><a href="#">Ver Mas</a></div>
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
