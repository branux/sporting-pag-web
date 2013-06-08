<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ListaNoticias.aspx.cs" Inherits="ListaNoticias" Title="Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <script type="text/javascript" src="../Scripts/listaNoticias.js"></script>
	<link rel="stylesheet" href="../Styles/scroll.css" type="text/css" media="screen"/>
    <input id="listaNoticias" type="hidden" runat="Server"/>
    <input id="currentPage" type="hidden" runat="Server"/>
    <div id="templatemo_content">      
    
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        
        <div id="main_column">
            <div class="main_column_section"> 
                    <h2>Noticias</h2>
                    <asp:Label ID="lblOutput" runat="server" ForeColor="Red"></asp:Label>
                    <div id="grillaNoticias"  class="overview">
                        <div id="paging_container" class="container">
                            <ul id="pageNoticias">
                          
                            </ul>
                            <div class="page_navigation"></div>
                        </div>
                    </div>
            </div>
        </div>  
    </div>
</asp:Content>
