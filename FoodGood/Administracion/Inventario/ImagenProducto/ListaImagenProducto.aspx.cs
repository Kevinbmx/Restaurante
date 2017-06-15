using FoodGood.Producto;
using FoodGood.Usuario;
using FoodGood.Modulo.BLL;
using FoodGood.Producto.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodGood.Familia;
using FoodGood.Familia.BLL;

public partial class Administracion_Inventario_ImagenProducto_ListaImagenProducto : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaProducto("");
            validarUsuario();
        }

    }


    public void validarUsuario()
    {
        try
        {
            Usuario objUsuario = LoginUtilities.GetUserLogged();
            if (objUsuario != null)
            {
                if (
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Lista_Producto) &&
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Lista_Producto) &&
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Lista_Producto))
                {
                    Response.Redirect("~/Administracion/Error.aspx");
                }
                //if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Lista_Producto))
                //{
                //    NewImaProductoButton.Visible = false;
                //}

                if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Lista_Producto))
                {
                    ListaProductosGridView.Visible = false;
                }
                else
                {

                    if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Lista_Producto))
                    {
                        this.ListaProductosGridView.Columns[0].Visible = false;
                    }
                    if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Lista_Producto))
                    {
                        this.ListaProductosGridView.Columns[1].Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Autentificacion/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            log.Error("erro al validar al Usuario");
            throw ex;
        }
    }

    public void cargarListaProducto(string query)
    {
        List<Producto> listaProducto = ProductoBLL.GetProductoListForSearch(query);
        if (listaProducto.Count > 0)
        {
            errorProducto.Visible = false;
        }
        else
        {
            errorProducto.Visible = true;
        }
        ListaProductosGridView.DataSource = listaProducto;
        ListaProductosGridView.DataBind();

    }


    protected void NewProductoButton_Click(object sender, EventArgs e)
    {
        Session["ProductoId"] = 0;
        Response.Redirect("~/Administracion/Inventario/Producto/RegistrarProducto.aspx");
    }
    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaProducto());
        searcher.Query = query;
        return searcher;
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "(@nombre \"" + busquedaProductoTxt.Text + "\" or @descripcion \"" + busquedaProductoTxt.Text + "\"or @uniDescripcion \"" + busquedaProductoTxt.Text + "\" or @familiaDescripcion \"" + busquedaProductoTxt.Text + "\")";
        //or @descripcion \"" + busquedaProductoTxt.Text + "\" or @uniDescripcion \"" + busquedaProductoTxt.Text + "\" or @familiaDescripcion \"" + busquedaProductoTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaProducto(query);
    }

    protected void ListaProductosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int productoId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                ProductoBLL.DeleteProducto(productoId);
                cargarListaProducto("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este producto esta siendo utilizado');", true);
                log.Error("Error al eliminar el producto con el id '" + productoId + "'", ex);
            }
        }
        if (e.CommandName == "Añadir")
        {
            Session["ProductoId"] = productoId;
            Response.Redirect("~/Administracion/Inventario/ImagenProducto/ImageProducto.aspx");
        }
    }


    protected void ListaProductosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string numeroIdFamilia = e.Row.Cells[2].Text;
                int id = Convert.ToInt32(numeroIdFamilia);
                Familia listaFamilia = FamiliaBLL.GetFamiliaById(id);

                e.Row.Cells[2].Text = listaFamilia.Descripcion;

            }
        }
        catch (Exception ex)
        {
            log.Error("Error al conseguir la descripcion de la familia", ex);
        }


    }

    protected void ListaProductosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaProductosGridView.PageIndex = e.NewPageIndex;
        cargarListaProducto("");
    }
}