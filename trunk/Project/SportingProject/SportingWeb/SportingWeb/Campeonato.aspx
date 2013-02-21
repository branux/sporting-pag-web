<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Campeonato.aspx.cs" Inherits="Campeonato" Title="Campeonato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    
    
    <div id="templatemo_content">      
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        <div id="main_column">
            <div class="main_column_section"> 
                <form id="myform" runat="server">
                    <asp:GridView ID="gridTablaPosiciones" runat="server" >
                    </asp:GridView>
                </form>  
            </div>
        </div>
    </div>
</asp:Content>
