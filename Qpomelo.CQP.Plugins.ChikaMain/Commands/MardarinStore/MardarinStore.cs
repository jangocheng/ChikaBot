using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class MardarinStore
    {
        public static int Auto(User Sender,string Msg)
        {
            if(Msg == "积分商店")
            {
                DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
                DataTable dt = dbh.Execute("select * from mardarin_store");
                if(dt.Rows.Count == 0)
                {
                    Sender.Send(Sender.AT + "商店暂时没有东西可以卖啦Orz");
                    dbh.Close();
                    return 1;
                }
                string toSend = " --- 积分商店 --- \n" + 
                                " ID 价格   物品";
                foreach(DataRow dr in dt.Rows)
                {
                    int num = 0;
                    string obj = "";
                    int.TryParse(dr["start_time"].ToString(), out num);
                    if(num > TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                    {
                        continue;
                    }
                    int.TryParse(dr["end_time"].ToString(), out num);
                    if (num < TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                    {
                        continue;
                    }
                    toSend = toSend + "\n" + dr["sid"].ToString() + " " + dr["use_points"].ToString() + "Point " + dr["title"].ToString();
                }

                Sender.Send(toSend + "\n请回复 /详情 商品ID 来查看详情");
                dbh.Close();
                return 1;
            }
            else if(Msg.IndexOf("购买") == 0)
            {
                int num = 0;
                string obj = "";
                obj = Msg.Replace("购买","");
                int.TryParse(obj, out num);
                if(num <= 0)
                {
                    Sender.Send(Sender.AT + "不存在序号为" + obj + "的商品！");
                    return 1;
                }
                DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
                DataTable dt = dbh.Execute("select * from mardarin_store where sid = " + num);
                if (dt.Rows.Count == 0)
                {
                    Sender.Send(Sender.AT + "不存在序号为" + obj + "的商品！");
                    dbh.Close();
                    return 1;
                }
                int.TryParse(dt.Rows[0]["start_time"].ToString(), out num);
                if (num > TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                {
                    Sender.Send(Sender.AT + "该商品未上架！");
                    return 1;
                }
                int.TryParse(dt.Rows[0]["end_time"].ToString(), out num);
                if (num < TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                {
                    Sender.Send(Sender.AT + "该商品已下架！");
                    return 1;
                }
                obj = dt.Rows[0]["use_points"].ToString();
                int.TryParse(obj, out num);
                if (Sender.MandarinPoint < num)
                {
                    Sender.Send(Sender.AT + "你所持的蜜柑Points不足够买这个东西！");
                    dbh.Close();
                    return 1;
                }
                Sender.MandarinPoint = Sender.MandarinPoint - num;
                string toDoJson = dt.Rows[0]["to_do_json"].ToString();
                string sendMsg = dt.Rows[0]["on_buyed_send_msg"].ToString();
                ToDoModel model = ToDoModelHelper.GetModel(toDoJson);
                Sender.Loveca = Sender.Loveca + model.loveca;
                Sender.MandarinPoint = Sender.MandarinPoint + model.mardarin_point;
                Sender.CouponTicket = Sender.CouponTicket + model.coupon_ticket;
                Sender.AtBanCard = Sender.AtBanCard + model.at_ban_card;
                Sender.RandomBanCard = Sender.RandomBanCard + model.random_ban_card;
                //Sender.NoNormalScoutTicket = Sender.NoNormalScoutTicket + model.no_normal_scout_ticket;
                Sender.ScoutTicket = Sender.ScoutTicket + model.scout_ticket;
                Sender.ElevenScoutTicket = Sender.ElevenScoutTicket + model.eleven_scout_ticket;

                Sender.Send(sendMsg);
                dbh.Close();
            }
            else if(Msg.IndexOf("详情") == 0)
            {
                int num = 0;
                string obj = "";
                obj = Msg.Replace("详情", "");
                int.TryParse(obj, out num);
                if (num <= 0)
                {
                    Sender.Send(Sender.AT + "不存在序号为" + obj + "的商品！");
                    return 1;
                }
                DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
                DataTable dt = dbh.Execute("select * from mardarin_store where sid = " + num);
                if (dt.Rows.Count == 0)
                {
                    Sender.Send(Sender.AT + "不存在序号为" + obj + "的商品！");
                    dbh.Close();
                    return 1;
                }
                obj = dt.Rows[0]["use_points"].ToString();
                int.TryParse(obj, out num);
                string toSendMsg = " --- 积分商店 --- ";
                toSendMsg = toSendMsg + "\n" + dt.Rows[0]["title"].ToString() + "\n" + dt.Rows[0]["description"].ToString().Replace("\\n","\n")
                    + "\n价格：" + num + "Points";
                int.TryParse(dt.Rows[0]["start_time"].ToString(), out num);
                if (num > TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                {
                    toSendMsg = toSendMsg + "\n该商品暂未上架";
                }
                int.TryParse(dt.Rows[0]["end_time"].ToString(), out num);
                if (num < TimeStampHelper.ConvertDateTimeInt(DateTime.Now))
                {
                    toSendMsg = toSendMsg + "\n该商品现已下架";
                }
                else
                {
                    toSendMsg = toSendMsg + "\n回复 /购买" + dt.Rows[0]["sid"].ToString() + " 来购买";
                }

                Sender.Send(toSendMsg);
                dbh.Close();
            }
            return 0;
        }
    }
}
