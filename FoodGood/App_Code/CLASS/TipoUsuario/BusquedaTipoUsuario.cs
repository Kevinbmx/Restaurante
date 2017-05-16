using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaTipoUsuario
/// </summary>
public class BusquedaTipoUsuario : ConfigColumns
{
    public BusquedaTipoUsuario()
   : base()
    {
        //BUSCAR POR EL ID DEL TIPO USARIO FOODGOOD
        Column col = new Column("[tu].[tipoUsuarioId]", "tipoUsuarioId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR LA DESCRIPCION
        col = new Column("[tu].[descripcion]", "descripcion", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

    }
}