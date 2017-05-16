using Foodgood.UnidadesMedidas.Clase;
using FoodGood.UnidadesMedidas.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_UnidadMedida_ListaUnidadMedida : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaUnidadMedida("");
        }
    }

    public void cargarListaUnidadMedida(string query)
    {
        List<UnidadMedida> listaArea = UnidadMedidaBLL.GetUnidadMedidaListForSearch(query);
        if (listaArea.Count > 0)
        {
            errorUsuario.Visible = false;
        }
        else
        {
            errorUsuario.Visible = true;
        }
        ListaUnidadMedidaGridView.DataSource = listaArea;
        ListaUnidadMedidaGridView.DataBind();

    }

    protected void NewUnidadMedidaButton_Click(object sender, EventArgs e)
    {
        Session["UnidadMedidaId"] = "null";
        Response.Redirect("~/Administracion/Inventario/UnidadMedida/RegistrarUnidadMedida.aspx");
    }
    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaArea());
        searcher.Query = query;
        return searcher;
    }


    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@descripcion \"" + busquedaUnidadMedidaTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaUnidadMedida(query);
    }




    protected void ListaUnidadMedidaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string unidadMedidaId = Convert.ToString(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                UnidadMedidaBLL.DeleteUnidadMedida(unidadMedidaId);
                cargarListaUnidadMedida("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que esta Area esta siendo utilizado');", true);
                log.Error("Error al eliminar el usuario con el id '" + unidadMedidaId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["UnidadMedidaId"] = unidadMedidaId;
            Response.Redirect("~/Administracion/Inventario/UnidadMedida/RegistrarUnidadMedida.aspx");
        }
    }



    protected void ListaUnidadMedidaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaUnidadMedidaGridView.PageIndex = e.NewPageIndex;
        cargarListaUnidadMedida("");
    }
}