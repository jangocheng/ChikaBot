using Newbe.CQP.Framework;
using Newbe.CQP.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class ThrowBall
    {
        public static int Auto(User Sender, string Msg)
        {
            if(Msg == "抛锦球")
            {
                if(Sender.Source.Group == null)
                {
                    Sender.Send("请在群内使用");
                    return 1;
                }

                if(Sender.RandomBanCard < 1)
                {
                    Sender.Send("锦球卡不足！请购买！\nP.S 你可以在抽奖卡池，积分商店购买到。");
                    return 1;
                }

                Sender.RandomBanCard--;

                ModelWithSourceString<IEnumerable<GroupMemberInfo>> model = 
                    CoolApiExtensions.GetGroupMemberList(MainPlugin.CoolQApi, Sender.Source.Group.QQId);

                List<GroupMemberInfo> members = new List<GroupMemberInfo>();

                foreach (GroupMemberInfo info in model.Model)
                {
                    if(info.Authority != "群主")
                    {
                        if(info.Authority != "管理员")
                        {
                            members.Add(info);
                        }
                    }
                }

                long banQQIndexOfMembers = members[new Random().Next(0, members.Count)].Number;
                int banTimeBySecond = new Random().Next(60, 600);
                string banTimeStr = banTimeBySecond + "s";
                MainPlugin.CoolQApi.SetGroupBan(Sender.Source.Group.QQId, banQQIndexOfMembers, banTimeBySecond);
                User BanUser = new User(banQQIndexOfMembers, Sender.Source);
                Sender.Send("恭喜" + BanUser.AT + "被" + Sender.AT + "砸中！禁言" + banTimeStr + "!");
                return 1;
            }
            else if (Msg.IndexOf("抛锦球") == 0)
            {
                if (Sender.Source.Group == null)
                {
                    Sender.Send("请在群内使用");
                    return 1;
                }

                if (Sender.AtBanCard < 1)
                {
                    Sender.Send("锦球卡不足！请购买！\nP.S 你可以在抽奖卡池，积分商店购买到。");
                    return 1;
                }
                if (Msg.IndexOf("[cq:at,qq=") == -1 && Msg.IndexOf("]") == -1)
                {
                    Sender.Send(Sender.AT + "请@你想禁言谁");
                    return 1;
                }
                string msg = Msg.Replace("抛锦球", "");
                long banQQ = 0;
                string obj = msg.Substring(
                    msg.IndexOf("[cq:at,qq=") + 10,
                    msg.Length - 11 - msg.Substring(msg.IndexOf("]"), msg.Length - msg.IndexOf("]")).Length + 1
                    );
                long.TryParse(obj, out banQQ);
                msg = msg.Replace("[cq:at,qq=" + obj + "]", "");
                if (msg.Replace(" ","") != "")
                {
                    Sender.Send(Sender.AT + "抛锦球的格式为 /抛锦球[@一个人]\n[@一个人]为选填项");
                    return 1;
                }
                User banUser = new User(banQQ, Sender.Source);
                ModelWithSourceString<GroupMemberInfo> banUserInfo = CoolApiExtensions.GetGroupMemberInfoV2(MainPlugin.CoolQApi, Sender.Source.Group.QQId, banUser.QQ, false);
                if(banUserInfo.Model.Authority == "群主" || banUserInfo.Model.Authority == "管理员")
                {
                    Sender.Send(Sender.AT + "你@的人太强大了，千歌不能砸中他！");
                    return 1;
                }
                Sender.AtBanCard--;
                int banTimeBySecond = new Random().Next(60, 600);
                int sec = 0, min = 0;
                min = (int)(sec / 60);
                sec = sec % 60;
                string banTimeStr = banTimeBySecond + "s";
                MainPlugin.CoolQApi.SetGroupBan(Sender.Source.Group.QQId, banUser.QQ, banTimeBySecond);
                Sender.Send("恭喜" + banUser.AT + "被" + Sender.AT + "砸中！禁言" + banTimeStr + "!");
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
