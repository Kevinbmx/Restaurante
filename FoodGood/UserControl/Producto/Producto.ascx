<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Producto.ascx.cs" Inherits="UserControl_Producto_Producto" %>

<div class="col-sm-6 col-md-4 size-item-Menu">
    <div class="thumbnail">
        <asp:Image runat="server"  ID="ImagenProducto" />
        <div class="caption">
            <asp:Panel ID="Panel_Titulo_Producto" runat="server">
                <h3>
                    <asp:Literal ID="TituloProducto" runat="server" Text='0'></asp:Literal>
                </h3>
            </asp:Panel>
            <%--<h3>Thumbnail label</h3>--%>
            <asp:Panel ID="PanelDescripcionProducto" runat="server">
                <asp:Label runat="server" ID="DescripcionProducto"> </asp:Label>
            </asp:Panel>
            <%--<p><%# Eval("Descripcion") %></p>--%>

            <asp:Panel ID="PanelPrecioProducto" runat="server">
                Bs.
                <asp:Literal ID="PrecioProducto" runat="server" Text='0'></asp:Literal>
            </asp:Panel>
            <%--<span><%# Eval("Precio") %></span>--%>
            <asp:HyperLink ID="addCartArticleButton" NavigateUrl="#addToCart" CssClass="btn redButton" runat="server" Style="border-radius: 100px; padding: 3px 10px; font-weight: bold;"><i class="fa fa-shopping-cart" aria-hidden="true"></i> Comprar</asp:HyperLink>
            <%--<p><a href="#" class="btn btn-primary" role="button">Button</a> <a href="#" class="btn btn-default" role="button">Button</a></p>--%>
        </div>
    </div>
</div>


<asp:HiddenField runat="server" ID="ImagenIdProducto" Value="0" />
<asp:HiddenField runat="server" ID="productoIdHiddenField" Value="0" />
