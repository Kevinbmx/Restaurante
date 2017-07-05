using FoodGood.Familia;
using FoodGood.Familia.BLL;
using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using FoodGood.Producto;
using FoodGood.Producto.BLL;
using FoodGood.Utilities;
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
public partial class Menu : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int _totalRows = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string strId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(strId))
            {
                familiaIdHiddenField.Value = strId;
                cargarProductoLista();
            }
            cargarFamiliaRepeater();

        }
    }

    private void cargarProductoLista()
    {
        try
        {
            string ordenar = "order by p.[nombre] asc";

            List<Producto> _cache = new List<Producto>();
            string armadoDeQuery = "@familiaId in(" + familiaIdHiddenField.Value + ")";
            string query = consultaSql(armadoDeQuery).SqlQuery();
            _totalRows = ProductoBLL.SearchProductoPaginacion(ref _cache, query, Pager1.PageSize, Pager1.CurrentRow, ordenar);
            familiaForIdRepeater.DataSource = _cache;
            familiaForIdRepeater.DataBind();

            Pager1.TotalRows = _totalRows;
            if (_cache.Count == 0)
            {
                noResult.Visible = true;
                Pager1.Visible = false;
                PagesButtons.Visible = true;
                return;
            }
            noResult.Visible = false;
            Pager1.Visible = true;
            Pager1.BuildPagination();
        }
        catch (Exception ex)
        {
            log.Error("error al cargar la lista de articulos", ex);
            throw ex;
            //return;
        }
    }



    [WebMethod]
    public static string agregarItemCarrito(string itemId)
    {
        try
        {
            string cookieName = "FoodGoodCartId";
            bool cookieExists = HttpContext.Current.Request.Cookies[cookieName] != null;
            if (!cookieExists)
            {
                PedidoUtilities.SetupShoppingCart();
            }
            //bool carritoEnCuota = PedidoUtilities.GetCarritoEnCuotas();
            Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
            //string carritoEnCuota = PedidoUtilities.GetCarritoEnCuotas();

            //if (itemEnCuota == carritoEnCuota)
            //    carrito = PedidoUtilities.GetCarrito();
            //else
            //    carritoEnCuota = itemEnCuota;

            int cantidad = 1;
            DatorProductoCarrito item = null;

            int articuloId = Convert.ToInt32(itemId);
            if (carrito.ContainsKey(itemId))
            {
                item = carrito[itemId];
            }
            else
            {
                //List<ImageFile> MyimageId = ArticuloBLL.GetImagenesIdsArticulo(articuloId);

                Producto objArticulo = ProductoBLL.GetProductoById(articuloId);
                item = new DatorProductoCarrito()
                {
                    ProductoId = articuloId,
                    Nombre = objArticulo.Nombre,
                    Descripcion = objArticulo.Descripcion,
                    UnidadMedidaId = objArticulo.UnidadMedidaId,
                    Cantidad = 0,
                    Precio = Convert.ToDecimal(objArticulo.Precio),
                    SubTotal = 0,
                    Stock = objArticulo.Stock,
                    FamiliaId = objArticulo.FamiliaId,
                    ImagenId = objArticulo.ImagenId
                };

                carrito.Add(itemId, item);
            }


            item.Cantidad = item.Cantidad + cantidad;

            item.SubTotal = item.Cantidad * item.Precio;
            PedidoUtilities.UpdateCarrito(carrito);
            //PedidoUtilities.UpdateCarritoCuotas(carritoEnCuota);

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(carrito);
        }

        catch (Exception ex)
        {
            //throw ex;
            log.Error("Error adding article to shopping cart", ex);
            return "error";
        }
    }


    [WebMethod]
    public static string actualizarCookiesAndHidden(string articuloId, int cantidad, decimal subtotal)
    {
        try
        {
            Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
            if (!articuloId.Equals("0") && cantidad != 0 && subtotal != 0)
            {
                carrito[articuloId].Cantidad = cantidad;
                carrito[articuloId].SubTotal = subtotal;
            }
            PedidoUtilities.UpdateCarrito(carrito);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string carritoSerializado = js.Serialize(carrito);
            return carritoSerializado;
        }
        catch (Exception ex)
        {
            log.Error("Error adding article to shopping cart", ex);
            return "error";
            //throw ex;
        }
    }


    [WebMethod]
    public static string removeItemCart(string id)
    {
        try
        {
            Dictionary<string, DatorProductoCarrito> carrito = PedidoUtilities.GetCarrito();
            carrito.Remove(id);
            PedidoUtilities.UpdateCarrito(carrito);
            return id;
        }
        catch (Exception ex)
        {
            log.Error("Error al remover item del carrito. ", ex);
            return "error";
        }
    }

    [WebMethod]
    public static Imagen obtenerDireccionImagen(string idImagen)
    {
        try
        {
            Imagen objImagen = ImagenBLL.GetImagenById(Convert.ToInt32(idImagen));
            if (objImagen == null)
            {
                return null;
            }
            return objImagen;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaProducto());
        searcher.Query = query;
        return searcher;
    }


    public void cargarFamiliaRepeater()
    {
        try
        {
            List<Familia> ListaFamilia = FamiliaBLL.GetFamiliaListForSearch("");
            FamiliaRepeateSlider.DataSource = ListaFamilia;
            FamiliaRepeateSlider.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void FamiliaRepeateSlider_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HiddenField ImagenIdParaFondo = (HiddenField)e.Item.FindControl("ImagenIdParaFondo");
            Panel SliderImagen = (Panel)e.Item.FindControl("SliderImagen");
            int imagenId = Convert.ToInt32(ImagenIdParaFondo.Value);
            Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
            if (objImagen == null)
            {
                SliderImagen.BackImageUrl = "img/ImgRestaurante/noImage.jpg";
            }
            else
            {
                SliderImagen.BackImageUrl = "img/ImgRestaurante/" + objImagen.Titulo;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void Pager1_PageChanged(int row)
    {
        cargarProductoLista();
    }
}