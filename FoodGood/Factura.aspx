<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Factura.aspx.cs" Inherits="Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--PANTALLA FINAL DE CONFIRMACION DE PEDIDO--%>
    <div class="finalstep  steps">
        <div class="container-fluid">
            <div class="row no-gutters" style="padding: 15px;">
                <div class="formTitle">Pedido Completado</div>
                <div class="borderBottom"></div>
            </div>
            <div class="row no-gutters">
                <div class="col-md-9">
                    <asp:Panel runat="server" ID="CompraHecha_pnl" class="compraHecha">
                        <div>
                            <div class="row">
                                <h1>Muchas Gracias</h1>
                                <ul>
                                    <%--<li id="pagoConPagosNet">Su código de Pagos Net para realizar el pago es: 1ASD1S<br />
                                            Para realizar el pago por favor pase por alguno de los centros de pago Pagos Net<br />
                                            <a href="http://www.sintesis.com.bo/red_cobertura.php">Centros de Cobranza Pagos Net
                                            </a>
                                        </li>
                                        <li id="pagoTigo">El monto a cancelar fue debitado de su cuenta de billetera electrónica Tigo.
                                        </li>
                                        <li>En breve un ejecutivo se pondrá en contacto con usted,<br />
                                            para hacer la verificación y confirmación de su compra.
                                        </li>--%>
                                    <li>Se ha enviado un correo a:                                                   
                                                    <asp:HyperLink NavigateUrl="#" runat="server" ID="CorreoResumenCarrito" /><br />
                                        Certificando la compra con la información para el seguimiento.<br />
                                    </li>
                                    <li>Si desea ver el estado de su pedido:
                                    </li>
                                </ul>
                            </div>
                            <div class="text-left" style="padding: 10px">
                                <asp:HyperLink NavigateUrl="#" runat="server" ID="VerOrdenLink" CssClass="btn btn-primary " Text="Ver estado" />
                                <a href="Productos.aspx" class="btn btn-default">Volver a la tienda</a>
                            </div>
                        </div>
                        <%--RESUMEN CARRITO--%>
                        <asp:Panel ID="ResumenPanel" runat="server">
                            <div class="row">
                                <h3>Resumen</h3>
                                <div>
                                    <div class="summary-box-header">
                                        <h5><strong>Datos de Envío</strong></h5>
                                    </div>
                                    <div class="summary-box-content">
                                        <label>Ciudad :</label><br />
                                        <asp:Literal ID="CiudadResumenLiteral" Text="nombre de ciudad" runat="server" />
                                        <br />
                                        <label>Dirección :</label><br />
                                        <asp:Literal ID="DireccionResumenLiteral" Text="texto direccion" runat="server" />
                                    </div>
                                </div>
                                <div>
                                    <div class="summary-box-header">
                                        <h5><strong>Datos de Facturacón</strong> </h5>
                                    </div>
                                    <div class="summary-box-content">
                                        <label>Razón Social :</label><br />
                                        <asp:Literal ID="RazonSocialResumenLiteral" Text="text" runat="server" />
                                        <br />
                                        <label>NIT :</label><br />
                                        <asp:Literal ID="NitResumenLiteral" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="summary-box-header">
                                    <h5><strong>Articulos del Pedido</strong></h5>
                                </div>
                                <div class="summary-box-content">
                                    <%--<asp:Panel ID="TitlesListResumen" CssClass="step1Title" runat="server">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-xs-4">Producto</div>
                                                    <asp:Panel ID="PrecioListTitleResumenPanel" CssClass="col-xs-2" runat="server">Precio</asp:Panel>
                                                    <div class="col-xs-2">Cantidad</div>
                                                    <asp:Panel ID="SubTotalListTitleResumenPanel" CssClass="col-xs-2 col-lg-1 margin-left" runat="server">SubTotal</asp:Panel>
                                                    <asp:Panel ID="CuotaListTitleResumenPanel" CssClass="col-xs-2 col-lg-1 margin-left" runat="server">Pago</asp:Panel>
                                                </div>
                                            </div>
                                        </asp:Panel>--%>
                                    <div class="step1Content">
                                        <%--<app:orderdetails id="DetallePedidoResumen_uc" runat="server" />--%>
                                        <asp:Panel runat="server" ID="ListaArticulosPagosNet_pnl">
                                            <asp:GridView ID="DetallePedidoPN_gv" runat="server" CssClass="table no-top-line order-items-details"
                                                AutoGenerateColumns="false" GridLines="None">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Articulo" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="col-md-3 col-sm-4 text-center">
                                                                    <asp:Image runat="server" ImageUrl='<%# "~/Images/image_"+ Eval("ImageId") +"_100_100.png" %>' AlternateText="ProductImage" />
                                                                </div>
                                                                <div class="col-md-9 col-sm-8 text-left">
                                                                    <p>
                                                                        <asp:HyperLink ID="ProductLink" runat="server"
                                                                            NavigateUrl='<%# "~/UnProducto.aspx?id=" + Eval("ArticuloId") %>' Target="_blank"
                                                                            CssClass="btn-link">
                                                                            <asp:Literal ID="NameLiteral" runat="server" Text='<%# Eval("NombreArticulo") %>'></asp:Literal><br />
                                                                        </asp:HyperLink>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Precio" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            Bs.
                                                                    <asp:Literal runat="server" Text='<%# Eval("PrecioForDisplay") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="order-item-details-quantity" />
                                                    <asp:TemplateField HeaderText="Sub-Total" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            Bs.
                                                                    <asp:Literal runat="server" Text='<%# Eval("SubTotalForDisplay") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="totalPagoResumen" CssClass="totalCarrito" runat="server">
                                            <div style="text-align: right;">
                                                Total:  Bs.
                                        <asp:Label ID="totalResumenLabel" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--FIN RESUMEN CARRITO--%>
                    </asp:Panel>
                </div>
            </div>
            <asp:HiddenField ID="DireccionIdSelectedHF" runat="server" />
            <asp:HiddenField ID="RazonSocialSelectedHF" runat="server" />
            <asp:HiddenField ID="NitSelectedHF" runat="server" />
        </div>
    </div>
</asp:Content>

