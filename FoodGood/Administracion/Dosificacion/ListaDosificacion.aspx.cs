using FoodGood.Dosificacion;
using FoodGood.Dosificacion.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Dosificacion_ListaDosificacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarDosificacion("");

        }
    }


    public void cargarDosificacion(string query)
    {
        List<Dosificacion> listaDosificacion = DosificacionBLL.GetCarritoListForSearch(query);
        if (listaDosificacion != null)
        {
            if (listaDosificacion.Count > 0)
            {
                errorDosificaion.Visible = false;
            }
            else
            {
                errorDosificaion.Visible = true;
            }

        }
        ListaDosificacionGridView.DataSource = listaDosificacion;
        ListaDosificacionGridView.DataBind();
    }



    protected void busquedaBtn_Click(object sender, EventArgs e)
    {

    }

    protected void ListaDosificacionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int DosificacionId = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Editar")
        {
            Session["DosificacionId"] = DosificacionId;
            Response.Redirect("~/Administracion/Dosificacion/RegistrarDosificacion.aspx");
        }
    }

    protected void ListaDosificacionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ListaDosificacionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void NewDosificacionButton_Click(object sender, EventArgs e)
    {
        Session["DosificacionId"] = 0;
        Response.Redirect("~/Administracion/Dosificacion/RegistrarDosificacion.aspx");
    }
}