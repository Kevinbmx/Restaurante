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

public partial class Cuenta_MisPedidos : System.Web.UI.Page
{
    private int _totalRows = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Usuario objUsuario = LoginUtilities.GetUserLogged();
            if (objUsuario != null)
            {
                queryHiddenField.Value = objUsuario.UsuarioId.ToString();

                cargarLIstaPedido(queryHiddenField.Value);
            }
        }
    }

    //private void verificarUsuarioIniciado()
    //{
    //    Usuario objUsuario = LoginUtilities.GetUserLogged();
    //    if (objUsuario != null)
    //    {

    //        string armadoDeQuery = "@usuarioId IN(" + objUsuario.UsuarioId + ")";
    //        string query = consultaSqlpedido(armadoDeQuery).SqlQuery();
    //        queryHiddenField.Value = query;
    //        cargarLIstaPedido(query);
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Autentificacion/Login.aspx");
    //    }
    //}

    protected void Pager_PageChanged(int row)
    {
        cargarLIstaPedido(queryHiddenField.Value);
    }

    public void cargarLIstaPedido(string queries)
    {

        try
        {
            string ordenar = "order by p.[pedidoId] desc";

            List<Pedido> _cache = new List<Pedido>();
            string armadoDeQuery = "@usuarioId IN(" + queries + ")";
            string query = consultaSqlpedido(armadoDeQuery).SqlQuery();
            _totalRows = PedidoBLL.SearchProductoPaginacion(ref _cache, query, Pager.PageSize, Pager.CurrentRow, ordenar);
            OrdersRepeater.DataSource = _cache;
            OrdersRepeater.DataBind();

            Pager.TotalRows = _totalRows;
            if (_cache.Count == 0)
            {
                //noResult.Visible = true;
                Pager.Visible = false;
                //PagesButtons.Visible = true;
                return;
            }
            //noResult.Visible = false;
            Pager.Visible = true;
            Pager.BuildPagination();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }



    public Searcher consultaSqlpedido(string query)
    {
        Searcher searcher = new Searcher(new BusquedaPedido());
        searcher.Query = query;
        return searcher;
    }

    protected void OrdersRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalle")
        {
            Response.Redirect("~/Cuenta/MisPedidos.aspx?oId=" + e.CommandArgument.ToString());
            return;
        }
    }


}