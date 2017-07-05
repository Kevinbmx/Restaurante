using FoodGood.Carrito.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_OrderDetails : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public string CarritoId
    {
        set { CarritoIdHiddenField.Value = value; }
        get { return CarritoIdHiddenField.Value; }
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    cargarPedidoDetalle();
    //}

    protected void DetallePedidoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
            return;

        log.Error("Error selecting order detail from database", e.Exception);
        e.ExceptionHandled = true;
    }

    //public void CargarControlDetalle()
    //{
    //    DetallePedidoGridView.DataBind();
    //}

    protected override void OnPreRender(EventArgs e)
    {
        cargarPedidoDetalle();
    }

    public void cargarPedidoDetalle()
    {
        try
        {
            string cartJson = "";
            if (!string.IsNullOrEmpty(CarritoId) && !CarritoId.Equals("0"))
            {
                cartJson = CarritoBLL.GetCarritoById(CarritoId).Contenido;
            }
            else
            {
                return;
            }
            if (cartJson != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, DatorProductoCarrito> carrito = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(cartJson);
                DetallePedidoGridView.DataSource = carrito.Values;
                DetallePedidoGridView.DataBind();
            }
            else
            {
                return;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    protected void DetallePedidoGridView_DataBound(object sender, EventArgs e)
    {
        try
        {
            //ImagenIdHiddenField.
            //HiddenField ImagenIdHiddenField = (HiddenField)e.GetType().f`FindControl("ImagenIdHiddenField");
            //Image ProductImage = (Image)e.Item.FindControl("ProductImage");
            //int imagenId = Convert.ToInt32(ImagenIdHiddenField.Value);
            //Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
            //if (objImagen == null)
            //{
            //    ProductImage.ImageUrl = "img/ImgRestaurante/noImage.jpg";
            //}
            //else
            //{
            //    ProductImage.ImageUrl = "img/ImgRestaurante/" + objImagen.Titulo;
            //}
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}