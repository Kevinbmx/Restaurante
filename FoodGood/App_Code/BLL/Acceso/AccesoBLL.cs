using AccesoDSTableAdapters;
using Foodgood.Accesos.Clase;
using log4net;
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

    public static List<Acceso> GetAccesoById(int usuarioId , int moduloId)
    {
        List<Acceso> theList = new List<Acceso>();
        Acceso theUser = null;
        AccesosTableAdapter theAdapter = new AccesosTableAdapter();
        try
        {
            AccesoDS.AccesosDataTable table = theAdapter.GetAccesoById(usuarioId, moduloId);

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


    private static Acceso FillUserRecord(AccesoDS.AccesosRow row)
    {
        Acceso theNewRecord = new Acceso(
            row.usuarioId,
            row.moduloId);
        return theNewRecord;
    }
}