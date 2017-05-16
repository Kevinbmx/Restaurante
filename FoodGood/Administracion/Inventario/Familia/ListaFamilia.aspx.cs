using Foodgood.Familias.Clase;
using FoodGood.Familias.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_Familia_ListaFamilia : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaFamilia("");
        }

    }
    public void cargarListaFamilia(string query)
    {
        List<Familia> listaArea = FamiliaBLL.GetFamiliaListForSearch(query);
        if (listaArea.Count > 0)
        {
            errorFamilia.Visible = false;
        }
        else
        {
            errorFamilia.Visible = true;
        }
        ListaFamiliaGridView.DataSource = listaArea;
        ListaFamiliaGridView.DataBind();

    }
    protected void NewFamiliaButton_Click(object sender, EventArgs e)
    {
        Session["FamiliaId"] = 0;
        Response.Redirect("~/Administracion/Inventario/Familia/RegistrarFamilia.aspx");
    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaFamilia());
        searcher.Query = query;
        return searcher;
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@descripcion \"" + busquedaFamiliaTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaFamilia(query);
    }

    protected void ListaFamiliaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int familiaId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                FamiliaBLL.DeleteFamilia(familiaId);
                cargarListaFamilia("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que esta familia esta siendo utilizado');", true);
                log.Error("Error al eliminar la Familia con el id '" + familiaId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["FamiliaId"] = familiaId;
            Response.Redirect("~/Administracion/Inventario/Familia/RegistrarFamilia.aspx");
        }
    }

    protected void ListaFamiliaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaFamiliaGridView.PageIndex = e.NewPageIndex;
        cargarListaFamilia("");
    }
}