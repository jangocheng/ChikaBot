using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Checkin
    {
        public User User { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="User">用户对象</param>
        public Checkin(User User)
        {
            this.User = User;
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <returns>酷Q消息码</returns>
        public int Check()
        {
            User Sender = User;

            if (Sender.IsCheckin == true)
            {
                MessageSendHelper.Send(Sender, Sender.AT + "你已经签到过了哦！");
                return 1;
            }

            int randomLoveca = 0, randomMandarinPoints = 0;
            string rewardCheckin = "";

            randomLoveca = new Random().Next(1, 6);
            randomMandarinPoints = new Random().Next(50, 200);
            rewardCheckin = "[CQ:image,file=UI/Items/Loveca.png] +" + randomLoveca + "\n[CQ:image,file=UI/Items/MardarinPoint.png] +" + randomMandarinPoints;
            if (DateTime.Now.Hour == 7 || DateTime.Now.Hour == 19)
            {
                randomLoveca = randomLoveca * 5;
                randomMandarinPoints = randomMandarinPoints * 5;
                rewardCheckin = rewardCheckin + "\n当前时间奖励翻倍！";
            }
            Sender.Loveca = Sender.Loveca + randomLoveca;
            Sender.MandarinPoint = Sender.MandarinPoint + randomMandarinPoints;

            Sender.Checkin();

            Sender.Send(Sender.AT + "\n" + rewardCheckin);

            return 1;
        }
    }
}

