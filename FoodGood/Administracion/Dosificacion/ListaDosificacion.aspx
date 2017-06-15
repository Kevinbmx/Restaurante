<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ListaDosificacion.aspx.cs" Inherits="Administracion_Dosificacion_ListaDosificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Lista Dosificacion</h5>
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
                        <asp:LinkButton ID="NewDosificacionButton" runat="server" OnClick="NewDosificacionButton_Click" CssClass="btn btn-primary "><i class='fa fa-user-plus'></i> Nueva Dosificacion</asp:LinkButton>
                    </p>
                </div>
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
                        <asp:GridView runat="server" ID="ListaDosificacionGridView" CssClass="table table-striped" OnRowCommand="ListaDosificacionGridView_RowCommand" OnRowDataBound="ListaDosificacionGridView_RowDataBound" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="10" PagerSettings-Position="Bottom" PagerSettings-Mode="Numeric" OnPageIndexChanging="ListaDosificacionGridView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditDosificacionButton" runat="server" CommandName="Editar" CommandArgument='<%# Eval("DosificacionId") %>' CssClass="text-success img-buttons" Text="<i class='fa fa-eye'></i>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" HeaderText="Desde" />
                                <asp:BoundField DataField="Hasta" HeaderText="Hasta" />
                                <asp:BoundField DataField="NumeroAutorizacion" HeaderText="NRO. Autorizacion" />
                                <asp:BoundField DataField="LlaveDosificacion" HeaderText="Llave Dosificacioin" />
                                <asp:BoundField DataField="FechaInicioForDisplay" HeaderText="FechaInicio" />
                                <asp:BoundField DataField="FechaFinalForDisplay" HeaderText="Fecha Final" />
                                <asp:BoundField DataField="FacturaActual" HeaderText="Factura Actual" />
                                <asp:BoundField DataField="Nit" HeaderText="Nit" />
                                <asp:BoundField DataField="Glosa" HeaderText="Glosa" />
                                <asp:BoundField DataField="CboEstado" HeaderText="Estado" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="errorDosificaion" runat="server" Visible="false">
                            <asp:Label runat="server">No se encuentra ninguna Dosificacion</asp:Label>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

