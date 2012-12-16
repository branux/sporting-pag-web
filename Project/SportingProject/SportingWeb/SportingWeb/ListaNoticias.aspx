<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ListaNoticias.aspx.cs" Inherits="ListaNoticias" Title="Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <div id="templatemo_content">      
    
        <div id="side_column">
            <div class="side_column_box">

            </div>
        </div>
        
        <div id="main_column_lista_noticias">
            <form id="frmListaNoticias" runat="server">
            <div id="slider">
                                <ul id="sliderContent">
				<asp:GridView ID="gridNoticias" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Width="100%" AllowPaging="True" CellPadding="2" 
                        CellSpacing="2" HorizontalAlign="Center" PageSize="10" ShowHeader="False">
                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            
                                    <Columns>
                                          <asp:TemplateField>         
                                                  <ItemTemplate>
                                                        <li class="sliderImage">
                                                            <a href=""><img src="<%# DataBinder.Eval(Container.DataItem, "pathmedium")%>" alt="1" /></a>
                                                            <span class="top"><strong><%# DataBinder.Eval(Container.DataItem, "titulo")%></strong><br /><%# DataBinder.Eval(Container.DataItem, "descripcion")%></span>
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
</asp:Content>
