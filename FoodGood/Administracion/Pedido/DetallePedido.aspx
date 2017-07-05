<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DetallePedido.aspx.cs" Inherits="Administracion_Pedido_DetallePedido" %>

<%@ Register Src="~/UserControl/AdminOrderDetails/AdminOrderDetails.ascx" TagPrefix="app" TagName="AdminOrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCFI9ELr1vjveXc11xbKa5L_tKMMvD_OHc ">
    </script>
    <%--<script src="Script/google-map.js"></script>--%>
    <script src="../../Script/gpsMapSelector.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Detalle de Pedido
            </h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="ReturnLinkButton" runat="server" CssClass="btn btn-warning"
                        NavigateUrl="~/Administracion/Pedido/ListaPedido.aspx">
                        <i class='fa fa-arrow-left'></i> Ir a la Lista de Pedidos
                    </asp:HyperLink>

                    <%--   <asp:HyperLink ID="AssignmentButton" runat="server" CssClass="btn btn-primary"
                        Visible="false"
                        data-toggle="modal" data-target="#ResponsableModal">
                        <i class="fa fa-user"></i>
                        <asp:Literal ID="PrefixLabel" runat="server" Text="Re-"></asp:Literal>Asignar responsable para Entrega
                    </asp:HyperLink>--%>
                    <asp:LinkButton ID="entregarButton" runat="server" CssClass="text-success img-buttons" Text="<i class='fa fa-check'>Entregar Pedido</i>"
                        OnClientClick="return confirm('¿Está seguro que quiere entregar este Pedido?')" OnClick="entregarButton_Click"></asp:LinkButton>

                    <asp:LinkButton ID="AnularPedidoButton" runat="server" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'>Anular Pedido</i>"
                        OnClientClick="return confirm('¿Está seguro de Anular este Pedido?')" OnClick="AnularPedidoButton_Click"></asp:LinkButton>
                    <%--<app:anulacionpedido id="ControlAnulacion" runat="server" visible="false"
                        urltoredirect="~/Administration/Pedido/DetallePedido.aspx" />--%>
                </div>
            </div>
            <app:AdminOrderDetails ID="OrderDetails" runat="server"></app:AdminOrderDetails>

        </div>
    </div>

    <%--    <div class="modal fade" id="ResponsableModal" tabindex="-1" role="dialog" aria-labelledby="Responsable">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Asignar responsable de Entrega</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Seleccione un Usuario</label>
                                <telerik:RadComboBox ID="ResponsableComboBox" runat="server" ZIndex="8101" Width="100%"
                                    Filter="Contains" EnableLoadOnDemand="true" ShowMoreResultsBox="true"
                                    OnClientSelectedIndexChanged="onUserChanged"
                                    EnableVirtualScrolling="true" EmptyMessage="- Seleccione un responsable -">
                                    <WebServiceSettings Method="GetUsuariosAdministradores" Path="~/Administration/WebServices/ComboBoxWebServices.asmx" />
                                </telerik:RadComboBox>
                            </div>
                            <div class="validation">
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic"
                                    ErrorMessage="Debe seleccionar un responsable"
                                    ValidationGroup="Responsable"
                                    ControlToValidate="ResponsableComboBox">
                                </asp:RequiredFieldValidator>
                            </div>

                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                  
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>--%>

    <asp:HiddenField ID="PedidoIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="SelectedUserId" runat="server" Value="0" />
    <script type="text/javascript">
        function onUserChanged(sender, args) {
            $("#<%= SelectedUserId.ClientID %>").val(sender.get_value());
        }
    </script>
</asp:Content>

