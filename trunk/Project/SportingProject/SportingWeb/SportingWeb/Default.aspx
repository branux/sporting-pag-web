<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" Inherits="Default" Title="Sporting Club" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <input id="currentPage" type="hidden" runat="Server"/>
 <div id="templatemo_content">
    
        <div id="main_column">
            <div class="main_column_section">
                <h2>Noticias</h2>
                <asp:Label ID="lblOutput" runat="server" ForeColor="Red"></asp:Label>
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
                                                            <li id="itemNoticia" class="sliderImage">
                                                                <a href="TemplateNoticia.aspx?id=<%# DataBinder.Eval(Container.DataItem, "id")%>">
                                                                    <img src="<%# DataBinder.Eval(Container.DataItem, "pathbig")%>" alt="" />
                                                                </a>
                                                                <span class="top">
                                                                    <a href="TemplateNoticia.aspx?id=<%# DataBinder.Eval(Container.DataItem, "id")%>">
                                                                        <h3 id="tituloNoticiaPortada"><%# DataBinder.Eval(Container.DataItem, "titulo")%></h3>
                                                                    </a>
                                                                    <%# evalWithMaxLength("descripcion", 100, "...") %>
                                                                </span>
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
        </div>
    
    	<div class="cleaner"></div>
    </div> <!-- end of content -->
</asp:Content>
