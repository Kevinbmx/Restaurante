using Foodgood.Productos.Clase;
using FoodGood.Productos.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_Producto_ListaProducto : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaProducto("");
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
        if (e.CommandName == "Editar")
        {
            Session["ProductoId"] = productoId;
            Response.Redirect("~/Administracion/Inventario/Producto/RegistrarProducto.aspx");
        }
    }

    protected void ListaProductosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ListaProductosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaProductosGridView.PageIndex = e.NewPageIndex;
        cargarListaProducto("");
    }
}