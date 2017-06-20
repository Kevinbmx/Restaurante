using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Carrito
{
    /// <summary>
    /// Summary description for Carrito
    /// </summary>
    public class Carrito
    {
        public String CarritoId { get; set; }
        public int? UsuarioId { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoVenta { get; set; }

        public Carrito()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Carrito(String carritoId, int? usuarioId, string contenido, DateTime fecha, string estadoVenta)
        {
            CarritoId = carritoId;
            UsuarioId = usuarioId;
            Contenido = contenido;
            Fecha = fecha;
            EstadoVenta = estadoVenta;
        }
    }
}