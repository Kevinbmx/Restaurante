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


    protected void Codigo_Click(object sender, EventArgs e)
    {
        codigoControl.Text = CodigoControl.generateControlCode("263401600021783", "14374", "3832311", "20171218", "486,30", "#%7s*ugvK@GFsAsa_yW2Dc4kF%xjVK*_@DSKJ8JVqQI}vdNIN=ahsTz3{+MF}RmK|38151|3004004520427#%7s40334530*ugvK@GFs746807401692As2007052955a_yW2D3382761c4|21302368|1HGnW|4E-62-66-62-65|");
        //string llave = "/((\"·)·Q\"$U)·U)=\"UEQW)DK==\"·$(Q·\"@#@·(·$(3489qskfjf3294u829342";
        //codigoControl.Text = CodigoControl.generateControlCode("263401700008676", "1", "1028241027", "20170607", "25981,6", llave);
    }
}