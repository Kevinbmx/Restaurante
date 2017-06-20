using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QRGenerator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string urlToQRCode = Request.QueryString["qrcode"];
        urlToQRCode = urlToQRCode.Trim();

        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap miBitmap = encoder.Encode(urlToQRCode);

        ImageCodecInfo Info = ImageCodecInfo.GetImageEncoders()[1];
        foreach (System.Drawing.Imaging.ImageCodecInfo codec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
        {
            if (codec.FilenameExtension.Contains(".jpg"))
            {
                Info = codec;
            }
        }

        EncoderParameters Params = new EncoderParameters(1);
        Params.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(100));
        Response.ContentType = Info.MimeType;
        Response.AddHeader("Content-Disposition", "attachment;Filename=\"qr.jpg\"");
        using (MemoryStream stream = new MemoryStream())
        {
            miBitmap.Save(stream, Info, Params);
            stream.WriteTo(Response.OutputStream);
            stream.Close();
        }
    }
}