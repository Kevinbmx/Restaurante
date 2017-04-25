using Foodgood.Areas.Clase;
using FoodGood.Areas.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Area_RegistrarArea : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(AreaIdHiddenField.Value))
            {
                cargarDatosArea();
            }
        }
    }

    public void cargarDatosArea()
    {
        Area theData = null;
        try
        {
            theData = AreaBLL.GetAreaById(Convert.ToInt32(AreaIdHiddenField.Value));

            if (theData == null)
            {
                Response.Redirect("~/Administracion/Area/ListaArea.aspx");
            }

            if (theData.AreaId != 0)
            {
                descripcionTextBox.Text = theData.Descripcion;
                SaveArea.Visible = false;
                UpdateAreaButton.Visible = true;
            }
        }
        catch
        {
            log.Error("Error al obtener la información del Usuario");
        }
    }

    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["AreaId"] != null && !string.IsNullOrEmpty(Session["AreaId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["AreaId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["UsuarioId"]);
            }
            if (ususrioId > 0)
            {
                AreaIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Area/ListaArea.aspx");
        }
        Session["AreaId"] = null;
    }

    protected void SaveArea_Click(object sender, EventArgs e)
    {
        Area objModulo = new Area();

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objModulo.Descripcion = descripcionTextBox.Text;
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objModulo.Descripcion))
        {
            AreaBLL.InserTArea(objModulo);
            Response.Redirect("~/Administracion/Area/ListaArea.aspx");
        }
    }

    protected void UpdateAreaButton_Click(object sender, EventArgs e)
    {
        Area objModulo = new Area();
        objModulo.AreaId = Convert.ToInt32(AreaIdHiddenField.Value);

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objModulo.Descripcion = descripcionTextBox.Text;
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objModulo.Descripcion))
        {
            AreaBLL.UpdateArea(objModulo);
            Response.Redirect("~/Administracion/Area/ListaArea.aspx");
        }
    }
}