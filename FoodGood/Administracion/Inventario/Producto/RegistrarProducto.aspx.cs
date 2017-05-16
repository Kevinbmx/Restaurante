using Foodgood.Familias.Clase;
using Foodgood.Productos.Clase;
using Foodgood.UnidadesMedidas.Clase;
using FoodGood.Familias.BLL;
using FoodGood.Productos.BLL;
using FoodGood.UnidadesMedidas.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_Producto_RegistrarProducto : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarUnidadMedidaComboBox("");
            cargarFamiliaComboBox("");
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(ProductoIdHiddenField.Value))
            {
                cargarDatosProductos();
            }
        }
    }

    public void cargarUnidadMedidaComboBox(string query)
    {
        List<UnidadMedida> theData = UnidadMedidaBLL.GetUnidadMedidaListForSearch(query);
        UnidadMedidaComboBox.DataSource = theData;
        UnidadMedidaComboBox.DataValueField = "unidadMedidaId";
        UnidadMedidaComboBox.DataTextField = "descripcion";
        UnidadMedidaComboBox.DataBind();
    }
    public void cargarFamiliaComboBox(string query)
    {
        List<Familia> theData = FamiliaBLL.GetFamiliaListForSearch(query);
        FamiliaComboBox.DataSource = theData;
        FamiliaComboBox.DataValueField = "familiaId";
        FamiliaComboBox.DataTextField = "descripcion";
        FamiliaComboBox.DataBind();
    }

    public void cargarDatosProductos()
    {
        Producto theData = null;
        try
        {
            theData = ProductoBLL.GetProductoById(Convert.ToInt32(ProductoIdHiddenField.Value));

            if (theData == null)
            {
                Response.Redirect("~/Administracion/inventario/Producto/ListaProducto.aspx");
            }

            if (theData.ProductoId != 0)
            {
                NombreTextBox.Text = theData.Nombre;
                DescripcionTextBox.Text = theData.Descripcion;
                UnidadMedidaComboBox.SelectedValue = theData.UnidadMedidaId;
                PrecioTextBox.Text = Convert.ToString(theData.Precio);
                stockTextBox.Text = Convert.ToString(theData.Stock);
                FamiliaComboBox.SelectedValue = Convert.ToString(theData.FamiliaId);
                SaveProducto.Visible = false;
                UpdateProductoButton.Visible = true;
            }
        }
        catch
        {
            log.Error("Error al obtener la información del producto");
        }
    }

    private void ProcessSessionParameteres()
    {
        int productoId = 0;
        if (Session["ProductoId"] != null && !string.IsNullOrEmpty(Session["ProductoId"].ToString()))
        {
            try
            {
                productoId = Convert.ToInt32(Session["ProductoId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion del productoId: " + Session["ProductoId"]);
            }
            if (productoId > 0)
            {
                ProductoIdHiddenField.Value = productoId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Inventario/Producto/ListaProducto.aspx");
        }
        Session["ProductoId"] = null;
    }

    protected void SaveProducto_Click(object sender, EventArgs e)
    {
        Producto objProducto = new Producto();

        if (!string.IsNullOrEmpty(NombreTextBox.Text))
        {
            objProducto.Nombre = NombreTextBox.Text.ToLower();
            ErrorNombre.Visible = false;
        }
        else
        {
            ErrorNombre.Visible = true;
        }


        if (!string.IsNullOrEmpty(DescripcionTextBox.Text))
        {
            objProducto.Descripcion = DescripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(PrecioTextBox.Text))
        {
            objProducto.Precio = Convert.ToDecimal(PrecioTextBox.Text);
            ErrorPrecio.Visible = false;
        }
        else
        {
            ErrorPrecio.Visible = true;
        }

        if (!string.IsNullOrEmpty(stockTextBox.Text))
        {
            objProducto.Stock = Convert.ToInt32(stockTextBox.Text);
            ErrorStock.Visible = false;
        }
        else
        {
            ErrorStock.Visible = true;
        }

        if (!string.IsNullOrEmpty(objProducto.Nombre) && !string.IsNullOrEmpty(objProducto.Descripcion) && objProducto.Precio >= 0 &&
            objProducto.Stock >= 0)
        {
            objProducto.UnidadMedidaId = UnidadMedidaComboBox.SelectedValue;
            objProducto.FamiliaId = Convert.ToInt32(FamiliaComboBox.SelectedValue);
            ProductoBLL.InserProducto(objProducto);
            Response.Redirect("~/Administracion/Inventario/Producto/ListaProducto.aspx");
        }
    }

    protected void UpdateProductoButton_Click(object sender, EventArgs e)
    {
        Producto objProducto = new Producto();
        objProducto.ProductoId = Convert.ToInt32(ProductoIdHiddenField.Value);
        if (!string.IsNullOrEmpty(NombreTextBox.Text))
        {
            objProducto.Nombre = NombreTextBox.Text.ToLower();
            ErrorNombre.Visible = false;
        }
        else
        {
            ErrorNombre.Visible = true;
        }

        if (!string.IsNullOrEmpty(DescripcionTextBox.Text))
        {
            objProducto.Descripcion = DescripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(PrecioTextBox.Text))
        {
            objProducto.Precio = Convert.ToDecimal(PrecioTextBox.Text);
            ErrorPrecio.Visible = false;
        }
        else
        {
            ErrorPrecio.Visible = true;
        }

        if (!string.IsNullOrEmpty(stockTextBox.Text))
        {
            objProducto.Stock = Convert.ToInt32(stockTextBox.Text);
            ErrorStock.Visible = false;
        }
        else
        {
            ErrorStock.Visible = true;
        }

        if (!string.IsNullOrEmpty(objProducto.Nombre) && !string.IsNullOrEmpty(objProducto.Descripcion) && objProducto.Precio >= 0 &&
        objProducto.Stock >= 0)
        {
            objProducto.UnidadMedidaId = UnidadMedidaComboBox.SelectedValue;
            objProducto.FamiliaId = Convert.ToInt32(FamiliaComboBox.SelectedValue);
            ProductoBLL.UpdateProducto(objProducto);
            Response.Redirect("~/Administracion/Inventario/Producto/ListaProducto.aspx");
        }

    }
}