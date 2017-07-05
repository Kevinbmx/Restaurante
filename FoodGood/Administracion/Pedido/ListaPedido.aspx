<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaPedido.aspx.cs" Inherits="Administracion_Pedido_ListaPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">


    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Pedidos integrada a Modulos</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <asp:TextBox ID="busquedaModuloTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaPedidoModuloGridView" CssClass="table table-striped" OnRowDataBound="ListaPedidoModuloGridView_RowDataBound" AutoGenerateColumns="false"  OnRowCommand="ListaPedidoModuloGridView_RowCommand"
                            AllowPaging="true" PageSize="5" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaPedidoModuloGridView_PageIndexChanging">
                            <Columns>
                                <%--   <asp:TemplateField HeaderText="Nuevo Modulo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="NuevoImageButton" runat="server" CommandName="Nuevo" CommandArgument='<%# Eval("PedidoId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-user-plus' style='color:#1ab394'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="VerImageButton" runat="server" CommandName="Ver" CommandArgument='<%# Eval("PedidoId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-eye'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre" />
                                <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido" />
                                <asp:BoundField DataField="FechaPedidoForDisplay" HeaderText="Fecha Pedido" />
                                <asp:BoundField DataField="Observacion" HeaderText="Estado" />
                                <asp:BoundField DataField="NombreDepartamento" HeaderText="Ciudad" />
                                <asp:BoundField DataField="NombreTipoPago" HeaderText="Tipo Pago" />
                                <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun Modulo</asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>



















</asp:Content>

