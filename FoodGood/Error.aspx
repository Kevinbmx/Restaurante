<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <section class="no-padding" id="Categoria">
        <div class="container-fluid colo-menu">
            <div class="row no-gutter ">
                <asp:Repeater runat="server" ID="FamiliaRepeater">
                    <ItemTemplate>
                        <div class="col-lg-4 col-sm-6">
                            <a class="portfolio-box">
                                <img src="img/Fondo4.jpg" class="img-responsive" alt="" />
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
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

</asp:Content>

