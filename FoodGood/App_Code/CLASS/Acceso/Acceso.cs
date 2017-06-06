using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Acceso
{
    /// <summary>
    /// Summary description for Acceso
    /// </summary>
    public class Acceso
    {
        public int UsuarioId { get; set; }
        public int ModuloId { get; set; }

        public Acceso(int usuarioId, int moduloId)
        {
            UsuarioId = usuarioId;
            ModuloId = moduloId;
        }


        public Acceso()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}