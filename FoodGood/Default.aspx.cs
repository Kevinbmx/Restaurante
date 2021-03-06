﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodGood.Familia.BLL;
using FoodGood.Familia;
using FoodGood.Imagen.BLL;
using FoodGood.Imagen;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarFamiliaRepeater();
        }
    }


    public void cargarFamiliaRepeater()
    {
        try
        {
            List<Familia> ListaFamilia = FamiliaBLL.GetFamiliaListForSearch("");
            FamiliaRepeater.DataSource = ListaFamilia;
            FamiliaRepeater.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void FamiliaRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HiddenField imagenlabel = (HiddenField)e.Item.FindControl("imagenlabel");
            Image imagenFamilia = (Image)e.Item.FindControl("imagenFamilia");
            int imagenId = Convert.ToInt32(imagenlabel.Value);
            Imagen objImagen = ImagenBLL.GetImagenById(imagenId);
            if (objImagen == null)
            {
                //imagenlabel.Text = "<img src='img/ImgRestaurante/noImage.jpg' class='img-responsive' alt='no Imagen'/>";
                imagenFamilia.ImageUrl = "~/img/ImgRestaurante/noImage.jpg";
                imagenFamilia.AlternateText = "no hay imagen";
            }
            else
            {
                imagenFamilia.ImageUrl = "~/img/ImgRestaurante/" + objImagen.Titulo;
                imagenFamilia.AlternateText = objImagen.Titulo;
                //imagenlabel.Text = "<img src='img/ImgRestaurante/" + objImagen.Titulo + "' class='img-responsive' alt='" + objImagen.Titulo + "'/>";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
}