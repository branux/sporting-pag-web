<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Equipo.aspx.cs" Inherits="Equipo" Title="Equipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
     <script type="text/javascript" src="../Scripts/plantel.js"></script>
     <input id="jugadoresPlantel" type="hidden" runat="Server"/>
     
     <div id="templatemo_content">      
    
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        
        <div id="main_column">
            <div class="main_column_section"> 
                <h2 class="tituloPlantel"><span></span><%= this.temporada%></h2>
                <p class="caption">
                   <img id="fotoPlantel" src = "<%= this.fotoPlantel%>" alt="" /> 
                    <span><%= this.infoPlantel%></span>
                </p>
                <h3 id="tituloJugadores">Jugadores</h3>
                <div id="plantelActual">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
