using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace FoodGood.Usuario
{
    /// <summary>
    /// Summary description for Usuario
    /// </summary>
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Email { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public long Nit { get; set; }

        public string NombreForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nombre); } }
        public string ApellidoForDisplay { get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Apellido); } }

        public Usuario(int usuarioId, string nombre, string apellido,
            string password, int tipoUsuarioId, string email, string celular1,
            string celular2, long nit)
        {
            UsuarioId = usuarioId;
            Nombre = nombre;
            Apellido = apellido;
            Password = password;
            TipoUsuarioId = tipoUsuarioId;
            Email = email;
            Celular1 = celular1;
            Celular2 = celular2;
            Nit = nit;
        }

        public Usuario()
        {

        }

    }
}