using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.MyCar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_MyCar
    {
        private readonly DL_Car _Car = new DL_Car();
        /// <summary>
        /// 获取当前登录着所有的Car信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public MD_CarList GetCarList(long UserCode)
        {
            MD_CarList o = _Car.GetCarList(UserCode);
            for (int i = 0; i < o.Car_Inventory_List.Count; i++)
            {
                if (o.Car_Inventory_List[i].CI_Picture_FN == null)
                {
                    o.Car_Inventory_List[i].CI_Picture_FN = "";
                }
            }
            return o;
        }
        public MD_CarList GetCarList(long UserCode, bool v)
        {
            MD_CarList o = _Car.GetCarList(UserCode,v);
            for (int i = 0; i < o.Car_Inventory_List.Count; i++)
            {
                if (o.Car_Inventory_List[i].CI_Picture_FN == null)
                {
                    o.Car_Inventory_List[i].CI_Picture_FN = "";
                }
            }
            return o;
        }
        /// <summary>
        /// 获取绑定顾问的个人车辆信息 IsBind=1 表示绑定了 IsBind=0 表示没有绑定
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <returns></returns>
        public string GetBindCarList_Json(long AU_Code)
        {
            return _Car.GetBindCarList_Json(AU_Code);
        }
        public string GetCarInfo_Json(int CI_Code)
        {
            return _Car.GetCarInfo_Json(CI_Code);
        }

        public string GetCarInfo_List(long au_code)
        {
            return _Car.GetCarInfo_List(au_code);
        }

        public string GetDafaultCarInfo(long au_code)
        {
            return _Car.GetDafaultCarInfo(au_code);
        }
        public MD_CarTypeList getMyCarTypeList()
        {
            return _Car.GetCarTypeList();
        }
        /// <summary>
        /// make下拉表数据
        /// </summary>
        /// <returns></returns>
        public string getMyCarMakeList()
        {
            return _Car.GetMakeList();
        }
        /// <summary>
        /// 次方法针对于Campaign的Param_List参数类型特定的函数结构 i=1 表示type=11，i=2 表示type=12.
        /// i=0;根据MK_Code得到Make；
        /// i=1;根据CM_Code反推得到Make；
        /// i=2;根据SC_Code反推得到Make；
        /// </summary>
        /// <param name="i"></param>
        /// <param name="CM_Code"></param>
        /// <returns></returns>
        public CT_Make getCarMake(int i, int CM_Code)
        {
            return _Car.GetMake(i, CM_Code);
        }
        public static string getMake_Model_Style(int i, int Code,bool isEn = true)
        {
            CRMTree.DAL.DL_Car Car = new CRMTree.DAL.DL_Car();
            CT_Car_Style style = Car.GetMake_Model_Style(i, Code);
            if (style == null)
            {
                return " ";
            }
            if (i == 1)
            {
                return isEn ? style.MK_Make_EN : style.MK_Make_CN;
            }
            else if (i == 2)
            {
                return isEn ? style.MK_Make_EN + "-" + style.CM_Model_EN : style.MK_Make_CN + "-" + style.CM_Model_CN;
            }
            else if (i == 3)
            {
                return isEn ? style.MK_Make_EN + "-" + style.CM_Model_EN + "-" + style.CS_Style_CN : style.MK_Make_CN + "-" + style.CM_Model_CN + "-" + style.CS_Style_CN;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Mode下拉表数据
        /// </summary>
        /// <returns></returns>
        public string getMyCarModeList(int MK_code)
        {
            return _Car.GetModeList(MK_code);
        }
        /// <summary>
        ///  i=0是表示，根据CM_Code 获取Model；i=1是表示，根据CS_Code反推Model 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="CM_Code"></param>
        /// <returns></returns>
        public CT_Car_Model getCarModel(int i, int CM_Code)
        {
            return _Car.GetModel(i, CM_Code);
        }
        /// <summary>
        /// Style列表
        /// </summary>
        /// <returns></returns>
        public string getMyCarStyleList(int CM_code)
        {
            return _Car.GetStyleList(CM_code);
        }
        public CT_Car_Style getMyStyle(int CS_Code)
        {
            return _Car.GetStyle(CS_Code);
        }
        /// <summary>
        /// 汽车年龄
        /// </summary>
        /// <param name="MK_code"></param>
        /// <returns></returns>
        public MD_CarYears getMyCarYearsList()
        {
            return _Car.GetCarYearsList();
        }
        /// <summary>
        ///汽车E颜色
        /// </summary>
        /// <returns></returns>
        public MD_CarColorsList getMyCatColorsList()
        {
            return _Car.GetColorsList();
        }
        /// <summary>
        ///汽车I颜色
        /// </summary>
        /// <returns></returns>
        public MD_CarColorsList getMyCatColorsListI()
        {
            return _Car.GetColorsListI();
        }

        #region 新 处理newCar
        /// <summary>
        /// 新增或是编辑都可以调用
        /// </summary>
        /// <param name="_c"></param>
        /// <returns></returns>
        public int modify_Car(CT_Car_Inventory _c)
        {
            return _Car.Modify_Car(_c);
        }
        public int CarBind_Adviser(CT_Car_Inventory o) 
        {
            return _Car.CarBind_Adviser(o);
        }
        #endregion

        #region
        public DataTable GetCarInfoByAU_Code(int DL_AU_Code)
        {
            return new DAL.DL_Car().GetCarInfoByAU_Code(DL_AU_Code);
        }
        public bool Delete(int AU_Code, int M_AU_Code)
        {
            return new DAL.DL_Car().Delete(AU_Code, M_AU_Code);
        }
        public bool DeleteCar(int CI, int M_AU_Code)
        {
            return new DAL.DL_Car().DeleteCar(CI, M_AU_Code);
        }
        #endregion
    }
}
