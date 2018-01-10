using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.Chat
{
    class Msg
    {
        public static int Auto(User Sender, string Msg)
        {

            DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
            DataTable table = dbh.Execute("select * from msg");
            foreach (DataRow row in table.Rows)
            {
                string case_json = row["case_json"].ToString();
                var casejson = JsonConvert.DeserializeObject(case_json) as JObject;
                bool isNext = false;
                foreach (string str in casejson["has"])
                {
                    if (Msg.IndexOf(str) == -1)
                    {
                        isNext = true;
                        break; // 进行匹配下个case_json
                    }
                }
                if(isNext == true)
                {
                    continue;
                }
                string send_json = row["send_json"].ToString();
                var sendjson = JsonConvert.DeserializeObject(send_json) as JObject;
                List<string> msgs = new List<string>();
                if((bool)sendjson["random"] == true)
                {
                    foreach(string str in sendjson["msgs"])
                    {
                        msgs.Add(str);
                    }
                    string sendStr = msgs[new Random().Next(0, msgs.Count)];
                    List<string> replace_by = new List<string>() { "<at>", "\\n"};
                    List<string> replace_to = new List<string>() { Sender.AT, "\n"};
                    int i = 0;
                    foreach(string str in replace_by)
                    {
                        sendStr = sendStr.Replace(replace_by[i],replace_to[i]);
                    }
                    Sender.Send(sendStr);
                    return 1;
                }

            }

            /*if (Msg.IndexOf("果南") != -1)
            {
                MessageSendHelper.Send(Sender, "哦？你说的是那个女装特别棒的水獭吗？");
                return 1;
            }
            if (Msg.IndexOf("emmm") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\emmm.jpg"));
                return 1;
            }
            if (Msg.IndexOf("狗开发") != -1 || Msg.IndexOf("苟开发") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\这话谁教你的.jpg"));
                return 1;
            }
            if (Msg.IndexOf("千果") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\抱歉拉拉真的是可以为所欲为的.jpg"));
                return 1;
            }
            if (Msg.IndexOf("捡垃圾") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\大哥你这个塑料瓶还要不要啊.jpg"));
                return 1;
            }
            if (Msg.IndexOf("女装") != -1 && Msg.IndexOf("程序员") != -1 || Msg.IndexOf("电獭") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\程序员从入门到女装.jpg"));
                return 1;
            }
            if (Msg.IndexOf("欧洲人") != -1 || Msg.IndexOf("欧洲狗") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\自己抽不到就说抽不到，不要赖别人.jpg"));
                return 1;
            }
            if (Msg.ToLower().IndexOf("qq小冰") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\我看你他妈是在刁难我QQ小冰.jpg"));
                return 1;
            }
            if (Msg.IndexOf("苟") != -1 || Msg.IndexOf("岂") != -1 || Msg.IndexOf("+1s") != -1 || Msg.IndexOf("-1s") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\禁止膜.jpg"));
                return 1;
            }
            if (Msg.IndexOf("原谅") != -1 || Msg.ToLower().IndexOf("ntr") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\当然是选择原谅她啊.jpg"));
                return 1;
            }
            if (Msg.IndexOf("活动卡") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\活动卡.jpg"));
                return 1;
            }
            if (Msg.IndexOf("药丸") != -1 || Msg.IndexOf("曜丸") != -1)
            {
                MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\吃枣药丸.jpg"));
                return 1;
            }
            if (Msg.IndexOf("杏树") != -1 || Msg.IndexOf("千歌") != -1 || Msg.Replace(" ", "").IndexOf("[CQ:at,qq=" + MainPlugin.CoolQApi.GetLoginQQ() + "]") != -1)
            {
                if (Msg.IndexOf("踢我") != -1)
                {
                    MessageSendHelper.Send(Sender, Sender.AT + " 这位朋友请停一停你的杏幻想，停一停！");
                    return 1;
                }
            }
            if (Sender.QQ == 1691528941)
            {
                Random stRd = new Random();
                if (stRd.Next(1, 20) == 1)
                {
                    MessageSendHelper.Send(Sender, Newbe.CQP.Framework.CoolQCode.Image("UI\\Face\\哦是吗.jpg"));
                    return 1;
                }


            }*/

            return 0;
        }
    }
}
