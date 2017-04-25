using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Foodgood.Areas.Clase
{
    /// <summary>
    /// Summary description for Area
    /// </summary>
    public class Area
    {
        public int AreaId { get; set; }
        public string Descripcion { get; set; }

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