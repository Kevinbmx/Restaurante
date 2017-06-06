<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarProducto.aspx.cs" Inherits="Administracion_Inventario_Producto_RegistrarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Producto" CssClass="title"></asp:Label></h5>
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
                            <div class="form-group">
                                <label id="ProductoNameLabel">Nombre del producto:</label>
                                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorNombre" Text="el nombre es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="DescipcionLabel">Descripcion del:</label>
                                <asp:TextBox ID="DescripcionTextBox" runat="server" MaxLength="3000" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDescripcion" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="UnidadMedidaLabel">Unidad Medida: </label>
                                <asp:DropDownList ID="UnidadMedidaComboBox" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label id="precioLabel">Precio:</label>
                                <asp:TextBox ID="PrecioTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorPrecio" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="stockLabel">Stock:</label>
                                <asp:TextBox ID="stockTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorStock" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="FamiliaLabel">Categoria: </label>
                                <asp:DropDownList ID="FamiliaComboBox" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveProducto" runat="server" CssClass="btn btn-primary" OnClick="SaveProducto_Click">
										<i class="fa fa-plus"></i> Crear Producto
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateProductoButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateProductoButton_Click">
										<i class="fa fa-plus"></i> Actualizar Producto
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Inventario/Producto/ListaProducto.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="ProductoIdHiddenField" />
</asp:Content>

