<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarUsuario.aspx.cs" Inherits="Administracion_Usuario_RegistrarUsuario" %>

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
                                <label id="UserNameLabel">Nombre:</label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorNombre" Text="el nombre es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="ApellidoLabel">Apellido: </label>
                                <asp:TextBox ID="ApellidoTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorApellido" Text="el apellido es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="PasswordLabel" runat="server">Contraseña: </label>
                                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorPassword" Text="La contraseña es requerida" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="ConfirmPasswordLabel" runat="server">Confirme tu Contraseña: </label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorConfirmar" Text="Debe confirmar su contraseña" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="PedidoLabel">Tipo Usuario: </label>
                                <asp:TextBox ID="TipoUsuarioTextBox" runat="server" CssClass="form-control" Text="administrador" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="tipoUsuarioIdHiddenField" runat="server" />
                                <%-- <asp:DropDownList ID="PedidoComboBox" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="PedidoComboBox_SelectedIndexChanged"
                                    DataSourceID="tipoUsuarioDataSet"
                                    DataValueField="tipoUsuarioId"
                                    DataTextField="descripcion">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="tipoUsuarioDataSet" runat="server"
                                    SelectMethod="GetTipoUser"
                                    TypeName="FoodGood.TipoUser.BLL.TipoUsuarioBll"></asp:ObjectDataSource>--%>
                            </div>
                            <div class="form-group">
                                <label id="EmailLabel">Correo Electrónico: </label>
                                <asp:TextBox ID="EmailText" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorEmail" Text="su E-mail es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="CellPhoneLabel">Teléfono Móvil: </label>
                                <asp:TextBox ID="CellPhoneTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorCelulare" Text="su E-mail es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="CellPhoneLabel2">Teléfono Móvil 2: </label>
                                <asp:TextBox ID="CellPhoneTextBox2" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                           <%-- <div class="form-group">
                                <asp:Label ID="numeroNitLabel" runat="server" Text="Numero de Nit:"></asp:Label>
                                <asp:TextBox ID="numeroNitTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>--%>

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveUsersAdmin" runat="server" CssClass="btn btn-primary" OnClick="SaveUsersAdmin_Click">
										<i class="fa fa-plus"></i> Crear Usuario
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateButton_Click">
										<i class="fa fa-plus"></i> Actualizar Usuario
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Usuario/ListaUsuario.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar	</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="UsuarioIdHiddenField" />
</asp:Content>

