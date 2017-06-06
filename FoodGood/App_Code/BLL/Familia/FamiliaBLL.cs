using FamiliaDSTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Familia.BLL
{
    /// <summary>
    /// Summary description for FamiliaBLL
    /// </summary>
    public class FamiliaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public FamiliaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertFamilia(Familia objfamilia)
        {
            try
            {
                FamiliaTableAdapter localAdapter = new FamiliaTableAdapter();
                object resutl = localAdapter.InsertFamilia(
                    string.IsNullOrEmpty(objfamilia.Descripcion) ? "" : objfamilia.Descripcion,
                    objfamilia.ImagenId);

                log.Debug("Se insertó la Familia" + objfamilia.Descripcion);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar la familia", q);
                throw q;
            }
        }

        public static void UpdateFamilia(Familia objFamilia)
        {
            if (objFamilia.FamiliaId <= 0)
                throw new ArgumentException("El Usuario no puede ser menor o igual a cero.");

            try
            {
                FamiliaTableAdapter localAdapter = new FamiliaTableAdapter();
                object resutl = localAdapter.UpdateFamilia(
                    string.IsNullOrEmpty(objFamilia.Descripcion) ? "" : objFamilia.Descripcion,
                    objFamilia.ImagenId,
                    objFamilia.FamiliaId);

                log.Debug("Se actualizo la familia con el id " + objFamilia.FamiliaId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar la familia", q);
                throw q;
            }
        }

        public static void DeleteFamilia(int FamiliaId)
        {
            if (FamiliaId <= 0)
                throw new ArgumentException("La familia no puede ser menor o igual a cero.");
            try
            {
                FamiliaTableAdapter theAdapter = new FamiliaTableAdapter();
                theAdapter.DeleteFamilia(FamiliaId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar la familia  .", ex);
                throw;
            }
        }

        public static Familia GetFamiliaById(int IdModulo)
        {
            FamiliaTableAdapter localAdapter = new FamiliaTableAdapter();

            if (IdModulo <= 0)
                return null;

            Familia theFamilia = null;

            try
            {
                FamiliaDS.FamiliaDataTable table = localAdapter.GetFamiliaById(IdModulo);

                if (table != null && table.Rows.Count > 0)
                {
                    FamiliaDS.FamiliaRow row = table[0];
                    theFamilia = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia la familia de la base de dato", q);
                return null;
            }

            return theFamilia;
        }


        public static List<Familia> GetFamiliaListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Familia> theList = new List<Familia>();
            Familia theUser = null;
            FamiliaTableAdapter theAdapter = new FamiliaTableAdapter();
            try
            {
                FamiliaDS.FamiliaDataTable table = theAdapter.GetFamiliaForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (FamiliaDS.FamiliaRow row in table.Rows)
                    {
                        theUser = FillUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("el error ocurrio mientras obtenia la lista de la familia de la base de datos", q);
                return null;
            }
            return theList;
        }


        private static Familia FillUserRecord(FamiliaDS.FamiliaRow row)
        {
            Familia theNewRecord = new Familia(
                row.familiaId,
                row.IsdescripcionNull() ? "" : row.descripcion,
                row.IsimagenIdNull() ? 0 : row.imagenId);
            return theNewRecord;
        }

    }
}