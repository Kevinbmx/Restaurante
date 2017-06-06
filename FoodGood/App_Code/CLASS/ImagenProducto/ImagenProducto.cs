using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.ImagenProducto
{
    /// <summary>
    /// Summary description for ImagenProducto
    /// </summary>
    public class ImagenProducto
    {
        public int ImagenProductoId { get; set; }
        public int ProductoId { get; set; }
        public int ImagenId { get; set; }
        public string Titulo { get; set; }

        public ImagenProducto()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ImagenProducto(int imagenProductoId, int productoId, int imagenId)
        {
            ImagenProductoId = imagenProductoId;
            ProductoId = productoId;
            ImagenId = imagenId;
        }

        public ImagenProducto(int imagenProductoId, int productoId, int imagenId, string titulo)
        {
            ImagenProductoId = imagenProductoId;
            ProductoId = productoId;
            ImagenId = imagenId;
            Titulo = titulo;
        }
    }
}