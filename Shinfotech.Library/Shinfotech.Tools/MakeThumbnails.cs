using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace Shinfotech.Tools
{
    /// <summary>
    /// 按比例缩略图片、添加图片水印
    /// Modify by 王鹏程 on 2010-12-15
    /// </summary>
    public static class MakeThumbnails
    {
        /// <summary>
        /// 按比例缩略图片
        /// </summary>
        /// <param name="strOriginalPath">原始图片绝对路径</param>
        /// <param name="strSavePath">缩略后图片保存绝对路径</param>
        /// <param name="intTargetWeight">目标缩略宽度</param>
        /// <param name="intTargetHeight">目标缩略高度</param>
        /// <returns>布尔</returns>
        public static Boolean ProRateMakeThumbnails(string strOriginalPath, string strSavePath, int intTargetWeight, int intTargetHeight)
        {
            Image originalImage = null;
            Image bitmap = null;
            Graphics g = null;
            try
            {
                originalImage = Image.FromFile(strOriginalPath);
                int oW = originalImage.Width;//原始图片宽
                int oH = originalImage.Height;//原始图片高
                int tW = oW;//最终显示到页面宽
                int tH = oH;//最终显示到页面高
                if (oW > intTargetWeight)//如果原始宽度大于固定宽度
                {
                    tW = intTargetWeight;//最终的宽度等于固定的宽度
                    tH = intTargetWeight * oH / oW;//最终的高度等于固定宽度乘以原始高度除以原始宽度
                    if (tH > intTargetHeight)
                    {
                        tH = intTargetHeight;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度
                    }

                }
                else if (oW < intTargetWeight)//如果原始宽度小于固定宽度
                {
                    tW = oW;
                    if (oH > intTargetHeight)
                    {
                        tH = intTargetHeight;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                    }
                }
                //else//如果原始宽度等于固定宽度
                //{
                //    if (oH > intTargetHeight)
                //    {
                //        tH = intTargetHeight;
                //        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                //    }
                //    if (oH < intTargetHeight)
                //    {
                //        tH = oH;
                //        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                //    }
                //    if (oH == intTargetHeight)
                //    {
                //        tH = oH;
                //        tW = oW;
                //    }
                //}
                //新建一个bmp图片 
                bitmap = new System.Drawing.Bitmap(tW, tH);
                //新建一个画板 
                g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //if (backColor != "")
                //{
                //    //清空画布并以指定颜色填充 
                g.Clear(System.Drawing.Color.Transparent);
                //}
                //在指定位置并且按指定大小绘制原图片的指定部分 
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, tW, tH),
                new System.Drawing.Rectangle(0, 0, oW, oH),
                System.Drawing.GraphicsUnit.Pixel);
                //以jpg格式保存缩略图 
                bitmap.Save(strSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (originalImage != null)
                {

                    originalImage.Dispose();
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
                //if (System.IO.File.Exists(strOriginalPath))
                //{
                //    System.IO.File.Delete(strOriginalPath);
                //} 
            }
        }
        /// <summary>
        /// 给图片加水印
        /// </summary>
        /// <param name="strOriginalPath">原始图片绝对路径</param>
        /// <param name="strSavePath">加水印后图片绝对路径</param>
        /// <returns>布尔</returns>
        public static Boolean MakeWatermark(string strOriginalPath, string strSavePath)
        {
            if (0 < strOriginalPath.Length && 0 < strSavePath.Length)
            {
                try
                {
                    Image image = Image.FromFile(strOriginalPath);
                    Image copyImage = Image.FromFile(HttpContext.Current.Server.MapPath("/images/watermark/watermark.png"));
                    Graphics g = Graphics.FromImage(image);

                    //图片中间加水印
                    //g.DrawImage(copyImage, new Rectangle(image.Width / 2 - copyImage.Width / 2, image.Height / 2 - copyImage.Height / 2, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                    //图片右下角加水印
                    g.DrawImage(copyImage, new Rectangle(image.Width -5- copyImage.Width, image.Height-5 - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                    g.Dispose();
                    //保存加水印过后的图片,删除原始图片
                    image.Save(strSavePath);
                    image.Dispose();
                    
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    //if (System.IO.File.Exists(strOriginalPath))
                    //{
                    //    System.IO.File.Delete(strOriginalPath);
                    //} 
                }
            }
            else
            {
                return true;
            }
            
        }


        /// <summary>
        /// 按比例缩略图片（不删除原图）
        /// </summary>
        /// <param name="strOriginalPath">原始图片绝对路径</param>
        /// <param name="strSavePath">缩略后图片保存绝对路径</param>
        /// <param name="intTargetWeight">目标缩略宽度</param>
        /// <param name="intTargetHeight">目标缩略高度</param>
        /// <returns>布尔</returns>
        public static Boolean ProRateMake(string strOriginalPath, string strSavePath, int intTargetWeight, int intTargetHeight)
        {
            Image originalImage = null;
            Image bitmap = null;
            Graphics g = null;
            try
            {
                originalImage = Image.FromFile(strOriginalPath);
                int oW = originalImage.Width;//原始图片宽
                int oH = originalImage.Height;//原始图片高
                int tW = oW;//最终显示到页面宽
                int tH = oH;//最终显示到页面高
                if (oW > intTargetWeight)//如果原始宽度大于固定宽度
                {
                    tW = intTargetWeight;//最终的宽度等于固定的宽度
                    tH = intTargetWeight * oH / oW;//最终的高度等于固定宽度乘以原始高度除以原始宽度
                    if (tH > intTargetHeight)
                    {
                        tH = intTargetHeight;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度
                    }

                }
                else if (oW < intTargetWeight)//如果原始宽度小于固定宽度
                {
                    tW = oW;
                    if (oH > intTargetHeight)
                    {
                        tH = intTargetHeight;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                    }
                }
                else//如果原始宽度等于固定宽度
                {
                    if (oH > intTargetHeight)
                    {
                        tH = intTargetHeight;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                    }
                    if (oH < intTargetHeight)
                    {
                        tH = oH;
                        tW = intTargetHeight * oW / oH;//最终的宽度等于固定高度乘以原始宽度除以原始高度                
                    }
                    if (oH == intTargetHeight)
                    {
                        tH = oH;
                        tW = oW;
                    }
                }
                //新建一个bmp图片 
                bitmap = new System.Drawing.Bitmap(tW, tH);
                //新建一个画板 
                g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //if (backColor != "")
                //{
                //    //清空画布并以指定颜色填充 
                g.Clear(System.Drawing.Color.Transparent);
         
                //}
                //在指定位置并且按指定大小绘制原图片的指定部分 
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, tW, tH),
                new System.Drawing.Rectangle(0, 0, oW, oH),
                System.Drawing.GraphicsUnit.Pixel);
                //以jpg格式保存缩略图 
                bitmap.Save(strSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (originalImage != null)
                {

                    originalImage.Dispose();
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
            }
        }


        /// <summary>
        /// 按比例缩略图片（不删除原图）
        /// </summary>
        /// <param name="strOriginalPath">原始图片绝对路径</param>
        /// <param name="strSavePath">缩略后图片保存绝对路径</param>
        /// <param name="intTargetWeight">目标缩略宽度</param>
        /// <param name="intTargetHeight">目标缩略高度</param>
        /// <returns>布尔</returns>
        public static Boolean Enlarged(string strOriginalPath, string strSavePath, int tW, int tH)
        {
            Image originalImage = null;
            Image bitmap = null;
            Graphics g = null;
            try
            {
                originalImage = Image.FromFile(strOriginalPath);
                int oW = originalImage.Width;//原始图片宽
                int oH = originalImage.Height;//原始图片高
                //新建一个bmp图片 
                bitmap = new System.Drawing.Bitmap(tW, tH);
                //新建一个画板 
                g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //设置背景透明 
                g.Clear(System.Drawing.Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分 
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, tW, tH),
                new System.Drawing.Rectangle(0, 0, oW, oH),
                System.Drawing.GraphicsUnit.Pixel);
                //以jpg格式保存缩略图 
                bitmap.Save(strSavePath, System.Drawing.Imaging.ImageFormat.Png);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (originalImage != null)
                {

                    originalImage.Dispose();
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
            }
        }

    }
}
