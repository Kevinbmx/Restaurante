using FoodGood.Carrito.BLL;
using FoodGood.Departamento;
using FoodGood.Dosificacion;
using FoodGood.Dosificacion.BLL;
using FoodGood.Factura;
using FoodGood.Factura.BLL;
using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using FoodGood.PagoCreditoTarjeta;
using FoodGood.PagoCreditoTarjeta.BLL;
using FoodGood.Pedido;
using FoodGood.Pedido.BLL;
using FoodGood.Usuario;
using FoodGood.Usuario.BLL;
using FoodGood.Utilities;
using FoodGood.Venta;
using FoodGood.Venta.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Carrito : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    //public PagoCreditoTarjeta objpagoCredito = new PagoCreditoTarjeta();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarItemArticulo();

            PagoCreditoTarjeta objpagoCredito = verSiDebeAlgunPedido();
            if (objpagoCredito.SaldoPagar != 0)
            {
                MontoPagar.Text = Convert.ToString(objpagoCredito.SaldoPagar);
                //pagaTarjetaButton.Visible = false;
                pagarDeudaButton.Visible = true;
                //if (objpagoCredito.SaldoPagar == 0)
                //{
                //    List<Pedido> listapedido = PedidoBLL.GetPedidoListForSearch("[p].ventaId in(" + objpagoCredito.VentaId + ")");
                //    CargarResumenPedido(listapedido[0].PedidoId);
                //}
            }
            else
            {
                //MontoPagar.Text = TotalLiteral.Text;
                //pagaTarjetaButton.Visible = true;
                pagarDeudaButton.Visible = true;
            }

        }

        //PagoCreditoTarjeta objpago = verSiDebeAlgunPedido();
        //if (objpago != null)
        //{
        //    CalcularPagoConTarjeta(objpago.VentaId);
        //}
        //string url = HttpContext.Current.Request.Url.AbsoluteUri;
        //ImageQRCode.ImageUrl = "~/GeneradoQR/QRGenerator.aspx?qrcode=" + "";
        //CargarResumenPedido(17);
    }


    //private void CargarCarrito()
    //{
    //    try
    //    {
    //        JavaScriptSerializer js = new JavaScriptSerializer();
    //        //string CarritoId = PedidoUtilities.obtenerIdCarrito();
    //        //if (!string.IsNullOrEmpty(cartKey))
    //        //    PedidoUtilities.SearchAndUpdateCarrito(cartKey);

    //        Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
    //        CartHiddenField1.Value = js.Serialize(carrito);

    //        if (carrito.Count > 0)
    //        {
    //            CarritoRepeater.DataSource = carrito.Values;
    //            CarritoRepeater.DataBind();
    //            decimal total = 0;
    //            foreach (var item in carrito.Values)
    //            {
    //                total += item.SubTotal;
    //            }
    //            //CantItem1.Text = Convert.ToString(carrito.Count);
    //            //cantItem2.Text = Convert.ToString(carrito.Count);
    //            //CantItem.Text = Convert.ToString(carrito.Count);
    //            //TotalLiteral1.Text = string.Format("{0:0.00}", total, CultureInfo.InvariantCulture);
    //            //TotalLiteral2.Text = string.Format("{0:0.00}", total, CultureInfo.InvariantCulture);
    //        }
    //        else
    //        {
    //            //carritoTitlesPanel.Visible = false;
    //            carritoListaPanel.Visible = false;
    //            //totalPanel.Visible = false;
    //            //paso2Buttonpanel.Visible = false;
    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "sumarTotal", "sumarTotal();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error("Error al cargar lista de articulos del carrito.", ex);
    //    }
    //}

    public void cargarTotalApagar()
    {
        Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
        decimal total = 0;
        foreach (var item in carrito.Values)
        {
            total += item.SubTotal;
        }
        MontoPagar.Text = total.ToString();
        TotalLiteral.Text = total.ToString();
    }

    public void cargarItemArticulo()
    {
        try
        {
            Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
            JavaScriptSerializer js = new JavaScriptSerializer();
            CartHiddenField.Value = js.Serialize(carrito);
            PedidolistRepeater.DataSource = carrito.Values;
            PedidolistRepeater.DataBind();
            decimal total = 0;
            foreach (var item in carrito.Values)
            {
                total += item.SubTotal;
            }
            CantItem.Text = Convert.ToString(carrito.Count);
            //cantItem2.Text = Convert.ToString(carrito.Count);
            TotalLiteral.Text = total.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture);
            MontoPagar.Text = total.ToString();
            //totalliteral1.Text = total.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture);

        }
        catch (Exception ex)
        {
            log.Error("Error al cargar Item de Articulo", ex);
            return;
        }
    }

    protected void CarritoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HiddenField ImagenIdHiddenField = (HiddenField)e.Item.FindControl("ImagenIdHiddenField");
            Image ProductImage = (Image)e.Item.FindControl("ProductImage");
            int imagenId = Convert.ToInt32(ImagenIdHiddenField.Value);
            Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
            if (objImagen == null)
            {
                ProductImage.ImageUrl = "img/ImgRestaurante/noImage.jpg";
            }
            else
            {
                ProductImage.ImageUrl = "img/ImgRestaurante/" + objImagen.Titulo;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    [WebMethod]
    public static string ExisteUsuarioIniciado()
    {
        try
        {
            string loginCookie = LoginUtilities.ObtenerLoginCookies();

            if (string.IsNullOrEmpty(loginCookie))
            {
                return null;
            }
            return loginCookie;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public static PagoCreditoTarjeta verSiDebeAlgunPedido()
    {
        //bool tieneDeuda = false;
        try
        {
            PagoCreditoTarjeta objPago = new PagoCreditoTarjeta();
            Usuario objUsuario = LoginUtilities.GetUserLogged();
            if (objUsuario != null)
            {
                List<PagoCreditoTarjeta> listaPago = PagoCreditoTarjetaBLL.GetPedidoListUsuarioById(objUsuario.UsuarioId);
                listaPago = listaPago.OrderByDescending(p => p.FechaPago).ToList();
                if (listaPago.Count > 0)
                {
                    //if (listaPago[0].SaldoPagar > 0)
                    //{
                    objPago = listaPago[0];
                    return objPago;
                    //}
                }
            }
            return objPago;
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    //[WebMethod]
    public void hacerElPedidoVentaYfactura(int tipoPago)
    {
        DateTime fechaPedido = DateTime.Now;
        try
        {
            string nombreCliente = txtNombreFactura.Text;
            string apellidoCliente = TextApellido.Text;
            int numNit = Convert.ToInt32(txtNit.Text);
            string montoFormateado = TotalLiteral.Text.Replace(".", ",");
            decimal montoTotal = Convert.ToDecimal(montoFormateado);
            decimal latitud = GpsSelectorControl.Latitud;
            decimal longitud = GpsSelectorControl.Longitud;
            try
            {
                Venta objVenta = new Venta();
                objVenta.NombreCliente = nombreCliente;
                objVenta.ApellidoCliente = apellidoCliente;
                objVenta.Nit = numNit;
                objVenta.MontoTotal = montoTotal;
                objVenta.PagoTotal = montoTotal;
                objVenta.MontoCambio = 0;
                objVenta.MontoDescuento = 0;
                objVenta.FechaPedido = fechaPedido;
                //objVenta.FechaEntrega = null;
                //objVenta.FechaAnulacion = ;
                objVenta.Estado = Resources.InitMasterPage.PendienteEnvio;
                objVenta.Latitud = latitud;
                objVenta.Longitud = longitud;
                int ventaIdInsertado = InsertVenta(objVenta);
                ventaIdHiddenfiel.Value = ventaIdInsertado.ToString();
                //escribirvalor.Text = ventaIdInsertado.ToString();
                try
                {
                    string carritoId = PedidoUtilities.obtenerIdCarrito();
                    Usuario objUsuario = LoginUtilities.GetUserLogged();
                    Pedido objPedido = new Pedido();
                    objPedido.UsuarioId = objUsuario.UsuarioId;
                    objPedido.DepartamentoId = Convert.ToInt32(CiudadComboBox.SelectedValue);
                    objPedido.Direccion = DireccionTextBox.Text;
                    objPedido.NombreCliente = nombreCliente;
                    objPedido.ApellidoCliente = apellidoCliente;
                    objPedido.Nit = numNit;
                    objPedido.FechaPedido = fechaPedido;
                    objPedido.CarritoId = carritoId;
                    objPedido.TipoPago = tipoPago;
                    objPedido.VentaId = ventaIdInsertado;
                    objPedido.MontoTotal = montoTotal;
                    objPedido.Latitud = latitud;
                    objPedido.Longitud = longitud;

                    //completar el llenado del objeto Factura con la ventaId y el codigo de contrcon la lista de disificacion



                    int pedidoId = insertPedido(objPedido);
                    pedidoIdHiddenField.Value = pedidoId.ToString();
                    if (tipoPago == 2)
                    {
                        int FacturaId = hacerFacturaPagaTerminada(ventaIdInsertado);
                        FacturaIdInsertadoHiddenField.Value = FacturaId.ToString();

                        CargarResumenPedido(pedidoId);
                    }
                    if (tipoPago == 1 || tipoPago == 3)
                    {
                        pagarCreditoTarjeta(montoTotal, ventaIdInsertado, objUsuario.UsuarioId, fechaPedido);
                    }
                    CarritoBLL.UpdateCarrtioADeshabilitado(PedidoUtilities.obtenerIdCarrito());
                    PedidoUtilities.borrarCarritoIdCookie();
                    if (tipoPago == 3)
                    {
                        verFactura.Visible = false;
                        //Response.Redirect("~/Carrito.aspx");
                        //return;
                    }

                    //string carritoIdCookie = PedidoUtilities.obtenerIdCarrito;
                    //PedidoBLL.UpdatePedVentFactura(pedidoId);


                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('tu pedido ha sido exitoso ')", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    public int pagarCreditoTarjeta(decimal montoTotal, int ventaIdInsertado, int usuarioId, DateTime fechaPedido)
    {
        int pagoIdInsertado = 0;
        PagoCreditoTarjeta objpago = new PagoCreditoTarjeta();
        decimal PagoCliente = Convert.ToDecimal(MontoPagar.Text.Replace(".", ","));
        decimal saldoaPagar = 0;
        if (PagoCliente < montoTotal)
        {
            saldoaPagar = montoTotal - PagoCliente;
        }
        if (PagoCliente > montoTotal)
        {
            PagoCliente = montoTotal;
        }

        objpago.VentaId = ventaIdInsertado;
        objpago.UsuarioId = usuarioId;
        objpago.FechaPago = fechaPedido;
        objpago.SaldoPagar = saldoaPagar;
        objpago.MontoAPagar = PagoCliente;
        objpago.NombreTarjeta = txtNombreTarjeta.Text;

        string valorTxtTarjeta = txtNumeroTarjeta.Text;
        string cuatroUltimosNumero = valorTxtTarjeta.Substring(15, 4);
        objpago.NumeroTarjeta = "****-****-****-" + cuatroUltimosNumero;
        pagoIdInsertado = PagoCreditoTarjetaBLL.InsertPagoCreditoTarjeta(objpago);
        return pagoIdInsertado;
    }

    public int insertPedido(Pedido objpedido)
    {
        try
        {
            int pedidoId = 0;
            if (objpedido != null)
            {
                pedidoId = PedidoBLL.InsertPedido(objpedido);
            }
            return pedidoId;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int insertFactura(Factura objFactura)
    {
        try
        {
            int facturaId = 0;
            if (objFactura != null)
            {
                facturaId = FacturaBLL.InsertFactura(objFactura);
            }
            return facturaId;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertVenta(Venta objVenta)
    {
        try
        {
            int VentaId = 0;
            if (objVenta != null)
            {
                VentaId = VentaBLL.InsertVenta(objVenta);
            }
            return VentaId;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaFamilia());
        searcher.Query = query;
        return searcher;
    }

    private void CargarResumenPedido(int pedidoId)
    {
        try
        {
            Pedido objPedido = PedidoBLL.GetPedidoById(pedidoId);
            Usuario objusuario = UsuariosBLL.GetUserById(objPedido.UsuarioId);

            string root = HttpContext.Current.Request.Url.Scheme + "://" +
                HttpContext.Current.Request.Url.Authority +
                HttpContext.Current.Request.ApplicationPath;
            VerOrdenLink.NavigateUrl = root + "/Account/OrderDetails.aspx?oId=" + objPedido.PedidoId;
            List<Departamento> listaDepartamento = DepartamentoBLL.GetDepartamento();
            for (int i = 0; i < listaDepartamento.Count; i++)
            {
                if (listaDepartamento[i].DepartamentoId == objPedido.DepartamentoId)
                {
                    CiudadResumenLiteral.Text = listaDepartamento[i].NombreDepartamento;
                }
            }
            CorreoResumenCarrito.Text = objusuario.Email;

            DireccionResumenLiteral.Text = objPedido.Direccion;
            RazonSocialResumenLiteral.Text = objPedido.NombreCliente + " " + objPedido.ApellidoCliente;
            NitResumenLiteral.Text = Convert.ToString(objPedido.Nit);

            DetallePedidoResumen_uc.CarritoId = objPedido.CarritoId;
            DetallePedidoResumen_uc.cargarPedidoDetalle();
            //cargarQRCode(Convert.ToInt32(FacturaIdInsertadoHiddenField.Value));
            //montoLetra.Text = letraMonto;
            totalResumenLabel.Text = objPedido.MontoTotal.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "cargarPasoFinal", "javascript:Ultimopaso();", true);

        }
        catch (Exception ex)
        {
            log.Error("Error al cargar el detelle del pedido hecho", ex);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {

        //itemCarrito.cargarItemArticulo();
        //Session["RefreshCart"] = null;
    }


    protected void PedidolistRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void PedidolistRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //try
        //{
        //    HiddenField ImagenId = (HiddenField)e.Item.FindControl("ImagenId");
        //    Image ProductImage = (Image)e.Item.FindControl("ProductImage");
        //    int imagenId = Convert.ToInt32(ImagenId.Value);
        //    Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
        //    if (objImagen == null)
        //    {
        //        //imagenlabel.Text = "<img src='img/ImgRestaurante/noImage.jpg' class='img-responsive' alt='no Imagen'/>";
        //        ProductImage.ImageUrl = "~/img/ImgRestaurante/noImage.jpg";
        //        ProductImage.AlternateText = "no hay imagen";
        //    }
        //    else
        //    {
        //        ProductImage.ImageUrl = "~/img/ImgRestaurante/" + objImagen.Titulo;
        //        ProductImage.AlternateText = objImagen.Titulo;
        //        //imagenlabel.Text = "<img src='img/ImgRestaurante/" + objImagen.Titulo + "' class='img-responsive' alt='" + objImagen.Titulo + "'/>";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }



    protected void Paso3Button_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "cargarPaso3", "javascript:paso3();", true);
    }

    protected void CiudadComboBox_DataBound(object sender, EventArgs e)
    {
        //CiudadComboBox.Items.Clear();
        //CiudadComboBox.Items.Insert(0, new ListItem("SANTA CRUZ", "3"));
        //CiudadComboBox.Items.Insert(0, new ListItem("-- Seleccione una Ciudad --", ""));
    }

    protected void valorLogin_Click(object sender, EventArgs e)
    {
        //escribirvalor.Text = LoginUtilities.ObtenerLoginCookies();
        //escribirvalor.Text = TotalLiteral.Text;
    }

    protected void paso4Button_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "cargarPaso3", "javascript:paso4();", true);
    }



    protected void ModalidaPagoButton_Click(object sender, EventArgs e)
    {
        if (radioCuotas.SelectedValue.Equals("1"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pagoTarjeta", "javascript:pagoTarjeta();", true);
            cargarTotalApagar();
            modalidadPagoId.Value = "1";
            MontoPagar.Enabled = false;
        }
        else if (radioCuotas.SelectedValue.Equals("2"))
        {
            hacerElPedidoVentaYfactura(2);
        }
        else if (radioCuotas.SelectedValue.Equals("3"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "cargarPaso3", "javascript:pagoTarjeta();", true);
            modalidadPagoId.Value = "3";
            cargarTotalApagar();
            MontoPagar.Enabled = true;

        }
    }


    public void CalcularPagoConTarjeta(int ventaId)
    {
        List<PagoCreditoTarjeta> listaPago = PagoCreditoTarjetaBLL.GetPedidoListForSearch("[p].[venta] IN(" + ventaId + ")");
        listaPago = listaPago.OrderByDescending(p => p.FechaPago).ToList();
        if (listaPago.Count >= 0)
        {
            MontoPagar.Text = Convert.ToString(listaPago[0].SaldoPagar);
        }
        else
        {
            MontoPagar.Text = TotalLiteral.Text.Replace(".", ",");

        }
    }

    public int hacerFacturaPagaTerminada(int ventaId)
    {
        int facturaId = 0;
        List<FoodGood.Factura.Factura> listaFactura = FacturaBLL.GetFacturaListForSearch("");
        List<Dosificacion> listaDosificacion = DosificacionBLL.GetCarritoListForSearch("");
        FoodGood.Factura.Factura objFactura = new FoodGood.Factura.Factura();
        Venta objventa = VentaBLL.GetVentaById(ventaId);
        listaDosificacion = listaDosificacion.OrderByDescending(p => p.DosificacionId).ToList();
        string numeroFacturaString = "";
        if (listaFactura != null && listaFactura.Count > 0)
        {
            listaFactura = listaFactura.OrderByDescending(p => p.FacturaId).ToList();
            int numeroFactura = Convert.ToInt32(listaFactura[0].Numero) + 1;
            if (numeroFactura <= listaDosificacion[0].Hasta)
            {
                numeroFacturaString = Convert.ToString(numeroFactura);
                objFactura.Numero = numeroFacturaString;
                objFactura.Nombre = Resources.InitMasterPage.NombreFactura;
                objFactura.Nit = Resources.InitMasterPage.Nit;
                objFactura.Fecha = objventa.FechaPedido;
                objFactura.FechaLimiteEmision = listaDosificacion[0].FechaFinal;
                objFactura.CodigoAutorizacion = listaDosificacion[0].NumeroAutorizacion;
                //me faltaria llenar el codigo de control y la ventaId
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('tu pedido no pudo ser exitoso intentelo mas tarde.Gracias ')", true);
                return facturaId;
            }
        }
        else
        {
            int numeroFactura = Convert.ToInt32(listaDosificacion[0].Desde) + 1;
            numeroFacturaString = Convert.ToString(numeroFactura);
            objFactura.Numero = numeroFacturaString;
            objFactura.Nombre = Resources.InitMasterPage.NombreFactura;
            objFactura.Nit = Resources.InitMasterPage.Nit;
            objFactura.Fecha = objventa.FechaPedido;
            objFactura.FechaLimiteEmision = listaDosificacion[0].FechaFinal;
            objFactura.CodigoAutorizacion = listaDosificacion[0].NumeroAutorizacion;
            //me faltaria llenar el codigo de control y la ventaId
        }
        objFactura.VentaId = ventaId;
        string codigoControlGenerador = CodigoControl.generateControlCode(listaDosificacion[0].NumeroAutorizacion,
            objFactura.Numero, objFactura.Nit,
            objventa.FechaPedido.ToString("yyyyMMdd"),
            Convert.ToString(objventa.MontoTotal),
            listaDosificacion[0].LlaveDosificacion);
        string letraMonto = NumeroALetra.ConvertirNumeroAPalabras(objventa.MontoTotal);
        objFactura.MontoPalabra = letraMonto;
        objFactura.CodigoControl = codigoControlGenerador;
        facturaId = FacturaBLL.InsertFactura(objFactura);
        return facturaId;
    }


    protected void verFactura_Click(object sender, EventArgs e)
    {
        Session["FacturaId"] = FacturaIdInsertadoHiddenField.Value;
        Response.Redirect("~/ImprimirFactura.aspx");
    }

    protected void pagaTarjetaButton_Click(object sender, EventArgs e)
    {
        int modoPagoId = Convert.ToInt32(modalidadPagoId.Value);
        hacerElPedidoVentaYfactura(modoPagoId);
    }

    //protected void verNuevoMonto_Click(object sender, EventArgs e)
    //{

    //    txtnuevomonto.Text = MontoPagar.Text;
    //}

    protected void pagarDeudaButton_Click(object sender, EventArgs e)
    {
        try
        {
            int modoPagoId = 0;
            bool seguir = false;
            if (!string.IsNullOrEmpty(modalidadPagoId.Value))
            {
                modoPagoId = Convert.ToInt32(modalidadPagoId.Value);
                hacerElPedidoVentaYfactura(modoPagoId);
                seguir = true;
                //Response.Redirect("~/Carrito.aspx");
                //return;
            }

            PagoCreditoTarjeta objpagoCredito = verSiDebeAlgunPedido();
            if (objpagoCredito.SaldoPagar == 0)
            {
                if (seguir)
                {
                    int FacturaId = hacerFacturaPagaTerminada(Convert.ToInt32(ventaIdHiddenfiel.Value));
                    FacturaIdInsertadoHiddenField.Value = FacturaId.ToString();
                    CargarResumenPedido(Convert.ToInt32(pedidoIdHiddenField.Value));
                    verFactura.Visible = true;
                    return;
                }
            }
            else
            {
                if (!seguir)
                {
                    int pagoId = pagarCreditoTarjeta(objpagoCredito.SaldoPagar, objpagoCredito.VentaId, objpagoCredito.UsuarioId, DateTime.Now);
                    List<PagoCreditoTarjeta> listaPago = PagoCreditoTarjetaBLL.GetPedidoListForSearch("[p].pagoId in(" + pagoId + ")");
                    listaPago = listaPago.OrderByDescending(p => p.FechaPago).ToList();
                    if (listaPago[0].SaldoPagar == 0)
                    {
                        FacturaIdInsertadoHiddenField.Value = Convert.ToString(hacerFacturaPagaTerminada(listaPago[0].VentaId));
                        List<Pedido> listaPedido = PedidoBLL.GetPedidoListForSearch("[p].ventaId in(" + listaPago[0].VentaId + ")");
                        CargarResumenPedido(listaPedido[0].PedidoId);
                        verFactura.Visible = true;
                        return;
                    }
                    else
                    {
                        Response.Redirect("~/Carrito.aspx");
                    }

                }
                else
                {
                    Response.Redirect("~/Carrito.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}