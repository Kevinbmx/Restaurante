using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace Foodgood.Familias.Clase
{
    /// <summary>
    /// Summary description for Familia
    /// </summary>
    public class Familia
    {
        public int FamiliaId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Descripcion); } }

        public Familia(int familiaId, string descripcion)
        {
            FamiliaId = familiaId;
            Descripcion = descripcion;
        }

        public Familia()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}