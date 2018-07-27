using CRMTree.BLL.Wechat;
using CRMTree.DAL;
using CRMTree.Model;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    /// <summary>
    /// 针对Campaign Run 的相关处理
    /// </summary>
    public class BL_CamRun
    {
        /// <summary>
        /// 实例化的微信相关的数据交互类
        /// </summary>
        DL_Wechat _d_wechat = new DL_Wechat();
        BL_Campaign _b_cam = new BL_Campaign();
        /// <summary>
        /// 获取素材的ID号 
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetMaterialId(int CG_Code, string fileName) 
        {
            return GetMaterialId(CG_Code, 5, fileName);
        }
        /// <summary>
        /// 获取素材的ID号 
        /// </summary>
        /// <param name="CG_Code">根据活动的ID</param>
        /// <param name="type">获得的类型 type=4,缩列图  type=5,图文模板</param>
        /// <returns></returns>
        public string GetMaterialId(int CG_Code, int type, string fileName)
        {
            if (type == 4)
            {
                return GetMaterialId_thumb(CG_Code, fileName);
            }
            else if (type == 5)
            {
                return GetMaterialId_news(CG_Code, fileName);
            }
            return null;
        }
        #region 缩略图素材处理
        /// <summary>
        /// 获取缩略图的MaID
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <returns></returns>
        private string GetMaterialId_thumb(int CG_Code, string fileName)
        {
            CT_Wechat_Multimedium _Multimed = _d_wechat.GETMultimedium(CG_Code, 1, 4, fileName);
            if (_Multimed == null)
            {
                return UploadMaterial_thumb(CG_Code, fileName);
            }
            else if (_Multimed.WM_Invalidation < DateTime.Now)
            {
                return UploadMaterial_thumb(_Multimed);
            }
            else
            {
                return _Multimed.WM_Media_Id;
            }
        }        
        /// <summary>
        /// 上床缩略图素材
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string UploadMaterial_thumb(int CG_Code, string fileName)
        {
            string imageUrl = GetImageUrl(fileName);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return null;
            }
            UploadFileInfo _u = wechatHandle.ConvertMaterial(MaterialType.thumb, imageUrl);
            if (_u == null)
            {
                return null;
            }
            int i = _d_wechat.AddMultimedium(new CT_Wechat_Multimedium
            {
                WM_CG_EV_Code = CG_Code,
                WM_CG_EV_Type = 1,
                WM_Tpe = 4,
                WM_Media = imageUrl,
                WM_Media_Id = _u.MediaId,
                WM_Create_Dt = _u.UploadDate,
                WM_Invalidation = _u.Invalidation,
                WM_fileName = fileName
            });
            return _u.MediaId;
        }
        /// <summary>
        /// 上床缩略图素材
        /// </summary>
        /// <param name="_Multimed"></param>
        /// <returns></returns>
        private string UploadMaterial_thumb(CT_Wechat_Multimedium _Multimed)
        {
            UploadFileInfo _u = wechatHandle.ConvertMaterial(MaterialType.thumb, _Multimed.WM_Media);
            if (_u == null)
            {
                return null;
            }
            int i = _d_wechat.UpdateMultimedium(_Multimed.WM_CG_EV_Code, _Multimed.WM_CG_EV_Type, _Multimed.WM_Tpe, _Multimed.WM_fileName, _u.MediaId, _u.Invalidation);
            return _u.MediaId;
        }
        #endregion
        #region 图文素材处理  - 高级接口
        /// <summary>
        /// 获取news的MaID
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <returns></returns>
        private string GetMaterialId_news(int CG_Code, string fileName)
        {
            CT_Wechat_Multimedium _Multimed = _d_wechat.GETMultimedium(CG_Code, 1, 5, fileName);
            if (_Multimed == null)
            {
                return UploadMaterial_news(CG_Code, fileName);
            }
            else if (_Multimed.WM_Invalidation < DateTime.Now)
            {
                return UploadMaterial_news(_Multimed);
            }
            else
            {
                return _Multimed.WM_Media_Id;
            }
        }
        private string UploadMaterial_news(int CG_Code, string fileName)
        {
            CT_Campaigns _cam = _b_cam.GetCampaign(CG_Code);
            if (_cam == null) {
                return null;
            }
            string _Id_thumb = GetMaterialId_thumb(CG_Code, fileName);
            if (string.IsNullOrEmpty(_Id_thumb))
            {
                return null;
            }
            string _content = GetHandleContent(fileName);
            if (string.IsNullOrEmpty(_content))
            { 
                return null;
            }
            High_news _new = new High_news()
            {
                thumb_media_id = _Id_thumb,
                title = _cam.CG_Desc,
                content = _content.Replace('"', '\''),
            };
            IList<High_news> _Ihigh = new List<High_news>();
            _Ihigh.Add(_new);
            string _news = wechatHandle.High_news(_Ihigh);
            if (string.IsNullOrEmpty(_news))
            {
                return null;
            }
            UploadFileInfo _u = wechatHandle.UploadImageText(_news);
            if (_u == null)
            {
                return null;
            }
            int i = _d_wechat.AddMultimedium(new CT_Wechat_Multimedium
            {
                WM_CG_EV_Code = CG_Code,
                WM_CG_EV_Type = 1,
                WM_Tpe = 5,
                WM_Media = _content.Replace('\'','"'),
                WM_Media_Id = _u.MediaId,
                WM_Create_Dt = _u.UploadDate,
                WM_Invalidation = _u.Invalidation,
                WM_fileName = fileName
            });
            return _u.MediaId;
        }
        private string UploadMaterial_news(CT_Wechat_Multimedium _Multimed)
        {
            CT_Campaigns _cam = _b_cam.GetCampaign(_Multimed.WM_CG_EV_Code);
            if (_cam == null)
            {
                return null;
            }
            string _Id_thumb = GetMaterialId_thumb(_Multimed.WM_CG_EV_Code, _Multimed.WM_fileName);
            if (string.IsNullOrEmpty(_Id_thumb))
            {
                return null;
            }
            High_news _new = new High_news()
            {
                thumb_media_id = _Id_thumb,
                title = _cam.CG_Desc,
                content = _Multimed.WM_Media.Replace('"','\''),
            };
            IList<High_news> _Ihigh = new List<High_news>();
            _Ihigh.Add(_new);
            string _news = wechatHandle.High_news(_Ihigh);
            if (string.IsNullOrEmpty(_news))
            {
                return null;
            }
            UploadFileInfo _u = wechatHandle.UploadImageText(_news);
            if (_u == null)
            {
                return null;
            }
            int i = _d_wechat.UpdateMultimedium(_Multimed.WM_CG_EV_Code, _Multimed.WM_CG_EV_Type, _Multimed.WM_Tpe, _Multimed.WM_fileName, _u.MediaId, _u.Invalidation);
            return _u.MediaId;
        }
        #endregion

        #region 公共小方法
        /// <summary>
        /// 从文件中查找图片路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetImageUrl(string fileName)
        {
            string fileContent = ReportWechat.GetFileInfo(fileName);
            return ReportWechat.GetImgSrc(fileContent);
        }
        /// <summary>
        /// 处理文本内的各种参数，后期的所以处理方式都在此处修改
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetHandleContent(string fileName)
        {
            return ReportWechat.GetFileInfo(fileName);
        }
        #endregion
    }
}
