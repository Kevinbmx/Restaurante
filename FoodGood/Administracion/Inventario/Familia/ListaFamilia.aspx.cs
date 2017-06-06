using FoodGood.Familia;
using FoodGood.Usuario;
using FoodGood.Familia.BLL;
using FoodGood.Modulo.BLL;
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
            validarUsuario();
            cargarListaFamilia("");
        }

    }


    public void validarUsuario()
    {
        try
        {
            Usuario objUsuario = LoginUtilities.GetUserLogged();
            if (objUsuario != null)
            {
                if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Tipo_Caracteristica) &&
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Tipo_Caracteristica) &&
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Tipo_Caracteristica) &&
          !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Tipo_Caracteristica))
                {
                    Response.Redirect("~/Administracion/Error.aspx");
                }
                if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Tipo_Caracteristica))
                {
                    NewFamiliaButton.Visible = false;
                }

                if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Tipo_Caracteristica))
                {
                    ListaFamiliaGridView.Visible = false;
                }
                else
                {

                    if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Editar_Tipo_Caracteristica))
                    {
                        this.ListaFamiliaGridView.Columns[0].Visible = false;
                    }
                    if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Tipo_Caracteristica))
                    {
                        this.ListaFamiliaGridView.Columns[1].Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Autentificacion/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            log.Error("erro al validar al Usuario");
            throw ex;
        }
    }

    public void cargarListaFamilia(string query)
    {
        List<Familia> listaFamilia = FamiliaBLL.GetFamiliaListForSearch(query);
        if (listaFamilia.Count > 0)
        {
            errorFamilia.Visible = false;
        }
        else
        {
            errorFamilia.Visible = true;
        }
        ListaFamiliaGridView.DataSource = listaFamilia;
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