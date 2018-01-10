using Newbe.CQP.Framework;
using Newbe.CQP.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Command
    {
        /// <summary>
        /// 同义化
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public static string rcc(string Msg)
        {
            string msg = Msg;
            DatabaseHelper dbh = new DatabaseHelper(MainPlugin.MainDatabase);
            DataTable tb = dbh.Execute("select * from rcc");
            foreach (DataRow row in tb.Rows)
            {
                msg = msg.Replace("<login_qq>", MainPlugin.CoolQApi.GetLoginQQ().ToString()).Replace(row["by_text"].ToString(), row["to_text"].ToString()).ToLower();
            }
            return msg;
            // 千歌 => chika
            /*return Msg.Replace("/", "").Replace("\\", "").Replace("#", "")
                .Replace("[CQ:at,qq=" + MainPlugin.CoolQApi.GetLoginQQ() + "]", "").ToLower()
                .Replace("硬度10", "硬度十").Replace("琴梨", "小鸟").Replace("日香", "妮可")
                .Replace("帐", "账").Replace("-","横").Replace("\"","双引号").Replace("'","单引号")
                .Replace("11连", "十一连").Replace("缪斯", "u").Replace("水团", "a").Replace("μ's", "u").Replace("aqours", "a")
                .Replace("咕服号状态","国服号状态").Replace("鸽服号状态","国服号状态").Replace(" ","");*/
        }

        /// <summary>
        /// 来源
        /// </summary>
        public User Sender { get; private set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; private set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime SendTime { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="User">来源用户</param>
        /// <param name="timeStamp">发送时间</param>
        public Command(string msg, User fromUser, string timeStamp)
        {
            Sender = fromUser;
            Msg = msg;
            SendTime = TimeStampHelper.GetTime(timeStamp);
        }
        /// <summary>
        /// 开始处理
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            try
            {
                // 全群发送同一条信息
                if (Msg.IndexOf("#全群通知") == 0)
                {
                    if (Sender.QQ == -10000)
                    {
                        IEnumerable<GroupInfo> groups = ExtrasCoolApiExtensions.GetGroupList(MainPlugin.CoolQApi);
                        string Broadcast = Msg.Replace("#全群通知", "");
                        foreach (GroupInfo group in groups)
                        {
                            User system = new User(-100000, new UserSource(UserSource.GroupMessage, new Group(group.GroupNumber), 0));
                            system.Send(Broadcast);
                        }
                        return 1;
                    }
                }

                // 强制回收垃圾
                if (Msg.IndexOf("#回收垃圾") == 0)
                {
                    if (Sender.QQ == 1813274096)
                    {
                        GC.Collect();
                        Sender.Send("void GC.Collect();成功");
                        return 1;
                    }
                }

                // 强制重新载入全部用户
                if (Msg.IndexOf("#重载用户") == 0)
                {
                    if (Sender.QQ == 1813274096)
                    {
                        IEnumerable<GroupInfo> info = ExtrasCoolApiExtensions.GetGroupList(MainPlugin.CoolQApi);
                        int groups = 0, users = 0;
                        foreach (GroupInfo inf in info)
                        {
                            Group gp = new Group(inf.GroupNumber);
                            ModelWithSourceString<IEnumerable<GroupMemberInfo>> members = CoolApiExtensions.GetGroupMemberList(MainPlugin.CoolQApi, inf.GroupNumber);
                            foreach (GroupMemberInfo i in members.Model)
                            {
                                User usr = new User(i.Number, new UserSource(UserSource.GroupMessage, new Group(gp.QQId), 0));
                                users++;
                            }
                            groups++;
                        }
                        GC.Collect();
                        Sender.Send("已经重新载入了" + groups + "个群，" + users + "个QQ。");
                        return 1;
                    }
                }

                // 如果被封禁则不处理
                if (Sender.Source.Group != null)
                {
                    if (Sender.Source.Group.IsBaned)
                    {
                        return 1;
                    }
                }
                if (Sender.IsBaned)
                {
                    return 1;
                }
                // 你群如果关了千歌不处理
                if (Sender.Source.Group != null)
                {
                    if (!Sender.Source.Group.IsEnable)
                    {
                        if (rcc(Msg) != "开千歌")
                        {
                            return 1;
                        }
                    }
                }

                // 菜单
                if (Menu.AutoSend(rcc(Msg), Sender) != 0)
                    return 1;

                // 开启千歌
                if (rcc(Msg) == "开千歌")
                {
                    ModelWithSourceString<GroupMemberInfo> info = CoolApiExtensions.GetGroupMemberInfoV2(MainPlugin.CoolQApi, Sender.Source.Group.QQId, Sender.QQ, false);
                    if (info.Model.Authority == "群主" || info.Model.Authority == "管理员" || Sender.QQ == 1813274096)
                    {
                        Sender.Send(Sender.AT + "千歌已开启");
                        Sender.Source.Group.IsEnable = true;
                        return 1;
                    }
                    else
                    {
                        Sender.Send(Sender.AT + "请管理员来进行操作");
                        return 1;
                    }
                }

                // 关闭千歌
                if (rcc(Msg) == "关千歌")
                {
                    ModelWithSourceString<GroupMemberInfo> info = CoolApiExtensions.GetGroupMemberInfoV2(MainPlugin.CoolQApi, Sender.Source.Group.QQId, Sender.QQ, false);
                    if (info.Model.Authority == "群主" || info.Model.Authority == "管理员" || Sender.QQ == 1813274096)
                    {
                        Sender.Send(Sender.AT + "千歌已关闭");
                        Sender.Source.Group.IsEnable = false;
                        return 1;
                    }
                    else
                    {
                        Sender.Send(Sender.AT + "请管理员来进行操作");
                        return 1;
                    }
                }

                // 群状态
                if (Sender.Source.Group != null)
                    if (rcc(Msg) == "群状态")
                        Status.Auto3(Sender.Source.Group);

                // 蜜柑商店
                if (MardarinStore.Auto(Sender, rcc(Msg)) != 0)
                    return 1;

                // 小游戏
                if (MiniGame.Auto(Sender, rcc(Msg)) != 0)
                    return 1;

                // 签到
                if (rcc(Msg) == "签到")
                    return new Checkin(Sender).Check();

                // 抛锦球
                if (ThrowBall.Auto(Sender, rcc(Msg)) == 1)
                    return 1;

                // 状态
                if (rcc(Msg) == "状态")
                    return Status.Auto(Sender);
                if (rcc(Msg) == "我的状态")
                    return Status.Auto2(Sender);

                // 档线
                if (rcc(Msg) == "档线")
                    return Yohane.StopLine.Auto(Sender);

                // 转账
                if (Transfer.Auto(Sender, rcc(Msg)) == 1)
                    return 1;

                // "Sudo"
                if (Sender.QQ == 1813274096)
                {
                    if (rcc(Msg).IndexOf("sudo") == 0)
                    {
                        string[] parm = Msg.Split('|');
                        if (parm.Length >= 3)
                        {
                            int num = 0;
                            int.TryParse(parm[1], out num);
                            Command cmd = new Command(parm[2], new User(num, Sender.Source), TimeStampHelper.ConvertDateTimeInt(DateTime.Now).ToString());
                            return cmd.Run();
                        }
                    }
                }

                // 抽卡
                if (ScoutSimulator.Msg.Auto(Sender, rcc(Msg)) != 0)
                    return 1;

                // 抽奖
                if (Lottery.Auto(Sender, rcc(Msg)) != 0)
                    return 1;

                // 鸽服号状态
                if (SIF.CN.Status.Auto(Sender, rcc(Msg)) != 0)
                    return 1;

                // 算运势
                if (rcc(Msg) == "算运势")
                    return Divination.Auto(Sender);

                // 点歌
                if (rcc(Msg).IndexOf("网易点歌") == 0)
                    return ChooseSong.Choose.Auto(Sender, rcc(Msg));

                // 算日美ID
                #region
                /*long getRF_id = 0;
                if (Msg.Length == 12)
                {
                    if (long.TryParse(Msg.Replace("算账号",""), out getRF_id) == true)
                    {
                        getRF_id = getRF_id * 526850996;
                        getRF_id = getRF_id % 999999937;
                        Sender.Send(Sender.AT + "Uid： " + Msg.Replace("算账号", "") + "\n" +
                            "Id: " + getRF_id);
                        return 1;
                    }
                    else
                    {
                        MessageSendHelper.Send(Sender, " × 计算失败！以下是狗开发的一点人生经验：\n - 不支持国服\n - 文本格式不对\n - 数值转换错误");
                        return 1;
                    }
                }
                else
                {
                    MessageSendHelper.Send(Sender, " × 计算失败！以下是狗开发的一点人生经验：\n - 不支持国服\n - 文本格式不对\n - 未知");
                    return 1;
                }*/

                #endregion

                // 聊天解析Beta
                if (Chat.Msg.Auto(Sender, Msg) != 0)
                    return 1;

            }
            catch (Exception ex)
            {
                Logger.Log
                    ("Ex：" + ex.Message, Logger.WARN, new long[] { }, 0);
                Sender.Send("ChikaBot Test System\n错误信息 " + ex.Message + "\n错误来源 " + ex.Source + "\n错误堆叠 " + ex.StackTrace);
                //ExceptionReportHelper.Send("消息处理", Sender, ex, Msg);
                //Sender.Send(" × 千歌酱出了一个Bug, 暂时不能陪你玩了...\n   错误类型:" + ex.GetType().ToString());
            }


            return 0;
        }
    }
}
