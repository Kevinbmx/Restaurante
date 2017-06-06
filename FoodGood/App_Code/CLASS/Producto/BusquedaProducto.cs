using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaProducto
/// </summary>
public class BusquedaProducto : ConfigColumns
{
    public BusquedaProducto()
    : base()
    {
        //BUSCAR POR EL ID DEL MODULO FOODGOOD
        Column col = new Column("[p].[productoId]", "productoId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL nombre del producto
        col = new Column("[p].[nombre]", "nombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR la descripcion del producto
        col = new Column("[p].[descripcion]", "descripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);


        //BUSCAR POR la descripcion de la uidad de medida del producto
        col = new Column("[u].[descripcion]", "uniDescripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR la descripcion de la uidad de medida del producto
        col = new Column("[fa].[descripcion]", "familiaDescripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[p].[familiaId]", "familiaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);



    }
}