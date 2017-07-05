using log4net;
using PedidoDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Pedido.BLL
{
    /// <summary>
    /// Summary description for PedidoBLL
    /// </summary>
    public class PedidoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public PedidoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static int InsertPedido(Pedido objPedido)
        {
            try
            {
                if (objPedido == null)
                {
                    throw new ArgumentException("El pedidoid no puede ser nulo o vacío.");
                }

                int? PedidoId = 0;
                PedidoTableAdapter localAdapter = new PedidoTableAdapter();
                object resutl = localAdapter.InsertPedido(
                    ref PedidoId,
                    objPedido.UsuarioId,
                    objPedido.DepartamentoId,
                    objPedido.Direccion,
                    objPedido.NombreCliente,
                    objPedido.ApellidoCliente,
                    objPedido.Nit,
                    objPedido.FechaPedido,
                    //objPedido.FechaEntrega,
                    //objPedido.Observacion,
                    objPedido.CarritoId,
                    objPedido.TipoPago,
                    objPedido.VentaId,
                    objPedido.MontoTotal,
                    objPedido.Latitud,
                    objPedido.Longitud);

                log.Debug("Se insertó el pedido al nombre de: " + objPedido.NombreCliente);
                return (int)PedidoId;
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el pedido", q);
                throw q;
            }
        }
        public static void UpdatePedido(Pedido objPedido)
        {
            if (objPedido.PedidoId <= 0)
                throw new ArgumentException("El PedidoId no puede ser menor o igual a cero.");

            try
            {
                PedidoTableAdapter localAdapter = new PedidoTableAdapter();
                object resutl = localAdapter.UpdatePedido(
                    objPedido.UsuarioId,
                    objPedido.DepartamentoId,
                    objPedido.Direccion,
                    objPedido.NombreCliente,
                    objPedido.ApellidoCliente,
                    objPedido.Nit,
                    objPedido.FechaPedido,
                    objPedido.FechaEntrega,
                    objPedido.Observacion,
                    objPedido.CarritoId,
                    objPedido.TipoPago,
                    objPedido.VentaId,
                    objPedido.MontoTotal,
                    objPedido.Latitud,
                    objPedido.Longitud,
                    objPedido.PedidoId);

                log.Debug("Se actualizo El Pedido al nombre de : " + objPedido.NombreCliente);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el pedido", q);
                throw q;
            }
        }

        public static void UpdatePedVentFacturaEntregado(int pedidoId)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("El PedidoId no puede ser nulo no puede ser menor o igual a cero.");

            try
            {
                PedidoTableAdapter localAdapter = new PedidoTableAdapter();
                object resutl = localAdapter.UpdateEstadosVentaPedFact(pedidoId);

                log.Debug("Se actualizo El Pedido al estado de entregado: " + pedidoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el estado del pedido", q);
                throw q;
            }
        }


        public static void UpdatePedVentFacturaAnulado(int pedidoId)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("El PedidoId no puede ser nulo no puede ser menor o igual a cero.");

            try
            {
                PedidoTableAdapter localAdapter = new PedidoTableAdapter();
                object resutl = localAdapter.UpdateEstadosAnulado(pedidoId);

                log.Debug("Se actualizo El Pedido al estado de anulado: " + pedidoId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el estado del pedido", q);
                throw q;
            }
        }


        public static void DeletePedido(int PedidoId)
        {
            if (PedidoId <= 0)
                throw new ArgumentException("el pedidoId no puede ser menor o igual a cero.");

            try
            {
                PedidoTableAdapter theAdapter = new PedidoTableAdapter();
                theAdapter.DeletePedido(PedidoId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el pedido.", ex);
                throw ex;
            }
        }



        public static Pedido GetPedidoById(int PedidoID)
        {
            PedidoTableAdapter localAdapter = new PedidoTableAdapter();

            if (PedidoID <= 0)
                return null;

            Pedido theUser = null;

            try
            {
                PedidoDS.PedidoDataTable table = localAdapter.GetPedidoById(PedidoID);

                if (table != null && table.Rows.Count > 0)
                {
                    PedidoDS.PedidoRow row = table[0];
                    theUser = FillPedidoRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting Pedido data", q);
                return null;
            }

            return theUser;
        }

        //------------------------------------------paginacion-----------------------------------------------------
        public static int SearchProductoPaginacion(ref List<Pedido> articulos, string where, int pageSize, int firstRow, string ordenar)
        {
            try
            {
                int? totalRows = 0;
                PedidoTableAdapter localAdapter = new PedidoTableAdapter();
                PedidoDS.PedidoDataTable theTable = localAdapter.searchPedidoBusqueda(where, pageSize, firstRow, ref totalRows, ordenar);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PedidoDS.PedidoRow row in theTable.Rows)
                    {
                        articulos.Add(FillPedidoRecord(row));
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




        public static List<Pedido> GetPedidoListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Pedido> theList = new List<Pedido>();
            Pedido theUser = null;
            PedidoTableAdapter theAdapter = new PedidoTableAdapter();
            try
            {
                PedidoDS.PedidoDataTable table = theAdapter.GetPedidoForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (PedidoDS.PedidoRow row in table.Rows)
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
        private static Pedido FillPedidoRecord(PedidoDS.PedidoRow row)
        {
            Pedido theNewRecord = new Pedido(
                row.pedidoId,
              row.IsusuarioIdNull() ? 0 : row.usuarioId,
                    row.departamentoId,
                    row.IsdireccionNull() ? "" : row.direccion,
                    row.IsnombreClienteNull() ? "" : row.nombreCliente,
                    row.IsapellidoClienteNull() ? "" : row.apellidoCliente,
                    row.nit,
                    row.fechaPedido,
                    row.IsfechaEntregaNull() ? DateTime.MinValue : row.fechaEntrega,
                    row.IsobservacionEntregaNull() ? "" : row.observacionEntrega,
                    row.carritoId,
                    row.IstipoPagoIdNull() ? 0 : row.tipoPagoId,
                    row.ventaId,
                    row.IsmontoTotalNull() ? 0 : row.montoTotal,
                    row.IslatitudNull() ? 0 : row.latitud,
                    row.IslongitudNull() ? 0 : row.longitud);
            return theNewRecord;
        }
    }
}