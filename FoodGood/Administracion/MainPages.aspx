<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MainPages.aspx.cs" Inherits="Administracion_Inicio_MainPages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="container">
        <h3>Le damos la bienvenida a la administracion de FoodGood</h3>
        <label>llave</label>
        <asp:TextBox ID="llaveTxt" runat="server"> </asp:TextBox>
        <label>valor</label>
        <asp:TextBox ID="valorTxt" runat="server"> </asp:TextBox>
        <asp:LinkButton ID="addresource" OnClick="addresource_Click" runat="server"></asp:LinkButton>
    </div>
</asp:Content>

