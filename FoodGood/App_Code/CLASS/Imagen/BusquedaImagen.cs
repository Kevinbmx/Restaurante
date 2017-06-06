using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaImagen
/// </summary>
public class BusquedaImagen : ConfigColumns
{
    public BusquedaImagen()
    : base()
    {
        //BUSCAR POR EL ID DE LA IMAGEN
        Column col = new Column("[i].[imagenId]", "imagenId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);


        //BUSCAR POR EL TITULO
        col = new Column("[i].[titulo]", "titulo", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }
}