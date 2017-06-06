<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaProducto.aspx.cs" Inherits="Administracion_Inventario_Producto_ListaProducto" %>

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
                <div class="col-md-12">
                    <p>
                        <asp:LinkButton ID="NewProductoButton" runat="server" OnClick="NewProductoButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nuevo Producto</asp:LinkButton>
                    </p>
                </div>
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
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditImageButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("productoId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteImageButton" runat="server" CommandName="Eliminar" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'></i>"
                                            CommandArgument='<%# Eval("productoId") %>'
                                            OnClientClick="return confirm('¿Está seguro de eliminar este producto?')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre del Producto" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="unidadMedidaId" HeaderText="Unidad de Medida" />
                                <asp:BoundField DataField="precio" HeaderText="Precio" />
                                <asp:BoundField DataField="stock" HeaderText="Stock" />
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
    

    <%-- <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div class="demo-container size-wide">
        <div class="header-container"></div>
        <div class="attachment-container">
            <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" CssClass="async-attachment" ID="AsyncUpload1"
                HideFileInput="true"
                AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx" />
        </div>
    </div>--%>
</asp:Content>

