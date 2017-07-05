using FacturaDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGood.Factura.BLL
{
    /// <summary>
    /// Summary description for FacturaBLL
    /// </summary>
    public class FacturaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public FacturaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static int InsertFactura(Factura objFactura)
        {
            try
            {
                if (objFactura == null)
                {
                    throw new ArgumentException("La Factura no puede ser nulo o vacío.");
                }

                int? facturaId = 0;
                FacturaTableAdapter localAdapter = new FacturaTableAdapter();
                object resutl = localAdapter.InsertFactura(
                    ref facturaId,
                    objFactura.Numero,
                    objFactura.Nombre,
                    objFactura.Nit,
                    objFactura.Fecha,
                    objFactura.FechaLimiteEmision,
                    objFactura.MontoPalabra,
                    objFactura.CodigoAutorizacion,
                    objFactura.CodigoControl,
                    objFactura.VentaId);

                log.Debug("Se insertó la Factura con el numero de: " + objFactura.Numero);
                return (int)facturaId;
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar la Factura", q);
                throw q;
            }
        }

        public static void UpdateFactura(Factura objFactura)
        {
            if (objFactura.FacturaId <= 0)
                throw new ArgumentException("La factura no puede ser menor o igual a cero.");

            try
            {
                FacturaTableAdapter localAdapter = new FacturaTableAdapter();
                object resutl = localAdapter.UpdateFactura(
                    objFactura.Numero,
                    objFactura.Nombre,
                    objFactura.Nit,
                    objFactura.Fecha,
                    objFactura.FechaLimiteEmision,
                    objFactura.MontoPalabra,
                    objFactura.CodigoAutorizacion,
                    objFactura.CodigoControl,
                    objFactura.VentaId,
                    objFactura.FacturaId);


                log.Debug("Se actualizo la Factura con el id " + objFactura.FacturaId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar la Factura", q);
                throw q;
            }
        }

        public static void DeleteFactura(int FacturaId)
        {
            if (FacturaId <= 0)
                throw new ArgumentException("La Factura no puede ser menor o igual a cero.");
            try
            {
                FacturaTableAdapter theAdapter = new FacturaTableAdapter();
                theAdapter.DeleteFactura(FacturaId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la Factura  .", ex);
                throw;
            }
        }

        public static Factura GetFacturaById(int IdModulo)
        {
            FacturaTableAdapter localAdapter = new FacturaTableAdapter();

            if (IdModulo <= 0)
                return null;

            Factura theFactura = null;

            try
            {
                FacturaDS.FacturaDataTable table = localAdapter.GetFacturaById(IdModulo);

                if (table != null && table.Rows.Count > 0)
                {
                    FacturaDS.FacturaRow row = table[0];
                    theFactura = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia la Factura de la base de dato", q);
                return null;
            }

            return theFactura;
        }


        public static List<Factura> GetFacturaListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Factura> theList = new List<Factura>();
            Factura theUser = null;
            FacturaTableAdapter theAdapter = new FacturaTableAdapter();
            try
            {
                FacturaDS.FacturaDataTable table = theAdapter.GetFacturaForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (FacturaDS.FacturaRow row in table.Rows)
                    {
                        theUser = FillUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de la Factura de la base de datos", q);
                return null;
            }
            return theList;
        }


        private static Factura FillUserRecord(FacturaDS.FacturaRow row)
        {
            Factura theNewRecord = new Factura(
                row.facturaId,
                row.numero,
                row.nombre,
                row.nit,
                row.fecha,
                row.fechaLimiteEmision,
                row.montoPalabra,
                row.codigoAutorizacion,
                row.codigoControl,
                row.ventaId);
            return theNewRecord;
        }
    }
}