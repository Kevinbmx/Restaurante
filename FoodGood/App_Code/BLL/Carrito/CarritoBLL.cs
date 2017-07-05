using CarritoDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FoodGood.Carrito.BLL
{
    /// <summary>
    /// Summary description for CarritoBLL
    /// </summary>
    public class CarritoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public CarritoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InsertCarrito(Carrito objCarrito)
        {
            try
            {
                CarritoTableAdapter localAdapter = new CarritoTableAdapter();
                object resutl = localAdapter.InsertCarrito(
                    objCarrito.CarritoId,
                    objCarrito.UsuarioId,
                    objCarrito.Contenido,
                    objCarrito.Fecha);
                log.Debug("Se insertó el Carrto con el idUsuario de: " + objCarrito.UsuarioId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Carrito", q);
                throw q;
            }
        }

        public static void UpdateCarrtio(Carrito objCarrito)
        {
            if (string.IsNullOrEmpty(objCarrito.CarritoId))
                throw new ArgumentException("El CarritoId no puede ser nulo.");
            try
            {
                CarritoTableAdapter localAdapter = new CarritoTableAdapter();
                object resutl = localAdapter.UpdateCarrito(
                     objCarrito.UsuarioId,
                    objCarrito.Contenido,
                    objCarrito.Fecha,
                    objCarrito.CarritoId);

                log.Debug("Se actualizo el Carrito con el id " + objCarrito.CarritoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Carrito", q);
                throw q;
            }
        }


        public static void UpdateCarrtioADeshabilitado(string carritoId)
        {
            if (string.IsNullOrEmpty(carritoId))
                throw new ArgumentException("El CarritoId no puede ser nulo.");
            try
            {
                CarritoTableAdapter localAdapter = new CarritoTableAdapter();
                object resutl = localAdapter.actualizarAVentaCarrito(carritoId);

                log.Debug("Se actualizo el Carrito con el id a deshabilitado" + carritoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Carrito a deshabilitado", q);
                throw q;
            }
        }
        public static void DeleteCarrito(string carritoId)
        {
            if (string.IsNullOrEmpty(carritoId))
                throw new ArgumentException("El carritoId no puede ser nulo.");
            try
            {
                CarritoTableAdapter theAdapter = new CarritoTableAdapter();
                theAdapter.DeleteCarrito(carritoId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el carrito.", ex);
                throw;
            }
        }
        //public static Area GetArea()
        //{
        //    AreaTableAdapter localAdapter = new AreaTableAdapter();

        //    Area theUser = null;
        //    try
        //    {
        //        AreaDS.AreaDataTable table = localAdapter.GetArea();

        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            AreaDS.AreaRow row = table[0];
        //            theUser = FillUserRecord(row);
        //        }
        //    }
        //    catch (Exception q)
        //    {
        //        log.Error("Un error ocurrio mientras obtenia el Area de la base de dato", q);
        //        return null;
        //    }
        //    return theUser;
        //}
        public static Carrito GetCarritoById(string idCarrito)
        {
            CarritoTableAdapter localAdapter = new CarritoTableAdapter();

            if (string.IsNullOrEmpty(idCarrito))
                return null;

            Carrito theUser = null;
            try
            {
                CarritoDS.CarritoDataTable table = localAdapter.GetCarritoById(idCarrito);

                if (table != null && table.Rows.Count > 0)
                {
                    CarritoDS.CarritoRow row = table[0];
                    theUser = FillCarritoRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el Area de la base de dato", q);
                return null;
            }
            return theUser;
        }

        //public void obtnerDatosProducto(string CarritoId)
        //{
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    string carritoSerializado = js.Serialize(carrito);
        //    obtnerDatosProducto(CarritoId);
        //}

        //public static Dictionary<string, DatorProductoCarrito> obtnerDatosProducto(string CarritoId)
        //{
        //    string cartJson = "";
        //    if (!string.IsNullOrEmpty(CarritoId))
        //    {
        //        cartJson = CarritoBLL.GetCarritoById(CarritoId).Contenido;
        //    }
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    Dictionary<string, DatorProductoCarrito> carrito = js.Deserialize<Dictionary<string, DatorProductoCarrito>>(cartJson);
        //    return carrito.Values;
        //}

        public static List<Carrito> GetCarritoListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
            {
                whereSql = "1 = 1";
            }

            List<Carrito> theList = new List<Carrito>();
            Carrito theUser = null;
            CarritoTableAdapter theAdapter = new CarritoTableAdapter();
            try
            {
                CarritoDS.CarritoDataTable table = theAdapter.GetCarritoForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (CarritoDS.CarritoRow row in table.Rows)
                    {
                        theUser = FillCarritoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista del Carrito de la base de datos", q);
                //return null;
            }
            return theList;
        }


        public static Carrito GetCarritoByIdUsurio(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("userId cannot be equals or less than zero");

            Carrito objCarrito = null;
            CarritoTableAdapter adapter = new CarritoTableAdapter();
            CarritoDS.CarritoDataTable table = adapter.GetCarritoByIdUsiuario(userId);
            if (table != null && table.Rows.Count > 0)
            {
                CarritoDS.CarritoRow row = table[0];
                objCarrito = FillCarritoRecord(row);
            }
            return objCarrito;
        }

        //public static List<Carrito> GetAreaJoinModuloListForSearch(string whereSql)
        //{
        //    if (string.IsNullOrEmpty(whereSql))
        //    {
        //        whereSql = "1 = 1";
        //    }

        //    List<Area> theList = new List<Area>();
        //    Area theUser = null;
        //    AreaTableAdapter theAdapter = new AreaTableAdapter();
        //    try
        //    {
        //        AreaDS.AreaDataTable table = theAdapter.GetAreaJoinModuloForSearch(whereSql);

        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            foreach (AreaDS.AreaRow row in table.Rows)
        //            {
        //                theUser = FillUserRecord(row);
        //                theList.Add(theUser);
        //            }
        //        }
        //    }
        //    catch (Exception q)
        //    {
        //        log.Error("el error ocurrio mientras obtenia la lista del Area de la base de datos", q);
        //        //return null;
        //    }
        //    return theList;
        //}

        //public static List<Carrito> GetArea()
        //{
        //    List<Carrito> theList = new List<Carrito>();
        //    Carrito theUser = null;
        //    CarritoTableAdapter theAdapter = new CarritoTableAdapter();
        //    try
        //    {
        //        CarritoDS.CarritoDataTable table = theAdapter.GetCarritoById();

        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            foreach (CarritoDS.CarritoRow row in table.Rows)
        //            {
        //                theUser = FillCarritoRecord(row);
        //                theList.Add(theUser);
        //            }
        //        }
        //    }
        //    catch (Exception q)
        //    {
        //        log.Error("el error ocurrio mientras obtenia la lista del Area de la base de datos", q);
        //        return null;
        //    }
        //    return theList;
        //}


        private static Carrito FillCarritoRecord(CarritoDS.CarritoRow row)
        {
            Carrito theNewRecord = new Carrito(
                row.carritoId,
                row.IsusuarioIdNull() ? 0 : row.usuarioId,
                row.contenido,
                row.fecha,
                row.estadoVenta);
            return theNewRecord;
        }
    }
}