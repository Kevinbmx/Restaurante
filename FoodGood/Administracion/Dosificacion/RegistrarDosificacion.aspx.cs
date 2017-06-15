using FoodGood.Dosificacion;
using FoodGood.Dosificacion.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Dosificacion_RegistrarDosificacion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(DosificacionIdHiddenField.Value))
            {
                updateDosificacion(DosificacionIdHiddenField.Value);
                SaveDosificacion.Visible = false;
                UpdateDosificacionButton.Visible = true;
            }
        }

    }


    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["DosificacionId"] != null && !string.IsNullOrEmpty(Session["DosificacionId"].ToString()))
        {

            try
            {
                ususrioId = Convert.ToInt32(Session["DosificacionId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la DosificacioinId: " + Session["DosificacioId"]);
            }
            if (ususrioId > 0)
            {
                DosificacionIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Dosificacion/ListaDosificacion.aspx");
        }
        Session["DosificacionId"] = null;
    }

    public void updateDosificacion(string dosificacionId)
    {
        Dosificacion objDosificacion = DosificacionBLL.GetCarritoById(Convert.ToInt32(dosificacionId));
        if (objDosificacion != null)
        {
            desdeTextBox.Text = Convert.ToString(objDosificacion.Desde);
            HastaTextBox.Text = Convert.ToString(objDosificacion.Hasta);
            NumeroAutorizacionTextBox.Text = objDosificacion.NumeroAutorizacion;
            LlaveDosificacionTextBox.Text = objDosificacion.LlaveDosificacion;
            fechaInicio.Text = objDosificacion.FechaInicio.ToString();
            fechaFinal.Text = objDosificacion.FechaFinal.ToString();
            FacturaActualTextBox.Text = Convert.ToString(objDosificacion.FacturaActual);
            NitTextBox.Text = Convert.ToString(objDosificacion.Nit);
            GlosaTextBox.Text = objDosificacion.Glosa;
            estadoLista.SelectedValue = Convert.ToString(objDosificacion.CboEstado);
            //if (objDosificacion.CboEstado == 0)
            //{
            //    estadoLista.Enabled = false;
            //}
            //else
            //{
            //    estadoLista.Enabled = true;
            //}
        }
    }


    protected void SaveDosificacion_Click(object sender, EventArgs e)
    {
        try
        {
            Dosificacion objDosificacion = new Dosificacion();

            if (!string.IsNullOrEmpty(desdeTextBox.Text))
            {
                objDosificacion.Desde = Convert.ToInt32(desdeTextBox.Text);
                ErrorDesde.Visible = false;
            }
            else
            {
                ErrorDesde.Visible = true;
            }


            if (!string.IsNullOrEmpty(HastaTextBox.Text))
            {
                objDosificacion.Hasta = Convert.ToInt32(HastaTextBox.Text);
                errorHasta.Visible = false;
            }
            else
            {
                errorHasta.Visible = true;
            }


            if (!string.IsNullOrEmpty(NumeroAutorizacionTextBox.Text))
            {
                objDosificacion.NumeroAutorizacion = NumeroAutorizacionTextBox.Text;
                errorNumAutorizacion.Visible = false;
            }
            else
            {
                errorNumAutorizacion.Visible = true;
            }


            if (!string.IsNullOrEmpty(LlaveDosificacionTextBox.Text))
            {
                objDosificacion.LlaveDosificacion = LlaveDosificacionTextBox.Text;
                errorLlave.Visible = false;
            }
            else
            {
                errorLlave.Visible = true;
            }

            if (!string.IsNullOrEmpty(fechaInicio.Text))
            {
                objDosificacion.FechaInicio = Convert.ToDateTime(fechaInicio.Text);
                errorFechaInicio.Visible = false;
            }
            else
            {
                errorFechaInicio.Visible = true;
            }

            if (!string.IsNullOrEmpty(fechaFinal.Text))
            {
                objDosificacion.FechaFinal = Convert.ToDateTime(fechaFinal.Text);
                errorFechaFinal.Visible = false;
            }
            else
            {
                errorFechaFinal.Visible = true;
            }

            if (!string.IsNullOrEmpty(FacturaActualTextBox.Text))
            {
                objDosificacion.FacturaActual = Convert.ToInt32(FacturaActualTextBox.Text);
                errorFacturaActual.Visible = false;
            }
            else
            {
                errorFacturaActual.Visible = true;
            }


            if (!string.IsNullOrEmpty(NitTextBox.Text))
            {
                objDosificacion.Nit = Convert.ToInt32(NitTextBox.Text);
                errorNit.Visible = false;
            }
            else
            {
                errorNit.Visible = true;
            }

            if (!string.IsNullOrEmpty(GlosaTextBox.Text))
            {
                objDosificacion.Glosa = GlosaTextBox.Text;
                errorGlosa.Visible = false;
            }
            else
            {
                errorGlosa.Visible = true;
            }


            if (objDosificacion.Desde >= 0 && objDosificacion.Hasta > 0 && !string.IsNullOrEmpty(objDosificacion.NumeroAutorizacion)
                && !string.IsNullOrEmpty(objDosificacion.LlaveDosificacion) && objDosificacion.FechaInicio != null
                && objDosificacion.FechaFinal != null && objDosificacion.FacturaActual > 0 && objDosificacion.Nit > 0
                && !string.IsNullOrEmpty(objDosificacion.Glosa))
            {
                if (existeDosificacionHabilitada())
                {
                    estadoLista.SelectedValue = "0";
                    objDosificacion.CboEstado = Convert.ToInt32(estadoLista.SelectedValue);
                    ClientScript.RegisterStartupScript(GetType(), "js", "alert('no se puede actualizar el estado por que otro esta activado');", true);
                }
                else
                {
                    objDosificacion.CboEstado = Convert.ToInt32(estadoLista.SelectedValue);
                }
                DosificacionBLL.InsertCarrito(objDosificacion);
                Response.Redirect("~/Administracion/Dosificacion/ListaDosificacion.aspx");
            }

            //string cuatroUltimosNumero = fechaInicio.Text.Substring(0, 10);
            //ClientScript.RegisterStartupScript(GetType(), "js", "alert('la fecha es:" + cuatroUltimosNumero + "');", true);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }




    protected void UpdateDosificacionButton_Click(object sender, EventArgs e)
    {
        try
        {
            Dosificacion objDosificacion = new Dosificacion();

            if (!string.IsNullOrEmpty(desdeTextBox.Text))
            {
                objDosificacion.Desde = Convert.ToInt32(desdeTextBox.Text);
                ErrorDesde.Visible = false;
            }
            else
            {
                ErrorDesde.Visible = true;
            }


            if (!string.IsNullOrEmpty(HastaTextBox.Text))
            {
                objDosificacion.Hasta = Convert.ToInt32(HastaTextBox.Text);
                errorHasta.Visible = false;
            }
            else
            {
                errorHasta.Visible = true;
            }


            if (!string.IsNullOrEmpty(NumeroAutorizacionTextBox.Text))
            {
                objDosificacion.NumeroAutorizacion = NumeroAutorizacionTextBox.Text;
                errorNumAutorizacion.Visible = false;
            }
            else
            {
                errorNumAutorizacion.Visible = true;
            }


            if (!string.IsNullOrEmpty(LlaveDosificacionTextBox.Text))
            {
                objDosificacion.LlaveDosificacion = LlaveDosificacionTextBox.Text;
                errorLlave.Visible = false;
            }
            else
            {
                errorLlave.Visible = true;
            }

            if (!string.IsNullOrEmpty(fechaInicio.Text))
            {
                objDosificacion.FechaInicio = Convert.ToDateTime(fechaInicio.Text);
                errorFechaInicio.Visible = false;
            }
            else
            {
                errorFechaInicio.Visible = true;
            }

            if (!string.IsNullOrEmpty(fechaFinal.Text))
            {
                objDosificacion.FechaFinal = Convert.ToDateTime(fechaFinal.Text);
                errorFechaFinal.Visible = false;
            }
            else
            {
                errorFechaFinal.Visible = true;
            }

            if (!string.IsNullOrEmpty(FacturaActualTextBox.Text))
            {
                objDosificacion.FacturaActual = Convert.ToInt32(FacturaActualTextBox.Text);
                errorFacturaActual.Visible = false;
            }
            else
            {
                errorFacturaActual.Visible = true;
            }


            if (!string.IsNullOrEmpty(NitTextBox.Text))
            {
                objDosificacion.Nit = Convert.ToInt32(NitTextBox.Text);
                errorNit.Visible = false;
            }
            else
            {
                errorNit.Visible = true;
            }

            if (!string.IsNullOrEmpty(GlosaTextBox.Text))
            {
                objDosificacion.Glosa = GlosaTextBox.Text;
                errorGlosa.Visible = false;
            }
            else
            {
                errorGlosa.Visible = true;
            }
            if (objDosificacion.Desde >= 0 && objDosificacion.Hasta > 0 && !string.IsNullOrEmpty(objDosificacion.NumeroAutorizacion)
                && !string.IsNullOrEmpty(objDosificacion.LlaveDosificacion) && objDosificacion.FechaInicio != null
                && objDosificacion.FechaFinal != null && objDosificacion.FacturaActual > 0 && objDosificacion.Nit > 0
                && !string.IsNullOrEmpty(objDosificacion.Glosa))
            {
                if (existeDosificacionHabilitada())
                {
                    estadoLista.SelectedValue = "0";
                    objDosificacion.CboEstado = Convert.ToInt32(estadoLista.SelectedValue);
                    ClientScript.RegisterStartupScript(GetType(), "js", "alert('no se puede actualizar el estado por que otro esta activado');", true);
                }
                else
                {
                    objDosificacion.CboEstado = Convert.ToInt32(estadoLista.SelectedValue);
                }
                objDosificacion.DosificacionId = Convert.ToInt32(DosificacionIdHiddenField.Value);
                objDosificacion.CboEstado = Convert.ToInt32(estadoLista.SelectedValue);
                DosificacionBLL.UpdateCarrtio(objDosificacion);
                Response.Redirect("~/Administracion/Dosificacion/ListaDosificacion.aspx");

            }

            //string cuatroUltimosNumero = fechaInicio.Text.Substring(0, 10);
            //ClientScript.RegisterStartupScript(GetType(), "js", "alert('la fecha es:" + cuatroUltimosNumero + "');", true);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public bool existeDosificacionHabilitada()
    {
        try
        {
            bool existehabilitado = false;

            List<Dosificacion> listadosificacion = DosificacionBLL.GetCarritoListForSearch("");
            if (listadosificacion != null)
            {
                for (int i = 0; i < listadosificacion.Count; i++)
                {
                    if (listadosificacion[i].CboEstado == 1)
                    {
                        existehabilitado = true;
                    }
                }
            }
            return existehabilitado;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}