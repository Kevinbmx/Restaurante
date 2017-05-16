<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaUsuario.aspx.cs" Inherits="Administracion_Usuario_ListaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Usuarios</h5>
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
                        <asp:LinkButton ID="NewUsuarioButton" runat="server" OnClick="NewUsuarioButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nuevo Usuario</asp:LinkButton>
                    </p>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <%--<input type="text" class="search-textbox" />--%>
                                <asp:TextBox ID="busquedaUsuarioTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                                <%--<a class="btn-search"><i class="fa fa-search" aria-hidden="true"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaUsuariosGridView" AllowPaging="True" PageSize="10"
                            OnPageIndexChanging="ListaUsuariosGridView_PageIndexChanging"
                            PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" CssClass="table table-striped"
                            AutoGenerateColumns="false" OnRowDataBound="ListaUsuariosGridView_RowDataBound" OnRowCommand="ListaUsuariosGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditImageButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("usuarioId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteImageButton" runat="server" CommandName="Eliminar" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'></i>"
                                            CommandArgument='<%# Eval("usuarioId") %>'
                                            OnClientClick="return confirm('¿Está seguro de eliminar el usuario?')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="NombreForDisplay" HeaderText="Nombre" />
                                <asp:BoundField DataField="ApellidoForDisplay" HeaderText="Apellido" />
                                <asp:BoundField DataField="tipoUsuarioId" HeaderText="tipoUsuario" />
                                <asp:BoundField DataField="email" HeaderText="E-mail" />
                                <asp:BoundField DataField="celular1" HeaderText="celular 1 " />
                                <%--      <asp:BoundField DataField="celular2" HeaderText="cleular 2" />
                                <asp:BoundField DataField="nit" HeaderText="nit" />--%>
                            </Columns>
                            <FooterStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <PagerSettings Mode="NumericFirstLast" />
                        </asp:GridView>
                        <asp:Panel ID="errorUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun Usuario</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

