using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class MessageSendHelper
    {
        public static void Send(User user,string msg)
        {
            switch(user.Source.SourceType)
            {
                case UserSource.DiscussMessage:
                    MainPlugin.CoolQApi.SendDiscussMsg(user.Source.DiscussNumber, msg);
                    break;
                case UserSource.GroupMessage:
                    MainPlugin.CoolQApi.SendGroupMsg(user.Source.Group.QQId, msg);
                    break;
                case UserSource.PrivateMessage:
                    MainPlugin.CoolQApi.SendPrivateMsg(user.QQ, msg.Replace(Newbe.CQP.Framework.CoolQCode.At(user.QQ) + "\n", "").Replace(Newbe.CQP.Framework.CoolQCode.At(user.QQ), ""));
                    break;
            }
        }
    }
}
