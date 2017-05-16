using Foodgood.User.Clase;
using FoodGood.TipoUser.BLL;
using FoodGood.User.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Cliente_ListaCliente : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string armadoDeQuery = "@tipousuarioId IN(2,3)";
            string query = consultaSql(armadoDeQuery).SqlQuery();
            cargarListaUsuario(query);
        }
    }


    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaUsuaio());
        searcher.Query = query;
        return searcher;
    }


    public void cargarListaUsuario(string query)
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
        ListaUsuariosGridView.DataSource = ListaUsuario;
        ListaUsuariosGridView.DataBind();

    }

    protected void busquedaBtn_Click(object sender, EventArgs e)
    {
        string armadoDeQuery = "@nombre \"" + busquedaUsuarioTxt.Text + "\" OR @apellido \"" + busquedaUsuarioTxt.Text + "\" OR @email \"" + busquedaUsuarioTxt.Text + "\"";
        string query = consultaSql(armadoDeQuery).SqlQuery();
        cargarListaUsuario(query);
    }

    protected void ListaUsuariosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int usuarioId = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Eliminar")
        {
            try
            {
                UsuariosBLL.DeleteUsuario(usuarioId);
                string armadoDeQuery = "@tipousuarioId IN(2,3)";
                string query = consultaSql(armadoDeQuery).SqlQuery();
                cargarListaUsuario(query);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('No se puede eliminar por que este Cliente esta siendo utilizado');", true);
                log.Error("Error al eliminar el usuario con el id '" + usuarioId + "'", ex);
            }
        }
        if (e.CommandName == "Editar")
        {
            Session["ClienteId"] = usuarioId;
            Response.Redirect("~/Administracion/Cliente/RegistrarCliente.aspx");
        }
    }

    protected void NewUsuarioButton_Click(object sender, EventArgs e)
    {
        Session["ClienteId"] = 0;
        Response.Redirect("~/Administracion/Cliente/RegistrarCliente.aspx");
    }

    protected void ListaUsuariosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string apellido = e.Row.Cells[3].Text;
                string IdTipoUsuario = e.Row.Cells[4].Text;
                string email = e.Row.Cells[5].Text;
                string celular = e.Row.Cells[7].Text;
                TipoUsuario objTipoUsuario = TipoUsuarioBLL.GetTipoUserById(Convert.ToInt32(IdTipoUsuario));
                e.Row.Cells[4].Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(objTipoUsuario.Descripcion);
                if (apellido.Equals("&nbsp;"))
                {
                    e.Row.Cells[3].Text = "-";
                }
                if (email.Equals("&nbsp;"))
                {
                    e.Row.Cells[5].Text = "-";
                }
                if (celular.Equals("&nbsp;"))
                {
                    e.Row.Cells[7].Text = "-";
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error al conseguir el nombre del Tipo de Usuario", ex);
        }
    }

    protected void ListaUsuariosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListaUsuariosGridView.PageIndex = e.NewPageIndex;
        cargarListaUsuario("");
    }
}