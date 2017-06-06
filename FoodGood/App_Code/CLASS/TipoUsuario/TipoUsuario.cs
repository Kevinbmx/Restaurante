using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FoodGood.TipoUsuario
{
    /// <summary>
    /// Summary description for TipoUsuario
    /// </summary>
    public class TipoUsuario
    {
        public int TipoUsuarioId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Descripcion); } }

        public TipoUsuario(int tipoUsuarioId, string descripcion)
        {
            TipoUsuarioId = tipoUsuarioId;
            Descripcion = descripcion;
        }

        public TipoUsuario()
        {

        }
    }
}