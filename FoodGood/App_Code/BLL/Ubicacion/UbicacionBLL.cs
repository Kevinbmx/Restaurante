using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UbicacionDSTableAdapters;

namespace FoodGood.Ubicacion.BLL
{
    /// <summary>
    /// Summary description for UbicacionBLL
    /// </summary>
    public class UbicacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public UbicacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertUbicacion(Ubicacion objAcceso)
        {
            try
            {
                UbicacionTableAdapter localAdapter = new UbicacionTableAdapter();
                object resutl = localAdapter.InsertUbicacionEnvio(
                    objAcceso.UsuarioId,
                    objAcceso.Descripcion,
                    objAcceso.Latitud,
                    objAcceso.Longitud);

                log.Debug("Se insertó la Ubicacion con el usuario Id: " + objAcceso.UsuarioId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Area", q);
                throw q;
            }
        }
        //public static Searcher consultaAccesoSql(string query)
        //{
        //    Searcher searcher = new Searcher(new BusquedaAcceso());
        //    searcher.Query = query;
        //    return searcher;
        //}

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

        //public static List<string> ListaUbicaciononPorUsuarioId(int usuarioId)
        //{
        //    List<FoodGood.Modulo.Modulo> listaModulos = ModuloBLL.GetModuloListForSearch("");

        //    string armadoDeQuery = "@usuarioId IN(" + usuarioId + ")";
        //    string query = consultaAccesoSql(armadoDeQuery).SqlQuery();
        //    List<Acceso> listaAccesoModuloIdDelUsuarioLogueado = AccesoBLL.GetAccesoListForSearch(query);
        //    List<string> moduloDescripcion = new List<string>();
        //    for (int i = 0; i < listaAccesoModuloIdDelUsuarioLogueado.Count; i++)
        //    {
        //        for (int j = 0; j < listaModulos.Count; j++)
        //        {
        //            if (listaAccesoModuloIdDelUsuarioLogueado[i].ModuloId.Equals(listaModulos[j].ModuloId))
        //            {
        //                moduloDescripcion.Add(listaModulos[j].Descripcion);
        //            }
        //        }
        //    }
        //    return moduloDescripcion;
        //}

        public static void DeleteUbicacion(Ubicacion theData)
        {
            if (theData.UbicacionId <= 0)
                throw new ArgumentException("La Ubicacion Id no puede ser nula.");
            try
            {
                UbicacionTableAdapter theAdapter = new UbicacionTableAdapter();
                theAdapter.DeleteUbicacionEnvio(theData.UbicacionId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el Acceso.", ex);
                throw;
            }
        }

        public static List<Ubicacion> GetUbicacionListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Ubicacion> theList = new List<Ubicacion>();
            Ubicacion theUser = null;
            UbicacionTableAdapter theAdapter = new UbicacionTableAdapter();
            try
            {
                UbicacionDS.UbicacionDataTable table = theAdapter.GetUbicacionForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (UbicacionDS.UbicacionRow row in table.Rows)
                    {
                        theUser = FillUbicacionRecord(row);
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
        public static Ubicacion GetUbicaionById(Ubicacion objAcceso)
        {
            UbicacionTableAdapter localAdapter = new UbicacionTableAdapter();

            if (objAcceso.UsuarioId <= 0)
                return null;

            Ubicacion theUser = null;
            try
            {
                UbicacionDS.UbicacionDataTable table = localAdapter.GetUbicacionById(objAcceso.UsuarioId);

                if (table != null && table.Rows.Count > 0)
                {
                    UbicacionDS.UbicacionRow row = table[0];
                    theUser = FillUbicacionRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el Area de la base de dato", q);
                return null;
            }
            return theUser;
        }

        private static Ubicacion FillUbicacionRecord(UbicacionDS.UbicacionRow row)
        {
            Ubicacion theNewRecord = new Ubicacion(
                row.ubicacionId,
                row.usuarioId,
                row.descripcion,
                row.latitud,
                row.longitud);
            return theNewRecord;
        }

    }
}