using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Venta
{
    /// <summary>
    /// Summary description for Venta
    /// </summary>
    public class Venta
    {
        public int VentaId { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public int Nit { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PagoTotal { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoDescuento { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public string Estado { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public Venta()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public Venta(int ventaId, string nombreCliente, string apellidoCliente, int nit, decimal montoTotal, decimal pagoTotal, decimal montoCambio,
            decimal montoDescuento, DateTime fechaPedido, DateTime fechaEntrega, DateTime fechaAnulacion, string estado, decimal latitud, decimal longitud)
        {
            VentaId = ventaId;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            Nit = nit;
            MontoTotal = montoTotal;
            PagoTotal = pagoTotal;
            MontoCambio = montoCambio;
            MontoDescuento = montoDescuento;
            FechaPedido = fechaPedido;
            FechaEntrega = fechaEntrega;
            FechaAnulacion = fechaAnulacion;
            Estado = estado;
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}