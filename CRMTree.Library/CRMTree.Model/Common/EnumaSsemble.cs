using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Common
{
    /// <summary>
    /// 国际化枚举值
    /// </summary>
    public enum EM_Language
    {
        /// <summary>
        /// 中文
        /// </summary>
        zh_cn,
        /// <summary>
        /// 英文
        /// </summary>
        en_us
    }
    public enum EM_ParameterMode
    {
        Page,
        Background
    }
    /// <summary>
    /// 图标类型枚举值
    /// </summary>
    public enum ChartType
    {
        Pie,
        Bar,
        Sch,
        Multi,
        Gauge,
        Drill,
        List
    }
    /// <summary>
    /// 用户身份枚举值
    /// </summary>
    public enum UserIdentity
    {
        Generic = 0,
        Dealer = 1,
        DealerGroup = 2,
        OEM = 3,
        CRMTree = 4,
        Customer = 5
    }
    public enum CommunicatorKey
    {
        Q1001_05
    }
    public enum _errCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 0,
        /// <summary>
        /// 值为空
        /// </summary>
        isNull = -100,
        /// <summary>
        /// 执行语句空
        /// </summary>
        isExecNull = -101,
        /// <summary>
        /// 对象为空
        /// </summary>
        isObjectNull = -1000,
        /// <summary>
        /// 系统处理错误
        /// </summary>
        systomError = -10000
    }
}
