<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaAcceso.aspx.cs" Inherits="Administracion_Acceso_ListaAcceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista de Accesos</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <%--  <div class="col-md-12">
                    <p>
                        <asp:LinkButton ID="NewAreaButton" runat="server" OnClick="NewAreaButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nuevo Modulo</asp:LinkButton>
                    </p>
                </div>--%>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="search-form">
                                <%--<input type="text" class="search-textbox" />--%>
                                <asp:TextBox ID="busquedaAccesoTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="busquedaBtn" CssClass="btn-search" runat="server" OnClick="busquedaBtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                                <%--<a class="btn-search"><i class="fa fa-search" aria-hidden="true"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="ListaAccesoGridView" CssClass="table table-striped" OnRowCommand="ListaAccesoGridView_RowCommand" OnRowDataBound="ListaAccesoGridView_RowDataBound" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaAccesoGridView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditAreaButton" runat="server" CommandName="Ver" CommandArgument='<%# Eval("usuarioId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-eye'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NombreForDisplay" HeaderText="Nombre de Usuario" />
                                <asp:BoundField DataField="email" HeaderText="E-mail" />
                                <asp:BoundField DataField="usuarioId" HeaderText="Cantidad de acceso" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorUsuario" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ningun acceso</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>





</asp:Content>

