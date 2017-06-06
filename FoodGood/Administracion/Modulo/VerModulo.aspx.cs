using FoodGood.Modulo;
using FoodGood.Usuario;
using FoodGood.Modulo.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Modulo_VerModulo : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(AreadeModuloIdHiddenField.Value))
            {
                cargarListaModulosdeArea(AreadeModuloIdHiddenField.Value);
            }
            validarUsuario();
        }
    }


    public void validarUsuario()
    {
        Usuario objUsuario = LoginUtilities.GetUserLogged();
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Modulo) &&
            !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Modulo) &&
            !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Modulo) &&
            !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Modulo))
        {
            Response.Redirect("~/Administracion/Error.aspx");
        }
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Modulo))
        {
            this.ListaModuloAreaGridView.Columns[0].Visible = false;
        }
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Modulo))
        {
            this.ListaModuloAreaGridView.Columns[1].Visible = false;
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
        Session["AreaModuloId"] = null;
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
        int areamoduloId = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Eliminar")
        {
            try
            {
                ModuloBLL.DeleteModulo(areamoduloId);
                cargarListaModulosdeArea(AreadeModuloIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este modulo esta siendo utilizado');", true);
                log.Error("Error al eliminar el usuario con el id '", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["ModuloId"] = areamoduloId;
            Session["booleanHabilitaArea"] = "true";

            Response.Redirect("~/Administracion/Modulo/RegistrarModulo.aspx");
        }
    }

    protected void ListaModuloAreaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        cargarListaModulosdeArea(AreadeModuloIdHiddenField.Value);
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {

    }
}