﻿using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaFamilia
/// </summary>
public class BusquedaFamilia : ConfigColumns
{
    public BusquedaFamilia()
  : base()
    {
        //BUSCAR POR EL ID DEL MODULO FOODGOOD
        Column col = new Column("[fa].[familiaId]", "familiaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL ID DE LA AREA
        col = new Column("[fa].[descripcion]", "descripcion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }
}