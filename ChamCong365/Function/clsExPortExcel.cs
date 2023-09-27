using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.Function
{
    public class clsExPortExcel
    {
        public static DataTable NewTables(string tableName, string[] colName, int[] DoRong)
        {
            DataTable table = new DataTable();
            table.TableName = tableName;
            for (int i = 0; i < colName.Count(); i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = colName[i];
                table.Columns.Add(col);
            }
            return table;
        }
    }
}
