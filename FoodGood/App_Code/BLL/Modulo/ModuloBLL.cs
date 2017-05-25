using log4net;
using ModuloDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foodgood.Modulo.Clase;

namespace FoodGood.Modulos.BLL
{
    /// <summary>
    /// Summary description for ModuloBLL
    /// </summary>
    public class ModuloBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public ModuloBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InsertModulos(Modulo objmodulo)
        {
            try
            {
                ModuloTableAdapter localAdapter = new ModuloTableAdapter();
                object resutl = localAdapter.InsertModulos(
                    objmodulo.AreaId,
                    string.IsNullOrEmpty(objmodulo.Descripcion) ? "" : objmodulo.Descripcion);

                log.Debug("Se insertó el usuario " + objmodulo.Descripcion);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el modulo", q);
                throw q;
            }
        }

        public static void UpdateModulo(Modulo objModulo)
        {
            if (objModulo.ModuloId <= 0)
                throw new ArgumentException("El Usuario no puede ser menor o igual a cero.");

            try
            {
                ModuloTableAdapter localAdapter = new ModuloTableAdapter();
                object resutl = localAdapter.UpdateModelo(
                    objModulo.AreaId,
                    string.IsNullOrEmpty(objModulo.Descripcion) ? "" : objModulo.Descripcion,
                    objModulo.ModuloId);

                log.Debug("Se actualizo el Modulo " + objModulo.ModuloId);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Modulo", q);
                throw q;
            }
        }

        public static void DeleteModulo(int moduloId)
        {
            if (moduloId <= 0)
                throw new ArgumentException("El MODULO no puede ser menor o igual a cero.");
            try
            {
                ModuloTableAdapter theAdapter = new ModuloTableAdapter();
                theAdapter.DeleteModulo(moduloId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el Modulo.", ex);
                throw;
            }
        }

        public static Modulo GetModuloById(int IdModulo)
        {
            ModuloTableAdapter localAdapter = new ModuloTableAdapter();

            if (IdModulo <= 0)
                return null;

            Modulo theUser = null;

            try
            {
                ModuloDS.ModuloIdDataTable table = localAdapter.GetModuloById(IdModulo);

                if (table != null && table.Rows.Count > 0)
                {
                    ModuloDS.ModuloIdRow row = table[0];
                    theUser = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("Un error ocurrio mientras obtenia el modulo de la base de dato", q);
                return null;
            }

            return theUser;
        }


        public static List<Modulo> GetModuloListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Modulo> theList = new List<Modulo>();
            Modulo theUser = null;
            ModuloTableAdapter theAdapter = new ModuloTableAdapter();
            try
            {
                ModuloDS.ModuloIdDataTable table = theAdapter.GetModuloForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (ModuloDS.ModuloIdRow row in table.Rows)
                    {
                        theUser = FillUserRecord(row);
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

        public static bool validarSiExisteModulo(int usuarioId, string moduloDescripcion)
        {
            try
            {
                bool existe = false;
                List<string> listaAccesos = AccesoBLL.ListaModuloDescripcionPorUsuarioId(usuarioId);
                for (int i = 0; i < listaAccesos.Count; i++)
                {
                    string descripcion = listaAccesos[i].ToString();
                    if (descripcion.Equals(moduloDescripcion))
                    {
                        return existe = true;
                    }
                }
                return existe;
            }
            catch (Exception ex)
            {
                log.Error("error al obtener la lsita de descripcion de los modulos " + ex);
                throw ex;
            }
        }


        private static Modulo FillUserRecord(ModuloDS.ModuloIdRow row)
        {
            Modulo theNewRecord = new Modulo(
                row.moduloId,
                row.areaId,
                row.IsdescripcionNull() ? "" : row.descripcion);
            return theNewRecord;
        }
    }
}