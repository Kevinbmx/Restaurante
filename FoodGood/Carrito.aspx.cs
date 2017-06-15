using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using FoodGood.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Carrito : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarItemArticulo();
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
        try
        {
            HiddenField ImagenId = (HiddenField)e.Item.FindControl("ImagenId");
            Image ProductImage = (Image)e.Item.FindControl("ProductImage");
            int imagenId = Convert.ToInt32(ImagenId.Value);
            Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
            if (objImagen == null)
            {
                //imagenlabel.Text = "<img src='img/ImgRestaurante/noImage.jpg' class='img-responsive' alt='no Imagen'/>";
                ProductImage.ImageUrl = "~/img/ImgRestaurante/noImage.jpg";
                ProductImage.AlternateText = "no hay imagen";
            }
            else
            {
                ProductImage.ImageUrl = "~/img/ImgRestaurante/" + objImagen.Titulo;
                ProductImage.AlternateText = objImagen.Titulo;
                //imagenlabel.Text = "<img src='img/ImgRestaurante/" + objImagen.Titulo + "' class='img-responsive' alt='" + objImagen.Titulo + "'/>";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void Pase3_Click(object sender, EventArgs e)
    {

    }

    protected void CiudadComboBox_DataBound(object sender, EventArgs e)
    {
        CiudadComboBox.Items.Clear();
        CiudadComboBox.Items.Insert(0, new ListItem("SANTA CRUZ", "3"));
        CiudadComboBox.Items.Insert(0, new ListItem("-- Seleccione una Ciudad --", ""));
    }
}