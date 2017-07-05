<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminOrderDetails.ascx.cs" Inherits="Administration_UserControls_AdminOrderDetails" %>
<%@ Register TagPrefix="app" TagName="GpsSelector" Src="~/UserControl/Mapa/GpsSelector.ascx" %>
<%@ Register Src="~/UserControl/OrderDetails/OrderDetails.ascx" TagName="OrderDetails" TagPrefix="app" %>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label>Fecha del Pedido</label><br />
            <asp:Literal ID="FechaPedido" runat="server"></asp:Literal>
        </div>

        <div class="form-group">
            <label>Estado</label><br />
            <asp:Label ID="Estado" runat="server" Style="display: inline-block; font-size: 13px"></asp:Label>
        </div>

        <div class="form-group">
            <label>Cliente</label><br />
            <asp:Literal ID="Cliente" runat="server"></asp:Literal>
        </div>

        <div class="form-group">
            <label>Ciudad de Envío</label><br />
            <asp:Literal ID="Ciudad" runat="server"></asp:Literal>
        </div>

        <div class="form-group">
            <label>Dirección de Envio</label><br />
            <asp:Literal ID="Direccion" runat="server"></asp:Literal>
        </div>

        <%--    <asp:Panel ID="PanelUsuarioEntrega" runat="server" Visible="false" CssClass="form-group">
            <label>Usuario asignado para la Entrega</label><br />
            <asp:Literal ID="UsuarioEntrega" runat="server"></asp:Literal>
        </asp:Panel>--%>

        <asp:PlaceHolder ID="PanelDatosEntrega" runat="server" Visible="false">
            <div class="form-group">
                <label>Fecha de Entrega</label>
                <asp:Literal ID="FechaEntrega" runat="server"></asp:Literal>
            </div>

            <%--  <div class="form-group">
                <label>Observación de Entrega</label>
                <asp:Literal ID="ObservacionEntrega" runat="server"></asp:Literal>
            </div>--%>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="DatosAnulacion" runat="server" Visible="false">
            <div class="form-group">
                <label>Usuario que realizó la Anulación</label><br />
                <asp:Literal ID="UsuarioAnulacion" runat="server"></asp:Literal>
            </div>

            <div class="form-group">
                <label>Fecha de Anulacion</label><br />
                <asp:Literal ID="FechaAnulacion" runat="server"></asp:Literal>
            </div>

<%--            <div class="form-group">
                <label>Motivo de Anulación</label><br />
                <asp:Literal ID="MotivoAnulacion" runat="server"></asp:Literal>
            </div>--%>
        </asp:PlaceHolder>

        <div class="form-group">
            <label>Monto Total</label><br />
            Bs.
            <asp:Literal ID="Total" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="col-md-6">
        <app:GpsSelector ID="GpsControl" runat="server" ReadOnly="true" />
    </div>

</div>

<div class="row">
    <div class="col-md-12">
        <h2>Articulos del Pedido</h2>
        <app:OrderDetails ID="OrderItemsControl" runat="server" />
    </div>
</div>
<asp:HiddenField ID="PedidoIdHiddenField" runat="server" Value="0" />
