<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemCarrito.ascx.cs" Inherits="UserControls_Carrito_ItemCarrito" %>
<div id="carritoRight" class="col-md-4 col-xs-12 col-sm-8 col-lg-3">
    <div class="panelHeader redContainer" id="carritoTitle">
        <div id="carritoIco"></div>
        <div id="carritoTitleText">
            <div id="carritoDeComprasTitle">
                Carrito de Compras
            </div>
            <div id="carritoItems">
                <asp:Label ID="cantItem2" runat="server" Text="0" CssClass="CantItem"></asp:Label>
                items
            </div>
        </div>
        <a href="javascript:void(0)" id="carritoClose"><i class="fa fa-times-circle fa-2x" aria-hidden="true"></i>
        </a>
    </div>

    <div id="carritoProductsList">
        <%--AQUI VA LA LISTA DE LOS ITEM DE LOS ARTICULOS PRODUCTOS DEL CARRITO--%>
        <asp:Repeater ID="PedidolistRepeater" runat="server"
            OnItemCommand="PedidolistRepeater_ItemCommand"
            OnItemDataBound="PedidolistRepeater_ItemDataBound">
            <ItemTemplate>
                <div id='<%# "ItemCarrito_P_" +Eval("ProductoId") %>' class="itemCarrito">
                    <div class="row">
                        <div class="col-xs-4">
                            <asp:HiddenField ID="ImagenId" runat="server" Value='<%# Eval("ImagenId") %>' />
                            <asp:Image ID="ProductImage" runat="server"
                                CssClass="img-responsive" Width="100" Height="100" />
                        </div>
                        <div class="col-xs-6">
                            <a class="nombreCarrito" href='<%# "UnProducto.aspx?id=" + Eval("ProductoId") %>'>
                                <asp:Literal ID="NombreItem" runat="server" Text='<%# Eval("Nombre")%>'></asp:Literal>
                            </a>
                            <%--   <div class="modeloCarrito">
                                <asp:Literal ID="ModeloLiteral" runat="server" Text='<%# Eval("modelo")%>'></asp:Literal>
                            </div>--%>
                            <asp:Panel ID="precioPanel" CssClass="precioCarrito alinear-precio" runat="server">
                                Bs.
                                     <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("PrecioForDisplay")%>'></asp:Literal>
                            </asp:Panel>
                            <asp:Panel ID="subtotalPanel" runat="server">
                                Sub-Total: Bs
                                <asp:Label ID="SubtotalLiteral" runat="server" Text=' <%# Eval("SubTotalForDisplay") %>' CssClass='<%# "Subtotal-" + Eval("ProductoId") %>'></asp:Label>
                            </asp:Panel>
                            <%--    <asp:Panel ID="CuotaPanel" CssClass="redText" runat="server">
                                Pago en
                                <asp:Label ID="cuotaLiteral" runat="server" Text='<%# Eval("MaxCuota") + " cuotas de Bs."  + Eval("CuotaForDisplay")%>'></asp:Label>
                            </asp:Panel>--%>
                            <%-- <asp:Panel ID="CantidadProductoPanel" runat="server">
                                Cantidad : 
                            <asp:Label ID="Cantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                            </asp:Panel>--%>
                            <asp:Panel ID="TextboxPanel" runat="server">
                                <asp:TextBox ID="CantidadTextBox" runat="server" type="number" min="1"
                                    max='<%# Eval("Stock") %>'
                                    CssClass="form-control input-sm qty-spinner input-number_noSpinners text-center"
                                    Style="display: block;"
                                    data-articuloid='<%# Eval("ProductoId") %>'
                                    Text='<%# Eval("Cantidad") %>'>
                                </asp:TextBox>
                            </asp:Panel>
                        </div>
                        <div class="col-xs-2 eliminarCarritoContainer">
                            <asp:LinkButton ID="Eliminar"
                                runat="server"
                                Text="x"
                                CommandName="RemoveFromCart"
                                CommandArgument='<%# Eval("ProductoId") %>'
                                Style="display: none;" />
                            <a href='<%#"javascript:removeItem(" + Eval("ProductoId") + ")" %>' class="btn redButton    " style="border-radius: 100%; padding: 2px 6px;">
                                <i class="fa fa-times" aria-hidden="true"></i>
                            </a>
                        </div>
                    </div>
                    <div class="row borderBottom"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField ID="CartHiddenField" runat="server" />

    <asp:Panel ID="carritoVacio" runat="server" CssClass="carritoSinItems">
        <div>
            -- Carrito vacio --
        </div>
    </asp:Panel>

    <asp:Panel ID="totalCarritoPanel" CssClass="totalCarrito SumaDelCarrito" runat="server">
        Total (
        <asp:Label ID="CantItem" runat="server" Text="0" CssClass="CantItem"></asp:Label>
        items): 
        <span class="precioTotalCarrito">Bs.
           <asp:Label ID="TotalLiteral" runat="server" Text="0" CssClass="total"></asp:Label>
        </span>
    </asp:Panel>

    <%--<div id="carritoCheckoutContainer">
                <div>Checkout</div>
            </div>--%>
    <div id="carritoSuscripcion" style="display: none;">
        <div id="carritoSuscripcionContainer">
            <div class="form-group">
                <asp:TextBox CssClass="form-control" runat="server" placeholder="tucorreo@direccionelectronica.com" />
            </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox runat="server" />Suscríbete para más ofertas
                </label>
            </div>
        </div>
    </div>
    <div style="margin: 10px;">
        <asp:HyperLink ID="pedidoLink" runat="server" CssClass="btn-primary btn btn-block" NavigateUrl="~/Carrito.aspx" Text="Realizar Pedido"></asp:HyperLink>
        <%--<a href="Pedido.aspx" class="btn-primary btn btn-block">Realizar Pedido</a>--%>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        loadSpinner();
        sumarTotal();
    });

    function loadSpinner() {
        // quantity spinner at shopping cart

        if ($('.qty-spinner').length > 0) {
            $('.qty-spinner').each(function () {
                0
                $(this).TouchSpin({
                    min: 1,
                    max: $(this).attr("max"),
                });
                $(this).change(function () {
                    if ($(this).val() == "") {
                        $(this).val("1");
                    }
                    var carrito = JSON.parse($("#<%= CartHiddenField.ClientID %>").val());
                    var item = carrito[$(this).data("articuloid")];
                    item.Cantidad = parseFloat($(this).val());
                    item.Precio = parseFloat(item.Precio)
                    item.SubTotal = item.Cantidad * item.Precio;

                    var arti = $(this).data("articuloid");
                    var cant = item.Cantidad;
                    var sub = item.SubTotal;

                    $(".Subtotal-" + $(this).data("articuloid")).html(formatNumber.new(item.SubTotal.toFixed(2)));
                    var total = 0;
                    for (var i in carrito) {
                        item = carrito[i];
                        total += item.SubTotal;
                    }

                    //console.log("articulo ID " + arti);
                    //console.log("Cantidad " + cant);
                    //console.log("Subtotal " + sub);
                    var obj = {
                        articuloId: arti,
                        cantidad: cant,
                        subtotal: sub
                    };
                    $.ajax({
                        url: "<%= VirtualPathUtility.ToAbsolute("~/Menu.aspx/actualizarCookiesAndHidden") %>",
                        data: JSON.stringify(obj),
                        dataType: 'json',
                        type: 'POST',
                        async: true,
                        contentType: 'application/json; charset=utf-8',
                        success: function (mydata) {
                            //console.log(mydata);
                            $("#<%= TotalLiteral.ClientID %>").html(formatNumber.new(total.toFixed(2)));
                            $("#ContentPlaceHolder2_TotalLiteral").html(formatNumber.new(total.toFixed(2)));
                            $("#ContentPlaceHolder2_total2").html(formatNumber.new(total.toFixed(2)));

                            $("#<%= CartHiddenField.ClientID %>").val(mydata.d);
                            $("#ContentPlaceHolder2_CartHiddenField").val(mydata.d);

                        }
                    });
                });
            });
        }
    }
</script>
