using System;
using System.Collections.Generic;

using System.Text;

namespace Shinfotech.Tools
{
    /// <summary>
    /// 字典类 
    /// </summary>
    public static class Dictionary
    {
        private static Dictionary<int, string> pay;       //付款方式字典类 

        private static Dictionary<object, object> status;       //信息状态

        private static Dictionary<int, string> trade;       //用户对应行业类别

        private static Dictionary<string, string> instancy;       //加急货盘类型

        private static Dictionary<string, string> tableName;   // 表名

        private static Dictionary<string, string> trendAction;   // 动态动作
        public static Dictionary<string, string> TrendAction
        {
            set { }
            get
            {
                trendAction = new Dictionary<string, string>();
                trendAction.Add("login", "登陆了物流多多网");
                trendAction.Add("add", "发布了");
                trendAction.Add("select", "查看了");
                trendAction.Add("edit", "装饰了");
                trendAction.Add("reg", "注册了物流多多网");
                trendAction.Add("readd", "重新发布了");
                trendAction.Add("close", "关闭了");
                trendAction.Add("approve_failed", "审核未通过");
                trendAction.Add("approve_succeed", "审核通过");
                trendAction.Add("modify", "修改了");
                trendAction.Add("delete", "删除了");
                trendAction.Add("update", "更新了");
                trendAction.Add("refresh","刷新了");
                return trendAction;
            }
        }

        private static Dictionary<int, string> userType;   // 用户类别
        public static Dictionary<int, string> UserType
        {
            set { }
            get
            {
                userType = new Dictionary<int, string>();
                userType.Add(1, "企业");
                userType.Add(2, "个人");
                userType.Add(3, "管理员");
                return userType;
            }
        }
        private static Dictionary<int, string> userTypeVar;   // 用户行业类别
        public static Dictionary<int, string> UserTypeVar
        {
            set { }
            get
            {
                userTypeVar = new Dictionary<int, string>();
                userTypeVar.Add(1, "物流服务商");
                userTypeVar.Add(2, "生产贸易商");
                return userTypeVar;
            }
        }

        private static Dictionary<string, int> userType1;   // 用户类型
        public static Dictionary<string, int> UserType1
        {
            set { }
            get
            {
                userType1 = new Dictionary<string, int>();
                userType1.Add("business", 1);  //个人
                userType1.Add("personal", 2); //企业
                return userType1;
            }
        }

        private static Dictionary<string, int> userTypeVar1;
        public static Dictionary<string, int> UserTypeVar1
        {
            set { }
            get
            {
                userTypeVar1 = new Dictionary<string, int>();
                userTypeVar1.Add("logistics", 1);  //物流服务上
                userTypeVar1.Add("trade", 2); //生产贸易商
                return userTypeVar1;
            }
        }



        public static Dictionary<string, string> TableName
        {
            get
            {
                tableName = new Dictionary<string, string>();
                tableName.Add("login", "");
                tableName.Add("land_CarCapacity", "[运力]");
                tableName.Add("land_carcapacity", "[运力]");
                tableName.Add("merchants", "[招商]");
                tableName.Add("land_cargoes", "[货源]");
                tableName.Add("air_cargo", "[货源]");
                tableName.Add("point", "[网点]");
                tableName.Add("linkman", "");
                tableName.Add("movehouse_cargo", "[货源]");
                tableName.Add("railage_capacity", "[运力]");
                tableName.Add("purchase", "[供求]");
                tableName.Add("air_capacity", "[运力]");
                tableName.Add("railage_cargo", "[货源]");
                tableName.Add("reg", "");
                tableName.Add("express_cargo", "[货源]");
                tableName.Add("express_capacity", "[运力]");
                tableName.Add("franchisee", "[加盟]");
                tableName.Add("movehouse_capacity", "[运力]");
                tableName.Add("website_diy", "");
                tableName.Add("land_SpecialLine", "[专线]");
                tableName.Add("land_specialline","[专线]");
                tableName.Add("product", "[供求]");
                tableName.Add("activle", "");
                tableName.Add("certificate", "[认证]");
                tableName.Add("certificate_auditPool", "");
                tableName.Add("logistics_equipment", "[供求]");
                tableName.Add("forwardersupply", "[供求]");
                tableName.Add("dispatchsupply", "[供求]");
                tableName.Add("storagesupply", "[供求]");
                tableName.Add("storagedemand", "[供求]");
                tableName.Add("tpl_server", "[供求]");
                tableName.Add("tpl_demand", "[供求]");
                tableName.Add("intermodal_capacity", "[运力]");
                tableName.Add("intermodal_cargo_fcl", "[货源]");
                tableName.Add("intermodal_cargo_lcl", "[货源]");
                tableName.Add("forwarderdemand", "[供求]");
                tableName.Add("dispatchdemand", "[供求]");
                tableName.Add("land_specialpallets", "[货源]");
                tableName.Add("iacc", "[供求]");
                return tableName;
            }
            set { }
        }

        /// <summary>
        /// 加急货盘类型
        /// </summary>
        public static Dictionary<string, string> Instancy
        {
            set { }
            get
            {
                instancy = new Dictionary<string, string>();
                instancy.Add("Air_pallet", "空运");
                instancy.Add("Sea_freight_pallets", "海运整箱");
                instancy.Add("Sea_freight_pal_lcl", "海运拼箱");
                instancy.Add("Sea_bul_bulk", "海运散杂货");
                instancy.Add("Express_pallet", "快递");
                instancy.Add("Land_SpecialPallets", "特种货盘");
                instancy.Add("Land_PalletSupply", "普通货盘");
                instancy.Add("Movehouse_MovingPallets", "搬家货盘");
                instancy.Add("railage_RailPallet", "铁路货盘");
                instancy.Add("storage_DistributionPallets", "仓储货盘");
                instancy.Add("intermodalism_LCLInfo", "联运拼箱");
                instancy.Add("intermodalism_IDPInfo", "联运整箱");

                return instancy;
            }
        }

