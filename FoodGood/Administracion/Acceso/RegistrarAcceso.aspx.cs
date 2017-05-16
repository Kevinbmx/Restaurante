using Foodgood.Accesos.Clase;
using Foodgood.Modulo.Clase;
using Foodgood.User.Clase;
using FoodGood.Modulos.BLL;
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

public partial class Administracion_Acceso_RegistrarAcceso : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(usuarioIdHiddenField.Value))
            {
                obtenerListadeId();
                llenarInformacionUsuario();
            }
        }
        //llenarListasModulos("");
    }
    public void llenarInformacionUsuario()
    {
        Usuario theData = UsuariosBLL.GetUserById(Convert.ToInt32(usuarioIdHiddenField.Value));
        NombreUserLabel.Text = theData.Nombre;
        Apellidolabel.Text = theData.Apellido;
        TipoUsuario objTipoUsuario = TipoUsuarioBLL.GetTipoUserById(theData.TipoUsuarioId);
        TipoUsuarioLabel.Text = objTipoUsuario.Descripcion;
        UserEmailLabel.Text = theData.Email;
    }
    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["UsuarioAccesoId"] != null && !string.IsNullOrEmpty(Session["UsuarioAccesoId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["UsuarioAccesoId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["UsuarioAccesoId"]);
            }
            if (ususrioId > 0)
            {
                usuarioIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Acceso/ListaAcceso.aspx");
        }
        Session["AreaId"] = null;
    }

    public void obtenerListadeId()
    {
        try
        {
            string usuarioId = usuarioIdHiddenField.Value;
            List<Acceso> listaAccesos = AccesoBLL.GetAccesoListForSearch("");
            List<string> listaIdModulosAsignados = new List<string>();

            if (listaAccesos.Count > 0)
            {
                idModulosAsignados.Value = "";

                for (int i = 0; i < listaAccesos.Count; i++)
                {
                    string idAcceso = Convert.ToString(listaAccesos[i].UsuarioId);
                    if (idAcceso.Equals(usuarioId))
                    {
                        idModulosAsignados.Value += andQueryIds(idModulosAsignados.Value) + listaAccesos[i].ModuloId;
                        listaIdModulosAsignados.Add(Convert.ToString(listaAccesos[i].ModuloId));
                    }
                }
            }

            List<Modulo> listaModulo = ModuloBLL.GetModuloListForSearch("");
            List<string> listaIdModulos = new List<string>();

            if (listaModulo.Count > 0)
            {
                for (int i = 0; i < listaModulo.Count; i++)
                {
                    listaIdModulos.Add(listaModulo[i].ModuloId.ToString());
                }
            }

            if (listaIdModulosAsignados.Count > 0)
            {
                for (int i = 0; i < listaIdModulosAsignados.Count; i++)
                {
                    for (int j = 0; j < listaIdModulos.Count; j++)
                    {
                        if (listaIdModulosAsignados[i].Equals(listaIdModulos[j]))
                        {
                            listaIdModulos.RemoveAt(j);
                        }
                    }
                }
                if (listaIdModulos.Count > 0)
                {
                    idModulosinSeleccionar.Value = "";
                    for (int i = 0; i < listaIdModulos.Count; i++)
                    {
                        idModulosinSeleccionar.Value += andQueryIds(idModulosinSeleccionar.Value) + listaIdModulos[i];
                    }
                    string armadoDeQuery = "@moduloId IN(" + idModulosinSeleccionar.Value + ")";
                    string queryModulo = consultaSqlModulo(armadoDeQuery).SqlQuery();
                    llenarListasModulos(queryModulo);
                }
                else
                {
                    string armadoDeQuery = "@moduloId IN(0)";
                    string queryModulo = consultaSqlModulo(armadoDeQuery).SqlQuery();
                    llenarListasModulos(queryModulo);
                }

                string armadoDeQueryAcceso = "@moduloId IN(" + idModulosAsignados.Value + ") and @usuarioId in(" + usuarioId + ")";
                string queryAcceso = consultaSqlAcceso(armadoDeQueryAcceso).SqlQuery();
                llenarListasDeAccesos(queryAcceso);
            }
            else
            {
                llenarListasModulos("");
                string armadoDeQueryAcceso = "@moduloId IN(0) and @usuarioId in(0)";
                string queryAcceso = consultaSqlAcceso(armadoDeQueryAcceso).SqlQuery();
                llenarListasDeAccesos(queryAcceso);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string andQueryIds(string text)
    { return !string.IsNullOrEmpty(text) ? "," : ""; }

    public Searcher consultaSqlModulo(string query)
    {
        Searcher searcher = new Searcher(new BusquedaModulo());
        searcher.Query = query;
        return searcher;
    }

    public Searcher consultaSqlAcceso(string query)
    {
        Searcher searcher = new Searcher(new BusquedaAcceso());
        searcher.Query = query;
        return searcher;
    }



    public void llenarListasModulos(string query)
    {
        try
        {
            List<Modulo> listaMlos = ModuloBLL.GetModuloListForSearch(query);
            if (listaMlos.Count > 0)
            {
                errorAcceso.Visible = false;
            }
            else
            {
                errorAcceso.Visible = true;
            }
            ListaAccesosListBox.DataSource = listaMlos;
            ListaAccesosListBox.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void llenarListasDeAccesos(string query)
    {
        try
        {
            List<Acceso> listaAcceso = AccesoBLL.GetAccesoListForSearch(query);
            ListaAccesoPermitidosListBox.DataSource = listaAcceso;
            ListaAccesoPermitidosListBox.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ListaAccesosListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string idSeleccionado = ListaAccesosListBox.SelectedValue.ToString();
        idModuloParaAsignar.Value = idSeleccionado;
        AddAccesoButton.Enabled = true;
    }


    protected void AddAccesoButton_Click(object sender, EventArgs e)
    {
        try
        {
            Acceso theData = new Acceso();
            theData.UsuarioId = Convert.ToInt32(usuarioIdHiddenField.Value);
            theData.ModuloId = Convert.ToInt32(idModuloParaAsignar.Value);
            AccesoBLL.InsertAcceso(theData);
            AddAccesoButton.Enabled = false;
            obtenerListadeId();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void removeAccesoButton_Click(object sender, EventArgs e)
    {
        try
        {
            Acceso theData = new Acceso();
            theData.UsuarioId = Convert.ToInt32(usuarioIdHiddenField.Value);
            theData.ModuloId = Convert.ToInt32(idModuloDeleteforAcceso.Value);
            AccesoBLL.DeleteAcceso(theData);
            removeAccesoButton.Enabled = false;
            obtenerListadeId();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ListaAccesoPermitidosListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string idSeleccionado = ListaAccesoPermitidosListBox.SelectedValue.ToString();
        idModuloDeleteforAcceso.Value = idSeleccionado;
        removeAccesoButton.Enabled = true;
    }

    protected void ListaAccesoPermitidosListBox_DataBound(object sender, EventArgs e)
    {
        Modulo theData = null;
        foreach (ListItem item in ListaAccesoPermitidosListBox.Items)
        {
            int valorIdModulo = Convert.ToInt32(item.Text);
            theData = ModuloBLL.GetModuloById(valorIdModulo);
            item.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(theData.Descripcion);
        }
    }
}