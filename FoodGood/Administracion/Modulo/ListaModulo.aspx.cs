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
            cargarListaModulos("");
        }

    }

    public void cargarListaModulos(string query)
    {
        List<Modulo> listaModulo = ModuloBLL.GetModuloListForSearch(query);
        if (listaModulo.Count > 0)
        {
            errorUsuario.Visible = false;
        }
        else
        {
            errorUsuario.Visible = true;
        }
        ListaModuloGridView.DataSource = listaModulo;
        ListaModuloGridView.DataBind();

    }
    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaModulo());
        searcher.Query = query;
        return searcher;
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@descripcion \"" + busquedaModuloTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaModulos(query);
    }

    protected void ListaModuloGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int moduloId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                ModuloBLL.DeleteModulo(moduloId);
                cargarListaModulos("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este modulo esta siendo utilizado');", true);
                log.Error("Error al eliminar el usuario con el id '" + moduloId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["ModuloId"] = moduloId;
            Response.Redirect("~/Administracion/Modulo/RegistrarModulo.aspx");
        }
    }


    protected void NewModuloButton_Click(object sender, EventArgs e)
    {
        Session["ModuloId"] = 0;
        Response.Redirect("~/Administracion/Modulo/RegistrarModulo.aspx");
    }

    protected void ListaModuloGridView_DataBound(object sender, EventArgs e)
    {

    }

    protected void ListaModuloGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string areaId = e.Row.Cells[2].Text;
                Area objArea = AreaBLL.GetAreaById(Convert.ToInt32(areaId));
                e.Row.Cells[2].Text = objArea.Descripcion;
            }
        }
        catch (Exception ex)
        {
            log.Error("Error al conseguir el nombre del Tipo de Usuario", ex);
        }
    }
}