<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.ascx.cs" Inherits="UserControls_OrderDetails" %>

<asp:GridView ID="DetallePedidoGridView" runat="server" OnDataBound="DetallePedidoGridView_DataBound"
    CssClass="table no-top-line order-items-details table-responsive"
    AutoGenerateColumns="false" GridLines="None">
    <Columns>
        <asp:TemplateField HeaderText="Articulo" ItemStyle-VerticalAlign="Middle">
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-3 col-sm-4 text-center">
                        <asp:Image ID="ProductImage" runat="server" CssClass="product-image" ImageUrl='<%# "~/img/ImageGenerator.aspx?W=80&H=80&tId=" + Eval("ImagenId") %>' AlternateText='<%# Eval("Descripcion") %>' />
                        <%--<asp:HiddenField runat="server" ID="ImagenIdHiddenField" Value='<%#Eval("ImagenId") %>' />--%>
                    </div>
                    <div class="col-md-9 col-sm-8 text-left">
                        <asp:HyperLink ID="ProductLink" runat="server" ToolTip='<%# Eval("Nombre") %>'
                            NavigateUrl='<%# "~/Menu.aspx?id=" + Eval("FamiliaId") %>' Target="_blank"
                            CssClass="product-title">
                            <asp:Literal ID="NameLiteral" runat="server" Text='<%# Eval("Nombre") %>'></asp:Literal><br />
                            <%--<asp:Label ID="CodigoLabel" runat="server" CssClass="label label-default orange-bg" Text='<%# Eval("CodigoExterno") %>'></asp:Label>--%>
                        </asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Precio" ItemStyle-VerticalAlign="Middle">
            <ItemTemplate>
                Bs. 
                <asp:Literal ID="PrecioLiteral" runat="server" Text='<%# Eval("PrecioForDisplay") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Cant." DataField="Cantidad" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="order-item-details-quantity" />
        <asp:TemplateField HeaderText="Sub-Total" ItemStyle-VerticalAlign="Middle">
            <ItemTemplate>
                Bs. 
                <asp:Literal ID="SubtotalLiteral" runat="server" Text='<%# Eval("SubTotalForDisplay") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<%--<asp:ObjectDataSource ID="DetallePedidoDataSource" runat="server"
    TypeName="FoodGood.Pedido.BLL.PedidoBLL"
    SelectMethod="obtnerDatosProducto"
    OnSelected="DetallePedidoDataSource_Selected">
    <SelectParameters>
        <asp:ControlParameter ControlID="CarritoIdHiddenField" Name="pedidoId" Type="Int32" PropertyName="Value" />
    </SelectParameters>
</asp:ObjectDataSource>--%>
<asp:HiddenField ID="CarritoIdHiddenField" runat="server" Value="0" />
