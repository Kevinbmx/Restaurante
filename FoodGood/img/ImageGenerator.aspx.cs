using FoodGood.Imagen;
using FoodGood.Imagen.BLL;
using FoodGood.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class img_ImageGenerator : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        string ImageR = Request.QueryString["tId"];
        string widthR = Request.QueryString["W"];
        string heightR = Request.QueryString["H"];
        string imageName = "";
        int width = 0;
        int height = 0;
        int ImageId = 0;

        if (string.IsNullOrEmpty(ImageR) || string.IsNullOrEmpty(widthR) || string.IsNullOrEmpty(heightR))
            return;

        try
        {
            width = Convert.ToInt32(widthR);
            height = Convert.ToInt32(heightR);
            ImageId = Convert.ToInt32(ImageR);
        }
        catch (Exception exc)
        {
            log.Error("Ocurrió un error al obtener los datos enviados para el ResizeImage. " + exc);
        }

        if (width == 0 || height == 0)
            return;

        Bitmap bmp = null;
        string extensionImage = "png";
        if (string.IsNullOrEmpty(ImageR) || ImageR.Equals("0"))
        {
            System.Drawing.Image imgDefault = System.Drawing.Image.FromFile(Server.MapPath("~/img/ImgRestaurante/noImage.jpg"));
            imageName = "NoImage";
            bmp = ImageUtilities.CreateThumbnail(imgDefault, width, height);
        }
        else
        {
            Imagen theFile = ImagenBLL.GetImagenById(ImageId);
            if (theFile == null)
                return;

            string pathImage = theFile.Directorio;
            FileInfo fileImage = new FileInfo(pathImage);

            if (fileImage.Exists)
            {
                imageName = theFile.Titulo;
                extensionImage = theFile.Extencion;
                System.Drawing.Image img = System.Drawing.Image.FromFile(pathImage);
                if (img != null)
                    bmp = ImageUtilities.CreateThumbnail(img, width, height);
            }
        }

        if (bmp == null)
            return;

        decimal quality = Math.Max(0, Math.Min(100, ImageUtilities.getQuality(width, height)));
        ImageCodecInfo Info = ImageCodecInfo.GetImageEncoders()[1];
        foreach (System.Drawing.Imaging.ImageCodecInfo codec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
        {
            if (codec.FilenameExtension.Contains(extensionImage.ToUpper()))
            {
                Info = codec;
            }
        }

        EncoderParameters Params = new EncoderParameters(1);
        Params.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(quality));
        Response.ContentType = Info.MimeType;
        Response.AddHeader("Content-Disposition", "attachment;Filename=\"" + imageName + "\"");
        using (MemoryStream stream = new MemoryStream())
        {
            bmp.Save(stream, Info, Params);
            stream.WriteTo(Response.OutputStream);
            stream.Close();
        }

        bmp.Dispose();
        Response.Flush();

    }
}
