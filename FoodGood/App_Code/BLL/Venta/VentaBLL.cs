using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentaDSTableAdapters;

namespace FoodGood.Venta.BLL
{
    /// <summary>
    /// Summary description for VentaBLL
    /// </summary>
    public class VentaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public VentaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertVenta(Venta objVenta)
        {
            try
            {
                VentaTableAdapter localAdapter = new VentaTableAdapter();
                object resutl = localAdapter.InsertVenta(
                    objVenta.NombreCliente,
                    objVenta.ApellidoCliente,
                    objVenta.Nit,
                    objVenta.MontoTotal,
                    objVenta.PagoTotal,
                    objVenta.MontoCambio,
                    objVenta.MontoDescuento,
                    objVenta.FechaPedido,
                    objVenta.FechaEntrega,
                    objVenta.FechaAnulacion,
                    objVenta.Estado,
                    objVenta.Latitud,
                    objVenta.Longitud);

                log.Debug("Se insertó la venta al nombre de: " + objVenta.NombreCliente);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar la venta", q);
                throw q;
            }
        }
        public static void UpdateVenta(Venta objVenta)
        {
            if (objVenta.VentaId <= 0)
                throw new ArgumentException("la ventaId no puede ser menor o igual a cero.");

            try
            {
                VentaTableAdapter localAdapter = new VentaTableAdapter();
                object resutl = localAdapter.UpdateVenta(
                   objVenta.NombreCliente,
                    objVenta.ApellidoCliente,
                    objVenta.Nit,
                    objVenta.MontoTotal,
                    objVenta.PagoTotal,
                    objVenta.MontoCambio,
                    objVenta.MontoDescuento,
                    objVenta.FechaPedido,
                    objVenta.FechaEntrega,
                    objVenta.FechaAnulacion,
                    objVenta.Estado,
                    objVenta.Latitud,
                    objVenta.Longitud,
                    objVenta.VentaId);

                log.Debug("Se actualizo la venta al nombre de : " + objVenta.NombreCliente);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar la venta", q);
                throw q;
            }
        }

        public static void DeleteVenta(int ventaId)
        {
            if (ventaId <= 0)
                throw new ArgumentException("La venta no puede ser menor o igual a cero.");

            try
            {
                VentaTableAdapter theAdapter = new VentaTableAdapter();
                theAdapter.DeleteVenta(ventaId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la venta.", ex);
                throw ex;
            }
        }



        public static Venta GetVentaById(int ventaID)
        {
            VentaTableAdapter localAdapter = new VentaTableAdapter();

            if (ventaID <= 0)
                return null;

            Venta theUser = null;

            try
            {
                VentaDS.VentaDataTable table = localAdapter.GetVentaById(ventaID);

                if (table != null && table.Rows.Count > 0)
                {
                    VentaDS.VentaRow row = table[0];
                    theUser = FillVentaRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting venta data", q);
                return null;
            }

            return theUser;
        }


        public static List<Venta> GetVentaListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Venta> theList = new List<Venta>();
            Venta theUser = null;
            VentaTableAdapter theAdapter = new VentaTableAdapter();
            try
            {
                VentaDS.VentaDataTable table = theAdapter.GetVentaForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (VentaDS.VentaRow row in table.Rows)
                    {
                        theUser = FillVentaRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de las venta de la base de datos", q);
                return null;
            }
            return theList;
        }
        private static Venta FillVentaRecord(VentaDS.VentaRow row)
        {
            Venta theNewRecord = new Venta(
                row.ventaId,
                row.nombreCliente,
                row.apellidoCliente,
                row.nit,
                row.montoTotal,
                row.pagoTotal,
                row.montoCambio,
                row.montoDescuento,
                row.fechaPedido,
                row.fechaEntrega,
                row.fechaAnulacion,
                row.estado,
                row.latitud,
                row.longitud);
            return theNewRecord;
        }
    }
}