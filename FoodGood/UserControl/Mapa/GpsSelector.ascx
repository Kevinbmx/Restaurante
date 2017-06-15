<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GpsSelector.ascx.cs" Inherits="UserControls_GpsSelector" %>
<asp:Panel ID="MapCanvas1" runat="server" Width="100%" Height="300px" Style="border: 1px solid #999; padding: 2px;"></asp:Panel>
<asp:HyperLink ID="ClearButton" runat="server" NavigateUrl="#">Borrar selección</asp:HyperLink>
<asp:Literal ID="InputLiteral" runat="server"></asp:Literal>
<script type="text/javascript">
    var lat = 0;
    var lng = 0;

    $(document).ready(function () {

        if ($("#<%= Lat1.ClientID %>").val() != '')
            lat = $("#<%= Lat1.ClientID %>").val();
        if ($("#<%= Lng1.ClientID %>").val() != '')
            lng = $("#<%= Lng1.ClientID %>").val();
        var editable = $("#<%= ReadonlyHF.ClientID %>").val() == "false";
        var selector = null;
        if (lat != 0 && lng != 0) {
            selector = new MapSelector('#<%= ClientID %>', '<%= MapCanvas1.ClientID %>', "#<%= Lat1.ClientID %>", "#<%= Lng1.ClientID %>", "Ubicación exacta", editable, "#<%= ClearButton.ClientID %>");
        } else {
            selector = new MapSelector('#<%= ClientID %>', '<%= MapCanvas1.ClientID %>', "#<%= Lat1.ClientID %>", "#<%= Lng1.ClientID %>", "Mover el marcador a la posición exacta", editable, "#<%= ClearButton.ClientID %>");
        }
    });

</script>
<asp:HiddenField ID="Lat1" runat="server" />
<asp:HiddenField ID="Lng1" runat="server" />
<asp:HiddenField ID="ReadonlyHF" runat="server" Value="false" />