        /// <summary>
        /// 付款方式字典类
        /// </summary>
        public static Dictionary<int, string> Pay
        {
            set { }
            get
            {
                pay = new Dictionary<int, string>();
                pay.Add(1, "面议");
                pay.Add(2, "预付");
                pay.Add(3, "月付");
                pay.Add(4, "到付");
                return pay;
            }
        }

        /// <summary>
        /// 信息状态
        /// </summary>
        public static Dictionary<object, object> Status
        {
            set { }
            get
            {
                status = new Dictionary<object, object>();
                //status.Add("Active", "<span style=\"color:green\">正常</span>");
                //status.Add("Disabled", "<span style=\"color:red\">禁用</span>");
                //status.Add("InActive", "<span style=\"color:red\">删除</span>");
                //status.Add("Approve", "未审核");
                //status.Add("Failed", "<span style=\"color:#FF5500\">未通过</span>");

                status.Add("active", "<span style=\"color:green\">正常</span>");
                status.Add("disabled", "<span style=\"color:red\">禁用</span>");
                status.Add("delete", "<span style=\"color:red\">删除</span>");
                status.Add("approve", "未审核");
                status.Add("approved", "未审核");
                status.Add("nonactivated", "<span style=\"color:#ccc\">未激活</span>");
                status.Add("Inactive", "<span style=\"color:#FF5500\">未通过</span>");
                status.Add("inactive", "<span style=\"color:#FF5500\">未通过</span>");
                return status;
            }
        }


        /// <summary>
        /// 付款方式字典类
        /// </summary>
        public static Dictionary<int, string> Trade
        {
            set { }
            get
            {
                trade = new Dictionary<int, string>();
                trade.Add(5, "wuliufuwu.xml");
                trade.Add(6, "wuliupeitao.xml");
                trade.Add(7, "hangyejianguan.xml");
                trade.Add(8, "shengchanmaoyi.xml");
                trade.Add(9, "geren.xml");
                return trade;
            }
        }

        /// <summary>
        /// 模块类别
        /// </summary>
        public static Dictionary<int, string> ModuleType
        {
            set { }
            get
            {
                Dictionary<int, string> moduleType = new Dictionary<int, string>();
                moduleType.Add(0, "其它");
                moduleType.Add(1, "物流服务商");
                moduleType.Add(2, "生产贸易商");
                return moduleType;
            }
        }

        /// <summary>
        /// 目录类别
        /// </summary>
        public static Dictionary<int, string> MenuType
        {
            set { }
            get
            {
                Dictionary<int, string> menuType = new Dictionary<int, string>();
                menuType.Add(1, "发布");
                menuType.Add(2, "管理");
                menuType.Add(3, "其他");
                return menuType;
            }
        }
        //private static Dictionary<int, string> modulerPrem;       //模块权限 
        public static Dictionary<int, string> ModulerPrem
        {
            set { }
            get
            {
                Dictionary<int, string> modulerPrem = new Dictionary<int, string>();
                modulerPrem.Add(1, "移除");
                modulerPrem.Add(2, "设置");
                return modulerPrem;
            }
        }

        public static Dictionary<int, string> ModularCategory        //模块种类 
        {
            set { }
            get
            {
                Dictionary<int, string> modularCategory = new Dictionary<int, string>();
                modularCategory.Add(1, "默认模块");
                modularCategory.Add(2, "应用模块");
                return modularCategory;
            }
        }
        

        //private static Dictionary<string, string> stencilColor;       //模版颜色
        public static Dictionary<string, string> StencilColor
        {
            set { }
            get
            {
                Dictionary<string, string> stencilColor = new Dictionary<string, string>();
                stencilColor.Add("Green", "绿");
                stencilColor.Add("Black", "黑");
                stencilColor.Add("Red", "红");
                stencilColor.Add("Yellow", "黄");
                stencilColor.Add("Purple", "紫");
                stencilColor.Add("Orange", "橙");
                stencilColor.Add("Brown", "褐");
                stencilColor.Add("Blue", "蓝");
                stencilColor.Add("Ash", "灰");
                stencilColor.Add("White", "白");
                return stencilColor;

            }
        }

        #region 黄页标签类别

        public static Dictionary<string, string> LabelType
        {
            set { }
            get
            {
                Dictionary<string, string> labelType = new Dictionary<string, string>();
                labelType.Add("li", "li");
                labelType.Add("tr", "tr");
                labelType.Add("div", "div");
                labelType.Add("p", "p");
                labelType.Add("dl", "dl"); 
                return labelType;
            }
        }

        #endregion

        #region 用户类型

        public static Dictionary<string, string> UType
        {
            set { }
            get
            {
                Dictionary<string, string> utype = new Dictionary<string, string>();
                utype.Add("1:1", "企业物流");
                utype.Add("1:2", "企业商贸");
                utype.Add("2:1", "个人物流");
                utype.Add("2:2", "个人商贸");
                return utype;
            }
        }

        #endregion

        #region 标签类型

        public static Dictionary<string, string> signType
        {
            set { }
            get
            {
                Dictionary<string, string> signtype = new Dictionary<string, string>();
                signtype.Add("1", "车辆");
                signtype.Add("2", "驾驶员");
                signtype.Add("3", "单位");
                return signtype;
            }
        }

        #endregion
 
    }
}
