<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaUnidadMedida.aspx.cs" Inherits="Administracion_Inventario_UnidadMedida_ListaUnidadMedida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Unidad de medidas</h5>
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
                        <asp:LinkButton ID="NewUnidadMedidaButton" runat="server" OnClick="NewUnidadMedidaButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nueva Unidad de Modelo</asp:LinkButton>
                    </p>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <%--<input type="text" class="search-textbox" />--%>
                                <asp:TextBox ID="busquedaUnidadMedidaTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                                <%--<a class="btn-search"><i class="fa fa-search" aria-hidden="true"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaUnidadMedidaGridView" 
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaUnidadMedidaGridView_PageIndexChanging"
                             CssClass="table table-striped" OnRowCommand="ListaUnidadMedidaGridView_RowCommand" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditUnidadButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("UnidadMedidaId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-pencil'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteImageButton" runat="server" CommandName="Eliminar" CssClass="text-danger img-buttons" Text="<i class='fa fa-trash-o'></i>"
                                            CommandArgument='<%# Eval("UnidadMedidaId") %>'
                                            OnClientClick="return confirm('¿Está seguro de eliminar esta Unidad de medida?')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnidadMedidaIdForDisplay" HeaderText="Abreviatura unidad medida" />
                                <asp:BoundField DataField="DescripcionForDisplay" HeaderText="Descripcion" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ninguna Unidad de Medida</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

