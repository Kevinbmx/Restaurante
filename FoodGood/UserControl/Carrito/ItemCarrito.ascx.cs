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
using FoodGood.Imagen;
using FoodGood.Imagen.BLL;

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
}