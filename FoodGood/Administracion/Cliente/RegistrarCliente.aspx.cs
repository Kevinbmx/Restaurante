using Foodgood.User.Clase;
using FoodGood.TipoUser.BLL;
using FoodGood.User.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Cliente_RegistrarCliente : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private bool habilitarIdentificadorUsuario = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            //IdentificartipoUsuario();
            if (!string.IsNullOrEmpty(UsuarioIdHiddenField.Value))
            {
                cargarDatosUsuario();
                habilitarIdentificadorUsuario = false;
            }
            IdentificartipoUsuario(habilitarIdentificadorUsuario);
        }
    }

    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["ClienteId"] != null && !string.IsNullOrEmpty(Session["ClienteId"].ToString()))
        {

            try
            {
                ususrioId = Convert.ToInt32(Session["ClienteId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del UsuarioId: " + Session["ClienteId"]);
            }
            if (ususrioId > 0)
            {
                UsuarioIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
        }
        Session["ClienteId"] = null;
    }

    public void cargarDatosUsuario()
    {
        Usuario theData = null;
        //TipoUsuario objTipoUsuario = null;
        try
        {
            theData = UsuariosBLL.GetUserById(Convert.ToInt32(UsuarioIdHiddenField.Value));
            //objTipoUsuario = TipoUsuarioBLL.GetTipoUserById(theData.TipoUsuarioId);
            if (theData == null)
            {
                Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
            }

            if (theData.UsuarioId != 0)
            {
                UserName.Text = theData.Nombre;
                ApellidoTextBox.Text = theData.Apellido;
                SaveUsersAdmin.Visible = false;
                UpdateButton.Visible = true;
                TipoUsuarioComboBox.SelectedValue = Convert.ToString(theData.TipoUsuarioId);
                password.Visible = false;
                password2.Visible = false;
                PasswordHiddenField.Value = theData.Password;
                //TipoUsuarioTextBox.Text = objTipoUsuario.Descripcion;
                //tipoUsuarioIdHiddenField.Value = Convert.ToString(objTipoUsuario.TipoUsuarioId);
                EmailText.Text = theData.Email;
                CellPhoneTextBox.Text = theData.Celular1;
                CellPhoneTextBox2.Text = theData.Celular2;
                numeroNitTextBox.Text = Convert.ToString(theData.Nit);
                if (theData.TipoUsuarioId == 2)
                {
                    email.Visible = false;
                }
                else
                {
                    email.Visible = true;
                }
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

            if (Convert.ToInt64(numeroNitTextBox.Text) > 0)
            {
                objUsuario.Nit = Convert.ToInt64(numeroNitTextBox.Text);
                ErrorApellido.Visible = false;
            }
            else
            {
                ErrorNit.Visible = true;
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
            else
            {
                objUsuario.Celular2 = CellPhoneTextBox2.Text;
            }

            int valor = Convert.ToInt32(TipoUsuarioComboBox.SelectedValue);
            objUsuario.TipoUsuarioId = valor;
            if (valor == 2)
            {
                objUsuario.Password = null;
                objUsuario.Email = null;
                if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) && !string.IsNullOrEmpty(objUsuario.Celular1)
               && objUsuario.Nit > 0)
                {
                    UsuariosBLL.UpdateUsuario(objUsuario);
                    Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
                }
            }
            else
            {

                if (string.IsNullOrEmpty(PasswordHiddenField.Value))

                {
                    if (!string.IsNullOrEmpty(PasswordTextBox.Text))
                    {
                        objUsuario.Password = PasswordTextBox.Text;
                        ErrorPassword.Visible = false;
                    }
                    else
                    {
                        ErrorPassword.Visible = true;
                        return;
                    }
                    //    if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) && !string.IsNullOrEmpty(objUsuario.Celular1)
                    //&& objUsuario.Nit > 0 && !string.IsNullOrEmpty(objUsuario.Email))
                    //    {
                    if (!objUsuario.Password.Equals(ConfirmPassword.Text))
                    {
                        ErrorConfirmar.Visible = true;
                        return;
                    }
                    //}

                }
                else
                {
                    objUsuario.Password = PasswordHiddenField.Value;
                }
                if (!string.IsNullOrEmpty(EmailText.Text))
                {
                    objUsuario.Email = EmailText.Text;
                    ErrorEmail.Visible = false;
                }
                else
                {
                    ErrorEmail.Visible = true;
                    return;
                }
                if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) && !string.IsNullOrEmpty(objUsuario.Celular1)
                 && objUsuario.Nit > 0 && !string.IsNullOrEmpty(objUsuario.Email))
                {
                    UsuariosBLL.UpdateUsuario(objUsuario);
                    Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
                }
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
                return;
            }

            if (!string.IsNullOrEmpty(ApellidoTextBox.Text))
            {
                objUsuario.Apellido = ApellidoTextBox.Text.ToLower();
                ErrorApellido.Visible = false;
            }
            else
            {
                ErrorApellido.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(CellPhoneTextBox.Text))
            {
                objUsuario.Celular1 = CellPhoneTextBox.Text;
                ErrorCelulare.Visible = false;
            }
            else
            {
                ErrorCelulare.Visible = true;
                return;
            }

            if (!string.IsNullOrEmpty(numeroNitTextBox.Text))
            {
                objUsuario.Nit = Convert.ToInt64(numeroNitTextBox.Text);
                ErrorApellido.Visible = false;
            }
            else
            {
                ErrorNit.Visible = true;
                return;
            }




            if (string.IsNullOrEmpty(CellPhoneTextBox2.Text))
            {
                objUsuario.Celular2 = "";
            }
            else
            {
                objUsuario.Celular2 = CellPhoneTextBox2.Text;
            }

            int valor = Convert.ToInt32(TipoUsuarioComboBox.SelectedValue);
            objUsuario.TipoUsuarioId = valor;
            if (valor == 2)
            {
                objUsuario.Password = null;
                objUsuario.Email = null;
                if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) && !string.IsNullOrEmpty(objUsuario.Celular1)
               && objUsuario.Nit > 0)
                {
                    UsuariosBLL.InsertUsuario(objUsuario);
                    Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    objUsuario.Password = PasswordTextBox.Text;
                    ErrorPassword.Visible = false;
                }
                else
                {
                    ErrorPassword.Visible = true;
                    return;
                }

                if (!string.IsNullOrEmpty(EmailText.Text))
                {
                    objUsuario.Email = EmailText.Text;
                    ErrorEmail.Visible = false;
                }
                else
                {
                    ErrorEmail.Visible = true;
                    return;
                }

                if (!string.IsNullOrEmpty(objUsuario.Nombre) && !string.IsNullOrEmpty(objUsuario.Apellido) && !string.IsNullOrEmpty(objUsuario.Celular1)
              && objUsuario.Nit > 0 && !string.IsNullOrEmpty(objUsuario.Email))
                {
                    if (objUsuario.Password.Equals(ConfirmPassword.Text))
                    {
                        UsuariosBLL.InsertUsuario(objUsuario);
                        Response.Redirect("~/Administracion/Cliente/ListaCliente.aspx");
                    }
                    else
                    {
                        ErrorConfirmar.Visible = true;
                    }
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

    public void IdentificartipoUsuario(bool estadoIdentificador)
    {
        if (estadoIdentificador)
        {
            int valor = Convert.ToInt32(TipoUsuarioComboBox.SelectedValue);
            if (valor == 2)
            {
                password.Visible = false;
                password2.Visible = false;
                email.Visible = false;
            }
            else
            {
                password.Visible = true;
                password2.Visible = true;
                email.Visible = true;
            }
        }
    }

    protected void TipoUsuarioComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        IdentificartipoUsuario(true);
    }
}