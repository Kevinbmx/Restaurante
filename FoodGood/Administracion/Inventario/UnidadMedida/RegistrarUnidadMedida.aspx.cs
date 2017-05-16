using Foodgood.UnidadesMedidas.Clase;
using FoodGood.UnidadesMedidas.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_UnidadMedida_RegistrarUnidadMedida : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!unidadMedidaIdHiddenField.Value.Equals("null"))
            {
                cargarDatosUnidadMedida();
            }
        }
    }

    private void ProcessSessionParameteres()
    {
        string UnidadMedidaid = "null";
        string unidadMedida = Convert.ToString(Session["UnidadMedidaId"]);
        if (Session["UnidadMedidaId"] != null && !string.IsNullOrEmpty(Session["UnidadMedidaId"].ToString()))
        {
            try
            {
                if (Session["UnidadMedidaId"].Equals("null"))
                {
                    UnidadMedidaid = null;
                }
                else
                {
                    UnidadMedidaid = Session["UnidadMedidaId"].ToString();
                }
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["UnidadMedidaId"]);
            }
            if (!string.IsNullOrEmpty(UnidadMedidaid))
            {
                unidadMedidaIdHiddenField.Value = UnidadMedidaid.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx");
        }
        Session["UnidadMedidaId"] = null;
    }

    public void cargarDatosUnidadMedida()
    {
        UnidadMedida theData = null;
        try
        {
            theData = UnidadMedidaBLL.GetUnidadMedidaById(unidadMedidaIdHiddenField.Value);

            if (theData == null)
            {
                Response.Redirect("~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx");
            }

            if (!string.IsNullOrEmpty(theData.UnidadMedidaId) && !string.IsNullOrEmpty(theData.UnidadMedidaId))
            {
                UnidadMedidaIdTextBox.Text = theData.UnidadMedidaId;
                descripcionTextBox.Text = theData.Descripcion;
                SaveUnidadMedida.Visible = false;
                UpdateUnidadMedidaButton.Visible = true;
            }
        }
        catch
        {
            log.Error("Error al obtener la información del Usuario");
        }
    }
    protected void SaveUnidadMedida_Click(object sender, EventArgs e)
    {
        UnidadMedida objUnidadMedida = new UnidadMedida();

        if (!string.IsNullOrEmpty(UnidadMedidaIdTextBox.Text))
        {
            objUnidadMedida.UnidadMedidaId = UnidadMedidaIdTextBox.Text.ToLower();
            ErrorAbreviatura.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objUnidadMedida.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objUnidadMedida.Descripcion) && !string.IsNullOrEmpty(objUnidadMedida.UnidadMedidaId))
        {
            UnidadMedidaBLL.InsertUnidadMedida(objUnidadMedida);
            Response.Redirect("~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx");
        }

    }

    protected void UpdateUnidadMedidaButton_Click(object sender, EventArgs e)
    {
        UnidadMedida objUnidadMedida = new UnidadMedida();
        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objUnidadMedida.UnidadMedidaId = UnidadMedidaIdTextBox.Text.ToLower();
            ErrorAbreviatura.Visible = false;
        }
        else
        {
            ErrorAbreviatura.Visible = true;
        }

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objUnidadMedida.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objUnidadMedida.Descripcion))
        {
            UnidadMedidaBLL.UpdateUnidadMedida(objUnidadMedida, unidadMedidaIdHiddenField.Value);
            Response.Redirect("~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx");
        }
    }
}