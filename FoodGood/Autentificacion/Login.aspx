<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Autentificacion_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="header-fondo">
        <div class="fondo-login col-md-4">
            <div>
                <h2>Ingresar</h2>
                <asp:TextBox runat="server" ID="UsuarioTxt" CssClass="form-control" placeholder="ingrese su correo"></asp:TextBox>
                <asp:Label runat="server" Text="ingrese su E-mail" ID="errorUsuario" Visible="false"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="PasswordTxt" TextMode="Password" placeholder="contraseña" CssClass="form-control"></asp:TextBox>
                <asp:Label runat="server" Text="ingrese su Contraseña" ID="ErrorPassword" Visible="false"></asp:Label>
                <br />
                <div>
                    <asp:LinkButton runat="server" ID="ingresarBotton" OnClick="ingresarBotton_Click" Text="Ingresar" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

