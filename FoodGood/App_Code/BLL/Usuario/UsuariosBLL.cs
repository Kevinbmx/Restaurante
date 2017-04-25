using Foodgood.User.Clase;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsuarioDSTableAdapters;

namespace FoodGood.User.BLL
{
    /// <summary>
    /// Summary description for UsuariosBLL
    /// </summary>
    public class UsuariosBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public UsuariosBLL()
        {

        }


        public static void InsertUsuario(Usuario objUsuario)
        {
            try
            {
                UsuarioTableAdapter localAdapter = new UsuarioTableAdapter();
                object resutl = localAdapter.InsertUsuario(
                    string.IsNullOrEmpty(objUsuario.Nombre) ? "" : objUsuario.Nombre,
                    string.IsNullOrEmpty(objUsuario.Apellido) ? "" : objUsuario.Apellido,
                    string.IsNullOrEmpty(objUsuario.Password) ? "" : objUsuario.Password,
                    objUsuario.TipoUsuarioId,
                    string.IsNullOrEmpty(objUsuario.Email) ? "" : objUsuario.Email,
                    string.IsNullOrEmpty(objUsuario.Celular1) ? "" : objUsuario.Celular1,
                    string.IsNullOrEmpty(objUsuario.Celular2) ? "" : objUsuario.Celular2,
                    objUsuario.Nit);

                log.Debug("Se insertó el usuario " + objUsuario.Nombre);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al insertar el Usuario", q);
                throw q;
            }
        }
        public static void UpdateUsuario(Usuario objUsuario)
        {
            if (objUsuario.UsuarioId <= 0)
                throw new ArgumentException("El Usuario no puede ser menor o igual a cero.");

            try
            {
                UsuarioTableAdapter localAdapter = new UsuarioTableAdapter();
                object resutl = localAdapter.UpdateUsuario(
                    string.IsNullOrEmpty(objUsuario.Nombre) ? "" : objUsuario.Nombre,
                    string.IsNullOrEmpty(objUsuario.Apellido) ? "" : objUsuario.Apellido,
                    objUsuario.TipoUsuarioId,
                    string.IsNullOrEmpty(objUsuario.Email) ? "" : objUsuario.Email,
                    string.IsNullOrEmpty(objUsuario.Celular1) ? "" : objUsuario.Celular1,
                    string.IsNullOrEmpty(objUsuario.Celular2) ? "" : objUsuario.Celular2,
                    objUsuario.Nit,
                    objUsuario.UsuarioId);

                log.Debug("Se actualizo el usuario " + objUsuario.Nombre);
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al actualizar el Usuario", q);
                throw q;
            }
        }

        public static void DeleteUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("El Usuario no puede ser menor o igual a cero.");

            try
            {
                UsuarioTableAdapter theAdapter = new UsuarioTableAdapter();
                theAdapter.DeleteUsuario(usuarioId);
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error al Eliminar el ClienteDireccion.", ex);
                throw ex;
            }
        }



        public static Usuario GetUserById(int IdUser)
        {
            UsuarioTableAdapter localAdapter = new UsuarioTableAdapter();

            if (IdUser <= 0)
                return null;

            Usuario theUser = null;

            try
            {
                UsuarioDS.UsuarioDataTable table = localAdapter.GetUsuarioById(IdUser);

                if (table != null && table.Rows.Count > 0)
                {
                    UsuarioDS.UsuarioRow row = table[0];
                    theUser = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting user data", q);
                return null;
            }

            return theUser;
        }


        public static List<Usuario> GetUsuarioListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<Usuario> theList = new List<Usuario>();
            Usuario theUser = null;
            UsuarioTableAdapter theAdapter = new UsuarioTableAdapter();
            try
            {
                UsuarioDS.UsuarioDataTable table = theAdapter.GetUsuarioForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (UsuarioDS.UsuarioRow row in table.Rows)
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
        private static Usuario FillUserRecord(UsuarioDS.UsuarioRow row)
        {
            Usuario theNewRecord = new Usuario(
                row.usuarioId,
                row.Nombre,
                row.IsapellidoNull() ? "" : row.apellido,
                row.IspasswordNull() ? "" : row.password,
                row.tipoUsuarioId,
                row.IsemailNull() ? "" : row.email,
                row.Iscelular1Null() ? "" : row.celular1,
                row.Iscelular2Null() ? "" : row.celular2,
                row.IsnitNull() ? 0 : row.nit);
            return theNewRecord;
        }
    }
}