using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class ExceptionReportHelper
    {
        /// <summary>
        /// 将错误上报给开发者(MainPlugin.DeveloperQQ)
        /// </summary>
        /// <param name="byModel">引起错误的模块</param>
        /// <param name="byUser">引发的用户</param>
        /// <param name="exception">catch到的异常对象</param>
        public static void Send(string byModel, User byUser, Exception exception, string Msg)
        {

            if(byUser == null)
            {
                MainPlugin.CoolQApi.SendPrivateMsg(
                    1813274096,
                    "---Bug report---\n" +
                    "错误模块: " + byModel + "\n" +
                    "错误信息：" + Msg + "\n" +
                    "来源: null\n" + 
                    "错误来源: " + exception.Source + "\n" +
                    "引起该错误的方法: " + exception.TargetSite.ToString() + "\n" +
                    "异常信息: " + exception.Message + "\n" +
                    "错误堆叠：" + exception.StackTrace
                );
                return;
            }

            string groupOrDiscussOrPrivateMsg = "";
            switch (byUser.Source.SourceType)
            {
                case UserSource.PrivateMessage:
                    groupOrDiscussOrPrivateMsg = "来源: 私聊信息\nfromQQ: " + byUser.QQ;
                    break;
                case UserSource.GroupMessage:
                    groupOrDiscussOrPrivateMsg = "来源: 群" + byUser.Source.Group.QQId + "\nfromQQ: " + byUser.QQ;
                    break;
                case UserSource.DiscussMessage:
                    groupOrDiscussOrPrivateMsg = "来源: 讨论组" + byUser.Source.DiscussNumber + "\nfromQQ: " + byUser.QQ;
                    break;
                default:
                    groupOrDiscussOrPrivateMsg = "来源: 未知" + byUser.Source.DiscussNumber + "\nfromQQ: " + byUser.QQ;
                    break;
            }

            string exceptionMessage = "";
            exceptionMessage = exception.Message;

            MainPlugin.CoolQApi.SendPrivateMsg(
                1813274096,
                "---Bug report---\n" +
                "错误模块: " + byModel + "\n" +
                "错误信息：" + Msg + "\n" + 
                groupOrDiscussOrPrivateMsg + "\n" +
                "错误来源: " + exception.Source + "\n" +
                "引起该错误的方法: " + exception.TargetSite.ToString() + "\n" +
                "异常信息: " + exceptionMessage + "\n" + 
                "错误堆叠：" + exception.StackTrace
            );
        }
    }
}
