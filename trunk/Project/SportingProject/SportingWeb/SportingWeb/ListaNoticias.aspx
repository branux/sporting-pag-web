<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ListaNoticias.aspx.cs" Inherits="ListaNoticias" Title="Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <script type="text/javascript" src="../Scripts/listaNoticias.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tinyscrollbar.min.js"></script>
	<link rel="stylesheet" href="../Styles/scroll.css" type="text/css" media="screen"/>
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
                    <!--<div id="scrollbar1">
		                <div class="scrollbar"><div class="track"><div class="thumb"><div class="end"></div></div></div></div>
		                <div class="viewport">-->
                                <div id="grillaNoticias"  class="overview"></div>
		                <!-- </div>
	                </div> -->
                </form>
            </div>
        </div>
    </div>
</asp:Content>
