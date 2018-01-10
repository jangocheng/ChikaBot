using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Status
    {
        public static int Auto(User Sender)
        {
            Sender.Send(" --- " + Sender.AT + ":UID" + Sender.Uid + " --- \n" +
                "[CQ:image,file=UI/Items/Loveca.png]" + Sender.Loveca + "\n" +
                "[CQ:image,file=UI/Items/MardarinPoint.png]" + Sender.MandarinPoint + "\n" + 
                "回复 /我的状态 查看更为详细的信息。(超长，不建议在群内使用)");
            return 1;
        }

        public static int Auto2(User Sender)
        {
            Sender.Send(Sender.AT + "\n" +
                "  --- 基础信息 --- \n" +
                " 注册日期：" + Sender.CreateTime.ToString("yyyy年M月d日 hh:mm:ss") + "\n" + 
                " 用户账号：" + Sender.Uid + "\n" + 
                " 最后签到：" + Sender.LastCheckinDate.ToString("yyyy年M月d日") + "\n" + 
                "  --- 普通道具 --- \n" + 
                " 抛锦球卡： [指定]" + Sender.AtBanCard + " [随机]" + Sender.RandomBanCard + "\n" + 
                " Loveca  ：" + Sender.Loveca + "\n" + 
                " 蜜柑点   ：" + Sender.MandarinPoint + "\n" + 
                "  --- 辅助道具 --- \n" + 
                " 招募券   ：" + Sender.ScoutTicket + "\n" + 
                " 十一连券：" + Sender.ElevenScoutTicket + "\n" + 
                " 辅助券   ：" + Sender.CouponTicket + "\n" + 
                " 招募奖励：" + Sender.HonorStudentBonus + "点\n" + 
                "  ----------------- \n" + 
                "如果需要转赠账号/销号请联系QQ1813274096"
            );

            return 1;
        }

        public static int Auto3(Group ByGroup)
        {
            User sender = new User(0, new UserSource(UserSource.GroupMessage, ByGroup, 0));
            sender.Send(" --- 你群信息(Debug) --- \n" + "GroupID:" + ByGroup.Gid + "\nFlags:" + ByGroup.Flags + "\nSettings:" + ByGroup.Settings);
            return 1;
        }
    }
}
