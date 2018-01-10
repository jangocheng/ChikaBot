using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class MiniGame
    {
        public static int Auto(User Sender,string Msg)
        {
            DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
            DataTable dt = dbh.Execute("select * from games where case_text = '" + Msg.Replace("-","横").Replace("\"","双引号").Replace("'","单引号") + "'");
            if(dt.Rows.Count == 0)
            {
                dbh.Close();
                return 0;
            }
            long start_time, end_time;
            long.TryParse(dt.Rows[0]["start_time"].ToString(), out start_time);
            long.TryParse(dt.Rows[0]["end_time"].ToString(), out end_time);
            if(start_time > TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
            {
                return 0;
            }
            if(end_time < TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
            {
                return 0;
            }
            string obj = dt.Rows[0]["bouns_random"].ToString();
            int bounsRandom = 0, faildRandom = 0;
            int.TryParse(obj, out bounsRandom);

            obj = dt.Rows[0]["faild_random"].ToString();
            int.TryParse(obj, out faildRandom);

            if (faildRandom >= new Random().Next(1, 101))
            {
                string faild_text = dt.Rows[0]["faild_text"].ToString();
                faild_text = faild_text.Replace("<use_num>", dt.Rows[0]["use_num"].ToString())
                         .Replace("<at>", Sender.AT)
                         .Replace("\\n", "\n");
                Sender.Send(faild_text);
                return 1;
            }

            SqliteHelper.SetConnectionString(MainPlugin.MainDatabase, "");
            SqliteHelper helper = new SqliteHelper();
            helper.ExecuteReader("update games set use_num = use_num + 1 where case_text = '" + Msg.Replace("-", "横").Replace("\"", "双引号").Replace("'", "单引号") + "'", new SQLiteParameter() { });

            if(bounsRandom >= new Random().Next(1, 101))
            {
                string toDoJson = dt.Rows[0]["to_do_json"].ToString();
                ToDoModel model = ToDoModelHelper.GetModel(toDoJson);
                Sender.Loveca = Sender.Loveca + model.loveca;
                Sender.MandarinPoint = Sender.MandarinPoint + model.mardarin_point;
                Sender.CouponTicket = Sender.CouponTicket + model.coupon_ticket;
                Sender.AtBanCard = Sender.AtBanCard + model.at_ban_card;
                Sender.RandomBanCard = Sender.RandomBanCard + model.random_ban_card;
                //Sender.NoNormalScoutTicket = Sender.NoNormalScoutTicket + model.no_normal_scout_ticket;
                Sender.ScoutTicket = Sender.ScoutTicket + model.scout_ticket;
                Sender.ElevenScoutTicket = Sender.ElevenScoutTicket + model.eleven_scout_ticket;
                string back_text_bouns = dt.Rows[0]["back_text_bouns"].ToString();
                back_text_bouns = back_text_bouns.Replace("<loveca>", model.loveca.ToString())
                         .Replace("<point>", model.mardarin_point.ToString())
                         .Replace("<coupon_ticket>", model.coupon_ticket.ToString())
                         .Replace("<at_ban_card>", model.at_ban_card.ToString())
                         .Replace("<random_ban_card>", model.random_ban_card.ToString())
                         //.Replace("<no_normal_scout_ticket>", model.no_normal_scout_ticket.ToString())
                         .Replace("<scout_ticket>", model.scout_ticket.ToString())
                         .Replace("<eleven_scout_ticket>", model.eleven_scout_ticket.ToString())
                         .Replace("<use_num>",dt.Rows[0]["use_num"].ToString())
                         .Replace("<at>",Sender.AT)
                         .Replace("\\n", "\n");
                Sender.Send(back_text_bouns);
                return 1;
            }
            string back_text = dt.Rows[0]["back_text"].ToString();
            back_text = back_text.Replace("<use_num>", dt.Rows[0]["use_num"].ToString())
                     .Replace("<at>", Sender.AT)
                     .Replace("\\n","\n");
            Sender.Send(back_text);
            return 1;
        }
    }
}
