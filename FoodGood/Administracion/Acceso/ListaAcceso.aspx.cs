using Foodgood.Accesos.Clase;
using Foodgood.User.Clase;
using FoodGood.Modulos.BLL;
using FoodGood.User.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Acceso_ListaAcceso : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string armadoDeQuery = "@tipousuarioId IN(1)";
            string query = consultaSqlUsuario(armadoDeQuery).SqlQuery();
            cargarListaUsuarioparaAcceso(query);
            validarUsuario();
        }
    }


    public void validarUsuario()
    {
        Usuario objUsuario = LoginUtilities.GetUserLogged();
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Acceso))
        {
            Response.Redirect("~/Administracion/Error.aspx");
        }
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Ver_Acceso))
        {
            this.ListaAccesoGridView.Columns[0].Visible = false;
        }
    }


    public void cargarListaUsuarioparaAcceso(string query)
    {
        try
        {
            List<Usuario> ListaUsuario = UsuariosBLL.GetUsuarioListForSearch(query);
            if (ListaUsuario.Count > 0)
            {
                errorUsuario.Visible = false;
            }
            else
            {
                errorUsuario.Visible = true;
            }
            ListaAccesoGridView.DataSource = ListaUsuario;
            ListaAccesoGridView.DataBind();
        }
        catch (Exception ex)
        {
            log.Error("erro al cargar la lista de usuario");
            throw ex;
        }
    }


    public Searcher consultaSqlUsuario(string query)
    {
        Searcher searcher = new Searcher(new BusquedaUsuaio());
        searcher.Query = query;
        return searcher;
    }

    protected void ListaAccesoGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int usuarioAccesoId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Ver")
        {
            try
            {
                Session["UsuarioAccesoId"] = usuarioAccesoId;
                Response.Redirect("~/Administracion/Acceso/RegistrarAcceso.aspx");
            }
            catch (Exception ex)
            {
                log.Error("Error al ir a registro de acceso por Usuario" + usuarioAccesoId + "'", ex);
            }
        }
    }
    public Searcher consultaSqlAcceso(string query)
    {
        Searcher searcher = new Searcher(new BusquedaAcceso());
        searcher.Query = query;
        return searcher;
    }

    protected void ListaAccesoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string email = e.Row.Cells[2].Text;
                string numeroAccesos = e.Row.Cells[3].Text;
                string id = numeroAccesos;

                string armadoDeQuery = "@usuarioId IN(" + id + ")";
                string query = consultaSqlAcceso(armadoDeQuery).SqlQuery();
                List<Acceso> listaAcceso = AccesoBLL.GetAccesoListForSearch(query);
                if (listaAcceso.Count > 0)
                {
                    e.Row.Cells[3].Text = Convert.ToString(listaAcceso.Count);
                }
                else
                {
                    e.Row.Cells[3].Text = "0";
                }
                if (email.Equals("&nbsp;"))
                {
                    e.Row.Cells[2].Text = "-";
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error al conseguir la cantidad de acceso de cada usuario", ex);
        }
    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@nombre \"" + busquedaAccesoTxt.Text + "\" OR @apellido \"" + busquedaAccesoTxt.Text + "\" OR @email \"" + busquedaAccesoTxt.Text + "\"";
        string query = consultaSqlUsuario(armadoDeQuery).SqlQuery();
        cargarListaUsuarioparaAcceso(query);
    }

    protected void ListaAccesoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaAccesoGridView.PageIndex = e.NewPageIndex;
        cargarListaUsuarioparaAcceso("");
    }
}