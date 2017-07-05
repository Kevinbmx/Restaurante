using PedidoAdministracionDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Pedido.BLL
{
    /// <summary>
    /// Summary description for PedidoAdminstracionBLL
    /// </summary>
    public class PedidoAdminstracionBLL
    {
        public PedidoAdminstracionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static List<PedidoAdministracion> GetPedidoAdministracionListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<PedidoAdministracion> theList = new List<PedidoAdministracion>();
            PedidoAdministracion theUser = null;
            PedidoAdministracionTableAdapter theAdapter = new PedidoAdministracionTableAdapter();
            try
            {
                PedidoAdministracionDS.PedidoAdministracionDataTable table = theAdapter.GetPedidoAdministracionForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (PedidoAdministracionDS.PedidoAdministracionRow row in table.Rows)
                    {
                        theUser = FillPedidoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                //log.Error("el error ocurrio mientras obtenia la lista de los pedido de la base de datos", q);
                return null;
            }
            return theList;
        }
        private static PedidoAdministracion FillPedidoRecord(PedidoAdministracionDS.PedidoAdministracionRow row)
        {
            PedidoAdministracion theNewRecord = new PedidoAdministracion(
                row.pedidoId,
                row.IsusuarioIdNull() ? 0 : row.usuarioId,
                row.nombreUsuario,
                row.apellidoUsuario,
                row.departamentoId,
                row.nombreDepartamento,
                row.IsdireccionNull() ? "" : row.direccion,
                row.IsnombreClienteNull() ? "" : row.nombreCliente,
                row.IsapellidoClienteNull() ? "" : row.apellidoCliente,
                row.nit,
                row.fechaPedido,
                row.IsfechaEntregaNull() ? DateTime.MinValue : row.fechaEntrega,
                row.IsobservacionEntregaNull() ? "" : row.observacionEntrega,
                row.carritoId,
                row.IstipoPagoIdNull() ? 0 : row.tipoPagoId,
                row.descripcion,
                row.ventaId,
                row.IsfechaAnulacionNull() ? DateTime.MinValue : row.fechaAnulacion,
                row.IsmontoTotalNull() ? 0 : row.montoTotal,
                row.IslatitudNull() ? 0 : row.latitud,
                row.IslongitudNull() ? 0 : row.longitud);
            return theNewRecord;
        }
    }
}