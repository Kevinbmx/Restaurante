using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace FoodGood.Area
{
    /// <summary>
    /// Summary description for Area
    /// </summary>
    public class Area
    {
        public int AreaId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Descripcion); } }

        public Area(int areaId, string descripcion)
        {
            AreaId = areaId;
            Descripcion = descripcion;
        }

        public Area()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}