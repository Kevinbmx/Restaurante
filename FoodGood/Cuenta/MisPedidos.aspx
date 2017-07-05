<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MisPedidos.aspx.cs" Inherits="Cuenta_MisPedidos" %>

<%@ Register Src="~/UserControl/Paginacion/PagerControl.ascx" TagName="pagercontrol" TagPrefix="app" %>
<%@ Register Src="~/UserControl/OrderDetails/OrderDetails.ascx" TagName="orderdetails" TagPrefix="app" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="shop-main">
        <div class="container">
            <!-- BREADCRUMBS -->

            <!-- END BREADCRUMBS -->
            <div class="row" style="margin-bottom: 200px;">
                <%--<div class="col-md-3">
                    <app:clientoptionsmenu runat="server" selecteditem="Orders" />
                </div>--%>
                <div class="col-md-9" style="margin-top: 100px; margin-bottom: 100px;">
                    <h2>Mis Ordenes</h2>

                    <asp:Repeater ID="OrdersRepeater" runat="server"
                        OnItemCommand="OrdersRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="summary-box-header">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="DetailsButton" runat="server" CssClass="btn btn-default"
                                            CommandName="VerDetalle" CommandArgument='<%# Eval("PedidoId") %>'>
                                            Ver Detalle
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Fecha del Pedido:</strong><br />
                                        <asp:Literal ID="FechaLiteral" runat="server" Text='<%# Eval("FechaPedido") %>'></asp:Literal>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Estado:</strong><br />
                                        <asp:Literal ID="EstadoLiteral" runat="server" Text='<%# Eval("Observacion") %>'></asp:Literal>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Total:</strong><br />
                                        Bs.
                                        <asp:Literal ID="TotalLiteral" runat="server" Text='<%# Eval("MontoTotal") %>'></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="summary-box-content">
                                <app:orderdetails ID="Details" runat="server" CarritoId='<%# Eval("CarritoId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p id="EmptyMessage" runat="server" visible="false" class="text-center">
                        -- No tiene registrado ningun pedido --
                    </p>
                    <app:pagercontrol ID="Pager" runat="server" PageSize="5" OnPageChanged="Pager_PageChanged" />
                    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="queryHiddenField" />

</asp:Content>

