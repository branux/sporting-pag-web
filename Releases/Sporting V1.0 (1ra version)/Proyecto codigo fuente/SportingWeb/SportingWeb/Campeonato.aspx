<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Campeonato.aspx.cs" Inherits="Campeonato" Title="Campeonato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <script type="text/javascript" src="../Scripts/campeonato.js"></script>
    <input id="nombreCampeonato" type="hidden" runat="Server"/>
    <input id="currentPage" type="hidden" runat="Server"/>
    <div id="templatemo_content">      
        <div id="main_column">
            <div class="main_column_section">
                <h2 id="tituloCampeonato"></h2>
                <form id="formCampeonato" runat="server"> 
                    <div id="tablaPosiciones">
                        <div>
                            <h3>Posiciones</h3>
                            <asp:GridView ID="gridTablaPosiciones" rules="none" runat="server" 
                                GridLines="Horizontal" >
                            </asp:GridView>
                            <asp:GridView ID="gridTablaFixture" rules="none" runat="server" 
                                GridLines="Horizontal" >
                            </asp:GridView>
                        </div> 
                    </div> 
                    <div id="fixture">
                        <div id="paging_container" class="container">
                            <ul id="pageFixture">
                          
                            </ul>
                            <div class="page_navigation"></div>
                        </div>       
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
