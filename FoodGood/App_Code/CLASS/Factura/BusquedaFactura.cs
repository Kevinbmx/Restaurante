using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusquedaFactura
/// </summary>
public class BusquedaFactura : ConfigColumns
{
    public BusquedaFactura() : base()
    {
        //BUSCAR POR EL ID DEL LA FACTURA FOODGOOD
        Column col = new Column("[f].[facturaId]", "facturaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        //BUSCAR POR EL NUMERO DE LA FACTURA
        col = new Column("[fa].[numero]", "numero", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL NOMBRE DE LA FACTURA
        col = new Column("[fa].[nombre]", "nombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL nit DE LA FACTURA
        col = new Column("[fa].[nit]", "nit", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL FECHA DE LA FACTURA
        col = new Column("[fa].[fecha]", "fecha", Column.ColumnType.Date);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL FECHA LIMITE EMISION DE LA FACTURA
        col = new Column("[fa].[fechaLimiteEmision]", "fechaLimiteEmision", Column.ColumnType.Date);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL MONTO PALABRA EMISION DE LA FACTURA
        col = new Column("[fa].[montoPalabra]", "montoPalabra", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);


        //BUSCAR POR EL CODIGO AUTORIZACION DE LA FACTURA
        col = new Column("[fa].[codigoAutorizacion]", "codigoAutorizacion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR EL CODIGO AUTORIZACION DE LA FACTURA
        col = new Column("[fa].[codigoControl]", "codigoControl", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        //BUSCAR POR LA VENTAID DE LA FACTURA
        col = new Column("[fa].[ventaId]", "ventaId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
    }
}