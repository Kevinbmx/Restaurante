using AreaDSTableAdapters;
using Foodgood.Areas.Clase;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Areas.BLL
{
    /// <summary>
    /// Summary description for AreaBLL
    /// </summary>
    public class AreaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public AreaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InserTArea(Area objUsuario)
        {
            try
            {
                AreaTableAdapter localAdapter = new AreaTableAdapter();
                object resutl = localAdapter.InsertArea(
                    string.IsNullOrEmpty(objUsuario.Descripcion) ? "" : objUsuario.Descripcion);

                log.Debug("Se insertó el area de: " + objUsuario.Descripcion);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Area", q);
                throw q;
            }
        }

        public static void UpdateArea(Area objArea)
        {
            if (objArea.AreaId <= 0)
                throw new ArgumentException("El Area no puede ser menor o igual a cero.");
            try
            {
                AreaTableAdapter localAdapter = new AreaTableAdapter();
                object resutl = localAdapter.UpdateArea(
                    string.IsNullOrEmpty(objArea.Descripcion) ? "" : objArea.Descripcion,
                    objArea.AreaId);

                log.Debug("Se actualizo el area " + objArea.AreaId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Area", q);
                throw q;
            }
        }

        public static void DeleteArea(int areaId)
        {
            if (areaId <= 0)
                throw new ArgumentException("El area no puede ser menor o igual a cero.");
            try
            {
                AreaTableAdapter theAdapter = new AreaTableAdapter();
                theAdapter.DeleteArea(areaId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el Area.", ex);
                throw;
            }
        }
        public static Area GetArea()
        {
            AreaTableAdapter localAdapter = new AreaTableAdapter();

            Area theUser = null;
            try
            {
                AreaDS.AreaDataTable table = localAdapter.GetArea();

                if (table != null && table.Rows.Count > 0)
                {
                    AreaDS.AreaRow row = table[0];
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
        public static Area GetAreaById(int idArea)
        {
            AreaTableAdapter localAdapter = new AreaTableAdapter();

            if (idArea <= 0)
                return null;

            Area theUser = null;
            try
            {
                AreaDS.AreaDataTable table = localAdapter.GetAreaById(idArea);

                if (table != null && table.Rows.Count > 0)
                {
                    AreaDS.AreaRow row = table[0];
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


        public static List<Area> GetModuloListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Area> theList = new List<Area>();
            Area theUser = null;
            AreaTableAdapter theAdapter = new AreaTableAdapter();
            try
            {
                AreaDS.AreaDataTable table = theAdapter.GetAreaForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (AreaDS.AreaRow row in table.Rows)
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


        private static Area FillUserRecord(AreaDS.AreaRow row)
        {
            Area theNewRecord = new Area(
                row.areaID,
                row.IsdescripcionNull() ? "" : row.descripcion);
            return theNewRecord;
        }
    }
}