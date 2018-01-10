using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    public enum MenuType { Main, Scout, Music, Command, SIF, MiniGame, Admin, MandarinStore, About, Register}

    class Menu
    {
        public static string MenuHead = " --- ChikaBot " + MainPlugin.VersionStr + " --- \n";
        public static string GetMenuText(MenuType Type)
        {
            switch(Type)
            {
                case MenuType.Main:
                    return "请回复以下任意一项来查看具体内容：\n抽卡 点歌 常用命令 SIF查询 小游戏 积分商店 关于 开千歌 关千歌 转账";
                case MenuType.Scout:
                    return "格式：<模式><卡池>[服务器]\n模式可选项：十一连 单抽 机票\n卡池可选项：us aqours \n穗乃果 凛 妮可 花阳 绘里 希 真姬 海 鸟\n千歌 曜 梨 果南 黛雅 鞠莉 花丸 夜羽 露比\n例 十一连曜 单抽水 机票μ ";
                case MenuType.Music:
                    return "点歌<歌名> 使用QQ音乐点歌\n网易点歌<歌名> 使用网易云音乐点歌\n例：网易点歌 未来の僕らは知ってるよ\n点歌 僕たちはひとつの光";
                case MenuType.Command:
                    return "签到 状态 转账";
                case MenuType.SIF:
                    return "以下功能由子曦和鳖两位dalao提供，感谢他们！\n<国/日/美>服号状态\n<绑定/解绑><国/日/美>号\n国服档线";
                case MenuType.MiniGame:
                    return "抛锦球：随机禁言某人随机1-10分钟，可以指定禁言者（不能是群主/管理）\n" + 
                        "吃B酱： 吃一口B酱，计数游戏，可能会掉落少许蜜柑Point\n" + 
                        "啃咸鱼：啃一口@蓝腮咸鱼(某早期用户)，计数游戏，可能会掉落少许Loveca";
                case MenuType.About:
                    return "ChikaBot " + MainPlugin.Version + "\n由ChikaBot Team开发";
                default:
                    return "";
            }
        }

        public static int AutoSend(string Msg, User Sender)
        {
            string MenuStr = Msg.Replace("/", "").Replace("\\", "").Replace("#", "").Replace("[CQ:at,qq=" + MainPlugin.CoolQApi.GetLoginQQ() + "]", "").ToLower();
            if (MenuStr == "菜单" || MenuStr == "帮助" || MenuStr == "使用手册")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.Main));
            }
            else if (MenuStr == "点歌")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.Music));
            }
            else if (MenuStr == "常用命令")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.Command));
            }
            else if (MenuStr == "sif查询")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.SIF));
            }
            else if (MenuStr == "小游戏")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.MiniGame));
            }
            else if (MenuStr == "关于")
            {
                Sender.Send(MenuHead + GetMenuText(MenuType.About));
            }
            else
            {
                return 0;
            }
            return 1;
        }
    }
}
