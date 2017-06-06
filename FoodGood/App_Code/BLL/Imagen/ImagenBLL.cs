using ImagenDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Imagen.BLL
{
    /// <summary>
    /// Summary description for ImagenBLL
    /// </summary>
    public class ImagenBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public ImagenBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int InsertImagen(Imagen objImagen, ref string titulo)
        {
            if (string.IsNullOrEmpty(objImagen.Titulo))
                throw new ArgumentException("El titulo no puede ser nulo o vacío.");
            if (objImagen.Size <= 0)
                throw new ArgumentException("El tamanho de la imagen no puede ser menor o igual a cero.");
            if (string.IsNullOrEmpty(objImagen.Extencion))
                throw new ArgumentException("La extension no puede ser nulo o vacio.");
            int? ImagenId = 0;
            try
            {
                ImagenTableAdapter theAdapter = new ImagenTableAdapter();
                theAdapter.InsertImagen(ref ImagenId,
                                        objImagen.Titulo,
                                        objImagen.Size,
                                        objImagen.Extencion,
                                        objImagen.Directorio,
                                        objImagen.FechaImagen);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Insertar la imagen.", ex);
                throw ex;
            }
            titulo = objImagen.Titulo;
            return (int)ImagenId;
        }




        public static void UpdateProducto(Imagen objImagen)
        {
            if (objImagen.ImagenId <= 0)
                throw new ArgumentException("el id de la Imagen no puede ser menor o igual a cero.");

            try
            {
                ImagenTableAdapter localAdapter = new ImagenTableAdapter();
                object resutl = localAdapter.UpdateImagen(
                    objImagen.Titulo,
                    objImagen.Size,
                    objImagen.Extencion,
                    objImagen.Directorio,
                    objImagen.FechaImagen,
                    objImagen.ImagenId);

                log.Debug("Se actualizo el producto " + objImagen.Titulo);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar la imagen", q);
                throw q;
            }
        }

        public static void DeleteProducto(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("El producto no puede ser menor o igual a cero.");
            try
            {
                ImagenTableAdapter theAdapter = new ImagenTableAdapter();
                theAdapter.DeleteImagen(productoId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la Imagen.", ex);
                throw;
            }
        }

        public static Imagen GetProductoById(int Idprocuto)
        {
            ImagenTableAdapter localAdapter = new ImagenTableAdapter();

            if (Idprocuto <= 0)
                return null;

            Imagen theUser = null;

            try
            {
                ImagenDS.ImagenDataTable table = localAdapter.GetImagenById(Idprocuto);

                if (table != null && table.Rows.Count > 0)
                {
                    ImagenDS.ImagenRow row = table[0];
                    theUser = FillImagenRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el modulo de la base de dato", q);
                return null;
            }

            return theUser;
        }


        //------------------------------------------paginacion-----------------------------------------------------
        public static int SearchFiles(ref List<Imagen> articulos, string where, int pageSize, int firstRow, string ordenar)
        {
            try
            {
                int? totalRows = 0;
                ImagenTableAdapter localAdapter = new ImagenTableAdapter();
                ImagenDS.ImagenDataTable theTable = localAdapter.GetSearchForImagen(where, pageSize, firstRow, ref totalRows, ordenar);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ImagenDS.ImagenRow row in theTable.Rows)
                    {
                        articulos.Add(FillImagenRecord(row));
                    }
                }
                return (int)totalRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //------------------------------------------------------------------------------------------------------------------


        //--------------------------------------------busqueda where---------------------------------------------------------
        public static List<Imagen> GetImagenListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Imagen> theList = new List<Imagen>();
            Imagen theUser = null;
            ImagenTableAdapter theAdapter = new ImagenTableAdapter();
            try
            {
                ImagenDS.ImagenDataTable table = theAdapter.GetSearchImagenForWhere(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (ImagenDS.ImagenRow row in table.Rows)
                    {
                        theUser = FillImagenRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los usuarios de la base de datos", q);
                throw q;
                //return null;
            }
            return theList;
        }



        private static Imagen FillImagenRecord(ImagenDS.ImagenRow row)
        {
            Imagen theNewRecord = new Imagen(
                row.imagenId,
                row.titulo,
                row.size,
                row.extension,
                row.directorio,
                row.fechaImagen);
            return theNewRecord;
        }
    }
}