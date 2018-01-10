using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.CQP.Framework;
using Qpomelo.CQP.Plugins.ChikaMain.Commands;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    public class MainPlugin : PluginBase
    {
        public new static ICoolQApi CoolQApi;

        public const string VersionStr = "v3.5.2 Release";
        public const int Build = 1242;
        public const string Version = "ChikaBot " + VersionStr;

        public static string RunPath = AppDomain.CurrentDomain.BaseDirectory + "\\";
        public static string CQPath = RunPath + "..\\";
        public static string ImagePath = CQPath + "data\\image\\";

        #region Build > 1500 时删除
        public static string UserDataSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Chika\\Datas\\Users.ini";
        public static string GroupDataSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Chika\\Datas\\Groups.ini";
        public static string DonateListSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Chika\\DonateList.txt";
        public static string LoginBounsListPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Chika\\LoginBouns.ini";
        #endregion

        public static string SifPathCn = RunPath + "Databases\\sif\\cn\\";
        public static string SifPathJp = RunPath + "Databases\\sif\\jp\\";
        public static string SifPathGlobal = RunPath + "Databases\\sif\\us\\";

        public static string MainDatabase = RunPath + "Databases\\Main.db";

        public const string IebSifDbApiKey = "test_apikey";


        public MainPlugin(ICoolQApi coolQApi) : base(coolQApi)
        {
            CoolQApi = coolQApi;
        }

        /// <summary>
        /// AppId需要与程序集名称相同
        /// </summary>
        public override string AppId => "Qpomelo.CQP.Plugins.ChikaMain";

        /// <summary>
        /// 处理群聊消息。
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1</param>
        /// <param name="sendTime">消息发送时间(时间戳)</param>
        /// <param name="fromGroup>来源群</param>
        /// <param name="fromQq">来源QQ</param>
        /// <param name="fromAnonymous">来源匿名用户</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">消息字体</param>
        /// <returns> 0 - 忽略消息 ; 1 - 拦截消息 </returns>
        public override int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQq, string fromAnonymous, string msg, int font)
        {
            Command command = new Command(msg, new User(fromQq, new UserSource(UserSource.GroupMessage, new Group(fromGroup), 0)), sendTime.ToString());
            return command.Run();
        }

        /// <summary>
        /// 处理私信消息
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1</param>
        /// <param name="sendTime">消息发送时间(时间戳)</param>
        /// <param name="fromQq">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">消息字体</param>
        /// <returns> 0 - 忽略消息 ; 1 - 拦截消息 </returns>
        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQq, string msg, int font)
        {
            Command command = new Command(msg, new User(fromQq, new UserSource(UserSource.PrivateMessage, null, 0)), sendTime.ToString());
            return command.Run();
        }

        /// <summary>
        /// 处理讨论组消息
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1</param>
        /// <param name="sendTime">消息发送时间(时间戳)</param>
        /// <param name="fromDiscuss">来源讨论组</param>
        /// <param name="fromQq">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">消息字体</param>
        /// <returns> 0 - 忽略消息 ; 1 - 拦截消息 </returns>
        public override int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQq, string msg, int font)
        {
            CoolQApi.SetDiscussLeave(fromDiscuss);
            return 1;
        }

        /// <summary>
        /// 处理菜单按下
        /// </summary>
        /// <returns></returns>
        public override int ProcessMenuClickA()
        {
            System.Windows.Forms.MessageBox.Show("ChikaApi - Core\nCode by ChikaDevelopTeam\n" + VersionStr + " Build" + Build);
            return base.ProcessMenuClickA();
        }
    }
}
