<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Carrito.aspx.cs" Inherits="Carrito" %>

<%@ Register TagPrefix="app" TagName="GpsSelector" Src="~/UserControl/Mapa/GpsSelector.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCFI9ELr1vjveXc11xbKa5L_tKMMvD_OHc ">
    </script>
    <%--<script src="Script/google-map.js"></script>--%>
    <script src="Script/gpsMapSelector.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <section class="bg-primary padin-carrito" id="about">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">

                    <h2 runat="server" id="tituloFamilia">Carrito</h2>
                </div>
            </div>
        </div>
    </section>
    <asp:LinkButton runat="server" ID="valorLogin" OnClick="valorLogin_Click" Text="ver valor"></asp:LinkButton>
    <asp:Label ID="escribirvalor" runat="server"></asp:Label>
    <div style="width: 100%;" class="row no-gutters">
        <div class="col-md-3" style="padding-bottom: 260px;">
            <div id="carritoWizardHeader">
                <div id="carritoHeaderImgContainer">
                    <img src="img/wizardHeader.png" />
                </div>
                <div id="carritoTituloContainer">
                    <div id="tituloWizardText">
                        Detalle del pedido
                    </div>
                    <div id="cantItemsWizard">
                        <asp:Label ID="cantItem2" runat="server" class="CantItem" Text="0"></asp:Label>
                        items 
                    </div>
                </div>
            </div>
            <div id="carritoWizardSteps" class="container-fluid">
                <div class="row">
                    <div class="carritoWizardOneStep">
                        <a href="javascript:paso1()" class="stepButton">Items</a>
                        <div class="stepDetails">
                            <asp:Panel ID="totalPasosPanel" CssClass="totalCarrito" runat="server">
                                Total (<asp:Label ID="CantItem1" runat="server" Text="0" CssClass="CantItem"></asp:Label>
                                items): 
                              <span class="precioTotalCarrito">Bs.
                <asp:Label ID="total2" runat="server" Text="0" CssClass="total"></asp:Label></span>
                            </asp:Panel>
                        </div>
                    </div>
                    <div id="detallePaso2" class="carritoWizardOneStep hidden">
                        <a href="javascript:paso2()" class="stepButton">Lugar de Entrega</a>
                        <%--<div class="stepDetails hidden">
                            </div>--%>
                    </div>
                    <div id="detallePaso3" class="carritoWizardOneStep hidden">
                        <a href="javascript:paso3()" class="stepButton">Datos de Factura</a>
                        <div class="stepDetails hidden">
                        </div>
                    </div>

                    <div id="detallePaso4" class="carritoWizardOneStep hidden">
                        <a href="javascript:paso4()" class="stepButton">Modalidad de Pago</a>
                        <div class="stepDetails hidden">
                        </div>
                    </div>
                    <div id="detallePaso6" class="carritoWizardOneStep hidden">
                        <a href="javascript:cargarPaso6()" class="stepButton">Detalles de Pago en Cuotas</a>
                        <div class="stepDetails hidden">
                        </div>
                    </div>
                    <div id="detallePaso7" class="carritoWizardOneStep hidden">
                        <a href="javascript:cargarPaso7()" class="stepButton">Medio de Pago</a>
                        <div class="stepDetails hidden">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-9">
            <div style="margin: 15px;">
                <div id="paso1">
                    <div style="margin-bottom: 6%; border-bottom: 1px solid;">
                        <h1>Lista de Tu Futuro Pedido</h1>
                    </div>
                    <%-- <div class="row">
                <asp:Panel ID="Panel1" CssClass="totalCarrito SumaDelCarrito" runat="server">
                    <div id="carritoItems">
                        Total (
            <asp:Label ID="cantItem2" runat="server" Text="0" CssClass="CantItem"></asp:Label>)
            items
                <span class="precioTotalCarrito">Bs.
               <asp:Label ID="totalliteral1" runat="server" Text="0" CssClass="total"></asp:Label>
                </span>
                    </div>
                </asp:Panel>
            </div>--%>
                    <asp:Image ID="ImageQRCode" runat="server"
                        AlternateText="Image text"
                        ImageAlign="right"
                        ImageUrl="~/GeneradoQR/QRGenerator.aspx" />
                    <div style="clear: both;"></div>

                    <div id="carritoProductsList">
                        <%--AQUI VA LA LISTA DE LOS ITEM DE LOS ARTICULOS PRODUCTOS DEL CARRITO--%>
                        <asp:Repeater ID="PedidolistRepeater" runat="server"
                            OnItemCommand="PedidolistRepeater_ItemCommand"
                            OnItemDataBound="PedidolistRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id='<%# "ItemCarrito_G_" +Eval("ProductoId") %>' class="itemCarrito">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <asp:HiddenField ID="ImagenId" runat="server" Value='<%# Eval("ImagenId") %>' />
                                            <asp:Image ID="ProductImage" runat="server"
                                                CssClass="img-responsive" Width="100" Height="100" />
                                        </div>
                                        <div class="col-xs-7">
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
                                            <a href='<%#"javascript:removeItem(" + Eval("ProductoId") + ")" %>' class="btn redButton" style="border-radius: 100%; padding: 2px 6px;">
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
                    <asp:Panel ID="totalCarritoPanel" CssClass="totalCarrito SumaDelCarrito" runat="server">
                        Total (
                <asp:Label ID="CantItem" runat="server" Text="0" CssClass="CantItem"></asp:Label>
                        items): 
                <span class="precioTotalCarrito">Bs.
                <asp:Label ID="TotalLiteral" runat="server" Text="0" CssClass="total"></asp:Label></span>
                        <a class="btn btn-primary" href="javascript:paso2();" style="float: right;">Siguiente Paso</a>

                    </asp:Panel>
                </div>
                <div id="paso2" style="display: none">
                    <%--  <div class="container">
                <div class="form-group">
                    <h2>Ubicación</h2>
                    <label>Marque su ubicacion para que su producto pueda ser entregado</label>
                    <app:GpsSelector ID="GpsSelectorControl" runat="server" />
                </div>
            </div>--%>
                    <div class="col-md-9">
                        <div class="margen-alrededor">
                            <div class="form-group">
                                <div>
                                    <label>Ciudad</label>
                                    <asp:DropDownList ID="CiudadComboBox" OnDataBound="CiudadComboBox_DataBound" runat="server" CssClass="form-control">
                                        <%--<asp:ListItem Value="0">---Eliga una Opcion---</asp:ListItem>--%>
                                        <asp:ListItem Value="1">Santa Cruz</asp:ListItem>
                                    </asp:DropDownList>
                                    <%-- <asp:ObjectDataSource ID="CiudadDataSource" runat="server"
                            SelectMethod="GetCiudadesByPais"
                            TypeName="Artexacta.App.Clasificadores.BLL.CiudadesBLL">
                            <SelectParameters>
                                <asp:Parameter Type="Int32" Name="idPais" DefaultValue="1" />
                            </SelectParameters>
                        </asp:ObjectDataSource>--%>
                                </div>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CiudadComboBox"
                                        ErrorMessage="Debe seleccionar una ciudad"
                                        ForeColor="Red"
                                        ValidationGroup="Direccion"
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Dirección</label>
                                <asp:TextBox ID="DireccionTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DireccionTextBox"
                                    ErrorMessage="Debe ingresar una ciudad"
                                    ForeColor="Red"
                                    ValidationGroup="Direccion"
                                    Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Ubicación</label>
                                <app:GpsSelector ID="GpsSelectorControl" runat="server" />
                            </div>
                            <div class="validation">
                                <asp:CustomValidator ID="CustomValidator1" runat="server"
                                    ErrorMessage="Debe seleccionar una ubicación en el mapa"
                                    ForeColor="Red"
                                    ClientValidationFunction="validateDirecionGPS"
                                    ValidationGroup="Direccion"
                                    Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="form-group">
                                <label class="fancy-checkbox">
                                    <asp:CheckBox ID="ChkGuardarDireccion" runat="server" />
                                    <span class="btn btn-link">Guardar en Mis Direcciones</span>
                                </label>
                            </div>
                            <div class="text-right">
                                <%--<a id="Pase3" class=" btn-primary btn">Siguiente Paso</a>--%>
                                <asp:LinkButton ID="Paso3Button" CssClass=" btn-primary btn" runat="server" OnClick="Paso3Button_Click" Text="Añadir y Seleccionar Dirección" ValidationGroup="Direccion" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="paso3" style="display: none">
                    <div class="col-md-9">
                        <div class="margen-alrededor">
                            <div class="row no-gutters titleInCart">
                                <div class="formTitle">Datos de facturacion</div>
                                <div class="borderBottom"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Factura a nombre de:</label>
                                        <asp:TextBox CssClass="form-control txtNombreFactura" runat="server" ID="txtNombreFactura" />
                                    </div>
                                    <div class="form-group">
                                        <label>Factura con apellido de:</label>
                                        <asp:TextBox CssClass="form-control txtNombreFactura" runat="server" ID="TextApellido" />
                                    </div>
                                    <div class="form-group">
                                        <label>NIT:</label>
                                        <asp:TextBox ID="txtNit" CssClass="form-control txtNit" runat="server" Text="" />
                                    </div>
                                    <div class="form-group">
                                        <%--<a class="margen btn btn-primary col-md-3 boton-continuar" id="ContinuarAFacturacionButton">Siguiente</a>--%>
                                        <%--                                    <asp:LinkButton ID="ingresar_direccion" runat="server" CssClass="margen btn btn-primary col-md-8 boton-continuar" OnClick="ingresar_direccion_Click" Text="Ingresar direcci&oacute;n para entrega de factura" />--%>
                                        <%--<a class="btn btn-primary col-md-8" href="javascript:cargarPaso4()" style="margin-left: 15px;">Ingresar direcci&oacute;n para entrega de factura</a>--%>
                                        <asp:LinkButton ID="paso4Button" Text="Continuar" CssClass="margen btn btn-primary col-md-3 boton-continuar" runat="server" OnClick="paso4Button_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
                <div id="paso4" style="display: none">
                    <div class="col-md-9">
                        <div class="margen-alrededor">
                            <div class="row no-gutters titleInCart">
                                <div class="formTitle">Modalida de pago</div>
                                <div class="borderBottom"></div>
                            </div>
                            <div class="row no-gutters">
                                <div class="col-md-6">
                                    <asp:RadioButtonList CssClass="radioCuotas" ID="radioCuotas" runat="server">
                                        <asp:ListItem Value="1" Selected="True">
                                        <span><i class="fa fa-credit-card fa-2x" aria-hidden="true"></i></span>
                                        Pago al contado con tarjeta
                                        </asp:ListItem>
                                        <asp:ListItem Value="2">
                                        <span><i class="fa fa-truck fa-2x" aria-hidden="true"></i></span>
                                        Pago personalmente contra entrega
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div>
                                        <asp:LinkButton ID="ModalidaPagoButton" CssClass="btn-primary btn" runat="server" OnClick="ModalidaPagoButton_Click">Seleccionar modalidad de pago</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <asp:HiddenField ID="LoginCookiesIdHiddenField" runat="server" />




    <script>
        $(document).ready(function () {
            //$("#paso2").fadeOut();
            //$("#paso3").fadeOut();
        });
        function miUbicacion() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(coordenadas, errores);
            } else {
                alert('Oops! Tu navegador no soporta geolocalización.');
            }
        }
        function coordenadas(position) {
            lat = position.coords.latitude; /*Guardamos nuestra latitud*/
            lng = position.coords.longitude; /*Guardamos nuestra longitud*/
            var gpsSelector = MapSelector.getMapSelector("<%= GpsSelectorControl.ClientID %>");
            gpsSelector.setMarker(lat, lng);
            //cargarMapa();
        }
        function validateDirecionGPS(sender, args) {
            args.IsValid = $("#<%= GpsSelectorControl.ClientID %>").val() != "";
        }

        function paso1() {
            $("#paso1").fadeIn();
            $("#paso2").fadeOut();
            $("#paso3").fadeOut();
            $("#paso4").fadeOut();
            //$("#detallePaso2").addClass("hidden");
            //$("#detallePaso3").addClass("hidden");
        }
        function paso2() {
            $.ajax({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Carrito.aspx/ExisteUsuarioIniciado") %>",
                //data: "",
                //dataType: 'json',
                type: 'POST',
                async: true,
                contentType: 'application/json; charset=utf-8',
                success: function (mydata) {
                    if (mydata.d == null) {
                        window.location.href = "<%= VirtualPathUtility.ToAbsolute("~/Autentificacion/Login.aspx") %>";
                    } else {
                        $("#<%= LoginCookiesIdHiddenField.ClientID %>").text(mydata.d);
                        $("#paso1").fadeOut();
                        $("#paso2").fadeIn();
                        $("#paso3").fadeOut();
                        $("#detallePaso2").removeClass("hidden");
                        //$("#detallePaso3").addClass("hidden");
                    }
                }
            });
        }
        function paso3() {
            $("#paso1").fadeOut();
            $("#paso2").fadeOut();
            $("#paso3").fadeIn();
            $("#paso4").fadeOut();
            $("#detallePaso2").removeClass("hidden");
            $("#detallePaso3").removeClass("hidden");
        }

        function paso4() {
            $("#paso1").fadeOut();
            $("#paso2").fadeOut();
            $("#paso3").fadeOut();
            $("#paso4").fadeIn();
            $("#detallePaso2").removeClass("hidden");
            $("#detallePaso3").removeClass("hidden");
            $("#detallePaso4").removeClass("hidden");
        }

        <%-- function hacerLacompra() {
            $("<%= GpsSelectorControl.ClientID %>")
        }--%>

    </script>
</asp:Content>

