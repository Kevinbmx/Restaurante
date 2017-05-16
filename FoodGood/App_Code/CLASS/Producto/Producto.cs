using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Foodgood.Productos.Clase
{
    /// <summary>
    /// Summary description for Producto
    /// </summary>
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedidaId { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int FamiliaId { get; set; }

        public Producto(int productoId, string nombre, string descripcion, string unidadMedidaId, decimal precio, int stock, int familiaId)
        {
            ProductoId = productoId;
            Nombre = nombre;
            Descripcion = descripcion;
            UnidadMedidaId = unidadMedidaId;
            Precio = precio;
            Stock = stock;
            FamiliaId = familiaId;
        }
        public Producto()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}