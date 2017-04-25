﻿using Foodgood.Areas.Clase;
using Foodgood.Modulo.Clase;
using FoodGood.Areas.BLL;
using FoodGood.Modulos.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Modulo_RegistrarModulo : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(ModuloIdHiddenField.Value))
            {
                cargarDatosArea();
            }
        }
    }

    protected void SaveModulo_Click(object sender, EventArgs e)
    {
        Modulo objModulo = new Modulo();

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
            objModulo.AreaId = Convert.ToInt32(AreaComboBox.SelectedValue);
            ModuloBLL.InsertModulos(objModulo);
            Response.Redirect("~/Administracion/Modulo/ListaModulo.aspx");
        }
    }
    public void cargarDatosArea()
    {
        Modulo theData = null;
        try
        {
            theData = ModuloBLL.GetModuloById(Convert.ToInt32(ModuloIdHiddenField.Value));

            if (theData == null)
            {
                Response.Redirect("~/Administracion/Modulo/ListaModulo.aspx");
            }

            if (theData.AreaId != 0)
            {
                descripcionTextBox.Text = theData.Descripcion;
                SaveModulo.Visible = false;
                UpdateModuloButton.Visible = true;
                AreaComboBox.SelectedValue = Convert.ToString(theData.AreaId);
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
        if (Session["ModuloId"] != null && !string.IsNullOrEmpty(Session["ModuloId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["ModuloId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del ModuloId: " + Session["ModuloId"]);
            }
            if (ususrioId > 0)
            {
                ModuloIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Modulo/ListaModulo.aspx");
        }
        Session["ModuloId"] = null;
    }
    protected void UpdateModuloButton_Click(object sender, EventArgs e)
    {
        Modulo objModulo = new Modulo();
        objModulo.ModuloId = Convert.ToInt32(ModuloIdHiddenField.Value);
        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objModulo.Descripcion = descripcionTextBox.Text;
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objModulo.Descripcion) )
        {
            objModulo.AreaId = Convert.ToInt32(AreaComboBox.SelectedValue);
            ModuloBLL.UpdateModulo(objModulo);
            Response.Redirect("~/Administracion/Modulo/ListaModulo.aspx");
        }
    }
}