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
}
