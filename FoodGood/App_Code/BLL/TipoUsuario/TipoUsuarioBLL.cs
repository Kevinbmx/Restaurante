using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TipoUsuarioDSTableAdapters;


namespace FoodGood.TipoUsuario.BLL
{
    /// <summary>
    /// Summary description for TipoUsuarioBLL
    /// </summary>
    public class TipoUsuarioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public TipoUsuarioBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertTipoUsuario(TipoUsuario objmodulo)
        {
            try
            {
                TipoUsersTableAdapter localAdapter = new TipoUsersTableAdapter();
                object resutl = localAdapter.InserTipoUsuairo(
                    string.IsNullOrEmpty(objmodulo.Descripcion) ? "" : objmodulo.Descripcion);

                log.Debug("Se insertó el Tipo usuario " + objmodulo.Descripcion);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Tipo de usuario", q);
                throw q;
            }
        }

        public static void UpdateTipoUsuario(TipoUsuario objTipUsuario)
        {
            if (objTipUsuario.TipoUsuarioId <= 0)
                throw new ArgumentException("El Usuario no puede ser menor o igual a cero.");

            try
            {
                TipoUsersTableAdapter localAdapter = new TipoUsersTableAdapter();
                object resutl = localAdapter.UpdateTipoUsuario(
                    string.IsNullOrEmpty(objTipUsuario.Descripcion) ? "" : objTipUsuario.Descripcion,
                    objTipUsuario.TipoUsuarioId);

                log.Debug("Se actualizo el Tipo De Usuario " + objTipUsuario.TipoUsuarioId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Tipo De Usuario", q);
                throw q;
            }
        }

        public static void DeleteTipoUsuario(int TipoUsuaioId)
        {
            if (TipoUsuaioId <= 0)
                throw new ArgumentException("El MODULO no puede ser menor o igual a cero.");
            try
            {
                TipoUsersTableAdapter theAdapter = new TipoUsersTableAdapter();
                theAdapter.DeleteTipoUsuario(TipoUsuaioId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el Tipo de Usuario.", ex);
                throw;
            }
        }

        public static List<TipoUsuario> GetTipoUser()
        {
            List<TipoUsuario> theList = new List<TipoUsuario>();
            TipoUsuario theUser = null;
            TipoUsersTableAdapter theAdapter = new TipoUsersTableAdapter();
            try
            {
                TipoUsuarioDS.TipoUsersDataTable table = theAdapter.GetTipoUsuario();

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (TipoUsuarioDS.TipoUsersRow row in table.Rows)
                    {
                        theUser = FillTipoUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("ocurrio un error mientras obtenia el tipoUsuario de la base de datos", q);
                return null;
            }
            return theList;
        }

        public static TipoUsuario GetTipoUserById(int tipoUsuarioId)
        {
            TipoUsersTableAdapter localAdapter = new TipoUsersTableAdapter();
            TipoUsuario theUser = null;
            try
            {
                TipoUsuarioDS.TipoUsersDataTable table = localAdapter.GetTipoUsuarioById(tipoUsuarioId);

                if (table != null && table.Rows.Count > 0)
                {
                    TipoUsuarioDS.TipoUsersRow row = table[0];
                    theUser = FillTipoUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("ocurrio un error mientras obtenia el tipoUsuario de la base de datos", q);
                return null;
            }
            return theUser;
        }

        public static List<TipoUsuario> GetTipoUsuarioListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<TipoUsuario> theList = new List<TipoUsuario>();
            TipoUsuario theUser = null;
            TipoUsersTableAdapter theAdapter = new TipoUsersTableAdapter();
            try
            {
                TipoUsuarioDS.TipoUsersDataTable table = theAdapter.GetTipoUsuarioForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (TipoUsuarioDS.TipoUsersRow row in table.Rows)
                    {
                        theUser = FillTipoUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los Tipo de usuarios de la base de datos", q);
                throw q;
                //return null;
            }
            return theList;
        }


        private static TipoUsuario FillTipoUserRecord(TipoUsuarioDS.TipoUsersRow row)
        {
            TipoUsuario theNewRecord = new TipoUsuario(
                row.tipoUsuarioId,
                row.descripcion);
            return theNewRecord;
        }
    }
}