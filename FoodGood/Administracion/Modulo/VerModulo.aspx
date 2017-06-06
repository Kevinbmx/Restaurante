<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="VerModulo.aspx.cs" Inherits="Administracion_Modulo_VerModulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Modulo por Area</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-warning min-letter"
                        NavigateUrl="~/Administracion/Modulo/ListaModulo.aspx">
                            <i class='fa fa-arrow-left'></i> Ir a la Lista de Modulos
                    </asp:HyperLink>
                </div>
                <%-- <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <asp:TextBox ID="busquedaModuloTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaModuloAreaGridView" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="ListaModuloAreaGridView_RowCommand"
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaModuloAreaGridView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="VerImageButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("moduloId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteImageButton" runat="server" CommandName="Eliminar" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'></i>"
                                            CommandArgument='<%# Eval("moduloId") %>'
                                            OnClientClick="return confirm('¿Está seguro de eliminar este Modulo de tu Area?')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="DescripcionForDisplay" HeaderText="Descripcion" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun Modulo de esta Area</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="AreadeModuloIdHiddenField" runat="server" />
</asp:Content>

