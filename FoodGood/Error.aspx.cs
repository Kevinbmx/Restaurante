using FoodGood.Familia.BLL;
using FoodGood.Familia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    public void cargarFamiliaRepeater()
    {
        List<Familia> ListaFamilia = FamiliaBLL.GetFamiliaListForSearch("");
        FamiliaRepeater.DataSource = ListaFamilia;
        FamiliaRepeater.DataBind();
    }
}