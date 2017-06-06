using FoodGood.Familia;
using FoodGood.Familia.BLL;
using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using log4net;
using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Inventario_Familia_RegistrarFamilia : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int _totalRows = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (!string.IsNullOrEmpty(FamiliaIdHiddenField.Value))
            {
                cargarDatosFamilia();
            }
            cargarImagenesRepeateLista();

        }
    }


    public void cargarDatosFamilia()
    {
        Familia theData = null;
        bool existeImagen = false;
        try
        {
            theData = FamiliaBLL.GetFamiliaById(Convert.ToInt32(FamiliaIdHiddenField.Value));


            if (theData == null)
            {
                Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
            }

            if (theData.FamiliaId != 0)
            {
                descripcionTextBox.Text = theData.Descripcion;
                List<Imagen> listaImagen = ImagenBLL.GetImagenListForSearch("");
                if (listaImagen.Count >= 0)
                {
                    for (int i = 0; i < listaImagen.Count; i++)
                    {
                        if (listaImagen[i].ImagenId == theData.ImagenId)
                        {
                            SelecteImage.ImageUrl = "~/img/ImgRestaurante/" + listaImagen[i].Titulo;
                            ImagenIdHiddenField.Value = listaImagen[i].ImagenId.ToString();
                            subirImagen.Visible = false;
                            cancelarImagen.Visible = true;
                            existeImagen = true;
                        }
                    }

                }
                if (!existeImagen)
                {
                    cancelarImagen.Visible = false;
                }
                SaveFamiliaButton.Visible = false;
                UpdateFamiliaButton.Visible = true;

            }
        }
        catch
        {
            log.Error("Error al obtener la información de la familia");
        }
    }

    public Searcher consultaSql(string query)
    {
        Searcher searcher = new Searcher(new BusquedaImagen());
        searcher.Query = query;
        return searcher;
    }

    protected void btnImagen_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            Imagen objImagen = new Imagen();
            string tituloImagen = FileUpload1.PostedFile.FileName;
            string armadoDeQuery = "(@titulo \"" + tituloImagen + "\")";
            string query = consultaSql(armadoDeQuery).SqlQuery();
            List<Imagen> listaImagen = ImagenBLL.GetImagenListForSearch(query);
            if (listaImagen.Count <= 0)
            {
                errorImagen.Visible = false;
                //string fileName = Path.GetRandomFileName();
                string fullPath = Server.MapPath("~/img/ImgRestaurante/") + tituloImagen;
                FileUpload1.PostedFile.SaveAs(fullPath);

                System.IO.FileInfo info = new System.IO.FileInfo(fullPath);
                objImagen.Directorio = fullPath;
                objImagen.Extencion = info.Extension;
                objImagen.Titulo = tituloImagen;
                objImagen.Size = info.Length;
                objImagen.FechaImagen = DateTime.Now;

                if (!string.IsNullOrEmpty(objImagen.Titulo) && objImagen.Size > 0 &&
                    !string.IsNullOrEmpty(objImagen.Extencion) && !string.IsNullOrEmpty(objImagen.Directorio) &&
                    objImagen.FechaImagen != null)
                {
                    string titulo = "";
                    int idImagen = ImagenBLL.InsertImagen(objImagen, ref titulo);
                    ImagenIdHiddenField.Value = Convert.ToString(idImagen);
                    SelecteImage.ImageUrl = "~/img/ImgRestaurante/" + titulo;
                    cargarImagenesRepeateLista();
                }
            }
            else
            {
                errorImagen.Visible = true;
            }
        }
    }


    private void ProcessSessionParameteres()
    {
        int ususrioId = 0;
        if (Session["FamiliaId"] != null && !string.IsNullOrEmpty(Session["FamiliaId"].ToString()))
        {
            try
            {
                ususrioId = Convert.ToInt32(Session["FamiliaId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la familia id: " + Session["FamiliaId"]);
            }
            if (ususrioId > 0)
            {
                FamiliaIdHiddenField.Value = ususrioId.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }
        Session["FamiliaId"] = null;
    }

    protected void SaveFamilia_Click(object sender, EventArgs e)
    {
        Familia objFamila = new Familia();

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objFamila.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }


        if (!string.IsNullOrEmpty(ImagenIdHiddenField.Value))
        {
            objFamila.ImagenId = Convert.ToInt32(ImagenIdHiddenField.Value);
            errorImagenseleccion.Visible = false;
        }
        else
        {
            errorImagenseleccion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objFamila.Descripcion))
        {
            FamiliaBLL.InsertFamilia(objFamila);
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }

    }

    public void cargarQuery()
    {
        BuscadorCriterioHF.Value = txtBuscador.Text.Trim();
        string query = "";
        string buscar = txtBuscador.Text.Trim();

        if (!string.IsNullOrEmpty(buscar))
        {
            query += " @titulo " + buscar;
            SearchQueryHiddenField.Value = query;
        }
        else
        {
            SearchQueryHiddenField.Value = buscar;
        }
    }

    protected void BuscadorButton_Click(object sender, EventArgs e)
    {
        cargarQuery();
        Pager1.CurrentRow = 0;
        cargarImagenesRepeateLista();
    }

    private void cargarImagenesRepeateLista()
    {
        string ordenar = "";
        //TIPO DE ORDEN
        ordenar = "order by i.[titulo] asc";
        try
        {
            Searcher searcher = new Searcher(new BusquedaImagen());
            searcher.Query = SearchQueryHiddenField.Value;

            List<Imagen> _cache = new List<Imagen>();
            try
            {
                _totalRows = ImagenBLL.SearchFiles(ref _cache, searcher.SqlQuery(), Pager1.PageSize, Pager1.CurrentRow, ordenar);
            }
            catch (Exception ex)
            {
                log.Error("Error serching product:", ex);
                throw ex;
                //return;
            }

            ImagenesRepeater.DataSource = _cache;
            ImagenesRepeater.DataBind();

            Pager1.TotalRows = _totalRows;
            if (_cache.Count == 0)
            {
                Pager1.Visible = false;
                PagesButtons.Visible = true;
                return;
            }
            Pager1.Visible = true;
            Pager1.BuildPagination();
        }
        catch (Exception ex)
        {
            log.Error("error al cargar la lista de articulos", ex);
            return;
        }
    }


    protected void UpdateFamiliaButton_Click(object sender, EventArgs e)
    {
        Familia objFamilia = new Familia();
        objFamilia.FamiliaId = Convert.ToInt32(FamiliaIdHiddenField.Value);

        if (!string.IsNullOrEmpty(descripcionTextBox.Text))
        {
            objFamilia.Descripcion = descripcionTextBox.Text.ToLower();
            ErrorDescripcion.Visible = false;
        }
        else
        {
            ErrorDescripcion.Visible = true;
        }

        if (!string.IsNullOrEmpty(ImagenIdHiddenField.Value))
        {
            objFamilia.ImagenId = Convert.ToInt32(ImagenIdHiddenField.Value);
            errorImagenseleccion.Visible = false;
        }
        else
        {
            errorImagenseleccion.Visible = true;
        }

        if (!string.IsNullOrEmpty(objFamilia.Descripcion))
        {
            FamiliaBLL.UpdateFamilia(objFamilia);
            Response.Redirect("~/Administracion/Inventario/Familia/ListaFamilia.aspx");
        }
    }

    protected void ImagenRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "addImageAFamilia")
        {
            try
            {
                int imagenId = Convert.ToInt32(e.CommandArgument.ToString());
                ImagenIdHiddenField.Value = imagenId.ToString();
                subirImagen.Visible = false;
                cancelarImagen.Visible = true;
                List<Imagen> listaImagen = ImagenBLL.GetImagenListForSearch("");
                for (int i = 0; i < listaImagen.Count; i++)
                {
                    if (listaImagen[i].ImagenId == imagenId)
                    {
                        SelecteImage.ImageUrl = "~/img/ImgRestaurante/" + listaImagen[i].Titulo;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('La imagen esta lista para ser insertada en esta familia');", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('La imagen no pudo ser insertada a la familia');", true);
                log.Error("La imagen no pudo ser insertada a la familia", ex);
                return;
                ////throw ex;
            }
        }
    }
    protected void Pager1_PageChanged(int row)
    {
        cargarImagenesRepeateLista();
    }

    protected void cancelarImagen_Click(object sender, EventArgs e)
    {
        SelecteImage.ImageUrl = "~/img/ImgRestaurante/noImage.jpg";
        subirImagen.Visible = true;
        cancelarImagen.Visible = false;
        ImagenIdHiddenField.Value = "";
    }
}