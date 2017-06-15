<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<%@ Register Src="~/UserControl/Paginacion/PagerControl.ascx" TagName="pagercontrol" TagPrefix="app" %>
<%@ Register Src="~/UserControl/Producto/Producto.ascx" TagName="Producto" TagPrefix="app" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="App_Themes/Menu/owl.carousel.css" rel="stylesheet" />
    <link href="App_Themes/Menu/owl.transitions.css" rel="stylesheet" />
    <link href="App_Themes/Menu/prettyPhoto.css" rel="stylesheet" />
    <link href="App_Themes/Menu/main.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="" id="seccion1">
        <section id="main-slider" class="wow fadeInDown seccion1 ">
            <div class="owl-carousel">
                <asp:Repeater runat="server" ID="FamiliaRepeateSlider" OnItemDataBound="FamiliaRepeateSlider_ItemDataBound">
                    <ItemTemplate>
                        <asp:Panel runat="server" CssClass="item" ID="SliderImagen">
                            <asp:HiddenField runat="server" ID="ImagenIdParaFondo" Value='<%#Eval("ImagenId") %>' />
                            <div class="slider-inner">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="carousel-content text-center">
                                                <h2><%# Eval("Descripcion") %></h2>
                                                <asp:HyperLink runat="server" ID="linkFamilia" Text="conocer" CssClass="btn btn-primary" NavigateUrl='<%#"~/Menu.aspx?id="+ Eval("FamiliaId")%>'></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>
    </div>
    <section class="bg-primary" id="about">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                    <asp:Image runat="server" ID="menuImagen" CssClass="menuImagen" ImageUrl="~/img/menu.png" />
                    <h2 runat="server" id="tituloFamilia"></h2>
                </div>
            </div>
        </div>
    </section>

    <section id="panel-Repeater-Menu" class="container">
        <asp:Panel runat="server" BackImageUrl="~/img/Fondo.svg">
            <div class="row">
                <asp:Repeater ID="familiaForIdRepeater" runat="server">
                    <ItemTemplate>
                        <app:Producto runat="server" ID="Producto"
                            productoId='<%# Eval("ProductoId")%>'
                            Titulo='<%# Eval("nombre")%>'
                            Descripcion='<%# Eval("Descripcion")%>'
                            Precio='<%# Eval("Precio")%>'
                            ImagenId='<%# Eval("ImagenId")%>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>

        <div class="row" style="margin-top: 50px;">
            <asp:Panel ID="PagesButtons" CssClass="text-center" runat="server">

                <app:pagercontrol ID="Pager1" runat="server" PageSize="12" OnPageChanged="Pager1_PageChanged" />
                <asp:HiddenField ID="SearchQueryHiddenField" runat="server" />
            </asp:Panel>
        </div>
        <div class="text-center">
            <br />
            <asp:Literal ID="noResult" Text="No se han encontrado resultados." Visible="false" runat="server" /><br />
            <br />
        </div>
    </section>
    <%--    <div class="panelMenu">
    </div>--%>



    <%--<div class="">--%>
    <%--<asp:Image runat="server" ID="imgaenFondoMenu" ImageUrl="~/img/ImgRestaurante/Koala.jpg" CssClass="size-image-menu" />--%>
    <%--  <div class="header-content" style="top: 27%;">
            <div class="header-content-inner">
                <asp:Image runat="server" ID="menuImagen" CssClass="menuImagen" ImageUrl="~/img/menu.png" />
                <hr />
            </div>
        </div>--%>
    <%--</div>--%>
    <asp:HiddenField runat="server" ID="familiaIdHiddenField" Value="0" />

</asp:Content>

