using Foodgood.Areas.Clase;
using Foodgood.Modulo.Clase;
using FoodGood.Areas.BLL;
using FoodGood.Modulos.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Modulo_ListaModulo : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaAreaModulos("");
        }
    }

    public void cargarListaAreaModulos(string query)
    {
        List<Area> listaAreaModulo = AreaBLL.GetAreaListForSearch(query);
        //List<Modulo> listaModulo = ModuloBLL.GetModuloListForSearch(query);
        if (listaAreaModulo.Count > 0)
        {
            errorUsuario.Visible = false;
        }
        else
        {
            errorUsuario.Visible = true;
        }
        ListaAreaModuloGridView.DataSource = listaAreaModulo;
        ListaAreaModuloGridView.DataBind();

    }
    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaModulo());
        searcher.Query = query;
        return searcher;
    }

    public Searcher consultaAreaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaArea());
        searcher.Query = query;
        return searcher;
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@descripcion \"" + busquedaModuloTxt.Text + "\"";
        string query = consultaAreaSql(armadoDeQuery).SqlQuery();
        cargarListaAreaModulos(query);
    }

    protected void ListaAreaModuloGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int areamoduloId = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Ver")
        {
            Session["AreaModuloId"] = areamoduloId;
            Response.Redirect("~Administracion/Modulo/VerModulo.aspx");
        }

        //if (e.CommandName == "Eliminar")
        //{
        //    try
        //    {
        //        ModuloBLL.DeleteModulo(moduloId);
        //        cargarListaAreaModulos("");
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este modulo esta siendo utilizado');", true);
        //        log.Error("Error al eliminar el usuario con el id '" + moduloId + "'", ex);
        //    }
        //}
        //if (e.CommandName == "Editar")
        //{
        //    Session["ModuloId"] = moduloId;
        //    Response.Redirect("~/Administracion/Modulo/RegistrarModulo.aspx");
        //}
    }


    protected void NewModuloButton_Click(object sender, EventArgs e)
    {
        Session["ModuloId"] = 0;
        Response.Redirect("~/Administracion/Modulo/RegistrarModulo.aspx");
    }

    protected void ListaAreaModuloGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string areaId = e.Row.Cells[2].Text;
                string armadoDeQuery = "@areaId IN(" + areaId + ")";
                string query = consultaSql(armadoDeQuery).SqlQuery();
                List<Modulo> objModulolista = ModuloBLL.GetModuloListForSearch(query);

                e.Row.Cells[2].Text = objModulolista.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            log.Error("Error al conseguir el nombre del Tipo de Usuario", ex);
        }
    }

    protected void ListaAreaModuloGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaAreaModuloGridView.PageIndex = e.NewPageIndex;
        cargarListaAreaModulos("");
    }
}