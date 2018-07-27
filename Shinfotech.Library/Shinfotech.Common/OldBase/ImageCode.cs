using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Collections.Generic;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 验证码处理类
    /// </summary>
    public class ImageCode
    {
        private Graphics g;
        private string m_validatenum;
        private Bitmap validateimage;

        /// <summary>
        /// 验证码的类
        /// </summary>
        public ImageCode()
        {
            string str = this.validatecodernd();
            this.m_validatenum = str;
        }

        /// <summary>
        /// 验证码的类
        /// </summary>
        /// <param name="validatelength"></param>
        public ImageCode(int validatelength)
        {
            string str = this.validatecodernd(validatelength);
            this.m_validatenum = str;
        }

        /// <summary>
        /// 输出随机验证串到页面上
        /// </summary>
        /// <param name="validatenum"></param>
        /// <param name="page"></param>
        public void validatecode(string validatenum, Page page)
        {
            int width = validatenum.Length * 7;
            this.validateimage = new Bitmap(width, 0x12, PixelFormat.Format24bppRgb);
            this.g = Graphics.FromImage(this.validateimage);
            this.g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(120, 30), Color.FromKnownColor(KnownColor.White), Color.FromKnownColor(KnownColor.White)), 0, 0, 120, 30);
            this.g.DrawString(validatenum, new Font("宋体", 9f), new SolidBrush(Color.Red), new PointF(2f, 4f));
            this.g.Save();
            MemoryStream stream = new MemoryStream();
            this.validateimage.Save(stream, ImageFormat.Jpeg);
            page.Response.ClearContent();
            page.Response.ContentType = "image/Jpeg";
            page.Response.BinaryWrite(stream.ToArray());
            this.g.Dispose();
            this.validateimage.Dispose();
            page.Response.End();
        }

        /// <summary>
        ///  以指定的高度输出随机数字的图片到页面上，
        /// </summary>
        /// <param name="checkCode"></param>
        /// <param name="page"></param>
        /// <param name="iW"></param>
        public void validatecode(string checkCode, Page page, double iW)
        {
            if ((checkCode != null) && (checkCode.Trim() != string.Empty))
            {
                Bitmap image = new Bitmap((int)Math.Ceiling((double)(checkCode.Length * iW)), 0x16);
                Graphics graphics = Graphics.FromImage(image);
                try
                {
                    Random random = new Random();
                    graphics.Clear(Color.White);
                    for (int i = 0; i < 0x19; i++)
                    {
                        int num2 = random.Next(image.Width);
                        int num3 = random.Next(image.Width);
                        int num4 = random.Next(image.Height);
                        int num5 = random.Next(image.Height);
                        graphics.DrawLine(new Pen(Color.Silver), num2, num4, num3, num5);
                    }
                    Font font = new Font("Arial", 12f, FontStyle.Italic | FontStyle.Bold);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                    graphics.DrawString(checkCode, font, brush, (float)2f, (float)2f);
                    for (int j = 0; j < 100; j++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next()));
                    }
                    graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Gif);
                    page.Response.ClearContent();
                    page.Response.ContentType = "image/Gif";
                    page.Response.BinaryWrite(stream.ToArray());
                }
                finally
                {
                    graphics.Dispose();
                    image.Dispose();
                }
            }
        }

        /// <summary>
        ///    返回一定长度的字符串（字母和数字）
        /// </summary>
        /// <returns></returns>
        private string validatecodernd()
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// 根据输入的长度产生随机数字
        /// </summary>
        /// <param name="vnumlength"></param>
        /// <returns></returns>
        private string validatecodernd(int vnumlength)
        {
            string[] strArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string str = "";
            Random random = new Random();
            for (int i = 0; i < vnumlength; i++)
            {
                str = str + strArray[random.Next(0, strArray.Length)].ToString();
            }
            return str;
        }

      




        /// <summary>
        ///  属性-随机数字
        /// </summary>
        public string ValidateNum
        {
            get
            {
                return this.m_validatenum;
            }
        }
    }
}

