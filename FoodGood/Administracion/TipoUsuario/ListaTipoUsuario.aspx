<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaTipoUsuario.aspx.cs" Inherits="Administracion_TipoUsuario_ListaTipoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Tipo Usuarios</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
               <%-- <div class="col-md-12">
                    <p>
                        <asp:LinkButton ID="NewTipoUsuarioButton" runat="server" OnClick="NewTipoUsuarioButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nuevo Modulo</asp:LinkButton>
                    </p>
                </div>--%>
              <%--  <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <asp:TextBox ID="busquedaAreaTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaTipoUsuarioGridView" CssClass="table table-striped" OnRowCommand="ListaTipoUsuarioGridView_RowCommand" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaTipoUsuarioGridView_PageIndexChanging">
                            <Columns>
                             <%--   <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditAreaButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("tipoUsuarioId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteImageButton" runat="server" CommandName="Eliminar" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'></i>"
                                            CommandArgument='<%# Eval("tipoUsuarioId") %>'
                                            OnClientClick="return confirm('¿Está seguro de eliminar esta Tipo De Usuario?')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="DescripcionForDisplay" HeaderText="Descripcion" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorTipoUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun Tipo de Usuario</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

