using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Pedido
{
    /// <summary>
    /// Summary description for PedidoAdministracion
    /// </summary>
    public class PedidoAdministracion
    {
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public int DepartamentoId { get; set; }
        public string NombreDepartamento { get; set; }
        public string Direccion { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public int Nit { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Observacion { get; set; }
        public string CarritoId { get; set; }
        public int TipoPago { get; set; }
        public string NombreTipoPago { get; set; }
        public int VentaId { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        public string FechaPedidoForDisplay
        {
            get { return FechaPedido.ToShortDateString(); }
        }

        public string FechaEntregaForDisplay
        {
            get { return FechaEntrega.ToShortDateString(); }
        }


        public PedidoAdministracion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public PedidoAdministracion(int pedidoId, int usuarioId, string nombreUsuario, string apellidoUsuario,
            int departamentoId, string nombreDepartamento, string direccion, string nombreCliente, string apellidoCliente,
            int nit, DateTime fechaPedido, DateTime fechaEntrega, string observacion, string carritoId, int tipoPago,
            string nombreTipoPago, int ventaId, DateTime fechaAnulacion, decimal montoTotal, decimal latitud, decimal longitud)
        {
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
            NombreUsuario = nombreUsuario;
            ApellidoUsuario = apellidoUsuario;
            DepartamentoId = departamentoId;
            NombreDepartamento = nombreDepartamento;
            Direccion = direccion;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            Nit = nit;
            FechaPedido = fechaPedido;
            FechaEntrega = fechaEntrega;
            Observacion = observacion;
            CarritoId = carritoId;
            TipoPago = tipoPago;
            NombreTipoPago = nombreTipoPago;
            VentaId = ventaId;
            FechaAnulacion = fechaAnulacion;
            MontoTotal = montoTotal;
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}