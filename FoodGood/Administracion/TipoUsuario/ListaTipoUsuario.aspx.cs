using FoodGood.TipoUsuario;
using FoodGood.TipoUsuario.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_TipoUsuario_ListaTipoUsuario : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarListaTipoUsuario("");
        }
    }

    public void cargarListaTipoUsuario(string query)
    {
        try
        {
            List<TipoUsuario> ListaTipoUsuario = TipoUsuarioBLL.GetTipoUsuarioListForSearch(query);
            if (ListaTipoUsuario.Count > 0)
            {
                errorTipoUsuario.Visible = false;
            }
            else
            {
                errorTipoUsuario.Visible = true;
            }
            ListaTipoUsuarioGridView.DataSource = ListaTipoUsuario;
            ListaTipoUsuarioGridView.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaTipoUsuario());
        searcher.Query = query;
        return searcher;
    }


    protected void NewTipoUsuarioButton_Click(object sender, EventArgs e)
    {
        Session["TipoUsurioId"] = 0;
        Response.Redirect("~/Administracion/TipoUsuario/RegistrarTipoUsuario.aspx");
    }

    //protected void busquedaBtn_Click(object sender, EventArgs e)
    //{
    //    string armadoDeQuery = "@descripcion \"" + busquedaAreaTxt.Text + "\"";
    //    string query = consultaSql(armadoDeQuery).SqlQuery();
    //    cargarListaTipoUsuario(query);
    //}

    protected void ListaTipoUsuarioGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int moduloId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                TipoUsuarioBLL.DeleteTipoUsuario(moduloId);
                cargarListaTipoUsuario("");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este Tipo de Usuario esta siendo utilizado');", true);
                log.Error("Error al eliminar el Tipo de usuario con el id '" + moduloId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["TipoUsurioId"] = moduloId;
            Response.Redirect("~/Administracion/TipoUsuario/RegistrarTipoUsuario.aspx");
        }
    }

    protected void ListaTipoUsuarioGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaTipoUsuarioGridView.PageIndex = e.NewPageIndex;
        cargarListaTipoUsuario("");
    }
}