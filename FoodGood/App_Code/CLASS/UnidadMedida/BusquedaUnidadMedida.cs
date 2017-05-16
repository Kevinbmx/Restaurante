using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaUnidadMedida
/// </summary>
public class BusquedaUnidadMedida : ConfigColumns
{
    public BusquedaUnidadMedida()
   : base()
    {
        //BUSCAR POR EL ID DEL MODULO FOODGOOD
        Column col = new Column("[um].[unidadMedidaId]", "unidadMedida", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL ID DE LA AREA
        col = new Column("um.[descripcion]", "descripcion", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }
}