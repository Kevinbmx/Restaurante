using log4net;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Globalization;
using FoodGood.Utilities;

public partial class UserControls_Carrito_ItemCarrito : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    //private string addCartString = "addCart", buyNowString = "buyNow", byCuotesString = "byCuotes";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarItemArticulo();
        }
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
            cantItem2.Text = Convert.ToString(carrito.Count);
            TotalLiteral.Text = total.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture);

        }
        catch (Exception ex)
        {
            log.Error("Error al cargar Item de Articulo", ex);
            return;
        }
    }

    protected void PedidolistRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //if (e.CommandName == "RemoveFromCart")
        //{
        //    try
        //    {
        //        string articuloId = e.CommandArgument.ToString();
        //        Dictionary<string, ItemCarrito> carrito = PedidoUtilities.GetCarrito();
        //        carrito.Remove(articuloId);
        //        PedidoUtilities.UpdateCarrito(carrito, false);
        //        Response.Redirect("Productos.aspx");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error al cargar los pedido ", ex);
        //        return;
        //    }
        //}

    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    cargarItemArticulo();

    //}

    //protected void UpdateCartButton_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        cargarItemArticulo();
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error("Error updating shopping cart", ex);
    //    }
    //}

    //[WebMethod]
    //public static void UpdateCart(Dictionary<string, ItemCarrito> carrito, bool enCuotas)
    //{
    //    PedidoUtilities.UpdateCarrito(carrito, enCuotas);
    //}

    //private void LoadRepeater(Dictionary<string, ItemCarrito> carrito)
    //{
    //    //if (carrito == null)
    //    //    carrito = PedidoUtilities.GetCarrito();
    //    //decimal total = 0;
    //    //PedidolistRepeater.DataSource = carrito.Values;
    //    //PedidolistRepeater.DataBind();
    //    //foreach (var item in carrito.Values)
    //    //{
    //    //    total += item.SubTotal;
    //    //}
    //    //TotalLiteral.Text = total.ToString();
    //    //JavaScriptSerializer js = new JavaScriptSerializer();
    //    //CartHiddenField.Value = js.Serialize(carrito);
    //    //bool visible = carrito.Count > 0;
    //    //Steps.Visible = visible;
    //    //OrderActions.Visible = visible;
    //    //ActionsShoppingCart.Visible = visible;
    //}

    protected void PedidolistRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //bool carritoEnCuota = PedidoUtilities.GetCarritoEnCuotas();

        //Panel precioPanel = (Panel)e.Item.FindControl("precioPanel");
        //Panel subtotalPanel = (Panel)e.Item.FindControl("subtotalPanel");
        //Panel cuotasPanel = (Panel)e.Item.FindControl("CuotaPanel");

        ////Panel cantidadProducto = (Panel)e.Item.FindControl("CantidadProductoPanel");
        //Panel TextboxPanel = (Panel)e.Item.FindControl("TextboxPanel");

        //if (carritoEnCuota)
        //{
        //    precioPanel.Visible = false;
        //    subtotalPanel.Visible = true;
        //    cuotasPanel.Visible = true;
        //    //cantidadProducto.Visible = true;
        //    TextboxPanel.Visible = true;
        //    totalCarritoPanel.Visible = false;
        //}
        //else
        //{
        //    cuotasPanel.Visible = false;
        //    //cantidadProducto.Visible = false;
        //}
    }
}