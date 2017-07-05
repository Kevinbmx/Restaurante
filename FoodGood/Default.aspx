<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="header-fondo">
        <div class="header-content fondo-content">
            <div class="header-content-inner">
                <h1 id="homeHeading">BIENVENIDO A FOOGOOD</h1>
                <hr />
                <p>Comer hasta chuparse los dedos</p>
                <%--<a href="#about" class="btn btn-primary btn-xl page-scroll">Ver Menu</a>--%>
            </div>
        </div>
    </div>
    <section class="bg-primary" id="about">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                    <h2 class="section-heading">¿Quieres algo rico? est&aacutes en el lugar</h2>
                    <hr class="light" />
                    <p class="text-faded">Saborearas la mejor comida jamas Imaginada</p>
                    <a href="Menu.aspx" class="page-scroll btn btn-default btn-xl sr-button">Ver Menu</a>
                </div>
            </div>
        </div>
    </section>

    <section id="services">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Servicio</h2>
                    <hr class="primary" />
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa fa-diamond text-primary sr-icons" aria-hidden="true"></i>
                        <h3>Glamour</h3>
                        <p class="text-muted">Te sentiras igual o a&uacuten mejor que en tu casa</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa-cutlery text-primary sr-icons"></i>
                        <h3>Atencion</h3>
                        <p class="text-muted">como jamas Imaginado y al instante</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa-map-marker text-primary sr-icons"></i>
                        <h3>Pide donde estes</h3>
                        <p class="text-muted">quedate y pide que "FOODGOOD" te lo lleva</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa-thumbs-o-up text-primary sr-icons"></i>
                        <h3>Comodidad</h3>
                        <p class="text-muted">Que no querras no irte</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="no-padding" id="Categoria">
        <div class="container-fluid colo-menu">
            <div class="row no-gutter ">
                <asp:Repeater runat="server" ID="FamiliaRepeater">
                    <ItemTemplate>
                        <div class="col-lg-3 col-sm-4">
                            <asp:HyperLink runat="server" ID="FamiliaId" CssClass="portfolio-box" NavigateUrl='<%#"~/Menu.aspx?id="+Eval("familiaId") %>'>
                                <%--<a class="portfolio-box">--%>
                                <asp:Label runat="server" ID="Imagen"></asp:Label>
                                <%--<asp:HiddenField runat="server" ID="imagenlabel" Value='<%#Eval("ImagenId") %>'></asp:HiddenField>--%>
                                <asp:Image runat="server" ID="imagenFamilia" ImageUrl='<%# "~/img/ImageGenerator.aspx?W=500&H=350&tId=" + Eval("ImagenId") %>' CssClass="img-responsive center-block" />
                                <%--<img src='<%# "img/ImgRestaurante/"+ Eval("titulo") %>' class="img-responsive" alt="" />--%>
                                <div class="portfolio-box-caption">
                                    <div class="portfolio-box-caption-content">
                                        <div class="project-category text-faded">
                                            Categoria
                                        </div>
                                        <div class="project-name">
                                            <%# Eval("Descripcion") %>
                                        </div>
                                    </div>
                                </div>
                                <%--</a>--%>
                            </asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <aside class="bg-dark">
        <div class="container text-center">
            <div class="call-to-action">
                <h2>Escoja una de estas deliciosas categorias</h2>
                <%--<a href="http://startbootstrap.com/template-overviews/creative/" class="btn btn-default btn-xl sr-button">Download Now!</a>--%>
            </div>
        </div>
    </aside>

    <section id="contact">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                    <h2 class="section-heading">Contactenos</h2>
                    <hr class="primary" />
                    <p>Haz tu pedido en este Sitio o puedes llamar al este numero de abajo </p>
                </div>
                <div class="col-lg-4 col-lg-offset-2 text-center">
                    <i class="fa fa-phone fa-3x sr-contact"></i>
                    <p>123-456-6789</p>
                </div>
                <div class="col-lg-4 text-center">
                    <i class="fa fa-envelope-o fa-3x sr-contact"></i>
                    <p><a href="mailto:your-email@your-domain.com">foodgood@gmail.com</a></p>
                </div>
            </div>
        </div>
    </section>

    <%-- <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>
    <script src="vendor/scrollreveal/scrollreveal.min.js"></script>
    <script src="vendor/magnific-popup/jquery.magnific-popup.min.js"></script>

    <!-- Theme JavaScript -->
    <script src="js/creative.min.js"></script>--%>
    <%--<script src="Script/jquery-1.10.2.min.js"></script>
    <script src="Script/jquery.min.js"></script>
    <script src="Script/bootstrap.min.js"></script>
    <script src="Script/scrollreveal.min.js"></script>
    <script src="Script/jquery.easing.1.3.js"></script>
    <script src="Script/jquery.magnific-popup.min.js"></script>
    <script src="Script/creative.min.js"></script>--%>
</asp:Content>
