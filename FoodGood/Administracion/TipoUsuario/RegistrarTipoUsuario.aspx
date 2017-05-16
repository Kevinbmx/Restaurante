<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarTipoUsuario.aspx.cs" Inherits="Administracion_TipoUsuario_RegistrarTipoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    
    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Tipo Usuario" CssClass="title"></asp:Label></h5>
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
                                <label id="UserNameLabel">Tipo Usuario:</label>
                                <asp:TextBox ID="descripcionTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDescripcion" Text="El tipo Usuario es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveTipoUsuairio" runat="server" CssClass="btn btn-primary" OnClick="SaveTipoUsuairio_Click">
										<i class="fa fa-plus"></i> Crear Area
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateTipoUsuarioButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateTipoUsuarioButton_Click">
										<i class="fa fa-plus"></i> Actualizar Area
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/TipoUsuario/ListaTipoUsuario.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="TiopUsuarioidHiddenField" />
</asp:Content>

