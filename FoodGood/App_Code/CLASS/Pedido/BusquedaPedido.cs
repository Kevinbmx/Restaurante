using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaPedido
/// </summary>
public class BusquedaPedido : ConfigColumns
{
    public BusquedaPedido()
   : base()
    {
        //BUSCAR POR EL ID DEL MODULO FOODGOOD
        Column col = new Column("[p].[pedidoId]", "productoId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);
        //BUSCAR POR EL nombre del producto
        col = new Column("[p].[usuarioId]", "usuarioId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[p].[nombreCliente]", "nombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

    }
}