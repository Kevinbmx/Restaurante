using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pedido
/// </summary>
public class Pedido
{
    public int PedidoId { get; set; }
    public int UsuarioId { get; set; }
    public int DepartamentoId { get; set; }
    public string Direccion { get; set; }
    public string NombreCliente { get; set; }
    public string ApellidoCliente { get; set; }
    public int Nit { get; set; }
    public DateTime FechaPedido { get; set; }
    public DateTime FechaEntrega { get; set; }
    public string Observacion { get; set; }
    public int CarritoId { get; set; }
    public int TipoPago { get; set; }
    public int VentaId { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }

    public Pedido()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Pedido(int pedidoId, int usuarioId, int departamentoId, string direccion, string nombreCliente, string apellidoCliente,
        int nit, DateTime fechaPedido, DateTime fechaEntrega, string observacion, int carritoId, int tipoPago, int ventaId, decimal montoTotal,
        decimal latitud, decimal longitud)
    {
        PedidoId = pedidoId;
        UsuarioId = usuarioId;
        DepartamentoId = departamentoId;
        Direccion = direccion;
        NombreCliente = nombreCliente;
        ApellidoCliente = apellidoCliente;
        Nit = nit;
        FechaPedido = fechaPedido;
        FechaEntrega = fechaEntrega;
        Observacion = observacion;
        CarritoId = carritoId;
        TipoPago = tipoPago;
        VentaId = ventaId;
        MontoTotal = montoTotal;
        Latitud = latitud;
        Longitud = longitud;
    }
}