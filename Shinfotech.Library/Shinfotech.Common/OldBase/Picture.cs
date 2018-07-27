using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
namespace ShInfoTech.Common
{
    /// <summary>
    /// 图片处理
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// 图片处理类
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sourceFile"></param>
        /// <param name="targetFile"></param>
        /// <param name="Enalebgcolor"></param>
        /// <param name="bgColor"></param>
        private static void BuilderThumb(Page page, Size size, string sourceFile, string targetFile, bool Enalebgcolor, Color bgColor)
        {
            int width = size.Width;
            int height = size.Height;
            Image image = Image.FromFile(sourceFile);
            ImageFormat rawFormat = image.RawFormat;
            Size thumbSize = GetThumbSize(new Size(width, height), new Size(image.Width, image.Height));
            Bitmap bitmap = new Bitmap(thumbSize.Width, thumbSize.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.High;
            if (Enalebgcolor)
            {
                graphics.Clear(bgColor);
            }
            graphics.DrawImage(image, new Rectangle(0, 0, thumbSize.Width, thumbSize.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            graphics.Dispose();
            EncoderParameters encoderParams = new EncoderParameters();
            long num3 = 100L;
            EncoderParameter parameter = new EncoderParameter(Encoder.Quality, num3);
            encoderParams.Param[0] = parameter;
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo encoder = null;
            for (int i = 0; i < (imageEncoders.Length - 1); i++)
            {
                if (imageEncoders[i].FormatDescription.Equals("JPEG"))
                {
                    encoder = imageEncoders[i];
                    break;
                }
            }
            if (page != null)
            {
                HttpResponse response = page.Response;
                response.Clear();
                if (rawFormat.Equals(ImageFormat.Gif))
                {
                    response.ContentType = "image/gif";
                }
                else
                {
                    response.ContentType = "image/jpeg";
                }
                if (encoder != null)
                {
                    bitmap.Save(response.OutputStream, encoder, encoderParams);
                }
                else
                {
                    bitmap.Save(response.OutputStream, rawFormat);
                }
            }
            else
            {
                bitmap.Save(targetFile, rawFormat);
            }
        }
        /// <summary>
        ///  产生缩略图：产生文件保存到硬盘
        /// </summary>
        /// <param name="size">缩略图的尺寸</param>
        /// <param name="sourceFile">生成缩略图的源文件路径.如:d:\aa\phone.gif</param>
        /// <param name="targetFile">生成缩略图后存放的路径.如:d:\aa\thumb\phone.gif</param>
        public static void CreateThumb(Size size, string sourceFile, string targetFile)
        {
            BuilderThumb(null, size, sourceFile, targetFile, false, new Color());
        }

        /// <summary>
        /// 获得缩略图:不产生文件
        /// </summary>
        ///<param name="page">页面page</param>
        ///<param name="size">缩略图的尺寸</param>
        ///<param name="sourceFile">源图片路径</param>
        public static void GetThum(Page page, Size size, string sourceFile)
        {
            BuilderThumb(page, size, sourceFile, "", false, new Color());
        }

        /// <summary>
        /// GetThumbSize
        /// </summary>
        /// <param name="max"></param>
        /// <param name="thumb"></param>
        /// <returns></returns>
        private static Size GetThumbSize(Size max, Size thumb)
        {
            double width = 0.0;
            double height = 0.0;
            double num3 = Convert.ToDouble(thumb.Width);
            double num4 = Convert.ToDouble(thumb.Height);
            double num5 = Convert.ToDouble(max.Width);
            double num6 = Convert.ToDouble(max.Height);
            if ((num3 < num5) && (num4 < num6))
            {
                width = num3;
                height = num4;
            }
            else if ((num3 / num4) > (num5 / num6))
            {
                width = max.Width;
                height = (width * num4) / num3;
            }
            else
            {
                height = max.Height;
                width = (height * num3) / num4;
            }
            return new Size(Convert.ToInt32(width), Convert.ToInt32(height));
        }
    }
}