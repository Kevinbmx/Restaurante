using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_GpsSelector : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public Unit Height
    {
        set { MapCanvas1.Height = value; }
        get { return MapCanvas1.Height; }
    }

    public decimal Latitud 
    {
        set { Lat1.Value = value.ToString(CultureInfo.InvariantCulture); }
        get
        {
            decimal value = 0;
            try
            {
                value = Convert.ToDecimal(Lat1.Value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                log.Error("Error trying to convert Lat1.Value to decimal value", ex); 
            }
            return value;
        }
    }

    public decimal Longitud
    {
        set { Lng1.Value = value.ToString(CultureInfo.InvariantCulture); }
        get
        {
            decimal value = 0;
            try
            {
                value = Convert.ToDecimal(Lng1.Value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                log.Error("Error trying to convert Lng1.Value to decimal value", ex);
            }
            return value;
        }
    }

    public bool ReadOnly
    {
        set { ReadonlyHF.Value = value.ToString().ToLower(); }
        get
        {
            bool value = false;
            try
            {
                value = Convert.ToBoolean(ReadonlyHF.Value);
            }
            catch (Exception ex)
            {
                log.Error("Error trying to convert Lng1.Value to decimal value", ex);
            }
            return value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        InputLiteral.Text = "<input type='text' style='display:none' id='" + ClientID + "'>";
        ClearButton.Visible = !ReadOnly;
    }
}