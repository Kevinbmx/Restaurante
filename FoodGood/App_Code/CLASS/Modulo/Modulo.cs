using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace FoodGood.Modulo
{
    /// <summary>
    /// Summary description for Modulo
    /// </summary>
    public class Modulo
    {
        public int ModuloId { get; set; }
        public int AreaId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Descripcion); } }

        public Modulo(int moduloId, int areaId, string descripcion)
        {
            ModuloId = moduloId;
            AreaId = areaId;
            Descripcion = descripcion;
        }

        public Modulo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}