<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarFamilia.aspx.cs" Inherits="Administracion_Inventario_Familia_RegistrarFamilia" %>

<%@ Register Src="~/UserControl/Paginacion/PagerControl.ascx" TagName="pagercontrol" TagPrefix="app" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Familia" CssClass="title"></asp:Label></h5>
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
                                <label id="UserNameLabel">Descripcion de Categoria:</label>
                                <asp:TextBox ID="descripcionTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDescripcion" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div id="addNewImage">
                                <div class="form-group">
                                    <div id="ImageRecienSubida">
                                        <label>Imagen de la familia</label><br />
                                        <asp:Image ID="SelecteImage" runat="server" Visible="true" Width="100" Height="100" CssClass="preview-image fondo-image" />
                                        <asp:Label runat="server" ID="errorImagen" Text="esta imagen se encuentra insertada" Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="errorImagenseleccion" Text="elija una imagen o inserte una nueva" Visible="false"></asp:Label>

                                    </div>
                                    <asp:LinkButton runat="server" ID="cancelarImagen" OnClick="cancelarImagen_Click" Text="cancelar Imagen"></asp:LinkButton>
                                    <div id="subirImagen" runat="server">
                                        <%--aqui va la imagen--%>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="btnImagen" runat="server" Text="Cargar" OnClick="btnImagen_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveFamiliaButton" runat="server" CssClass="btn btn-primary" OnClick="SaveFamilia_Click">
										<i class="fa fa-plus"></i> Crear Categoria
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateFamiliaButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateFamiliaButton_Click">
										<i class="fa fa-plus"></i> Actualizar Categoria
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Inventario/Familia/ListaFamilia.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de todas las imagenes
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
                                        <asp:LinkButton runat="server" ID="addImageArticulo" CommandName="addImageAFamilia" Text="Añadir"
                                            OnClientClick="return confirm('¿Esta seguro que desas añadir esta imagen como fondo de de familia?')"
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

    <asp:HiddenField ID="ImagenIdHiddenField" runat="server" />
    <asp:HiddenField runat="server" ID="FamiliaIdHiddenField" />
</asp:Content>

