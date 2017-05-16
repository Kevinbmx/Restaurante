using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaModulo
/// </summary>
public class BusquedaModulo : ConfigColumns
{
    public BusquedaModulo()
       : base()
    {
        //BUSCAR POR EL ID DEL MODULO FOODGOOD
        Column col = new Column("[m].[moduloId]", "moduloId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL ID DE LA AREA
        col = new Column("[m].[areaId]", "areaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR LA DESCRIPCION 
        col = new Column("[m].[descripcion]", "descripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }
}