using FoodGood.Producto;
using log4net;
using ProductoDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Producto.BLL
{
    /// <summary>
    /// Summary description for ProductoBLL
    /// </summary>
    public class ProductoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public ProductoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InserProducto(Producto objProducto)
        {
            try
            {
                ProductoTableAdapter localAdapter = new ProductoTableAdapter();
                object resutl = localAdapter.InsertProducto(
                    string.IsNullOrEmpty(objProducto.Nombre) ? "" : objProducto.Nombre,
                string.IsNullOrEmpty(objProducto.Descripcion) ? "" : objProducto.Descripcion,
                    string.IsNullOrEmpty(objProducto.UnidadMedidaId) ? "" : objProducto.UnidadMedidaId,
                    objProducto.Precio,
                    objProducto.Stock,
                    objProducto.FamiliaId);

                log.Debug("Se insertó el producto " + objProducto.Nombre);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el nombre", q);
                throw q;
            }
        }

        //------------------------------------------paginacion-----------------------------------------------------
        public static int SearchProductoPaginacion(ref List<Producto> articulos, string where, int pageSize, int firstRow, string ordenar)
        {
            try
            {
                int? totalRows = 0;
                ProductoTableAdapter localAdapter = new ProductoTableAdapter();
                ProductoDS.ProductoDataTable theTable = localAdapter.GetSearchForProducto(where, pageSize, firstRow, ref totalRows, ordenar);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProductoDS.ProductoRow row in theTable.Rows)
                    {
                        articulos.Add(FillProdutowithImagenRecord(row));
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



        public static void UpdateProducto(Producto objProducto)
        {
            if (objProducto.ProductoId <= 0)
                throw new ArgumentException("El producto no puede ser menor o igual a cero.");

            try
            {
                ProductoTableAdapter localAdapter = new ProductoTableAdapter();
                object resutl = localAdapter.UpdateProducto(
                    string.IsNullOrEmpty(objProducto.Nombre) ? "" : objProducto.Nombre,
                    string.IsNullOrEmpty(objProducto.Descripcion) ? "" : objProducto.Descripcion,
                    string.IsNullOrEmpty(objProducto.UnidadMedidaId) ? "" : objProducto.UnidadMedidaId,
                    objProducto.Precio,
                    objProducto.Stock,
                    objProducto.FamiliaId,
                    objProducto.ProductoId);

                log.Debug("Se actualizo el producto " + objProducto.ProductoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el producto", q);
                throw q;
            }
        }

        public static void DeleteProducto(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("El producto no puede ser menor o igual a cero.");
            try
            {
                ProductoTableAdapter theAdapter = new ProductoTableAdapter();
                theAdapter.DeleteProducto(productoId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el prooducto.", ex);
                throw;
            }
        }

        public static Producto GetProductoById(int Idprocuto)
        {
            ProductoTableAdapter localAdapter = new ProductoTableAdapter();

            if (Idprocuto <= 0)
                return null;

            Producto theUser = null;

            try
            {
                ProductoDS.ProductoDataTable table = localAdapter.GetProductoById(Idprocuto);

                if (table != null && table.Rows.Count > 0)
                {
                    ProductoDS.ProductoRow row = table[0];
                    theUser = FillProdutowithImagenRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el modulo de la base de dato", q);
                return null;
            }

            return theUser;
        }


        public static List<Producto> GetProductoListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Producto> theList = new List<Producto>();
            Producto theUser = null;
            ProductoTableAdapter theAdapter = new ProductoTableAdapter();
            try
            {
                ProductoDS.ProductoDataTable table = theAdapter.GetProductoForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (ProductoDS.ProductoRow row in table.Rows)
                    {
                        theUser = FillProdutowithImagenRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los productos de la base de datos", q);
                return null;
            }
            return theList;
        }


        private static Producto FillProdutoRecord(ProductoDS.ProductoRow row)
        {
            Producto theNewRecord = new Producto(
                row.productoId,
                row.IsdescripcionNull() ? "" : row.nombre,
                row.IsdescripcionNull() ? "" : row.descripcion,
                row.IsdescripcionNull() ? "" : row.unidadMedidaId,
                row.precio,
                row.stock,
                row.familiaId);
            return theNewRecord;
        }

        private static Producto FillProdutowithImagenRecord(ProductoDS.ProductoRow row)
        {
            Producto theNewRecord = new Producto(
                row.productoId,
                row.IsdescripcionNull() ? "" : row.nombre,
                row.IsdescripcionNull() ? "" : row.descripcion,
                row.IsdescripcionNull() ? "" : row.unidadMedidaId,
                row.precio,
                row.stock,
                row.familiaId,
                row.IsImagenIdNull() ? 0 : row.ImagenId);
            return theNewRecord;
        }

    }
}