using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Lottery
    {
        public static int Auto(User Sender,string Msg)
        {
            if(Msg == "抽奖")
            {
                if(Sender.MandarinPoint < 500)
                {
                    Sender.Send("你的蜜柑Points不足500！");
                    return 1;
                }
                Sender.MandarinPoint = Sender.MandarinPoint - 500;
                Random rd = new Random();
                int num = rd.Next(1, 10000);
                if (num == 1)
                {
                    if (rd.Next(1, 10000) == 1)
                    {
                        Sender.Send(Sender.AT + "恭喜你中了Loveca * 1~这颗Loveca可以在 国服 日服 美服兑换，请联系Q柚兑换吧~");
                        Command cmd = new Command("#全群通知恭喜" + Sender.QQ + "抽奖中了一颗SIF中的Loveca~", new User(-10000, new UserSource(UserSource.GroupMessage, Sender.Source.Group, 0)), TimeStampHelper.ConvertDateTimeInt(DateTime.Now).ToString());
                        new User(1813274096, new UserSource(UserSource.PrivateMessage, null, 0)).Send(Sender.QQ + "获得了一颗Loveca，请及时兑换");
                        return 1;
                    }
                    else
                    {
                        num = rd.Next(1, 10000);
                    }
                }
                else if (num > 1 && num <= 500)
                {
                    Sender.Send(Sender.AT + " 获得绿卷一张~已发送到你的背包~");
                    Sender.ScoutTicket++;
                    return 1;
                }
                else if(num > 500 && num <= 2000)
                {
                    Sender.Send(Sender.AT + " 获得Loveca一块，这个Loveca只能在机器人内使用！");
                    Sender.Loveca++;
                    return 1;
                }
                else if(num > 2000 && num <= 4000)
                {
                    Sender.Send(Sender.AT + " 获得蜜柑Poins × 500点~已发送到你的背包~");
                    Sender.MandarinPoint = Sender.MandarinPoint + 500;
                    return 1;
                }
                else if(num > 4000 && num <= 7000)
                {
                    Sender.Send(Sender.AT + " 获得蜜柑Points × 100点~已发送到你的背包~");
                    Sender.MandarinPoint = Sender.MandarinPoint + 100;
                    return 1;
                }
                else
                {
                    Sender.Send(Sender.AT + " 什么都没得到哦~");
                    return 1;
                }
            }
            return 0;
        }
    }
}
