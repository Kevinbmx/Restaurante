<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="header-fondo">
        <div style="padding-top: 400px;">
            <asp:Label ID="codigoControl" runat="server"></asp:Label>
            <asp:LinkButton runat="server" ID="Codigo" OnClick="Codigo_Click" Text="eliminar"></asp:LinkButton>


            <asp:LinkButton ID="botonFehca" Text="fecha Actual" runat="server" OnClick="botonFehca_Click"></asp:LinkButton>
            <asp:Label ID="fechaActual" runat="server"></asp:Label>
            <asp:Label ID="fechaActualCorta" runat="server"></asp:Label>
        </div>
    </div>


</asp:Content>

