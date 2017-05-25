using Foodgood.User.Clase;
using FoodGood.User.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMainMenuScript();
            string currentPage = Page.Request.AppRelativeCurrentExecutionFilePath;
            if (currentPage.Equals("~/Autentificacion/Login.aspx") || currentPage.Equals("~/Autentificacion/Registrar.aspx"))
            {
                listaDefaultd.Visible = false;
            }
            string valorCookies = LoginUtilities.ObtenerLoginCookies();
            if (!string.IsNullOrEmpty(valorCookies))
            {
                Usuario objUsuarioLogueado = LoginUtilities.LoginUser(valorCookies);
                string nombreApellido = objUsuarioLogueado.Nombre + " " + objUsuarioLogueado.Apellido;
                UserFullNameLiteral.Text = nombreApellido.Length > 18 ? nombreApellido.Substring(0, 18) + "..." : nombreApellido;
                usuarioNoLogueado.Visible = false;
                opcionesUsuairo.Visible = true;
            }
            else
            {
                usuarioNoLogueado.Visible = true;
                opcionesUsuairo.Visible = false;

            }
        }
    }

    private void LoadMainMenuScript()
    {
        StringBuilder scriptText = new StringBuilder("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/jquery-1.10.2.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/jquery.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/bootstrap.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/scrollreveal.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/jquery.easing.1.3.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/jquery.magnific-popup.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/creative.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        StringBuilder imagText = new StringBuilder("<img src=\"");
        imagText.Append(ResolveClientUrl("~/img/gorroChef.png"));
        imagText.Append("\" />\n");


        logo_imag.Text = imagText.ToString();
        JqueryAndMainMenuScript.Text = scriptText.ToString();
    }

 

    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {

    }



    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        LoginUtilities.CloseSesion();
        Response.Redirect("~/Default.aspx");
    }
}
