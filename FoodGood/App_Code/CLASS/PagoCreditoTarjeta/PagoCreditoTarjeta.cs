using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.PagoCreditoTarjeta
{
    /// <summary>
    /// Summary description for PagoCreditoTarjeta
    /// </summary>
    public class PagoCreditoTarjeta
    {
        public int PagoId { get; set; }
        public int VentaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal SaldoPagar { get; set; }
        public decimal MontoAPagar { get; set; }
        public string NombreTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public PagoCreditoTarjeta()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public PagoCreditoTarjeta(int pagoId, int ventaId, int usuarioId, DateTime fechaPago, decimal saldoPagar, decimal montoPagar, string nombreTarjeta, string numeroTarjeta)
        {
            PagoId = pagoId;
            VentaId = ventaId;
            UsuarioId = usuarioId;
            FechaPago = fechaPago;
            SaldoPagar = saldoPagar;
            MontoAPagar = montoPagar;
            NombreTarjeta = nombreTarjeta;
            NumeroTarjeta = numeroTarjeta;
        }

    }
}