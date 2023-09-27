using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.Function
{
    public class clsEx
    {
        private DataSet m_CSDL;
        public DataSet CSDL
        {
            get
            {
                return m_CSDL;
            }

            set
            {
                m_CSDL = value;
            }
        }
    }
}
