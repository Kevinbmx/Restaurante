using Foodgood.Familias.Clase;
using FoodGood.Familias.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_Familia_RegistrarFamilia : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(FamiliaIdHiddenField.Value))
            {
                cargarDatosFamilia();
            }
        }
    }


    public void cargarDatosFamilia()
    {
        Familia theData = null;
        try
        {
            theData = FamiliaBLL.GetFamiliaById(Convert.ToInt32(FamiliaIdHiddenField.Value));

            if (theData == null)
            {
                Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
            }

            if (theData.FamiliaId != 0)
            {
                descripcionTextBox.Text = theData.Descripcion;
                SaveFamiliaButton.Visible = false;
                UpdateFamiliaButton.Visible = true;
            }
        }
        catch
        {
            log.Error("Error al obtener la información de la familia");
        }
    }

    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["FamiliaId"] != null && !string.IsNullOrEmpty(Session["FamiliaId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["FamiliaId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la familia id: " + Session["FamiliaId"]);
            }
            if (ususrioId > 0)
            {
                FamiliaIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }
        Session["FamiliaId"] = null;
    }

    protected void SaveFamilia_Click(object sender, EventArgs e)
    {
        Familia objFamila = new Familia();

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objFamila.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objFamila.Descripcion))
        {
            FamiliaBLL.InsertFamilia(objFamila);
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }

    }

    protected void UpdateFamiliaButton_Click(object sender, EventArgs e)
    {
        Familia objFamilia = new Familia();
        objFamilia.FamiliaId = Convert.ToInt32(FamiliaIdHiddenField.Value);

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objFamilia.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objFamilia.Descripcion))
        {
            FamiliaBLL.UpdateFamilia(objFamilia);
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }
    }
}