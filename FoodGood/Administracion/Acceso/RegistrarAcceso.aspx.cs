using Foodgood.Accesos.Clase;
using Foodgood.Areas.Clase;
using Foodgood.Modulo.Clase;
using Foodgood.User.Clase;
using FoodGood.Areas.BLL;
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
                obtenerListadeId(areaIdHiddenFieldForCombo.Value);
                llenarInformacionUsuario();
            }
            llenarRepeaterArea();
            validarUsuario();
            //llenarCombo();
        }
        //llenarListasModulos("");
    }

    public void validarUsuario()
    {
        Usuario objUsuario = LoginUtilities.GetUserLogged();
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Acceso) &&
            !ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Acceso))
        {
            Response.Redirect("~/Administracion/Error.aspx");
        }
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Crear_Acceso))
        {
            addAllModuloButton.Visible = false;
            AddAccesoButton.Visible = false;
        }
        if (!ModuloBLL.validarSiExisteModulo(objUsuario.UsuarioId, Resources.Validacion.Eliminar_Acceso))
        {
            RemoveAllModuloButton.Visible = false;
            removeAccesoButton.Visible = false;
        }
    }



    public void llenarInformacionUsuario()
    {
        try
        {
            Usuario theData = UsuariosBLL.GetUserById(Convert.ToInt32(usuarioIdHiddenField.Value));
            NombreUserLabel.Text = theData.Nombre;
            Apellidolabel.Text = theData.Apellido;
            TipoUsuario objTipoUsuario = TipoUsuarioBLL.GetTipoUserById(theData.TipoUsuarioId);
            TipoUsuarioLabel.Text = objTipoUsuario.Descripcion;
            UserEmailLabel.Text = theData.Email;
        }
        catch (Exception)
        {
            log.Error("error al llenar la imformacion del usuario");
            throw;
        }

    }


    public void llenarRepeaterArea()
    {
        try
        {
            List<Area> listaArea = AreaBLL.GetArea();
            listaArea.Sort((p, q) => string.Compare(p.Descripcion, q.Descripcion));
            checkAreaRepeater.DataSource = listaArea.OrderBy(p => p.Descripcion).ToList();
            checkAreaRepeater.DataBind();
        }
        catch (Exception ex)
        {
            log.Error("error al llenar repeater de check de las areas" + ex);
            throw;
        }

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
        Session["UsuarioAccesoId"] = null;
    }

    public void obtenerListadeId(string areaId)
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
            string buscarModulo = "";
            if (!string.IsNullOrEmpty(areaId))
            {
                string armadoDeQuery = "@areaId IN(" + areaId + ")";
                buscarModulo = consultaSqlModulo(armadoDeQuery).SqlQuery();
            }

            List<Modulo> listaModulo = ModuloBLL.GetModuloListForSearch(buscarModulo);
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
                    if (!string.IsNullOrEmpty(areaId))
                    {
                        armadoDeQuery += " and @areaId IN(" + areaId + ")";

                    }
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
                if (!string.IsNullOrEmpty(areaId))
                {
                    armadoDeQueryAcceso += "and @areaId IN(" + areaId + ")";
                }
                string queryAcceso = consultaSqlAcceso(armadoDeQueryAcceso).SqlQuery();
                llenarListasDeAccesos(queryAcceso);
            }
            else
            {
                string queryModulo = "";
                if (!string.IsNullOrEmpty(areaId))
                {
                    string armadoDeQuery = "@areaId IN(" + areaId + ")";
                    queryModulo = consultaSqlModulo(armadoDeQuery).SqlQuery();
                }
                llenarListasModulos(queryModulo);
                string armadoDeQueryAcceso = "@moduloId IN(0) and @usuarioId in(0)";
                string queryAcceso = consultaSqlAcceso(armadoDeQueryAcceso).SqlQuery();
                llenarListasDeAccesos(queryAcceso);
            }
        }
        catch (Exception ex)
        {
            log.Error("error al llenar las lista de accesos" + ex);
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
            log.Error("error al llenar los modulos de la lista" + ex);
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
            log.Error("error al llenar la lista de acceso" + ex);
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
            obtenerListadeId(areaIdHiddenFieldForCombo.Value);
        }
        catch (Exception ex)
        {
            log.Error("error al añadir los accesos" + ex);
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
            obtenerListadeId(areaIdHiddenFieldForCombo.Value);
        }
        catch (Exception ex)
        {
            log.Error("error al remover los accesos" + ex);
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
        try
        {
            Modulo theData = null;
            foreach (ListItem item in ListaAccesoPermitidosListBox.Items)
            {
                int valorIdModulo = Convert.ToInt32(item.Text);
                theData = ModuloBLL.GetModuloById(valorIdModulo);
                item.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(theData.Descripcion);
            }
        }
        catch (Exception ex)
        {
            log.Error("error al obtener la lista de modulo para poner la descripcion a la lista" + ex);
            throw ex;
        }
    }



    protected void RemoveAllModuloButton_Click(object sender, EventArgs e)
    {
        try
        {
            string queryModulo = "";
            if (!areaIdHiddenFieldForCombo.Value.Equals(""))
            {
                string armadoDeQuery = "@areaId IN(" + areaIdHiddenFieldForCombo.Value + ")";
                queryModulo = consultaSqlAcceso(armadoDeQuery).SqlQuery();
            }
            List<Acceso> listaAcceso = AccesoBLL.GetAccesoListForSearch(queryModulo);
            Acceso theData = new Acceso();
            theData.UsuarioId = Convert.ToInt32(usuarioIdHiddenField.Value);
            for (int i = 0; i < listaAcceso.Count; i++)
            {
                theData.ModuloId = listaAcceso[i].ModuloId;
                AccesoBLL.DeleteAcceso(theData);
            }
            obtenerListadeId(areaIdHiddenFieldForCombo.Value);
        }
        catch (Exception ex)
        {
            log.Error("error al obtener la lista de acceso" + ex);
            throw ex;
        }

    }

    protected void addAllModuloButton_Click(object sender, EventArgs e)
    {
        try
        {
            string queryModulo = "";
            if (!areaIdHiddenFieldForCombo.Value.Equals(""))
            {
                string armadoDeQuery = "@areaId IN(" + areaIdHiddenFieldForCombo.Value + ")";
                queryModulo = consultaSqlModulo(armadoDeQuery).SqlQuery();
            }
            List<Modulo> listaMlos = ModuloBLL.GetModuloListForSearch(queryModulo);
            Acceso theData = new Acceso();
            theData.UsuarioId = Convert.ToInt32(usuarioIdHiddenField.Value);
            for (int i = 0; i < listaMlos.Count; i++)
            {
                theData.ModuloId = listaMlos[i].ModuloId;
                if (!existeAcceso(theData))
                {
                    AccesoBLL.InsertAcceso(theData);
                }
            }
            obtenerListadeId(areaIdHiddenFieldForCombo.Value);
        }
        catch (Exception ex)
        {
            log.Error("error al obtener la lista " + ex);
            throw;
        }
    }

    public bool existeAcceso(Acceso objAcceso)
    {
        try
        {
            bool existe = false;
            Acceso objAccesoNuevo = AccesoBLL.GetAccesoById(objAcceso);
            if (objAccesoNuevo != null)
            {
                existe = true;
            }
            return existe;
        }
        catch (Exception ex)
        {
            log.Error("error al obtener acceso by Id" + ex);
            throw ex;
        }

    }

    protected string construirQueryCategoriasIds()
    {
        string areas = "";
        foreach (RepeaterItem ri in checkAreaRepeater.Items)
        {
            CheckBox chk = (CheckBox)ri.FindControl("AreaChecbox");
            HiddenField hd = (HiddenField)ri.FindControl("AreaIdHiddenField");
            if (chk.Checked)
            {
                areas += andQueryIds(areas) + hd.Value;
            }
        }
        return areas;
    }

    protected void AreaChecbox_CheckedChanged(object sender, EventArgs e)
    {
        string areasId = construirQueryCategoriasIds();
        areaIdHiddenFieldForCombo.Value = areasId;
        obtenerListadeId(areaIdHiddenFieldForCombo.Value);
        //CheckBox check = (CheckBox)e.Item.FindControl("AreaChecbox");
        //Message.Text = check.Checked.ToString();
    }

    protected void checkAreaRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //CheckBox check = (CheckBox)e.Item.FindControl("AreaChecbox");
        //HiddenField hidden = (HiddenField)e.Item.FindControl("AreaIdHiddenField");

        //string idCheck = hidden.Value;
    }
}