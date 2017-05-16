using Foodgood.Modulo.Clase;
using FoodGood.Modulos.BLL;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Modulo_VerModulo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(AreadeModuloIdHiddenField.Value))
            {
                cargarListaModulosdeArea("");
            }
        }
    }
    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["AreaModuloId"] != null && !string.IsNullOrEmpty(Session["AreaModuloId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["AreaModuloId"]);
            }
            catch
            {
                //log.Error("no se pudo realizar la conversion del ModuloId: " + Session["AreaModuloId"]);
            }
            if (ususrioId > 0)
            {
                AreadeModuloIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Modulo/ListaModulo.aspx");
        }
        Session["ModuloId"] = null;
    }

    public void cargarListaModulosdeArea(string query)
    {
        string armadoDeQuery = "@areaId IN(" + query + ")";
        string consulta = consultaSql(armadoDeQuery).SqlQuery();
        List<Modulo> listaAreaModulo = ModuloBLL.GetModuloListForSearch(consulta);
        //List<Modulo> listaModulo = ModuloBLL.GetModuloListForSearch(query);
        if (listaAreaModulo.Count > 0)
        {
            errorUsuario.Visible = false;
        }
        else
        {
            errorUsuario.Visible = true;
        }
        ListaModuloAreaGridView.DataSource = listaAreaModulo;
        ListaModuloAreaGridView.DataBind();

    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaModulo());
        searcher.Query = query;
        return searcher;
    }
    protected void ListaModuloAreaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void ListaModuloAreaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}