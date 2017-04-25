using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaUsuaio
/// </summary>
public class BusquedaUsuaio : ConfigColumns
{
    public BusquedaUsuaio()
     : base()
    {
        //BUSCAR POR EL ID DEL USUARIO FOODGOOD
        Column col = new Column("[u].[usuarioId]", "usuarioId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL NOMBRE
        col = new Column("[u].[Nombre]", "nombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL APELLIDO
        col = new Column("[u].[apellido]", "apellido", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL EMAIL
        col = new Column("[u].[email]", "email", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

    }
}