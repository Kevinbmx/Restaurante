using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Foodgood.Modulo.Clase
{
    /// <summary>
    /// Summary description for Modulo
    /// </summary>
    public class Modulo
    {
        public int ModuloId { get; set; }
        public int AreaId { get; set; }
        public string Descripcion { get; set; }


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