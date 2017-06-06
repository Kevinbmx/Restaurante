<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ImageProducto.aspx.cs" Inherits="Administracion_Inventario_ImagenProducto_ImageProducto" %>

<%@ Register Src="~/UserControl/Paginacion/PagerControl.ascx" TagName="pagercontrol" TagPrefix="app" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Imagen para Articulo
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
                    <div class="col-md-12">
                        <asp:HyperLink ID="ReturnLinkButton" runat="server" CssClass="btn btn-warning min-letter"
                            NavigateUrl="~/Administracion/Inventario/ImagenProducto/ListaImagenProducto.aspx">
                            <i class='fa fa-arrow-left'></i> Ir a la Lista de Articulo
                        </asp:HyperLink>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <label>Codigo del Producto</label><br />
                            <asp:Literal ID="CodigoLiteral" runat="server"></asp:Literal>
                        </div>

                        <div class="form-group">
                            <label>Nombre del Producto</label><br />
                            <asp:Literal ID="NombreLiteral" runat="server"></asp:Literal>
                        </div>

                        <div id="addNewImage">
                            <div class="form-group">
                                <div id="ImageRecienSubida">
                                    <label>Imagen del articulo</label><br />
                                    <asp:Image ID="SelecteImage" runat="server" Visible="true" Width="100" Height="100" CssClass="preview-image fondo-image" />
                                    <asp:Label runat="server" ID="errorImagen" Text="esta imagen se encuentra insertada" Visible="false"></asp:Label>
                                </div>
                                <div>
                                    <%--aqui va la imagen--%>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnImagen" runat="server" Text="Cargar" OnClick="btnImagen_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:LinkButton ID="SaveImagenButton" runat="server" OnClick="SaveImagenButton_Click"
                                CssClass="btn btn-primary">
                                <i class="fa fa-floppy-o"></i>Guardar
                            </asp:LinkButton>
                            <asp:HyperLink ID="CancelLinkButton" runat="server" CssClass="btn btn-danger"
                                NavigateUrl="~/Administracion/Inventario/ImagenProducto/ListaImagenProducto.aspx">
                                <i class="fa fa-times"></i> Cancelar
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de imagenes del Articulo
            </h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-12" style="text-align: center;">

                    <asp:Repeater runat="server" ID="ImagenProductoRepeater" OnItemCommand="ImagenProductoRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="col-md-3 col-sm-6 altura-content" style="margin-top: 20px;">
                                <div class="row no-gutters text-center">
                                    <div style="width: 150px; height: 150px; margin: 0px auto;">
                                        <asp:Image ID="Imagenes" runat="server" ImageUrl='<%#"~/img/ImgRestaurante/"+ Eval("Titulo") %>' CssClass="img-responsive preview-image fondo-image" />
                                    </div>
                                    <div style="font-weight: bold">
                                        <literal class="text" style="max-width: 100%;"><%# Eval("Titulo") %></literal>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="imagenProductoId"
                                            Text="Eliminar" runat="server"
                                            OnClientClick="return confirm('¿Esta seguro que desas eliminar esta imagen del Producto?')"
                                            CommandName="RemoveImage"
                                            CommandArgument='<%# Eval("ImagenProductoId") %>'
                                            CssClass="btn" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label runat="server" ID="errorImagenProductoRepeater" Text="no se encuentra ninguna imagen de este producto" Visible="false"></asp:Label>
                </div>

            </div>
        </div>
    </div>

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de todas las imagenes relacionadas al Articulo
            </h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-6">
                    <div id="searchBar" style="width: 100%; margin-bottom: 40px;">
                        <asp:TextBox runat="server" ID="txtBuscador" placeholder="Buscador" />
                        <asp:LinkButton ID="BuscadorButton" CssClass="search-button" Text="Buscar" runat="server" OnClick="BuscadorButton_Click" />
                        <asp:HiddenField ID="BuscadorCriterioHF" runat="server" Value="" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align: center;">
                    <asp:Repeater runat="server" ID="ImagenesRepeater" OnItemCommand="ImagenRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="col-md-3 col-sm-6 altura-content" style="margin-top: 20px;">
                                <div class="row no-gutters text-center">
                                    <div style="width: 150px; height: 150px; margin: 0px auto;">
                                        <asp:Image runat="server" ImageUrl='<%#"~/img/ImgRestaurante/"+ Eval("Titulo") %>' CssClass=" img-responsive fondo-image" />
                                    </div>
                                    <div style="font-weight: bold">
                                        <literal class="text" style="max-width: 100%;"><%# Eval("fechaImagen") %></literal>
                                    </div>
                                    <div>
                                        <literal class="text" style="max-width: 100%;"><%# Eval("titulo") %></literal>
                                    </div>
                                    <div>
                                        <asp:LinkButton runat="server" ID="addImageArticulo" CommandName="addImageArticulo" Text="Añadir"
                                            OnClientClick="return confirm('¿Esta seguro que desas añadir esta imagen al articulo?')"
                                            CommandArgument='<%# Eval("ImagenId") %>' CssClass="btn "></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row" style="margin-top: 50px;">
                <asp:Panel ID="PagesButtons" CssClass="text-center" runat="server">

                    <app:pagercontrol ID="Pager1" runat="server" PageSize="12" OnPageChanged="Pager1_PageChanged" />
                    <asp:HiddenField ID="SearchQueryHiddenField" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="ProductoIdHiddenField" runat="server" />
    <asp:HiddenField ID="ImagenIdHiddenField" runat="server" />
    <asp:HiddenField ID="SelectedFileIdHiddenField" runat="server" Value="0" />
</asp:Content>

