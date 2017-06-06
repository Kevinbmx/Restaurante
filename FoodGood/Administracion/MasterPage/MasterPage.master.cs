using FoodGood.Menu.BLL;
//using Foodgood.Menus.Clase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMainMenuScript();
            IsUserAuthorizedPage();
            ConstructMenu();

        }
    }

    private void LoadMainMenuScript()
    {
        StringBuilder scriptText = new StringBuilder("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/jquery-1.10.2.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Script/bootstrap.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Administracion/Script/inspinia.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Administracion/Script/jquery.metisMenu.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Administracion/Script/jquery.slimscroll.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");
        JqueryAndMainMenuScript.Text = scriptText.ToString();
    }

    private bool IsUserAuthorizedPage()
    {
        string currentPage = Page.Request.AppRelativeCurrentExecutionFilePath;

        // The following is a list of all the pages that are open to 
        // authenticated users.  These users do not need specific permissions
        // to access the page. 
        string[] openPages = {
             "~/Administration/MainPages.aspx",
             "~Administracion/TipoUsuario/ListaTipoUsuario.aspx",
             "~Administracion/Cliente/ListaCliente.aspx",
             "~Administracion/Usuario/ListaUsuario.aspx",
             "~Administracion/Area/ListaArea.aspx",
             "~Administracion/Modulo/ListaModulo.aspx",
             "~Administracion/Acceso/ListaAcceso.aspx",
             "~/Administracion/Inventario/UnidadMedida/ListaUnidadMedida.aspx",
             "~/Administracion/Inventario/Familia/ListaFamilia.aspx",
             "~/Administracion/Inventario/Producto/ListaProducto.aspx",
             "~/Administracion/Inventario/ImagenProducto/ListaImagenProducto.aspx"
        };

        for (int i = 0; i < openPages.Length; i++)
        {
            if (currentPage.Equals(openPages[i]))
                return true;
        }

        // SECURITY pages
        //string[] securityPages = new string[] {
        //     "~Administracion/TipoUsuario/ListaTipoUsuario.aspx",
        //     "~Administracion/Cliente/ListaCliente.aspx",
        //     "~Administracion/Usuario/ListaUsuario.aspx",
        //     "~Administracion/Area/ListaArea.aspx",
        //     "~Administracion/Modulo/ListaModulo.aspx",
        //     "~Administracion/Acceso/ListaAcceso.aspx"
        //};

        //for (int i = 0; i < securityPages.Length; i++)
        //{
        //    if (currentPage.Equals(securityPages[i]) &&
        //        LoginSecurity.IsUserAuthorizedPermission("MANAGE_SECURITY"))
        //        return true;
        //}

        // Nothing else worked.  The user should not be allowed to access the page.
        return false;
    }

    private void ConstructMenu()
    {
        List<FoodGood.Menu.Menu> theMenu;
        List<FoodGood.Menu.Menu> theVisibleMenu;
        theMenu = MenuBLL.ReadMenuFromXMLConfiguration();

        List<string> theClases = new List<string>();

        // We have to construct the set of "menu classes" for the user.  These will determine what
        // menus the user has access to.

        //if (!LoginSecurity.IsUserAuthenticated())
        //{
        //    Response.Redirect("~/Authentication/Login.aspx");
        //}

        //theClases.Add("CHANGEPASS");

        //if (LoginSecurity.IsUserAuthorizedPermission("MANAGE_SECURITY"))
        theClases.Add("SECURITY");

        //if (LoginSecurity.IsUserAuthorizedPermission("ADMIN_CLASIFICADORES"))
        //    theClases.Add("CLASIFICADORES");

        //if (LoginSecurity.IsUserAuthorizedPermission("ADMIN_PERSONS"))
        //    theClases.Add("PERSONAS");

        theVisibleMenu = MenuBLL.RecursiveConstructionOfVisibleMenus(theMenu, theClases);
        string visibleXML = MenuBLL.GetMenuXML(theVisibleMenu, 0);
        sideMenu.Text = visibleXML;
        //MainRadMenu.LoadXml(visibleXML);
    }

    protected void cerrarSesion_Click(object sender, EventArgs e)
    {
        LoginUtilities.CloseSesion();
        Response.Redirect("~/Default.aspx");
    }
}
