<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Contacto" Title="Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">

    <div id="templatemo_content">      
        <div id="side_column">
            <div class="side_column_box">
               <!-- <img id="publicidad" src = "../Images/publi.jpg" /> -->
            </div>
        </div>
        <div id="main_column">
            <div class="main_column_section"> 
                <table style="width: 100%">
            	    <tr>
            	        <td>
                            <p>Escriba su duda, consulta, queja o sugerencia. Complete el formulario con sus datos.</p>
                            <p>(*) campos obligatorios.</p>
                        </td>
                    </tr>
            	    <tr>
            	        <td align="center">
                            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="360" height="300">
                                <param name="movie" value="Recursos/contacto.swf" />
                                <param name="quality" value="high" />
                                <param name="wmode" value="transparent" />
                                <embed src="Recursos/contacto.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="360" height="300" wmode="transparent"></embed>
                            </object>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
