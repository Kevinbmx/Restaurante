using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatorProductoCarrito
/// </summary>
public class DatorProductoCarrito
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string UnidadMedidaId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal SubTotal { get; set; }
    public int Stock { get; set; }
    public int FamiliaId { get; set; }
    public int ImagenId { get; set; }

    public string PrecioForDisplay
    {
        get { return Precio.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture); }
    }
    public string SubTotalForDisplay
    { get { return SubTotal.ToString("0,0.00", System.Globalization.CultureInfo.InvariantCulture); } }


    public DatorProductoCarrito()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DatorProductoCarrito(int productoId, string nombre, string descripcion, string unidadMedidaId, int cantidad,
        decimal precio, int stock, int familiaId, int imagenId)
    {
        ProductoId = productoId;
        Nombre = nombre;
        Descripcion = descripcion;
        UnidadMedidaId = unidadMedidaId;
        Cantidad = cantidad;
        Precio = precio;
        Stock = stock;
        FamiliaId = familiaId;
        ImagenId = imagenId;
    }
}