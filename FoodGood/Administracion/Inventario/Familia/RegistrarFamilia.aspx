<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarFamilia.aspx.cs" Inherits="Administracion_Inventario_Familia_RegistrarFamilia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
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

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveFamiliaButton" runat="server" CssClass="btn btn-primary" OnClick="SaveFamilia_Click">
										<i class="fa fa-plus"></i> Crear Categoria
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateFamiliaButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateFamiliaButton_Click">
										<i class="fa fa-plus"></i> Actualizar Categoria
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Area/ListaArea.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="FamiliaIdHiddenField" />
</asp:Content>

