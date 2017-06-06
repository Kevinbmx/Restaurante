using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Imagen
{
    /// <summary>
    /// Summary description for Imagen
    /// </summary>
    public class Imagen
    {
        public int ImagenId { get; set; }
        public string Titulo { get; set; }
        public long Size { get; set; }
        public string Extencion { get; set; }
        public string Directorio { get; set; }
        public DateTime FechaImagen { get; set; }

        public Imagen()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Imagen(int imagenId, string titulo, long size, string extencion, string directorio, DateTime fechaImagen)
        {
            ImagenId = imagenId;
            Titulo = titulo;
            Size = size;
            Extencion = extencion;
            Directorio = directorio;
            FechaImagen = fechaImagen;
        }
    }
}