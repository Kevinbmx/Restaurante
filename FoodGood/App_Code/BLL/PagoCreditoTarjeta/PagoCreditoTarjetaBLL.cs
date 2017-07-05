using log4net;
using PagoCreditoTarjetaDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGood.PagoCreditoTarjeta.BLL
{
    /// <summary>
    /// Summary description for PagoCreditoTarjetaBLL
    /// </summary>
    public class PagoCreditoTarjetaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public PagoCreditoTarjetaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int InsertPagoCreditoTarjeta(PagoCreditoTarjeta objPagoCreditoTarjeta)
        {
            try
            {
                if (objPagoCreditoTarjeta == null)
                {
                    throw new ArgumentException("El pedidoid no puede ser nulo o vacío.");
                }
                int? PagoId = 0;
                PagoCreditoTarjetaTableAdapter localAdapter = new PagoCreditoTarjetaTableAdapter();
                object resutl = localAdapter.InsertPagoCreditoTarjeta(
                    ref PagoId,
                    objPagoCreditoTarjeta.VentaId,
                    objPagoCreditoTarjeta.UsuarioId,
                    objPagoCreditoTarjeta.FechaPago,
                    objPagoCreditoTarjeta.SaldoPagar,
                    objPagoCreditoTarjeta.MontoAPagar,
                    objPagoCreditoTarjeta.NombreTarjeta,
                    objPagoCreditoTarjeta.NumeroTarjeta);

                log.Debug("Se insertó un pago a la ventaId: " + objPagoCreditoTarjeta.VentaId);
                return (int)PagoId;
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el pago", q);
                throw q;
            }
        }

        public static List<PagoCreditoTarjeta> GetPedidoListUsuarioById(int usuairoId)
        {
            if (usuairoId <= 0)
                return null;

            List<PagoCreditoTarjeta> theList = new List<PagoCreditoTarjeta>();
            PagoCreditoTarjeta theUser = null;
            PagoCreditoTarjetaTableAdapter theAdapter = new PagoCreditoTarjetaTableAdapter();
            try
            {
                PagoCreditoTarjetaDS.PagoCreditoTarjetaDataTable table = theAdapter.GetPagoCreditoTarjetaById(usuairoId);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (PagoCreditoTarjetaDS.PagoCreditoTarjetaRow row in table.Rows)
                    {
                        theUser = FillPedidoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los pedido de la base de datos", q);
                return null;
            }
            return theList;
        }

        //public static PagoCreditoTarjeta GetPagoCreditoTarjetaByIdUsuario(int UsuarioId)
        //{
        //    PagoCreditoTarjetaTableAdapter localAdapter = new PagoCreditoTarjetaTableAdapter();

        //    if (UsuarioId <= 0)
        //        return null;

        //    PagoCreditoTarjeta theUser = null;

        //    try
        //    {
        //        PagoCreditoTarjetaDS.PagoCreditoTarjetaDataTable table = localAdapter.GetPagoCreditoTarjetaById(UsuarioId);

        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            PagoCreditoTarjetaDS.PagoCreditoTarjetaRow row = table[0];
        //            theUser = FillPedidoRecord(row);
        //        }
        //    }
        //    catch (Exception q)
        //    {
        //        log.Error("An error was ocurred while geting Pedido data", q);
        //        return null;
        //    }

        //    return theUser;
        //}



        public static List<PagoCreditoTarjeta> GetPedidoListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<PagoCreditoTarjeta> theList = new List<PagoCreditoTarjeta>();
            PagoCreditoTarjeta theUser = null;
            PagoCreditoTarjetaTableAdapter theAdapter = new PagoCreditoTarjetaTableAdapter();
            try
            {
                PagoCreditoTarjetaDS.PagoCreditoTarjetaDataTable table = theAdapter.GetPagoCreditoTarjetaForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (PagoCreditoTarjetaDS.PagoCreditoTarjetaRow row in table.Rows)
                    {
                        theUser = FillPedidoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los pagos de la base de datos", q);
                return null;
            }
            return theList;
        }

        private static PagoCreditoTarjeta FillPedidoRecord(PagoCreditoTarjetaDS.PagoCreditoTarjetaRow row)
        {
            PagoCreditoTarjeta theNewRecord = new PagoCreditoTarjeta(
             row.pagoId,
             row.ventaId,
             row.usuarioId,
             row.fechaPago,
             row.saldoPagar,
             row.montoPagar,
             row.nombreTarjeta,
             row.numeroTarjeta);
            return theNewRecord;
        }
    }
}