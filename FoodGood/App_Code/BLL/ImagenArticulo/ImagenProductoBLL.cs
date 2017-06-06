using ImagenProductoDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.ImagenProducto.BLL
{
    /// <summary>
    /// Summary description for ImagenProductoBLL
    /// </summary>
    public class ImagenProductoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public ImagenProductoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertImageProducto(ImagenProducto newData)
        {
            if (newData == null)
            {
                throw new ArgumentException("el articuloImagen no puede ser null");
            }
            try
            {
                ImagenProductoTableAdapter localAdapter = new ImagenProductoTableAdapter();
                //DateTime? nullDate = null;
                localAdapter.InsertImagenProducto(
                                            newData.ProductoId,
                                            newData.ImagenId
                                            );
                log.Debug("Se insertó la imagen en su Articulo" + newData.ImagenProductoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error mientras se insertaba la imagen en el Articulo", q);
                throw q;
            }
        }


        public static void DeleteImagenProducto(int imagenProductoId)
        {
            if (imagenProductoId <= 0)
                throw new ArgumentException("la imagenproductoID no puede ser menor o igual a cero.");
            try
            {
                ImagenProductoTableAdapter theAdapter = new ImagenProductoTableAdapter();
                theAdapter.DeleteImagenProducto(imagenProductoId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la Imagen del producto.", ex);
                throw;
            }
        }

        public static List<ImagenProducto> GetImagenProductoById(int Idprocuto)
        {
            ImagenProductoTableAdapter localAdapter = new ImagenProductoTableAdapter();

            if (Idprocuto <= 0)
                return null;


            List<ImagenProducto> theList = new List<ImagenProducto>();
            ImagenProducto theUser = null;
            ImagenProductoTableAdapter theAdapter = new ImagenProductoTableAdapter();
            try
            {
                ImagenProductoDS.ImagenProductoDataTable table = theAdapter.GetImagenProductoById(Idprocuto);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (ImagenProductoDS.ImagenProductoRow row in table.Rows)
                    {
                        theUser = FillImagenProductoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de las Imagenes del producto Id :\"" + Idprocuto + "\" de la base de datos", q);
                throw q;
                //return null;
            }
            return theList;
        }


        private static ImagenProducto FillImagenRecord(ImagenProductoDS.ImagenProductoRow row)
        {
            ImagenProducto theNewRecord = new ImagenProducto(
                row.imagenProductoId,
                row.productoId,
                row.imagenId);
            return theNewRecord;
        }

        private static ImagenProducto FillImagenProductoRecord(ImagenProductoDS.ImagenProductoRow row)
        {
            ImagenProducto theNewRecord = new ImagenProducto(
                row.imagenProductoId,
                row.productoId,
                row.imagenId,
                row.titulo);
            return theNewRecord;
        }
    }
}