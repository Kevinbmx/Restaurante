using FoodGood.Imagen;
using FoodGood.ImagenProducto;
using FoodGood.Producto;
using FoodGood.Imagen.BLL;
using FoodGood.ImagenProducto.BLL;
using FoodGood.Producto.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_ImagenProducto_ImageProducto : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int _totalRows = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            CargarProducto();
            cargarImagenesRepeateLista();
        }
    }
    private void CargarProducto()
    {
        if (string.IsNullOrEmpty(ProductoIdHiddenField.Value))
        {
            Response.Redirect("~/Administracion/Inventario/ImagenProducto/ListaImagenProducto.aspx");
            return;
        }
        try
        {
            Producto objProducto = ProductoBLL.GetProductoById(Convert.ToInt32(ProductoIdHiddenField.Value));
            CodigoLiteral.Text = objProducto.ProductoId.ToString();
            NombreLiteral.Text = objProducto.Nombre;

            cargarListaImagenesDelProducto(ProductoIdHiddenField.Value);
        }
        catch (Exception ex)
        {
            log.Error("Error al cargar caracteristica del articulo ", ex);
        }
    }

    public void cargarListaImagenesDelProducto(string productoId)
    {
        try
        {
            List<ImagenProducto> listaImgProducto = ImagenProductoBLL.GetImagenProductoById(Convert.ToInt32(productoId));
            if (listaImgProducto.Count == 0)
            {
                errorImagenProductoRepeater.Visible = true;
            }
            else
            {
                errorImagenProductoRepeater.Visible = false;
            }
            ImagenProductoRepeater.DataSource = listaImgProducto;
            ImagenProductoRepeater.DataBind();
        }
        catch (Exception ex)
        {
            log.Error("no se puedo obtener la lista de imagenes del producto con el Id \"" + productoId + "\"");
            throw ex;
        }
    }


    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaImagen());
        searcher.Query = query;
        return searcher;
    }


    private void ProcessSessionParameters()
    {
        if (Session["ProductoId"] != null && !string.IsNullOrEmpty(Session["ProductoId"].ToString()))
        {
            ProductoIdHiddenField.Value = Session["ProductoId"].ToString();
        }
        Session["ProductoId"] = null;
    }

    protected void btnImagen_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            Imagen objImagen = new Imagen();
            string tituloImagen = FileUpload1.PostedFile.FileName;
            string armadoDeQuery = "(@titulo \"" + tituloImagen + "\")";
            string query = consultaSql(armadoDeQuery).SqlQuery();
            List<Imagen> listaImagen = ImagenBLL.GetImagenListForSearch(query);
            if (listaImagen.Count <= 0)
            {
                errorImagen.Visible = false;
                //string fileName = Path.GetRandomFileName();
                string fullPath = Server.MapPath("~/img/ImgRestaurante/") + tituloImagen;
                FileUpload1.PostedFile.SaveAs(fullPath);

                System.IO.FileInfo info = new System.IO.FileInfo(fullPath);
                objImagen.Directorio = fullPath;
                objImagen.Extencion = info.Extension;
                objImagen.Titulo = tituloImagen;
                objImagen.Size = info.Length;
                objImagen.FechaImagen = DateTime.Now;

                if (!string.IsNullOrEmpty(objImagen.Titulo) && objImagen.Size > 0 &&
                    !string.IsNullOrEmpty(objImagen.Extencion) && !string.IsNullOrEmpty(objImagen.Directorio) &&
                    objImagen.FechaImagen != null)
                {
                    string titulo = "";
                    int idImagen = ImagenBLL.InsertImagen(objImagen, ref titulo);
                    ImagenIdHiddenField.Value = Convert.ToString(idImagen);
                    SelecteImage.ImageUrl = "~/img/ImgRestaurante/" + titulo;
                    CargarProducto();
                    cargarImagenesRepeateLista();
                }
            }
            else
            {
                errorImagen.Visible = true;
            }
        }
    }

    protected void SaveImagenButton_Click(object sender, EventArgs e)
    {
        ImagenProducto ObjImagenProducto = new ImagenProducto();
        ObjImagenProducto.ProductoId = Convert.ToInt32(ProductoIdHiddenField.Value);
        ObjImagenProducto.ImagenId = Convert.ToInt32(ImagenIdHiddenField.Value);
        ImagenProductoBLL.InsertImageProducto(ObjImagenProducto);
        CargarProducto();
        cargarImagenesRepeateLista();

    }

    public void cargarQuery()
    {
        BuscadorCriterioHF.Value = txtBuscador.Text.Trim();
        string query = "";
        string buscar = txtBuscador.Text.Trim();

        if (!string.IsNullOrEmpty(buscar))
        {
            query += " @titulo " + buscar;
            SearchQueryHiddenField.Value = query;
        }
        else
        {
            SearchQueryHiddenField.Value = buscar;
        }
    }

    protected void BuscadorButton_Click(object sender, EventArgs e)
    {
        cargarQuery();
        Pager1.CurrentRow = 0;
        cargarImagenesRepeateLista();
    }

    protected void ImagenProductoRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "RemoveImage")
        {
            try
            {
                int ImagenProductoId = Convert.ToInt32(e.CommandArgument.ToString());
                ImagenProductoBLL.DeleteImagenProducto(ImagenProductoId);
                CargarProducto();
            }
            catch (Exception ex)
            {
                log.Error("Error al eliminar la imagen del articulo ", ex);
                return;
            }
        }
    }

    private void cargarImagenesRepeateLista()
    {
        string ordenar = "";
        //TIPO DE ORDEN
        ordenar = "order by i.[titulo] asc";
        try
        {
            Searcher searcher = new Searcher(new BusquedaImagen());
            searcher.Query = SearchQueryHiddenField.Value;

            List<Imagen> _cache = new List<Imagen>();
            try
            {
                _totalRows = ImagenBLL.SearchFiles(ref _cache, searcher.SqlQuery(), Pager1.PageSize, Pager1.CurrentRow, ordenar);
            }
            catch (Exception ex)
            {
                log.Error("Error serching product:", ex);
                throw ex;
                //return;
            }

            ImagenesRepeater.DataSource = _cache;
            ImagenesRepeater.DataBind();

            Pager1.TotalRows = _totalRows;
            if (_cache.Count == 0)
            {
                Pager1.Visible = false;
                PagesButtons.Visible = true;
                return;
            }
            Pager1.Visible = true;
            Pager1.BuildPagination();
        }
        catch (Exception ex)
        {
            log.Error("error al cargar la lista de articulos", ex);
            return;
        }
    }


    protected void ImagenRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "addImageArticulo")
        {
            try
            {
                ImagenProducto data = new ImagenProducto();
                bool repetido = false;
                int imagenId = Convert.ToInt32(e.CommandArgument.ToString());

                data.ImagenId = imagenId;

                data.ProductoId = Convert.ToInt32(ProductoIdHiddenField.Value);
                List<ImagenProducto> listaImgProducto = ImagenProductoBLL.GetImagenProductoById(Convert.ToInt32(ProductoIdHiddenField.Value));
                //List<ImageArticulo> articuloImg = ImageArticuloBLL.GetImagenesArticulo(Convert.ToInt32(CodigoLiteral.Text));
                for (int i = 0; i < listaImgProducto.Count; i++)
                {
                    if (imagenId == listaImgProducto[i].ImagenId)
                    {
                        repetido = true;
                    }
                }
                if (repetido == false)
                {
                    ImagenProductoBLL.InsertImageProducto(data);
                    cargarListaImagenesDelProducto(CodigoLiteral.Text);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('La imagen no se puede insertar por que estaria repetida');", true);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('La imagen no pudo ser insertada al articulo');", true);
                log.Error("La imagen no pudo ser insertada al articulo", ex);
                return;
                //throw ex;
            }
        }
    }

    protected void Pager1_PageChanged(int row)
    {
        cargarImagenesRepeateLista();
    }
}