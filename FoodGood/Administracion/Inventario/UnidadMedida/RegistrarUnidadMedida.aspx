<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarUnidadMedida.aspx.cs" Inherits="Administracion_Inventario_UnidadMedida_RegistrarUnidadMedida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Unidad de Medida" CssClass="title"></asp:Label></h5>
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
                                <label id="UserPiezaIdLabel">Abreviatura de medida:</label>
                                <asp:TextBox ID="UnidadMedidaIdTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorAbreviatura" Text="La Abreviatura es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="UserNameLabel">Descripcion:</label>
                                <asp:TextBox ID="descripcionTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDescripcion" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveUnidadMedida" runat="server" CssClass="btn btn-primary" OnClick="SaveUnidadMedida_Click">
										<i class="fa fa-plus"></i> Crear Unidad de Medida
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateUnidadMedidaButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateUnidadMedidaButton_Click">
										<i class="fa fa-plus"></i> Actualizar Area
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="unidadMedidaIdHiddenField" Value="null" />
</asp:Content>

