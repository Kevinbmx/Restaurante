using FoodGood.Carrito.BLL;
using FoodGood.Factura.BLL;
using FoodGood.Pedido;
using FoodGood.Pedido.BLL;
using FoodGood.Venta;
using FoodGood.Venta.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImprimirFactura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarFactura();

        }
    }
    public void cargarFactura()
    {
        int facturaId = Convert.ToInt32(Session["FacturaId"]);
        //int facturaId = 11;
        try
        {
            int ventaId = cargarQRCode(facturaId);

            List<Pedido> listaPedido = PedidoBLL.GetPedidoListForSearch("p.[ventaId]in (" + ventaId + ")");
            string cartJson = "";
            if (!string.IsNullOrEmpty(listaPedido[0].CarritoId))
            {
                cartJson = CarritoBLL.GetCarritoById(listaPedido[0].CarritoId).Contenido;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, DatorProductoCarrito> carrito = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(cartJson);
            pedidoRepeater.DataSource = carrito.Values;
            pedidoRepeater.DataBind();

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private int cargarQRCode(int facturaId)
    {
        string Nit = "";
        string numeroFactura = "";
        string numeroAutorizacion = "";
        string fechapedido = "";
        string montototal = "";
        string importeCreditoFiscal = "";
        string codigocontrol = "";
        string nitcomprador = "";
        string importeICE = "";
        string importeVentaNoGuardada = "";
        string importeNoSujetoCreditoFiscal = "";
        string descuento = "";

        FoodGood.Factura.Factura objfactura = FacturaBLL.GetFacturaById(facturaId);
        Venta objventa = VentaBLL.GetVentaById(objfactura.VentaId);
        Nit = objfactura.Nit;
        numeroFactura = objfactura.Numero;
        numeroAutorizacion = objfactura.CodigoAutorizacion;
        fechapedido = objfactura.Fecha.ToString("yyyyMMdd");
        montototal = objventa.MontoTotal.ToString();
        importeCreditoFiscal = objventa.MontoTotal.ToString();
        codigocontrol = objfactura.CodigoControl;
        nitcomprador = objventa.Nit.ToString();
        importeVentaNoGuardada = "0";
        importeICE = "0";
        importeNoSujetoCreditoFiscal = "0";
        descuento = "0";


        string codeQRArmado = Nit + "|" + numeroFactura + "|" + numeroAutorizacion + "|" + fechapedido + "|" + montototal
            + "|" + importeCreditoFiscal + "|" + codigocontrol + "|" + nitcomprador + "|" + importeICE
            + "|" + importeVentaNoGuardada + "|" + importeNoSujetoCreditoFiscal + "|" + descuento;
        //Nit | numeroFactura | numeroAutorizacion | fechapedido(dd / mm / yyyy) | 
        //montototal | importeCreditoFiscal(montoTotal) | codigocontrol | nitcomprador | 
        //importeICE(“0”) | importeVentaNoGuardada(“0”) | importeNoSujetoCreditoFiscal(if ((montoTotal - montoPagado) > 0.001))| descuento(“0”)
        ImageQRCode.ImageUrl = "~/GeneradoQR/QRGenerator.aspx?qrcode=" + codeQRArmado;

        //lleno los campos de la factura 
        //lleno el precio total
        totalFacturaLabel.Text = Convert.ToString(objventa.MontoTotal);

        //lleno el nombre del cliente,fecha,nit
        nombreCliente.Text = objventa.NombreCliente + " " + objventa.ApellidoCliente;
        nitCliente.Text = Convert.ToString(objventa.Nit);
        fechaCliente.Text = objfactura.Fecha.ToString();

        //llenar dato de la empresa FoodGood
        numeroNitLabel.Text = objfactura.Nit;
        numeroFacturaLabel.Text = "000" + objfactura.Numero;
        numeroAurotizacionLabel.Text = objfactura.CodigoAutorizacion;


        //llenar el monto en palabras
        montoPalabraLabel.Text = objfactura.MontoPalabra;

        //llenar codigo control

        codigoControlLabel.Text = objfactura.CodigoControl;

        fechaLimiteEmisionLabel.Text = objfactura.FechaLimiteEmision.ToShortDateString();

        return objfactura.VentaId;
    }
}