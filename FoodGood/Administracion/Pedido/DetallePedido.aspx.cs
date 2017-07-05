using FoodGood.Pedido;
using FoodGood.Pedido.BLL;
using FoodGood.Usuario;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Pedido_DetallePedido : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        Usuario objUsuario = LoginUtilities.GetUserLogged();
        if (objUsuario != null)
        {
            ProcessSessionParameters();
            LoadOrder();
        }
        else
        {
            Response.Redirect("~/Autentificacion/Login.aspx");
        }
    }



    private void LoadOrder()
    {
        try
        {
            int pedidoId = Convert.ToInt32(PedidoIdHiddenField.Value);
            Pedido objPedido = PedidoBLL.GetPedidoById(pedidoId);
            OrderDetails.PedidoId = pedidoId;
            //ControlAnulacion.PedidoId = pedidoId;
            //ControlAnulacion.UsuarioId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            //PrefixLabel.Visible = false;

            if (objPedido.Observacion == "entregado")
            {
                AnularPedidoButton.Visible = false;
                entregarButton.Visible = false;
                //AssignmentButton.Visible = true;
                //ControlAnulacion.Visible = true;
            }
            if (objPedido.Observacion == "anulado")
            {
                AnularPedidoButton.Visible = false;
                entregarButton.Visible = false;
                //ControlAnulacion.Visible = true;
                //AssignmentButton.Visible = true;
                //PrefixLabel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error("Error loading order", ex);
            throw;
        }
    }

    private void ProcessSessionParameters()
    {
        if (Session["PedidoId"] != null && !string.IsNullOrEmpty(Session["PedidoId"].ToString()))
        {
            try
            {
                int pedidoId = Convert.ToInt32(Session["PedidoId"].ToString());
                PedidoIdHiddenField.Value = pedidoId.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error getting PedidoId from session", ex);
            }
            Session["PedidoId"] = null;
            return;
        }

        Response.Redirect("~/Administracion/Pedido/ListaPedido.aspx");

    }



    protected void AnularPedidoButton_Click(object sender, EventArgs e)
    {
        PedidoBLL.UpdatePedVentFacturaAnulado(Convert.ToInt32(PedidoIdHiddenField.Value));
        LoadOrder();
    }

    protected void entregarButton_Click(object sender, EventArgs e)
    {
        PedidoBLL.UpdatePedVentFacturaEntregado(Convert.ToInt32(PedidoIdHiddenField.Value));
        LoadOrder();
    }
}