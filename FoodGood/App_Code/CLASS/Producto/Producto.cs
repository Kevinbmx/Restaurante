using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Producto
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
        public int ImagenId { get; set; }

        public string PrecioForDisplay
        {
            get { return Precio.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture); }
        }

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

        public Producto(int productoId, string nombre, string descripcion, string unidadMedidaId, decimal precio, int stock, int familiaId, int imagenId)
        {
            ProductoId = productoId;
            Nombre = nombre;
            Descripcion = descripcion;
            UnidadMedidaId = unidadMedidaId;
            Precio = precio;
            Stock = stock;
            FamiliaId = familiaId;
            ImagenId = imagenId;
        }
        public Producto()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}