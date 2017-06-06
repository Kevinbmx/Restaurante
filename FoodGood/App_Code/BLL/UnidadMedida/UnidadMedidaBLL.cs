using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnidadMedidaDSTableAdapters;

namespace FoodGood.UnidadMedida.BLL
{
    /// <summary>
    /// Summary description for UnidadMedidaBLL
    /// </summary>
    public class UnidadMedidaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public UnidadMedidaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InsertUnidadMedida(UnidadMedida objmodulo)
        {
            try
            {
                UnidadMedidaTableAdapter localAdapter = new UnidadMedidaTableAdapter();
                object resutl = localAdapter.InsertUnidadMedida(
                    string.IsNullOrEmpty(objmodulo.UnidadMedidaId) ? "" : objmodulo.UnidadMedidaId,
                    string.IsNullOrEmpty(objmodulo.Descripcion) ? "" : objmodulo.Descripcion);

                log.Debug("Se insertó la unidad de medida " + objmodulo.Descripcion);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar la unidad de medida", q);
                throw q;
            }
        }

        public static void UpdateUnidadMedida(UnidadMedida objUnidadMedida, string unidadMedidaIdComp)
        {
            if (string.IsNullOrEmpty(objUnidadMedida.UnidadMedidaId) && string.IsNullOrEmpty(unidadMedidaIdComp))
                throw new ArgumentException("La UnidadMedida no puede ser nulo.");

            try
            {
                UnidadMedidaTableAdapter localAdapter = new UnidadMedidaTableAdapter();
                object resutl = localAdapter.UpdatUnidadMedida(
                    string.IsNullOrEmpty(objUnidadMedida.UnidadMedidaId) ? "" : objUnidadMedida.UnidadMedidaId,
                    string.IsNullOrEmpty(objUnidadMedida.Descripcion) ? "" : objUnidadMedida.Descripcion,
                    string.IsNullOrEmpty(unidadMedidaIdComp) ? "" : unidadMedidaIdComp);

                log.Debug("Se actualizo la Unidad de medida " + objUnidadMedida.UnidadMedidaId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar la Unidad de medida", q);
                throw q;
            }
        }

        public static void DeleteUnidadMedida(string unidadMedidaId)
        {
            if (string.IsNullOrEmpty(unidadMedidaId))
                throw new ArgumentException("La unidad de medida nula.");
            try
            {
                UnidadMedidaTableAdapter theAdapter = new UnidadMedidaTableAdapter();
                theAdapter.DeleteUnidadMedida(unidadMedidaId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la Unidad de medida.", ex);
                throw;
            }
        }

        public static UnidadMedida GetUnidadMedidaById(string IdUnidadMedida)
        {
            UnidadMedidaTableAdapter localAdapter = new UnidadMedidaTableAdapter();

            if (string.IsNullOrEmpty(IdUnidadMedida))
                return null;

            UnidadMedida theUser = null;

            try
            {
                UnidadMedidaDS.UnidadMedidaDataTable table = localAdapter.GetUnidadMedidaById(IdUnidadMedida);

                if (table != null && table.Rows.Count > 0)
                {
                    UnidadMedidaDS.UnidadMedidaRow row = table[0];
                    theUser = FillUnidadMedidaRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el modulo de la base de dato", q);
                return null;
            }

            return theUser;
        }


        public static List<UnidadMedida> GetUnidadMedidaListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<UnidadMedida> theList = new List<UnidadMedida>();
            UnidadMedida theUser = null;
            UnidadMedidaTableAdapter theAdapter = new UnidadMedidaTableAdapter();
            try
            {
                UnidadMedidaDS.UnidadMedidaDataTable table = theAdapter.GetUnidadMediadForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (UnidadMedidaDS.UnidadMedidaRow row in table.Rows)
                    {
                        theUser = FillUnidadMedidaRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de los usuarios de la base de datos", q);
                return null;
            }
            return theList;
        }


        private static UnidadMedida FillUnidadMedidaRecord(UnidadMedidaDS.UnidadMedidaRow row)
        {
            UnidadMedida theNewRecord = new UnidadMedida(
                row.unidadMedidaId,
                row.descripcion);
            return theNewRecord;
        }
    }
}