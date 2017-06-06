<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagerControl.ascx.cs" Inherits="UserControls_PagerControl" %>

<asp:Panel ID="PagePanel" runat="server" CssClass="paginationPanel">
    <asp:LinkButton ID="FistPageLinkButton" runat="server" OnClick="FistPageLinkButton_Click"
        CssClass="btn fistPage" Text="Primera Pagina"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="PreviousPageLinkButton" runat="server"
        OnClick="PreviousPageLinkButton_Click" CssClass="btn previousPage"
        Text="Pagina anterior">
    </asp:LinkButton>
    &nbsp;
    <asp:Label ID="InfoPageLabel" runat="server" CssClass="numberPage"></asp:Label>
    &nbsp;
    <asp:LinkButton ID="NextPageLinkButton" runat="server"
        OnClick="NextPageLinkButton_Click" CssClass="btn nextPage"
        Text="Siguiente pagina">
    </asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="LastPageLinkButton" runat="server"
        OnClick="LastPageLinkButton_Click" CssClass="btn lastPage"
        Text="Ultima pagina">
    </asp:LinkButton>
</asp:Panel>
<asp:Label ID="CurrentRowHiddenField" runat="server" Text="0" Visible="false" />
<asp:Label ID="TotalRowsHiddenField" runat="server" Text="0" Visible="false" />
<asp:Label ID="PageSizeHiddenField" runat="server" Text="10" Visible="false" />
<asp:Label ID="InvisibilityMethodHiddenField" runat="server" Text="PropertyControl" Visible="false" />

<script type="text/javascript">
    $(document).ready(function () {
        $('.fistPage').html('<i class="fa fa-angle-double-left fa-lg" aria-hidden="true"></i>');
        $('.previousPage').html('<i class="fa fa-angle-left fa-lg" aria-hidden="true"></i>');
        $('.nextPage').html('<i class="fa fa-angle-right fa-lg" aria-hidden="true"></i>');
        $('.lastPage').html('<i class="fa fa-angle-double-right fa-lg" aria-hidden="true"></i>');
    });
</script>
