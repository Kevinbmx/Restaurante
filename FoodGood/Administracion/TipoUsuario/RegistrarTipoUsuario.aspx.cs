using FoodGood.TipoUser.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_TipoUsuario_RegistrarTipoUsuario : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(TiopUsuarioidHiddenField.Value))
            {
                cargarDatosTipoUsuario();
            }
        }
    }
    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["TipoUsurioId"] != null && !string.IsNullOrEmpty(Session["TipoUsurioId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["TipoUsurioId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["UsuarioId"]);
            }
            if (ususrioId > 0)
            {
                TiopUsuarioidHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/TipoUsuario/ListaTipoUsuario.aspx");
        }
        Session["TipoUsurioId"] = null;
    }

    public void cargarDatosTipoUsuario()
    {
        TipoUsuario theData = null;
        try
        {
            theData = TipoUsuarioBLL.GetTipoUserById(Convert.ToInt32(TiopUsuarioidHiddenField.Value));

            if (theData == null)
            {
                Response.Redirect("~/Administracion/TipoUsuario/ListaTipoUsuario.aspx");
            }

            if (theData.TipoUsuarioId != 0)
            {
                descripcionTextBox.Text = theData.Descripcion;
                SaveTipoUsuairio.Visible = false;
                UpdateTipoUsuarioButton.Visible = true;
            }
        }
        catch
        {
            log.Error("Error al obtener la información del tipo de Usuario");
        }
    }

    protected void SaveTipoUsuairio_Click(object sender, EventArgs e)
    {
        TipoUsuario objTipoUsuario = new TipoUsuario();

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objTipoUsuario.Descripcion = descripcionTextBox.Text;
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objTipoUsuario.Descripcion))
        {
            TipoUsuarioBLL.InsertTipoUsuario(objTipoUsuario);
            Response.Redirect("~/Administracion/TipoUsuario/ListaTipoUsuario.aspx");
        }
    }

    protected void UpdateTipoUsuarioButton_Click(object sender, EventArgs e)
    {
        TipoUsuario objTipoUsuario = new TipoUsuario();

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objTipoUsuario.Descripcion = descripcionTextBox.Text;
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objTipoUsuario.Descripcion))
        {
            objTipoUsuario.TipoUsuarioId = Convert.ToInt32(TiopUsuarioidHiddenField.Value);
            TipoUsuarioBLL.UpdateTipoUsuario(objTipoUsuario);
            Response.Redirect("~/Administracion/TipoUsuario/ListaTipoUsuario.aspx");
        }
    }
}