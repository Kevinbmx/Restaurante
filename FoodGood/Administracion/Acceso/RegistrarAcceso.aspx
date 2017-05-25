<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarAcceso.aspx.cs" Inherits="Administracion_Acceso_RegistrarAcceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">


    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>Asignar Accesos</h5>
            <div class="ibox-tools">
                <a class="collapse-link">
                    <i class="fa fa-chevron-up"></i>
                </a>
            </div>
        </div>
        <div class="ibox-content">
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-warning min-letter"
                        NavigateUrl="~/Administracion/Acceso/ListaAcceso.aspx">
                            <i class='fa fa-arrow-left'></i> Ir a la Lista de Acceso
                    </asp:HyperLink>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label id="PedidoLabel">Elegir Area para ver sus modulos: </label>
                        <br />
                        <asp:Repeater runat="server" ID="checkAreaRepeater" OnItemDataBound="checkAreaRepeater_ItemDataBound">
                            <ItemTemplate>
                                <label>
                                    <asp:CheckBox ID="AreaChecbox" Text='<%# Eval("descripcion")%>'
                                        AutoPostBack="true" runat="server" data-id='<%# Eval("areaId")%>' OnCheckedChanged="AreaChecbox_CheckedChanged" CssClass="checkBoxFiltro" Style="color: gray; margin-right: 30px;" />
                                    <asp:HiddenField ID="AreaIdHiddenField" runat="server" Value='<%# Eval("AreaId")%>' />

                                </label>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <asp:DropDownList ID="AreaComboBox" runat="server" CssClass="form-control"
                                Enabled="true">
                            </asp:DropDownList>--%>
                        <%--DataSourceID="tipoUsuarioDataSet"--%>
                        <%--  DataValueField="areaId"
                            DataTextField="descripcion"--%>


                        <%--<asp:ObjectDataSource ID="tipoUsuarioDataSet" runat="server"
                            SelectMethod='GetArea'
                            TypeName="FoodGood.Areas.BLL.AreaBLL"></asp:ObjectDataSource>--%>
                    </div>


                </div>
                <div class="col-md-12">
                    <div class="row" style="margin: 15px 0;">
                        <div class="col-md-5 col-sm-5 col-xs-12 text-center">
                            <asp:Label ID="UsersInLabel" runat="server" CssClass="font-bold" Text="Lista de Modulos"></asp:Label>
                            <asp:ListBox ID="ListaAccesosListBox" runat="server" Height="250px" SelectionMode="Multiple" CssClass="form-control full-width"
                                AutoPostBack="True" OnSelectedIndexChanged="ListaAccesosListBox_SelectedIndexChanged"
                                DataTextField="descripcionForDisplay" DataValueField="moduloId"></asp:ListBox>
                            <asp:LinkButton runat="server" ID="addAllModuloButton" OnClick="addAllModuloButton_Click" Text="Añadir todos los Accesos"></asp:LinkButton>
                            <asp:Label runat="server" ID="errorAcceso" Text="no se encuentra lista de Modulos"></asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-2 col-xs-12">
                            <div class="center-arrows">
                                <p>
                                    <asp:LinkButton ID="AddAccesoButton" runat="server" Text="<i class='fa fa-arrow-right'></i>"
                                        OnClick="AddAccesoButton_Click" ToolTip="Permitir Acceso al Usuario" Enabled="false" />
                                </p>
                                <p>
                                    <asp:LinkButton ID="removeAccesoButton" runat="server" Text="<i class='fa fa-arrow-left'></i>"
                                        OnClick="removeAccesoButton_Click" ToolTip="Remover Acceso del Usuario" Enabled="false" />
                                </p>
                            </div>
                        </div>
                        <div class="col-md-5 col-sm-5 col-xs-12 text-center">
                            <asp:Label ID="UsersNotInLabel" runat="server" CssClass="font-bold" Text="Lista de accesos Permitidos"></asp:Label>
                            <asp:ListBox ID="ListaAccesoPermitidosListBox" runat="server" Height="250px" SelectionMode="Multiple" CssClass="form-control full-width" DataValueField="moduloId" DataTextField="moduloId"
                                AutoPostBack="True" OnSelectedIndexChanged="ListaAccesoPermitidosListBox_SelectedIndexChanged" OnDataBound="ListaAccesoPermitidosListBox_DataBound"></asp:ListBox>
                            <asp:LinkButton runat="server" ID="RemoveAllModuloButton" OnClick="RemoveAllModuloButton_Click" Text="Quitar Todos los Accesos"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="EmployeeRolePanel" runat="server" Visible="true" CssClass="frame">
                                <p>
                                    <h6>
                                        <asp:Label ID="TitleLabel" runat="server" Text="Información del Usuario Seleccionado"
                                            Font-Size="Small" Font-Bold="true"></asp:Label></h6>
                                </p>
                                <p>
                                    <asp:Label ID="nombeUser1Label" runat="server" Text="Nombre de Usuario:"></asp:Label>
                                    <asp:Label ID="NombreUserLabel" runat="server"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="apellido1Label" runat="server" Text="Apellido:"></asp:Label>
                                    <asp:Label ID="Apellidolabel" runat="server"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="tipoUsuario1label" runat="server" Text="Tipo de Usuario:"></asp:Label>
                                    <asp:Label ID="TipoUsuarioLabel" runat="server"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="userEmail1Label" runat="server" Text="Correo Electrónico:"></asp:Label>
                                    <asp:Label ID="UserEmailLabel" runat="server"></asp:Label>
                                </p>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="usuarioIdHiddenField" />
    <asp:HiddenField runat="server" ID="idModulosAsignados" />
    <asp:HiddenField runat="server" ID="idModulosinSeleccionar" />
    <asp:HiddenField runat="server" ID="idModuloParaAsignar" />
    <asp:HiddenField runat="server" ID="idModuloDeleteforAcceso" />
    <asp:HiddenField runat="server" ID="areaIdHiddenFieldForCombo" />
</asp:Content>

