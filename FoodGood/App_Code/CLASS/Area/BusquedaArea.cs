using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaArea
/// </summary>
public class BusquedaArea : ConfigColumns
{
    public BusquedaArea()
    : base()
    {
        //BUSCAR POR EL ID DEL USUARIO FOODGOOD
        Column col = new Column("[a].[areaId]", "areaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);


        //BUSCAR POR EL NOMBRE
        col = new Column("[a].[descripcion]", "descripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }

}