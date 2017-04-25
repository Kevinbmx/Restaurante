<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="Administracion_MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="form-group">
        <asp:Label ID="FechaFinalLabel" AssociatedControlID="FechaFinlaDatePicker" runat="server" Text="Fecha Final" />
        <telerik:RadDatePicker ID="FechaFinlaDatePicker" runat="server" ZIndex="8200"
            DateInput-DateFormat="dd/MM/yyyy" DateInput-DisplayDateFormat="dd/MM/yyyy" Skin="aetemplate" EnableEmbeddedSkins="false">
        </telerik:RadDatePicker>
    </div>

</asp:Content>

