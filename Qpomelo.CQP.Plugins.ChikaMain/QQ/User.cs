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
    class User
    {
        /// <summary>
        /// 在数据库中创建某用户的数据
        /// </summary>
        /// <param name="QQ"></param>
        void CreateUser(long QQ)
        {
            if (Exists())
                return;
            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("insert into user values(null, " + QQ + ", 0,0,0,'',0" + TimeStampHelper.ConvertDateTimeInt(DateTime.Now) + " )", new SQLiteParameter() { });
        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="Qq">用户Qq</param>
        /// <returns></returns>
        bool Exists()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + MainPlugin.MainDatabase + ";Version=3;");
            m_dbConnection.Open();

            string sql = "select COUNT(*) from user where qqid = " + QQ;
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
        /// 判断用户类SIF物品是否存在
        /// </summary>
        /// <param name="Qq">用户Qq</param>
        /// <returns></returns>
        bool ExistsSif()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + MainPlugin.MainDatabase + ";Version=3;");
            m_dbConnection.Open();

            string sql = "select COUNT(*) from sifitem where uid = " + Uid;
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
        /// 在数据库中创建某用户的类SIF数据
        /// </summary>
        /// <param name="QQ"></param>
        void CreateUserSif()
        {
            if (ExistsSif())
                return;
            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("insert into sifitem values(" + Uid + ",0,0,0,0,0)", new SQLiteParameter() { });
        }

        /// <summary>
        /// 判断用户Chika物品是否存在
        /// </summary>
        /// <param name="Qq">用户Qq</param>
        /// <returns></returns>
        bool ExistsChika()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + MainPlugin.MainDatabase + ";Version=3;");
            m_dbConnection.Open();

            string sql = "select COUNT(*) from chika_item where uid = " + Uid;
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
        /// 在数据库中创建某用户的类SIF数据
        /// </summary>
        /// <param name="QQ"></param>
        void CreateUserChika()
        {
            if (ExistsChika())
                return;
            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("insert into chika_item values(" + Uid + ",0,0,0)", new SQLiteParameter() { });
        }

        /// <summary>
        /// 判断用户SIFID数据行是否存在
        /// </summary>
        /// <param name="Qq">用户Qq</param>
        /// <returns></returns>
        bool ExistsSIFID()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + MainPlugin.MainDatabase + ";Version=3;");
            m_dbConnection.Open();

            string sql = "select COUNT(*) from sifid where uid = " + Uid;
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
        /// 在数据库中创建某用户的SIFID数据行
        /// </summary>
        /// <param name="QQ"></param>
        void CreateUserSIFID()
        {
            if (ExistsSIFID())
                return;
            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("insert into sifid values(" + Uid + ",'',0,'',0,'',0,'',0,'',0)", new SQLiteParameter() { });
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="QQNum">QQ</param>
        /// <param name="source">来源</param>
        public User(long QQNum, UserSource source)
        {
            QQ = QQNum;
            Source = source;
            if (!Exists())
                CreateUser(QQ);
            if (!ExistsSif())
                CreateUserSif();
            if (!ExistsSIFID())
                CreateUserSIFID();
            if (!ExistsChika())
                CreateUserChika();

            Loveca = Loveca + LovecaOld;
            MandarinPoint = MandarinPoint + MandarinPointOld;
            CouponTicket = CouponTicket + CouponTicketOld;
            HonorStudentBonus = HonorStudentBonus + HonorStudentBonusOld;
            LovecaOld = 0;
            MandarinPointOld = 0;
            CouponTicketOld = 0;
            HonorStudentBonusOld = 0;
        }

        /// <summary>
        /// 用户QQ
        /// </summary>
        public long QQ
        {
            private set;
            get;
        }
        /// <summary>
        /// UserID
        /// </summary>
        public long Uid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where qqid = " + QQ, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["uid"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            private set { }
        }
        /// <summary>
        /// 用户来源
        /// </summary>
        public UserSource Source
        {
            get;
            set;
        }
        /// <summary>
        /// CoolQ AT码
        /// </summary>
        public string AT { get { return "[CQ:at,qq=" + QQ.ToString() + "]  "; } private set { } }
        /// <summary>
        /// 向这个用户发送信息
        /// </summary>
        /// <param name="Message">信息</param>
        public void Send(string Message)
        {
            MessageSendHelper.Send(this, Message);
        }

        /// <summary>
        /// Loveca
        /// </summary>
        public long Loveca
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifitem where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["loveca"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifitem set loveca = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 优等生招募券
        /// </summary>
        public long ScoutTicket
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifitem where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["scout_ticket"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifitem set scout_ticket = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 十一连优等生招募券
        /// </summary>
        public long ElevenScoutTicket
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifitem where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["eleven_scout_ticket"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifitem set eleven_scout_ticket = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 优等生奖励
        /// </summary>
        public long HonorStudentBonus
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifitem where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["honor_student_bonus"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifitem set honor_student_bonus = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 十一连辅助券
        /// </summary>
        public long ElevenCouponTicket { get { return 0; } set { } }
        /// <summary>
        /// 辅助招募券
        /// </summary>
        public long CouponTicket
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifitem where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["coupon_ticket"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifitem set coupon_ticket = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 蜜柑点数
        /// </summary>
        public long MandarinPoint
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["mardarin_point"].ToString();
                }
                long.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update user set mardarin_point = " + value + " where uid  = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 今天是否签到
        /// </summary>
        public bool IsCheckin
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long timeStamp = 0;
                while (reader.Read())
                {
                    obj = reader["last_checkin_date"].ToString();
                }
                long.TryParse(obj, out timeStamp);
                DateTime LastCheckinDate = TimeStampHelper.GetTime(timeStamp.ToString());
                if (LastCheckinDate == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0))
                    return true;
                return false;
            }
            private set { }
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
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
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
                if (value == false)
                    num = 1;
                helper.ExecuteReader("update user set baned = " + num + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 账号密码，适用于Manger APP
        /// </summary>
        public string Passwd
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["passwd"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update user set passwd = '" + value + "' where uid  = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 账号创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["account_create_time_stamp"].ToString();
                }
                return TimeStampHelper.GetTime(obj);
            }
            private set {

            }
        }

        /// <summary>
        /// 签到
        /// </summary>
        public void Checkin()
        {
            if (IsCheckin)
                return;
            IsCheckin = true;

            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            SQLiteDataReader reader = helper.ExecuteReader(
                "update user set last_checkin_date = " +
                TimeStampHelper.ConvertDateTimeInt(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)).ToString() +
                " where uid = " + Uid, new SQLiteParameter() { });
        }
        /// <summary>
        /// 最后签到日期
        /// </summary>
        public DateTime LastCheckinDate
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from user where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["last_checkin_date"].ToString();
                }
                return TimeStampHelper.GetTime(obj);
            }
            private set
            {

            }
        }

        /// <summary>
        /// 指定锦球卡
        /// </summary>
        public int AtBanCard
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from chika_item where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                int num = 0;
                while (reader.Read())
                {
                    obj = reader["at_ban_card"].ToString();
                }
                int.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update chika_item set at_ban_card = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 随机锦球卡
        /// </summary>
        public long RandomBanCard
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from chika_item where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                int num = 0;
                while (reader.Read())
                {
                    obj = reader["random_ban_card"].ToString();
                }
                int.TryParse(obj, out num);
                return num;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update chika_item set random_ban_card = " + value + " where uid = " + Uid, new SQLiteParameter() { });
            }
        }

        /// <summary>
        /// 国服九位数字账号
        /// </summary>
        public string CnSifFriendId
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["cn_friend_id"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set cn_friend_id = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 国服Id
        /// </summary>
        public string CnSifUid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["cn_uid"].ToString();
                }
                long.TryParse(obj, out num);
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set cn_uid = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 日服九位数字账号
        /// </summary>
        public string JpSifFriendId
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["jp_friend_id"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set jp_friend_id = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 日服Id
        /// </summary>
        public string JpSifUid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["jp_uid"].ToString();
                }
                long.TryParse(obj, out num);
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set jp_uid = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 韩服九位数字账号
        /// </summary>
        public string KrSifFriendId
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["kr_friend_id"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set kr_friend_id = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 韩服Id
        /// </summary>
        public string KrSifUid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["kr_uid"].ToString();
                }
                long.TryParse(obj, out num);
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set kr_uid = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 台服九位数字账号
        /// </summary>
        public string TwSifFriendId
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["tw_friend_id"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set tw_friend_id = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 台服Id
        /// </summary>
        public string TwSifUid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["tw_uid"].ToString();
                }
                long.TryParse(obj, out num);
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set tw_uid = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 美服九位数字账号
        /// </summary>
        public string UsSifFriendId
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                while (reader.Read())
                {
                    obj = reader["global_friend_id"].ToString();
                }
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set global_friend_id = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }
        /// <summary>
        /// 美服Id
        /// </summary>
        public string UsSifUid
        {
            get
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                SQLiteDataReader reader = helper.ExecuteReader("select * from sifid where uid = " + Uid, new SQLiteParameter() { });
                string obj = "";
                long num = 0;
                while (reader.Read())
                {
                    obj = reader["global_uid"].ToString();
                }
                long.TryParse(obj, out num);
                return obj;
            }
            set
            {
                SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
                SqliteHelper helper = new SqliteHelper();
                helper.ExecuteReader("update sifid set global_uid = '" + value + "' where uid = " + Uid, new SQLiteParameter() { });
            }
        }

        /// <summary>
        /// Loveca
        /// </summary>
        [Obsolete]
        public long LovecaOld
        {
            get
            {
                string strLoveca = INIHelper.ReadIniKeys("Q" + QQ.ToString(), "Loveca", MainPlugin.UserDataSavePath);
                long loveca = 0;

                if (long.TryParse(strLoveca, out loveca) == false)
                {
                    return 0;
                }


                return loveca;
            }
            set
            {
                INIHelper.WriteIniKeys("Q" + QQ.ToString(), "Loveca", value.ToString(), MainPlugin.UserDataSavePath);
            }
        }
        /// <summary>
        /// 辅助招募券
        /// </summary>
        [Obsolete]
        public long CouponTicketOld
        {
            get
            {
                string strLoveca = INIHelper.ReadIniKeys("Q" + QQ.ToString(), "CouponTicket", MainPlugin.UserDataSavePath);
                long couponTicket = 0;

                if (Int64.TryParse(strLoveca, out couponTicket) == false)
                {
                    return 0;
                }


                return couponTicket;
            }
            set
            {
                INIHelper.WriteIniKeys("Q" + QQ.ToString(), "CouponTicket", value.ToString(), MainPlugin.UserDataSavePath);
            }
        }
        /// <summary>
        /// 蜜柑点数
        /// </summary>
        [Obsolete]
        public long MandarinPointOld
        {
            get
            {
                string strLoveca = INIHelper.ReadIniKeys("Q" + QQ.ToString(), "MandarinPoint", MainPlugin.UserDataSavePath);
                long mandarinPoint = 0;

                if (Int64.TryParse(strLoveca, out mandarinPoint) == false)
                {
                    return 0;
                }


                return mandarinPoint;
            }
            set
            {
                INIHelper.WriteIniKeys("Q" + QQ.ToString(), "MandarinPoint", value.ToString(), MainPlugin.UserDataSavePath);
            }
        }
        /// <summary>
        /// 优等生奖励
        /// </summary>
        [Obsolete]
        public long HonorStudentBonusOld
        {
            get
            {
                string strLoveca = INIHelper.ReadIniKeys("Q" + QQ.ToString(), "HonorStudentBonus", MainPlugin.UserDataSavePath);
                long honorStudentBonus = 0;

                if (Int64.TryParse(strLoveca, out honorStudentBonus) == false)
                {
                    return 0;
                }


                return honorStudentBonus;
            }
            set
            {
                INIHelper.WriteIniKeys("Q" + QQ.ToString(), "HonorStudentBonus", value.ToString(), MainPlugin.UserDataSavePath);
            }
        }
    }
}
