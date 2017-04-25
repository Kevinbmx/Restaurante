using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TipoUsuario
/// </summary>
public class TipoUsuario
{
    public int TipoUsuarioId { get; set; }
    public string Descripcion { get; set; }

    public TipoUsuario(int tipoUsuarioId, string descripcion)
    {
        TipoUsuarioId = tipoUsuarioId;
        Descripcion = descripcion;
    }

    public TipoUsuario()
    {

    }

}