using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class UserSource
    {
        /// <summary>
        /// 私信消息
        /// </summary>
        public const int PrivateMessage = 1;
        /// <summary>
        /// 群消息
        /// </summary>
        public const int GroupMessage = 2;
        /// <summary>
        /// 讨论组消息
        /// </summary>
        public const int DiscussMessage = 3;

        /// <summary>
        /// 来源类型
        /// </summary>
        public int SourceType { get; set; } 

        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnonymous { get; set; }
        /// <summary>
        /// 匿名标识符
        /// </summary>
        public string Anonymous { get; set; }

        /// <summary>
        /// 群号码
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 讨论组号码
        /// </summary>
        public long DiscussNumber { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SourceType">类型</param>
        /// <param name="GroupNumber">群号</param>
        /// <param name="DiscussNumber">讨论组号</param>
        public UserSource(int SourceType,Group Group,long DiscussNumber)
        {
            this.SourceType = SourceType;
            this.Group = Group;
            this.DiscussNumber = DiscussNumber;
        }
    }
}
