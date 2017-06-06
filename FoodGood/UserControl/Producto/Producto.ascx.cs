using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Producto_Producto : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        cargarImagen();
        addCartArticleButton.NavigateUrl = "javascript:addItem(" + productoId + ")";

    }

    public string Titulo
    {
        set { TituloProducto.Text = value; }
        get { return TituloProducto.Text; }
    }

    public string Descripcion
    {
        set { DescripcionProducto.Text = value; }
        get { return DescripcionProducto.Text; }
    }
    public string Precio
    {
        set { PrecioProducto.Text = value; }
        get { return PrecioProducto.Text; }
    }



    public int ImagenId
    {
        set { ImagenIdProducto.Value = value.ToString(); }
        get
        {
            int value = 0;
            try
            { value = Convert.ToInt32(ImagenIdProducto.Value); }
            catch (Exception ex)
            {
                log.Error("Error al convertir ImagenIdProducto.Value a un valor entero", ex);
            }
            return value;
        }
    }

    public int productoId
    {
        set { productoIdHiddenField.Value = value.ToString(); }
        get
        {
            int value = 0;
            try
            { value = Convert.ToInt32(productoIdHiddenField.Value); }
            catch (Exception ex)
            {
                log.Error("Error al convertir productoIdHiddenField.Value a un valor entero", ex);
            }
            return value;
        }
    }



    public void cargarImagen()
    {
        try
        {
            if (ImagenId != 0 || !ImagenId.Equals(""))
            {
                Imagen objImagen = ImagenBLL.GetProductoById(ImagenId);
                if (objImagen == null)
                {
                    ImagenProducto.ImageUrl = "~/img/ImgRestaurante/noImage.jpg";
                }
                else
                {
                    ImagenProducto.ImageUrl = "~/img/ImgRestaurante/" + objImagen.Titulo;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}