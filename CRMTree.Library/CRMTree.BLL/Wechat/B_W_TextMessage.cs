using CRMTree.Model.Adviser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CRMTree.BLL.Wechat
{
    public class B_W_TextMessage
    {
        /// <summary>
        /// 
        //你好！请选择以下经销商为你推荐的顾问\n\n凯迪拉克上海旗舰店：\n【1001】选择米提\n【1002】选择莉莉\n【1003】选择提拉\n上海宝马旗舰店：\n【2001】选择天能\n【2002】选择欧拉\n【2003】选择埃克拉斯\n\n请您先输入顾问编号，该顾问就能为你提供服务!
        /// </summary>
        /// <returns></returns>
        public static string GetMessage()
        {
            BL_Advisers a = new BL_Advisers();
            DealerEmplList DE = a.GetDealerEmplList();
            if (DE == null || DE.Dealer == null || DE.Empl == null)
            {
                return "没有联系到适合您的顾问,请跟具体经销商联系";
            }

            string senCon = "请选择以下经销商为你推荐的顾问\n如果你在2分钟内没有做出选择，我们会自动断开顾问选择连接\n\n";
            for (int i = 0; i < DE.Dealer.Count; i++)
            {
                senCon += DE.Dealer[i].AD_Name_CN.Trim() + "\n";
                for (int n = 0; n < DE.Empl.Count; n++)
                {
                    if (DE.Dealer[i].AD_Code == DE.Empl[n].DE_AD_OM_Code)
                    {
                        senCon += "【" + DE.Empl[n].DE_Code + "】" + DE.Empl[n].AU_Name.Trim() + " \n";
                    }
                }
            }
            senCon += "\n请您先输入顾问编号，该顾问就能为你提供服务!";
            return senCon;
        }
    }
}
