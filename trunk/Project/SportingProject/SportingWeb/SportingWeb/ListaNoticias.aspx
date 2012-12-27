<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ListaNoticias.aspx.cs" Inherits="ListaNoticias" Title="Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <script type="text/javascript" src="../Scripts/listaNoticias.js"></script>
    <script src="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js" type="text/javascript"></script>
    
    <input id="listaNoticias" type="hidden" runat="Server"/>
    
    <div id="templatemo_content">      
    
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        
        <div id="main_column">
            <div class="main_column_section"> 
                <form id="form1" runat="server">
                    <div id="grillaNoticias"></div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
