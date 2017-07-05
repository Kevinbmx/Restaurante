using FoodGood.Pedido;
using FoodGood.Pedido.BLL;
using FoodGood.Usuario;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Pedido_ListaPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Usuario objUsuario = LoginUtilities.GetUserLogged();
            if (objUsuario != null)
            {
                cargarPedido("");
            }
            else
            {
                Response.Redirect("~/Autentificacion/Login.aspx");
            }
        }
    }

    public void cargarPedido(string query)
    {
        List<PedidoAdministracion> listaPedidoAdministracion = PedidoAdminstracionBLL.GetPedidoAdministracionListForSearch(query);
        ListaPedidoModuloGridView.DataSource = listaPedidoAdministracion;
        ListaPedidoModuloGridView.DataBind();
    }


    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaUsuaio());
        searcher.Query = query;
        return searcher;
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {

    }

    protected void ListaPedidoModuloGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ListaPedidoModuloGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaPedidoModuloGridView.PageIndex = e.NewPageIndex;
        cargarPedido("");
    }

    protected void ListaPedidoModuloGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int usuarioAccesoId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Ver")
        {
            try
            {
                Session["PedidoId"] = usuarioAccesoId;
                Response.Redirect("~/Administracion/Pedido/DetallePedido.aspx");
            }
            catch (Exception ex)
            {
                //log.Error("Error al ir a registro de acceso por Usuario" + usuarioAccesoId + "'", ex);
            }
        }

    }
}