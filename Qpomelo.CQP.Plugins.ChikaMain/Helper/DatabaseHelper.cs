using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class DatabaseHelper
    {
        SQLiteConnection connection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DbDir">数据库文件存储位置</param>
        public DatabaseHelper(string DbDir)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + DbDir + ";Version=3;");
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>返回结果</returns>
        public DataTable Execute(string sqlCommand)
        {
            connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlCommand,connection);
            
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();
            return ds.Tables[0];
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            
        }
    }
}
