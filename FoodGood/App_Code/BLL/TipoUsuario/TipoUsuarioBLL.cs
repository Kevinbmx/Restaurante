using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TipoUsuarioDSTableAdapters;


namespace FoodGood.TipoUser.BLL
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
        public static TipoUsuario GetTipoUser()
        {
            TipoUsersTableAdapter localAdapter = new TipoUsersTableAdapter();
            TipoUsuario theUser = null;
            try
            {
                TipoUsuarioDS.TipoUsersDataTable table = localAdapter.GetTipoUsuario();

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

        private static TipoUsuario FillTipoUserRecord(TipoUsuarioDS.TipoUsersRow row)
        {
            TipoUsuario theNewRecord = new TipoUsuario(
                row.tipoUsuarioId,
                row.descripcion);
            return theNewRecord;
        }
    }
}