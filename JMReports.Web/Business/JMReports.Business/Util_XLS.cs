using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb; 

namespace JMReports.Business
{
    /// <summary>
    /// XLS文件工具
    /// </summary>
    public class Util_XLS
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="ServerFileName">xls文件路径</param>
        /// <param name="SelectSQL">查询SQL语句</param>
        /// <returns>DataSet</returns>
        public static System.Data.DataSet SelectFromXLS(string ServerFileName, string SelectSQL)
        {
            string connStr = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + ServerFileName + "';Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbDataAdapter da = null;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                conn.Open();
                da = new OleDbDataAdapter(SelectSQL, conn);
                da.Fill(ds, "SelectResult");
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return ds;

        }

        /// <summary>
        /// 获取工作表对应的SQL表名
        /// </summary>
        /// <param name="SheetName">工作表名</param>
        /// <returns>SQL表名</returns>
        public static string ConvertToSQLSheetName(string SheetName)
        {
            return "[" + SheetName + "$]";
        }

        /// <summary>
        /// 执行无返回查询
        /// </summary>
        /// <param name="ServerFileName">xls文件路径</param>
        /// <param name="QuerySQL">待执行的SQL语句</param>
        public static void ExcuteNonQuery(string ServerFileName, string QuerySQL)
        {
            string connStr = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + ServerFileName + "';Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand(QuerySQL, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception AnyError)
            {
                conn.Close();
                throw AnyError;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
