﻿<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Campeonato.aspx.cs" Inherits="Campeonato" Title="Campeonato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <script type="text/javascript" src="../Scripts/campeonato.js"></script>
    
    <div id="templatemo_content">      
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        <div id="main_column">
            <div class="main_column_section">
                <form id="formCampeonato" runat="server"> 
                    <div id="fixture">
                            
                    </div>
                    <div id="tablaPosiciones">
                        <div>
                            <h3>Posiciones</h3>
                            <asp:GridView ID="gridTablaPosiciones" rules="none" runat="server" >
                            </asp:GridView>
                        </div> 
                    </div> 
                </form>
            </div>
        </div>
    </div>
</asp:Content>
