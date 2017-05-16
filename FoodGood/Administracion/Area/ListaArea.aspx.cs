using Foodgood.Areas.Clase;
using FoodGood.Areas.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Area_ListaArea : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaAreas("");
        }
    }

    public void cargarListaAreas(string query)
    {
        List<Area> listaArea = AreaBLL.GetAreaListForSearch(query);
        if (listaArea.Count > 0)
        {
            errorArea.Visible = false;
        }
        else
        {
            errorArea.Visible = true;
        }
        ListaAreaGridView.DataSource = listaArea;
        ListaAreaGridView.DataBind();

    }

    protected void NewAreaButton_Click(object sender, EventArgs e)
    {
        Session["AreaId"] = 0;
        Response.Redirect("~/Administracion/Area/RegistrarArea.aspx");
    }
    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaArea());
        searcher.Query = query;
        return searcher;
    }


    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@descripcion \"" + busquedaAreaTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaAreas(query);
    }




    protected void ListaAreaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int moduloId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                AreaBLL.DeleteArea(moduloId);
                cargarListaAreas("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que esta Area esta siendo utilizado');", true);
                log.Error("Error al eliminar el usuario con el id '" + moduloId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["AreaId"] = moduloId;
            Response.Redirect("~/Administracion/Area/RegistrarArea.aspx");
        }
    }

    protected void ListaAreaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaAreaGridView.PageIndex = e.NewPageIndex;
        cargarListaAreas("");
    }
}