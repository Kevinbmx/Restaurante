using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaAcceso
/// </summary>
public class BusquedaAcceso : ConfigColumns
{
    public BusquedaAcceso()
   : base()
    {
        //BUSCAR POR EL ID DEL usuarioId FOODGOOD
        Column col = new Column("[ac].[usuarioId]", "usuarioId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL MODULOID
        col = new Column("[ac].[moduloId]", "moduloId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL areaIs
        col = new Column("[a].[areaID]", "areaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);



    }
}