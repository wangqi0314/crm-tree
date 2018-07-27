using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    /// <summary>
    /// 汽车年龄
    /// </summary>
   public class MD_CarYears
    {
        private IList<CT_Years> _Car_Years;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Years> Car_Years
        {
            get { return _Car_Years; }
            set { _Car_Years = value; }
        }
    }
}
