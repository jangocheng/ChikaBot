using Newbe.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class Group
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long QQId { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="qqid">群号</param>
        public Group(long qqid)
        {
            QQId = qqid;
            if (Exists(qqid) != true)
            {
                CreateGroup(qqid);
            }

        }

        /// <summary>
        /// 判断该群是否存在于数据库记录
        /// </summary>
        /// <param name="qqid"></param>
        /// <returns></returns>
        public static bool Exists(long qqid)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + MainPlugin.MainDatabase + ";Version=3;");
            m_dbConnection.Open();

            string sql = "select COUNT(*) from qqgroup where qqid = " + qqid.ToString();
            string str = "";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                str = reader["COUNT(*)"].ToString();
            }
            m_dbConnection.Close();

            int num = 0;
            int.TryParse(str, out num);

            if (num != 0)
                return true;

            return false;
        }
        /// <summary>
        /// 创建该群记录
        /// </summary>
        /// <param name="qqid"></param>
        public static void CreateGroup(long qqid)
        {
            if (Exists(qqid) == true)
            {
                return;
            }

            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("insert into qqgroup values(null, " + qqid + ",0, 0, '', '' )",new SQLiteParameter() { });
        }

        /// <summary>
        /// GroupID
        /// </summary>
        public long Gid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from qqgroup where qqid = " + QQId, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["gid"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            private set { }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from qqgroup where gid = " + Gid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["enable"].ToString();
                }
                long.TryParse(obj, out num);

                if (num == 0)
                    return true;

                return false;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                int num = 0;
                if (value == false)
                    num = 1;
                helper.ExecuteReader("update qqgroup set enable = " + num + " where gid = " + Gid, new SQLiteParameter() { });
            }
        }

        /// <summary>
        /// 是否被封禁
        /// </summary>
        public bool IsBaned
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from qqgroup where gid = " + Gid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["baned"].ToString();
                }
                long.TryParse(obj, out num);

                if (num != 0)
                    return true;

                return false;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                int num = 0;
                if (value == true)
                    num = 1;
                helper.ExecuteReader("update qqgroup set baned = " + num + " where gid = " + Gid, new SQLiteParameter() { });
            }
        }

        /// <summary>
        /// 该群的设置，Json
        /// </summary>
        public string Settings
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from qqgroup where gid = " + Gid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["settings"].ToString();
                }

                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update qqgroup set settings = '" + value + "' where gid = " + Gid, new SQLiteParameter() { });
            }
        }

        /// <summary>
        /// 该群的的附加选项，Json
        /// </summary>
        public string Flags
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from qqgroup where gid = " + Gid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["flags"].ToString();
                }

                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update qqgroup set flags = '" + value + "' where gid = " + Gid, new SQLiteParameter() { });
            }
        }
    }
}
