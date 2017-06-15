using DosificacioDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Dosificacion.BLL
{
    /// <summary>
    /// Summary description for DosificacionBLL
    /// </summary>
    public class DosificacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public DosificacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InsertCarrito(Dosificacion objDosificacion)
        {
            try
            {
                DosificacionTableAdapter localAdapter = new DosificacionTableAdapter();
                object resutl = localAdapter.InsertDosificacion(
                    objDosificacion.Desde,
                    objDosificacion.Hasta,
                    objDosificacion.NumeroAutorizacion,
                    objDosificacion.LlaveDosificacion,
                    objDosificacion.FechaInicio,
                    objDosificacion.FechaFinal,
                    objDosificacion.FacturaActual,
                    objDosificacion.Nit,
                    objDosificacion.Glosa,
                    objDosificacion.CboEstado);
                log.Debug("Se insertó el Carrto con el idUsuario de: " + objDosificacion.DosificacionId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Carrito", q);
                throw q;
            }
        }

        public static void UpdateCarrtio(Dosificacion objCarrito)
        {
            if (objCarrito.DosificacionId <= 0)
                throw new ArgumentException("El CarritoId no puede ser nulo.");
            try
            {
                DosificacionTableAdapter localAdapter = new DosificacionTableAdapter();
                object resutl = localAdapter.UpdateDosificacion(
                     objCarrito.Desde,
                    objCarrito.Hasta,
                    objCarrito.NumeroAutorizacion,
                    objCarrito.LlaveDosificacion,
                    objCarrito.FechaInicio,
                    objCarrito.FechaFinal,
                    objCarrito.FacturaActual,
                    objCarrito.Nit,
                    objCarrito.Glosa,
                    objCarrito.CboEstado,
                    objCarrito.DosificacionId);

                log.Debug("Se actualizo el Carrito con el id " + objCarrito.DosificacionId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Carrito", q);
                throw q;
            }
        }

        public static void DeleteCarrito(int carritoId)
        {
            if (carritoId <= 0)
                throw new ArgumentException("El carritoId no puede ser nulo.");
            try
            {
                DosificacionTableAdapter theAdapter = new DosificacionTableAdapter();
                theAdapter.DeleteDosificaion(carritoId);
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
        public static Dosificacion GetCarritoById(int dosificacionId)
        {
            DosificacionTableAdapter localAdapter = new DosificacionTableAdapter();

            if (dosificacionId <= 0)
                return null;

            Dosificacion theUser = null;
            try
            {
                DosificacioDS.DosificacionDataTable table = localAdapter.GetDosificacionById(dosificacionId);

                if (table != null && table.Rows.Count > 0)
                {
                    DosificacioDS.DosificacionRow row = table[0];
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
        public static List<Dosificacion> GetCarritoListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
            {
                whereSql = "1 = 1";
            }

            List<Dosificacion> theList = new List<Dosificacion>();
            Dosificacion theUser = null;
            DosificacionTableAdapter theAdapter = new DosificacionTableAdapter();
            try
            {
                DosificacioDS.DosificacionDataTable table = theAdapter.GetDosificacionForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DosificacioDS.DosificacionRow row in table.Rows)
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


        //public static Dosificacion GetCarritoByIdUsurio(int userId)
        //{
        //    if (userId <= 0)
        //        throw new ArgumentException("userId cannot be equals or less than zero");

        //    Dosificacion objCarrito = null;
        //    DosificacionTableAdapter adapter = new DosificacionTableAdapter();
        //    DosificacioDS.DosificacionDataTable table = adapter.GetDosificacionById(userId);
        //    if (table != null && table.Rows.Count > 0)
        //    {
        //        CarritoDS.CarritoRow row = table[0];
        //        objCarrito = FillCarritoRecord(row);
        //    }
        //    return objCarrito;
        //}

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


        private static Dosificacion FillCarritoRecord(DosificacioDS.DosificacionRow row)
        {
            Dosificacion theNewRecord = new Dosificacion(
                row.dosificacionId,
                row.desde,
                row.hasta,
                row.numeroAutorizacion,
                row.llaveDosificacion,
                row.fechaInicio,
                row.fechaFinal,
                row.facturaActual,
                row.nit,
                row.glosa,
                row.cboEstado);
            return theNewRecord;
        }

    }
}