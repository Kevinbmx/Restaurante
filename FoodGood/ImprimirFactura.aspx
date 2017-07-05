<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImprimirFactura.aspx.cs" Inherits="ImprimirFactura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div class="ancho-Factura">
            <div id="headFactura" class="head-factura col-md-12">
                <div class="" style="text-align: center; width: 100%;">
                    <asp:Label runat="server" Text="<%$ Resources: InitMasterPage, NombreFactura %>"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="Casa Matriz"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="<%$ Resources: InitMasterPage, DireccionEmpresa %>"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="<%$ Resources: InitMasterPage, TelefonoEmpresa %>"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="Santa Cruz - Bolvia"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="FACTURA ORIGINAL"></asp:Label>
                </div>
            </div>

            <div id="DatoEmpresa" class="dato-factura col-md-12">
                <div class="col-md-6">
                    <asp:Label runat="server" Text="NIT"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="FACTURA No:"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="AUTORIZACION"></asp:Label>
                    <br />
                </div>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="numeroNitLabel" Text="54654654"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="numeroFacturaLabel" Text="00012"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="numeroAurotizacionLabel" Text="154651231"></asp:Label>
                    <br />
                </div>
            </div>


            <div id="datoGlosa" class="dato-cliente col-md-12">
                <div id="glosa" style="margin-bottom: 10px;" class="col-md-12">
                    <asp:Label runat="server" Text="venta al por mayor de una variedad de productos que no reflejen una especialidad(supermercados, distribuido)."></asp:Label>
                </div>


            </div>

            <div id="datoCliente" class="dato-Cliente col-md-12">
                <div class="col-md-6">
                    <asp:Label runat="server" Text="Fecha"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="Señor:"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="Nit/CI"></asp:Label>
                    <br />
                </div>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="fechaCliente" Text="12/03/2017"></asp:Label>
                    <%--    <div style="margin-left: 15px;">
                        Hora:
                    <asp:Label runat="server" Text="12:30"></asp:Label>
                    </div>--%>
                    <br />
                    <asp:Label runat="server" ID="nombreCliente" Text="Kevin Delgadillo Salazar"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="nitCliente" Text="154651231"></asp:Label>
                    <br />
                </div>

            </div>

            <div id="titulodetallePedido" class="detalle-pedido col-md-12">
                <div class="col-md-12">
                    <label>DETALLE</label>
                </div>
                <div class="col-md-12" style="text-align: right;">
                    <div style="display: flex;">
                        <div class="col-md-3"></div>
                        <div class="col-md-2">
                            <label>CANT.</label>
                        </div>
                        <div class="col-md-3">
                            <label>P.UNIT</label>

                        </div>
                        <div class="col-md-4">
                            <label>SUB TOTAL</label>
                        </div>
                    </div>
                </div>
            </div>

            <div id="detallePedido" class="detalle-pedido col-md-12">
                <asp:Repeater runat="server" ID="pedidoRepeater">
                    <ItemTemplate>
                        <div>
                            <div class="col-md-12">
                                <asp:Label runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                <%--<label>patasca</label>--%>
                            </div>
                            <div class="col-md-12">
                                <div style="display: flex;">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2">
                                        <%--<asp:Label runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>--%>
                                        <label><%# Eval("Cantidad") %></label>
                                    </div>
                                    <div class="col-md-3">
                                        <label><%# Eval("PrecioForDisplay") %></label>
                                        <%--<asp:Label runat="server" Text='<%# Eval("PrecioForDisplay") %>'></asp:Label>--%>
                                    </div>
                                    <div class="col-md-4">
                                        <%--<asp:Literal ID="SubtotalLiteral" runat="server" Text=''></asp:Literal>--%>
                                        <label><%# Eval("SubTotalForDisplay") %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--  <div>
                    <div class="col-md-12">
                        <label>patasca</label>
                    </div>
                    <div class="col-md-12">
                        <div style="display: flex;">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label>12</label>
                            </div>
                            <div class="col-md-3">
                                <label>10.00</label>

                            </div>
                            <div class="col-md-4">
                                <label>120.00</label>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-12" style="text-align: right;">
                    Total:  Bs.
                                        <asp:Label ID="totalFacturaLabel" runat="server" Text="120.00"></asp:Label>
                </div>
            </div>

            <div id="detallePrecioPedido" class="detalle-Precio col-md-12">
                <div style="margin-bottom: 30px;">
                    SON:
                <asp:Label runat="server" ID="montoPalabraLabel" Text="CIENTO VEINTE CON 00/100"></asp:Label>
                </div>
                <div>
                    CODIGO CONTROL:
                    <asp:Label runat="server" ID="codigoControlLabel" Text="C1-BR-G5-F2-TR"></asp:Label>
                </div>
                <div>
                    FECHA LIMITE EMISION:
                    <asp:Label runat="server" ID="fechaLimiteEmisionLabel" Text="21/01/2018"></asp:Label>
                </div>
                <div style="text-align: center; margin-top: 20px; margin-bottom: 10px;">
                    <asp:Image ID="ImageQRCode" runat="server"
                        AlternateText="Image text"
                        ImageUrl="~/GeneradoQR/QRGenerator.aspx" />
                    <div style="clear: both;"></div>
                </div>
                <div style="text-align: center; margin-bottom: 15px;">
                    <label>ESTA FACTURA CONTRIBUYE AL DESARROLLO DEL PAIS. EL USO ILICITO DE ESTA SERA DANCIONADO DE ACUERDO A LA LEY</label>
                </div>
                <div style="text-align: center">
                    <label>LEY N* 453 EL PROVEEDOR DEBERA SUMINISTRAR EL SERVICIO EN LAS MODALIDADES Y TERMINOS OFERTADOS O CONVENIDOS</label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
