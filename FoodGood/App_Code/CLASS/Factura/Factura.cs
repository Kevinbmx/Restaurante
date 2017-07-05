using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGood.Factura
{
    /// <summary>
    /// Summary description for Factura
    /// </summary>
    public class Factura
    {
        public int FacturaId { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaLimiteEmision { get; set; }
        public string MontoPalabra { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string CodigoControl { get; set; }
        public int VentaId { get; set; }

        public Factura()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Factura(int facturaId, string numero, string nombre, string nit, DateTime fecha,
                         DateTime fechaLimiteEmision, string montoPalabra, string codigoAutorizacion,
                         string codigoControl, int ventaId)
        {
            FacturaId = facturaId;
            Numero = numero;
            Nombre = nombre;
            Nit = nit;
            Fecha = fecha;
            FechaLimiteEmision = fechaLimiteEmision;
            MontoPalabra = montoPalabra;
            CodigoAutorizacion = codigoAutorizacion;
            CodigoControl = codigoControl;
            VentaId = ventaId;
        }
    }
}