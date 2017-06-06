<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaImagenProducto.aspx.cs" Inherits="Administracion_Inventario_ImagenProducto_ListaImagenProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Productos</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <%--   <div class="col-md-12">
                    <p>
                        <asp:LinkButton ID="NewImaProductoButton" runat="server" OnClick="NewImaProductoButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nuevo Producto</asp:LinkButton>
                    </p>
                </div>--%>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <asp:TextBox ID="busquedaProductoTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaProductosGridView" CssClass="table table-striped"
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaProductosGridView_PageIndexChanging"
                            OnRowCommand="ListaProductosGridView_RowCommand" OnRowDataBound="ListaProductosGridView_RowDataBound" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Añadir" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="AñadirImageButton" runat="server" CommandName="Añadir" CommandArgument='<%# Eval("productoId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="nombre" HeaderText="Nombre del Producto" />
                                <asp:BoundField DataField="familiaId" HeaderText="Tipo Familia" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorProducto" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun Producto</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>

