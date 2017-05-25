using AccesoDSTableAdapters;
using Foodgood.Accesos.Clase;
using Foodgood.Modulo.Clase;
using FoodGood.Modulos.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccesoBLL
/// </summary>
public class AccesoBLL
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public AccesoBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void InsertAcceso(Acceso objAcceso)
    {
        try
        {
            AccesosTableAdapter localAdapter = new AccesosTableAdapter();
            object resutl = localAdapter.InsertAcceso(
                objAcceso.UsuarioId,
                objAcceso.ModuloId);

            log.Debug("Se insertó el area de: " + objAcceso.UsuarioId);
        }
        catch (Exception q)
        {
            log.Error("Ocurrió un error al insertar el Area", q);
            throw q;
        }
    }
    public static Searcher consultaAccesoSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaAcceso());
        searcher.Query = query;
        return searcher;
    }

    //public static bool UsuarioTieneAcceso(int usuarioId, string moduloDescripcion)
    //{
    //    string armadoDeQuery = "@usuarioId IN(" + usuarioId + ")";
    //    string query = consultaAccesoSql(armadoDeQuery).SqlQuery();
    //    List<Acceso> listaAccesoModuloIdDelUsuarioLogueado = AccesoBLL.GetAccesoListForSearch(query);





    //    //List<Modulo> lista_Modulos = ModuloBLL.GetModuloListForSearch("@descripcion");
    //    //for (int i = 0; i < lista_Modulos.Count; i++)
    //    //{
    //    //    if (lista_Modulos[i].Descripcion.Equals(moduloDescripcion))
    //    //        return true;
    //    //}
    //    return false;
    //}

    public static List<string> ListaModuloDescripcionPorUsuarioId(int usuarioId)
    {
        List<Modulo> listaModulos = ModuloBLL.GetModuloListForSearch("");

        string armadoDeQuery = "@usuarioId IN(" + usuarioId + ")";
        string query = consultaAccesoSql(armadoDeQuery).SqlQuery();
        List<Acceso> listaAccesoModuloIdDelUsuarioLogueado = AccesoBLL.GetAccesoListForSearch(query);
        List<string> moduloDescripcion = new List<string>();
        for (int i = 0; i < listaAccesoModuloIdDelUsuarioLogueado.Count; i++)
        {
            for (int j = 0; j < listaModulos.Count; j++)
            {
                if (listaAccesoModuloIdDelUsuarioLogueado[i].ModuloId.Equals(listaModulos[j].ModuloId))
                {
                    moduloDescripcion.Add(listaModulos[j].Descripcion);
                }
            }
        }
        return moduloDescripcion;
    }

    public static void DeleteAcceso(Acceso theData)
    {
        if (theData.UsuarioId <= 0 && theData.ModuloId <= 0)
            throw new ArgumentException("El usuario y el modulo no puede ser menor o igual a cero.");
        try
        {
            AccesosTableAdapter theAdapter = new AccesosTableAdapter();
            theAdapter.DeleteAcceso(theData.UsuarioId, theData.ModuloId);
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al Eliminar el Acceso.", ex);
            throw;
        }
    }

    public static List<Acceso> GetAccesoListForSearch(string whereSql)
    {
        if (string.IsNullOrEmpty(whereSql))
            whereSql = "1 = 1";

        List<Acceso> theList = new List<Acceso>();
        Acceso theUser = null;
        AccesosTableAdapter theAdapter = new AccesosTableAdapter();
        try
        {
            AccesoDS.AccesosDataTable table = theAdapter.GetAccesoForSearch(whereSql);

            if (table != null && table.Rows.Count > 0)
            {
                foreach (AccesoDS.AccesosRow row in table.Rows)
                {
                    theUser = FillUserRecord(row);
                    theList.Add(theUser);
                }
            }
        }
        catch (Exception q)
        {
            log.Error("el error ocurrio mientras obtenia la lista del Area de la base de datos", q);
            return null;
        }
        return theList;
    }

    //public static List<Acceso> GetAccesoById(int usuarioId, int moduloId)
    //{
    //    List<Acceso> theList = new List<Acceso>();
    //    Acceso theUser = null;
    //    AccesosTableAdapter theAdapter = new AccesosTableAdapter();
    //    try
    //    {
    //        AccesoDS.AccesosDataTable table = theAdapter.GetAccesoById(usuarioId, moduloId);

    //        if (table != null && table.Rows.Count > 0)
    //        {
    //            foreach (AccesoDS.AccesosRow row in table.Rows)
    //            {
    //                theUser = FillUserRecord(row);
    //                theList.Add(theUser);
    //            }
    //        }
    //    }


    //    catch (Exception q)
    //    {
    //        log.Error("el error ocurrio mientras obtenia la lista del Area de la base de datos", q);
    //        return null;
    //    }

    //    return theList;
    //}
    public static Acceso GetAccesoById(Acceso objAcceso)
    {
        AccesosTableAdapter localAdapter = new AccesosTableAdapter();

        if (objAcceso.UsuarioId <= 0 && objAcceso.ModuloId <= 0)
            return null;

        Acceso theUser = null;
        try
        {
            AccesoDS.AccesosDataTable table = localAdapter.GetAccesoById(objAcceso.UsuarioId, objAcceso.ModuloId);

            if (table != null && table.Rows.Count > 0)
            {
                AccesoDS.AccesosRow row = table[0];
                theUser = FillUserRecord(row);
            }
        }
        catch (Exception q)
        {
            log.Error("Un error ocurrio mientras obtenia el Area de la base de dato", q);
            return null;
        }
        return theUser;
    }

    private static Acceso FillUserRecord(AccesoDS.AccesosRow row)
    {
        Acceso theNewRecord = new Acceso(
            row.usuarioId,
            row.moduloId);
        return theNewRecord;
    }
}