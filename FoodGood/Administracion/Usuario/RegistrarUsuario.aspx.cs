using FoodGood.Usuario;
using FoodGood.TipoUsuario.BLL;
using FoodGood.Usuario.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodGood.TipoUsuario;

public partial class Administracion_Usuario_RegistrarUsuario : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(UsuarioIdHiddenField.Value))
            {
                cargarDatosUsuario();
            }
        }
    }

    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["UsuarioId"] != null && !string.IsNullOrEmpty(Session["UsuarioId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["UsuarioId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["UsuarioId"]);
            }
            if (ususrioId > 0)
            {
                UsuarioIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Usuario/ListaUsuario.aspx");
        }
        Session["UsuarioId"] = null;
    }

    public void cargarDatosUsuario()
    {
        Usuario theData = null;
        TipoUsuario objTipoUsuario = null;
        try
        {
            theData = UsuariosBLL.GetUserById(Convert.ToInt32(UsuarioIdHiddenField.Value));
            objTipoUsuario = TipoUsuarioBLL.GetTipoUserById(theData.TipoUsuarioId);
            if (theData == null)
            {
                Response.Redirect("~/Administracion/Usuario/ListaUsuario.aspx");
            }

            if (theData.UsuarioId != 0)
            {
                UserName.Text = theData.Nombre;
                ApellidoTextBox.Text = theData.Apellido;
                PasswordTextBox.Text = theData.Password;
                PasswordTextBox.Visible = false;
                ConfirmPassword.Visible = false;
                PasswordLabel.Visible = false;
                ConfirmPasswordLabel.Visible = false;
                SaveUsersAdmin.Visible = false;
                UpdateButton.Visible = true;
                TipoUsuarioTextBox.Text = objTipoUsuario.Descripcion;
                tipoUsuarioIdHiddenField.Value = Convert.ToString(objTipoUsuario.TipoUsuarioId);
                //PedidoComboBox.SelectedValue = Convert.ToString(theData.TipoUsuarioId);
                EmailText.Text = theData.Email;
                CellPhoneTextBox.Text = theData.Celular1;
                CellPhoneTextBox2.Text = theData.Celular2;
            }
        }
        catch
        {
            log.Error("Error al obtener la información del Usuario");
        }
    }
    public void UpdateUsuario()
    {
        try
        {
            Usuario objUsuario = new Usuario();
            objUsuario.UsuarioId = Convert.ToInt32(UsuarioIdHiddenField.Value);

            if (!string.IsNullOrEmpty(UserName.Text))
            {
                objUsuario.Nombre = UserName.Text;
                ErrorNombre.Visible = false;
            }
            else
            {
                ErrorNombre.Visible = Visible;
            }


            if (!string.IsNullOrEmpty(ApellidoTextBox.Text))
            {
                objUsuario.Apellido = ApellidoTextBox.Text;
                ErrorApellido.Visible = false;
            }
            else
            {
                ErrorApellido.Visible = Visible;
            }

            if (!string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                objUsuario.Password = PasswordTextBox.Text;
            }

            if (!string.IsNullOrEmpty(EmailText.Text))
            {
                objUsuario.Email = EmailText.Text;
                ErrorEmail.Visible = false;

            }
            else
            {
                ErrorEmail.Visible = Visible;
            }

            if (!string.IsNullOrEmpty(CellPhoneTextBox.Text))
            {
                objUsuario.Celular1 = CellPhoneTextBox.Text;
                ErrorCelulare.Visible = false;
            }
            else
            {
                ErrorCelulare.Visible = Visible;
            }
            if (string.IsNullOrEmpty(CellPhoneTextBox2.Text))
            {
                objUsuario.Celular2 = "";
            }
            objUsuario.TipoUsuarioId = Convert.ToInt16(tipoUsuarioIdHiddenField.Value);
            objUsuario.Celular2 = CellPhoneTextBox2.Text;
            objUsuario.Nit = 0;
            if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) &&
               !string.IsNullOrEmpty(objUsuario.Email) && !string.IsNullOrEmpty(objUsuario.Celular1)
              )
            {
                UsuariosBLL.UpdateUsuario(objUsuario);
                Response.Redirect("~/Administracion/Usuario/ListaUsuario.aspx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void SaveUsersAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario objUsuario = new Usuario();

            if (!string.IsNullOrEmpty(UserName.Text))
            {
                objUsuario.Nombre = UserName.Text.ToLower();
                ErrorNombre.Visible = false;
            }
            else
            {
                ErrorNombre.Visible = true;
            }

            if (!string.IsNullOrEmpty(ApellidoTextBox.Text))
            {
                objUsuario.Apellido = ApellidoTextBox.Text.ToLower();
                ErrorApellido.Visible = false;
            }
            else
            {
                ErrorApellido.Visible = true;
            }


            if (!string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                objUsuario.Password = PasswordTextBox.Text;
                ErrorPassword.Visible = false;
            }
            else
            {
                ErrorPassword.Visible = true;
            }

            if (!string.IsNullOrEmpty(EmailText.Text))
            {
                objUsuario.Email = EmailText.Text;
                ErrorEmail.Visible = false;
            }
            else
            {
                ErrorEmail.Visible = true;
            }

            if (!string.IsNullOrEmpty(CellPhoneTextBox.Text))
            {
                objUsuario.Celular1 = CellPhoneTextBox.Text;
                ErrorCelulare.Visible = false;
            }
            else
            {
                ErrorCelulare.Visible = true;
            }
            if (string.IsNullOrEmpty(CellPhoneTextBox2.Text))
            {
                objUsuario.Celular2 = "";
            }
            List<TipoUsuario> objTipoUsuario = TipoUsuarioBLL.GetTipoUser();
            for (int i = 0; i < objTipoUsuario.Count; i++)
            {
                if (objTipoUsuario[i].Descripcion.Equals(TipoUsuarioTextBox.Text))
                {
                    tipoUsuarioIdHiddenField.Value = Convert.ToString(objTipoUsuario[i].TipoUsuarioId);
                }

            }

            objUsuario.TipoUsuarioId = Convert.ToInt16(tipoUsuarioIdHiddenField.Value);
            objUsuario.Celular2 = CellPhoneTextBox2.Text;
            objUsuario.Nit = 0;
            if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) &&
               !string.IsNullOrEmpty(objUsuario.Email) && !string.IsNullOrEmpty(objUsuario.Celular1)
               && !string.IsNullOrEmpty(objUsuario.Password))
            {
                if (objUsuario.Password.Equals(ConfirmPassword.Text))
                {
                    UsuariosBLL.InsertUsuario(objUsuario);
                    ErrorConfirmar.Visible = false;
                    Response.Redirect("~/Administracion/Usuario/ListaUsuario.aspx");
                }
                else
                {
                    ErrorConfirmar.Text = "No coincidió su contraseña";
                    ErrorConfirmar.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaTipoUsuario());
        searcher.Query = query;
        return searcher;
    }


    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        UpdateUsuario();
    }

    //protected void PedidoComboBox_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int idTipoUsuarioId = Convert.ToInt32(tipoUsuarioIdHiddenField);
    //    TipoUsuario theData = null;
    //    try
    //    {
    //        theData = TipoUsuarioBLL.GetTipoUserById(idTipoUsuarioId);

    //        if (theData.Descripcion.Equals("Administrador"))
    //        {

    //        }
    //    }
    //    catch
    //    {
    //        log.Error("Error al obtener la información del tipo de Usuario");
    //    }
    //}
}