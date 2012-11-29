<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="Noticia.aspx.cs" Inherits="SportingWeb.Noticia1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHBody" runat="server">
    <div id="sporting_middle_section">
        <div class="sporting_container">
            <div class="sporting_content_area">
                <h3><strong>
                    <asp:Label ID="lblTituloNoticia" runat="server" class="tituloArticulo"></asp:Label>
                </strong></h3>
                <p><strong>
                    <asp:Label ID="lblSubtituloNoticia" runat="server"></asp:Label>
                </strong></p>
                <p>
                    <asp:Label ID="lblTextoNoticia" runat="server"></asp:Label>
                </p>
                <!--<p>
                    <asp:Label ID="lblFbLikeButton" runat="server"></asp:Label>
                </p>-->
            </div>
    	</div><!-- End Of Container  -->
    </div><!-- End Of Mid Section -->
</asp:Content>

