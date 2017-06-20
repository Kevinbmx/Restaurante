using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGood.Ubicacion
{
    /// <summary>
    /// Summary description for Ubicacion
    /// </summary>
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        public Ubicacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Ubicacion(int ubicacionId, int usuarioId, string descripcion,
            decimal latitud, decimal longitud)
        {
            UbicacionId = ubicacionId;
            UsuarioId = usuarioId;
            Descripcion = descripcion;
            Latitud = latitud;
            Longitud = longitud;

        }
    }
}