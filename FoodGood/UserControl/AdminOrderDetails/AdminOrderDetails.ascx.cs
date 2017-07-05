using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SearchComponent;
using FoodGood.Pedido;
using FoodGood.Pedido.BLL;

public partial class Administration_UserControls_AdminOrderDetails : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int PedidoId
    {
        set { PedidoIdHiddenField.Value = value.ToString(); }
        get
        {
            int pedidoId = 0;
            try
            {
                pedidoId = Convert.ToInt32(PedidoIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Error trying to convert PedidoIdHiddenField.Value to integer value", ex);
            }
            return pedidoId;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    LoadOrder();
    }
    protected override void OnPreRender(EventArgs e)
    {
        if (!IsPostBack)
            LoadOrder();
    }


    private void LoadOrder()
    {
        try
        {
            Searcher searcher = new Searcher(new BusquedaPedido());
            string query = "p.[pedidoId] = " + PedidoIdHiddenField.Value;

            //int totalRows = 0;
            List<PedidoAdministracion> result = PedidoAdminstracionBLL.GetPedidoAdministracionListForSearch(query);
            if (result.Count != 1)
            {

                return;
            }
            PedidoAdministracion objPedidoAdministracion = result[0];



            FechaPedido.Text = objPedidoAdministracion.FechaPedidoForDisplay;
            Estado.Text = objPedidoAdministracion.Observacion;
            Cliente.Text = objPedidoAdministracion.NombreCliente + " " + objPedidoAdministracion.ApellidoCliente;
            Ciudad.Text = objPedidoAdministracion.NombreDepartamento;
            Direccion.Text = objPedidoAdministracion.Direccion;
            OrderItemsControl.CarritoId = objPedidoAdministracion.CarritoId;
            Total.Text = objPedidoAdministracion.MontoTotal.ToString();

            if (objPedidoAdministracion.Observacion == "pendiente_envio")
            {
                Estado.CssClass = "label label-default";
            }

            if (objPedidoAdministracion.Observacion == "en_camino")
            {
                //PanelUsuarioEntrega.Visible = true;
                //UsuarioEntrega.Text = objPedidoAdministracion.UsuarioEntrega;
                Estado.CssClass = "label label-info";
            }

            if (objPedidoAdministracion.Observacion == "entregado")
            {
                Estado.CssClass = "label label-success";
                PanelDatosEntrega.Visible = true;
                FechaEntrega.Text = string.IsNullOrEmpty(objPedidoAdministracion.FechaEntrega.ToShortDateString()) ? "-" : objPedidoAdministracion.FechaEntregaForDisplay;
                //ObservacionEntrega.Text = objPedidoAdministracion.ObservacionEntrega.Trim() == "" ? "-" : objPedidoAdministracion.ObservacionEntrega;
            }

            if (objPedidoAdministracion.Observacion == "anulado")
            {
                DatosAnulacion.Visible = true;
                //UsuarioAnulacion.Text = objPedidoAdministracion.UsuarioAnulacion;
                FechaAnulacion.Text = string.IsNullOrEmpty(objPedidoAdministracion.FechaAnulacion.ToShortDateString()) ? "-" : objPedidoAdministracion.FechaEntregaForDisplay;
                //MotivoAnulacion.Text = string.IsNullOrEmpty(objPedidoAdministracion.MotivoAnulacion.Trim()) ? "-" : objPedidoAdministracion.MotivoAnulacion;
            }


            GpsControl.Latitud = objPedidoAdministracion.Latitud;
            GpsControl.Longitud = objPedidoAdministracion.Longitud;

        }
        catch (Exception ex)
        { log.Error("Error al cargar la orden.", ex); }
    }
}