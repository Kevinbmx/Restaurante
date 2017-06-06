using FoodGood.Usuario;
using FoodGood.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Autentificacion_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Usuario user = LoginUtilities.GetUserLogged();
            if (user != null)
            {
                Response.Redirect("~/Default.aspx");
            }

        }
    }


    protected void ingresarBotton_Click(object sender, EventArgs e)
    {
        Usuario user = new Usuario();
        if (!string.IsNullOrEmpty(UsuarioTxt.Text))
        {
            errorUsuario.Visible = false;
            user.Email = UsuarioTxt.Text;
        }
        else
        {
            errorUsuario.Visible = true;
        }

        if (!string.IsNullOrEmpty(PasswordTxt.Text))
        {
            ErrorPassword.Visible = false;
            user.Password = PasswordTxt.Text;
        }
        else
        {
            ErrorPassword.Visible = true;
        }

        if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password))
        {
            verificarUsuario(user);
        }
    }

    public void verificarUsuario(Usuario objUsuario)
    {
        string clave = CryptographyFunctions.SHA1HashTheString(objUsuario.Email + objUsuario.Password);
        Usuario user = LoginUtilities.LoginUser(clave);
        if (user != null)
        {
            if (user.TipoUsuarioId == 1)
            {
                Response.Redirect("~/Administracion/MainPages.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

}