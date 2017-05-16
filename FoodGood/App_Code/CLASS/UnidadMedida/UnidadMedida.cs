using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace Foodgood.UnidadesMedidas.Clase
{
    /// <summary>
    /// Summary description for UnidadMedida
    /// </summary>
    public class UnidadMedida
    {
        public string UnidadMedidaId { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedidaIdForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(UnidadMedidaId); } }
        public string DescripcionForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Descripcion); } }

        public UnidadMedida(string unidadMedidaId, string descripcion)
        {
            UnidadMedidaId = unidadMedidaId;
            Descripcion = descripcion;
        }

        public UnidadMedida()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}