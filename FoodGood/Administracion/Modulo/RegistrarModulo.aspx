<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarModulo.aspx.cs" Inherits="Administracion_Modulo_RegistrarModulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Usuario" CssClass="title"></asp:Label></h5>
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
                                <label id="PedidoLabel">Area: </label>
                                <asp:DropDownList ID="AreaComboBox" runat="server" CssClass="form-control"
                                    DataSourceID="tipoUsuarioDataSet"
                                    DataValueField="areaId"
                                    DataTextField="descripcion">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="tipoUsuarioDataSet" runat="server"
                                    SelectMethod='GetArea'
                                    TypeName="FoodGood.Areas.BLL.AreaBLL"></asp:ObjectDataSource>
                            </div>

                            <div class="form-group">
                                <label id="UserNameLabel">Descripcion:</label>
                                <asp:TextBox ID="descripcionTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDescripcion" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveModulo" runat="server" CssClass="btn btn-primary" OnClick="SaveModulo_Click">
										<i class="fa fa-plus"></i> Crear Modulo
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateModuloButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateModuloButton_Click">
										<i class="fa fa-plus"></i> Actualizar Modulo
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Modulo/ListaModulo.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="ModuloIdHiddenField" />
</asp:Content>

