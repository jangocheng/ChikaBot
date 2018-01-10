using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Transfer
    {
        public static int Auto(User Sender, string Msg)
        {
            if (Msg.IndexOf("转账") == 0)
            {
                string msg = "",obj = "";
                long byQQ, sendQQ;
                byQQ = Sender.QQ;
                if (Msg.IndexOf("[cq:at,qq=") == -1 && Msg.IndexOf("]") == -1)
                {
                    Sender.Send(Sender.AT + "请@你需要转账的对象");
                    return 1;
                }
                int trNum = 0;
                msg = Msg.Replace("转账", "");
                obj = msg.Substring(
                    msg.IndexOf("[cq:at,qq=") + 10,
                    msg.Length - 11 - msg.Substring(msg.IndexOf("]"), msg.Length - msg.IndexOf("]")).Length + 1
                    );
                long.TryParse(obj, out sendQQ);
                msg = msg.Replace("[cq:at,qq=" + obj + "]","");

                int.TryParse(msg, out trNum);
                if(trNum <= 0)
                {
                    Sender.Send(Sender.AT + "请输入一个有效的数值！");
                    return 1;
                }
                if(Sender.Loveca < trNum)
                {
                    Sender.Send(Sender.AT + "你剩余的Loveca不足以转出！");
                    return 1;
                }

                User SendQQ = new User(sendQQ, Sender.Source);
                SendQQ.Loveca = SendQQ.Loveca + trNum;
                SendQQ.Send(SendQQ.AT + " " + Sender.AT + "向你转账Loveca" + trNum);
                Sender.Loveca = Sender.Loveca - trNum;
                Sender.Send(Sender.AT + "成功转出Loveca" + trNum);
                return 1;
            }

            return 0;
        }
    }
}
