using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class Divination
    {
        public static int Auto(User Sender)
        {
            Random rd = new Random();
            int i = rd.Next(1, 1000);

            if (i > 999)
            {
                Sender.Send( Sender.AT + "\n[结果] 欧欧欧欧欧欧欧欧皇\n[几率] 1‰\n[点评] dalao您是官托吧？");
            }
            else if (i > 900)
            {
                Sender.Send( Sender.AT + "\n[结果] 欧皇\n[几率]10‰\n[点评] 震惊抽卡界，UR信手拈来，连盛大和不许摸都惧怕，你究竟是何等人才？");
            }
            else if (i > 800)
            {
                Sender.Send( Sender.AT + "\n[结果] 欧洲人\n[几率] 200‰\n[点评] 虽然UR数量较少，但在sif4.0后随手一个SSR的你，也是这里的强者。");
            }
            else if (i > 600)
            {
                Sender.Send( Sender.AT + "\n[结果] 亚洲人\n[几率] 400‰\n[点评] 相貌平平的亚洲死肥宅，总是偶尔有一次运气爆棚拿到好卡呢~");
            }
            else if (i > 200)
            {
                Sender.Send( Sender.AT + "\n[结果] 非洲人\n[几率] 800‰\n[点评] 即使是非洲人，也总有偷渡的那一天！");
            }
            else if (i > 100)
            {
                Sender.Send( Sender.AT + "\n[结果] 非洲穷苦老百姓\n[几率] 900‰\n[点评] 杀不尽的欧洲狗，流不尽的非洲泪！");
            }
            else
            {
                Sender.Send( Sender.AT + "\n[结果] 酋长\n[几率] 1000‰\n[点评] 酋长您这辈子都不可能的了，安心养老吧。");
            }


            return 1;
        }
    }
}
