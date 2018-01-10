using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.ChikaModel
{
    /// <summary>
    /// 解析消息
    /// </summary>
    class CommandAnalyze
    {
        public string Msg { get; private set; }
        public long QQ { get; private set; }
        public long Group { get; private set; }
        public string Anonymous { get; private set; }
        public bool IsGroup { get; private set; }
        public bool IsAnonymous { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">接收到的信息</param>
        /// <param name="qq">来源QQ</param>
        /// <param name="group">来源群(如果有的话，没有就填0或者null)</param>
        /// <param name="anonymous">来源匿名者(如果有的话，没有就丢个空字符串或者null)</param>
        /// <param name="isGroup">是否来源群，如果group丢0/null的话填true</param>
        /// <param name="isAnonymous">是否来源匿名者，如果anonymous丢空字符串/null的话填true</param>
        public CommandAnalyze(string msg,long qq,long group,string anonymous,bool isGroup,bool isAnonymous)
        {
            Msg = msg;
            QQ = qq;
            Group = group;
            Anonymous = anonymous;
            IsGroup = isGroup;
            IsAnonymous = isAnonymous;
        }

        /// <summary>
        /// 开始解析命令
        /// </summary>
        /// <returns> 如果处理了话会丢1 </returns>
        public int Run()
        {



            return 0;
        }
    }
}
