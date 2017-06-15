<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrarDosificacion.aspx.cs" Inherits="Administracion_Dosificacion_RegistrarDosificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="ibox float-e-margins">
        <div class="ibox-title">
            <h5>
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Dosificacion" CssClass="title"></asp:Label></h5>
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
                                <label id="desde">Desde:</label>
                                <asp:TextBox ID="desdeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="ErrorDesde" Text="el numero de desde es requirido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="hasta">Hasta:</label>
                                <asp:TextBox ID="HastaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorHasta" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="nuemroAutorizacion">Numero de Autorizacion:</label>
                                <asp:TextBox ID="NumeroAutorizacionTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorNumAutorizacion" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="llaveDosificacion">Llave Dosificacion:</label>
                                <asp:TextBox ID="LlaveDosificacionTextBox" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorLlave" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="fechaIniciolabel">Fecha Inicio:</label>
                                <div class='input-group date' id="datetimepicker1">
                                    <asp:TextBox runat="server" ID="fechaInicio" class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <%--<asp:Calendar runat="server"></asp:Calendar>--%>
                                <%--<asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                <asp:Label runat="server" ID="errorFechaInicio" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label id="fechaFinalLabel">FechaFinal:</label>
                                <div class='input-group date' id="datetimepicker2">
                                    <asp:TextBox runat="server" ID="fechaFinal" class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:Label runat="server" ID="errorFechaFinal" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="facturaActualLabel">Factura Actual:</label>
                                <asp:TextBox ID="FacturaActualTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorFacturaActual" Text="La factura actual es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="nitlabel">Nit:</label>
                                <asp:TextBox ID="NitTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorNit" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="glosaLabel">Glosa:</label>
                                <asp:TextBox ID="GlosaTextBox" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                <asp:Label runat="server" ID="errorGlosa" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label id="EstadoLabel">Estado:</label>
                                <asp:DropDownList runat="server" ID="estadoLista" CssClass="form-control">
                                    <asp:ListItem Value="0" Selected="True">Inhabilitado</asp:ListItem>
                                    <asp:ListItem Value="1">Habilitado</asp:ListItem>
                                </asp:DropDownList>

                                <%--<asp:TextBox ID="EstadoTextBox" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                <asp:Label runat="server" ID="errorEstado" Text="La descripcion es requerido" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                            <div class="text-center" style="margin-top: 15px;">
                                <asp:LinkButton ID="SaveDosificacion" runat="server" CssClass="btn btn-primary" OnClick="SaveDosificacion_Click">
										<i class="fa fa-plus"></i> Crear Dosificacion
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateDosificacionButton" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="UpdateDosificacionButton_Click">
										<i class="fa fa-plus"></i> Actualizar Dosificacion
                                </asp:LinkButton>
                                <asp:HyperLink ID="cancelBoton" runat="server" NavigateUrl="~/Administracion/Dosificacion/ListaDosificacion.aspx" CssClass="btn btn-danger"><i class="fa fa-times"></i> Cancelar</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <asp:HiddenField runat="server" ID="DosificacionIdHiddenField" />
    <%--<asp:HiddenField runat="server" ID="AreaIdHiddenField" />--%>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'DD/MM/YYYY'
            });
            $('#datetimepicker2').datetimepicker({
                format: 'DD/MM/YYYY',
                useCurrent: false //Important! See issue #1075
            });
            $("#datetimepicker1").on("dp.change", function (e) {
                $('#datetimepicker2').data("DateTimePicker").minDate(e.date);
            });
            $("#datetimepicker2").on("dp.change", function (e) {
                $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
            });


            //$('#datetimepicker10').datetimepicker({
            //    viewMode: 'years',
            //    format: 'MM/YYYY'
            //});

        });
    </script>
</asp:Content>

