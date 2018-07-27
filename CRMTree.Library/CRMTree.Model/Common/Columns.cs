using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMTree.Model.Common
{
    class ColumnsAttribute : Attribute
    {
        public ColumnsAttribute(string ColumnsName)
        {
            this.ColumnsName = ColumnsName;
        }

        public string ColumnsName
        {
            get;
            private set;
        }
    }
}
