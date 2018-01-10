using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ScoutSimulator
{
    class Scout
    {
        public static int MakePictureEleven(User Sender, List<UnitTypes> unitTypeIds, DataTable unitMTable, bool dontUseTicket = false)
        {
            foreach(UnitTypes tp in unitTypeIds)
            {
                if(tp == UnitTypes.WatanabeYou)
                {
                    Sender.Send("曜酱去拿粉色的R信封了，千歌酱陪你等下吧~");
                    break;
                }
            }

            List<int> caseUrList = new List<int>();
            List<int> caseSSrList = new List<int>();
            List<int> caseSrList = new List<int>();
            List<int> caseRList = new List<int>();

            int i3 = 0;
            foreach(DataRow row in unitMTable.Rows)
            {
                i3++;
                foreach(UnitTypes tp in unitTypeIds)
                {
                    List<int> cse = CsEnumToDbUnitTypeId(tp);
                    foreach(int i in cse)
                    {
                        string cardUnitTypeIdStr = row["unit_type_id"].ToString();
                        int cardUnitTypeId = 0;
                        int.TryParse(cardUnitTypeIdStr, out cardUnitTypeId);
                        if(cardUnitTypeId == i)
                        {
                            string unitIdStr = row["unit_id"].ToString();
                            int unitId = 0;
                            int.TryParse(unitIdStr, out unitId);
                            string rarityStr = row["rarity"].ToString();
                            int rarity = 0;
                            int.TryParse(rarityStr, out rarity);
                            switch (rarity)
                            {
                                case 3:
                                    caseSrList.Add(unitId);
                                    break;
                                case 4:
                                    caseUrList.Add(unitId);
                                    break;
                                case 5:
                                    caseSSrList.Add(unitId);
                                    break;
                                case 2:
                                    caseRList.Add(unitId);
                                    break;
                                default:
                                    throw new Exception("手动引发的异常");
                            }
                        }

                    }
                }
                
                
            }

            string str = "";
            str = str + "UR:\n";
            foreach(int i in caseUrList)
            {
                str = str + i + " ";
            }
            str = str + "\nSSR:\n";
            foreach (int i in caseSSrList)
            {
                str = str + i + " ";
            }
            str = str + "\nSR:\n";
            foreach (int i in caseSrList)
            {
                str = str + i + " ";
            }
            str = str + "\nR:\n";
            foreach (int i in caseRList)
            {
                str = str + i + " ";
            }
            Sender.Send(i3 + "\n" + str);

            List<int> scoutCards = new List<int>();
            Random rd = new Random();
            for (int i = 0; i < 11; i++)
            {
                int rdNum = rd.Next(1, 101);
                List<int> selectRarity = null;
                if (rdNum == 1)
                {
                    selectRarity = caseUrList;
                }
                else if(rdNum > 1 && rdNum <= 5)
                {
                    selectRarity = caseSSrList;
                }
                else if(rdNum > 5 && rdNum <= 20)
                {
                    selectRarity = caseSrList;
                }
                else
                {
                    selectRarity = caseRList;
                }
                // Sender.Send(rdNum.ToString());
                int selectR = selectRarity[new Random().Next(0, selectRarity.Count)];
                //Sender.Send(selectR.ToString());
                scoutCards.Add(selectR);
                GC.Collect();
            }

            string usePropIconPath = "";
            long useProp = 0, hasProp = 0;
            if(Sender.ElevenScoutTicket > 0)
            {
                Sender.ElevenScoutTicket--;
                usePropIconPath = MainPlugin.ImagePath + "UI/Items/十一连招募券.png";
                useProp = 1;
                hasProp = Sender.ElevenScoutTicket;
            }
            else
            {
                if(Sender.Loveca < 50)
                {
                    Sender.Send("Loveca不够哦~攒更多的Loveca或者去购买十一连招募券吧~");
                    return 1;
                }
                Sender.Loveca = Sender.Loveca - 50;
                usePropIconPath = MainPlugin.ImagePath + "UI/Items/Loveca.png";
                useProp = 50;
                hasProp = Sender.Loveca;
            }
            Sender.CouponTicket++;

            #region 绘画背景和所持道具等
            Bitmap bmp = new Bitmap(960, 640);
            Graphics gp = Graphics.FromImage(bmp);
            // 背景
            gp.DrawImage(Image.FromFile(MainPlugin.RunPath + "UI/Backgrounds/b_st_166.png"), 0, 0, 960, 640);
            // UI？就是SIF那些下方Tabs，按钮什么的图片
            gp.DrawImage(Image.FromFile(MainPlugin.RunPath + "UI/ElevenScout.png"), 0, 0, 960, 640);
            // 所使用的道具icon
            gp.DrawImage(Image.FromFile(usePropIconPath), 204, 468, 37, 37);
            gp.DrawImage(Image.FromFile(usePropIconPath), 204, 506, 37, 37);
            // 所剩余的道具string
            gp.DrawString(useProp.ToString(), new Font("方正粗圆_GBK", 16), new SolidBrush(Color.Black), new Point(240, 473));
            gp.DrawString(hasProp.ToString(), new Font("方正粗圆_GBK", 16), new SolidBrush(Color.Black), new Point(240, 513));
            #endregion
            #region 绘画卡面
            // 可直接使用的坐标
            int[] PointLeft = { 22, 175, 328, 481, 634, 787, 106, 259, 412, 565, 718 };
            int[] PointTop = { 100, 100, 100, 100, 100, 100, 256, 256, 256, 256, 256 };
            // 画进去
            int ia = 0; // 计数用
            foreach (int id in scoutCards)
            {
                gp.DrawImage(GetMiniHeadWithAllBorder(id,unitMTable), PointLeft[ia] + 9, PointTop[ia], 128, 128);
                ia++;
            }
            #endregion

            bmp.Save(MainPlugin.ImagePath + "cache\\" + Sender.QQ + "_scout.png");
            Sender.Send(Sender.AT + "\n[CQ:image,file=cache/" + Sender.QQ + "_scout.png]\n辅助招募券+1");
            GC.Collect();
            return 1;
        }

        public static int MakePictureOnceCoupon(User Sender, List<UnitTypes> unitTypeIds, DataTable unitMTable, bool dontUseTicket = false, bool couponticket = false)
        {
            List<int> caseCards = new List<int>();

            foreach(UnitTypes tp in unitTypeIds)
            {
                foreach (DataRow row in unitMTable.Rows)
                {
                    if(row["rarity"].ToString() == "1")
                    {
                        continue;
                    }
                    foreach(int i in CsEnumToDbUnitTypeId(tp))
                    {
                        if (row["unit_type_id"].ToString() == i.ToString())
                        {
                            string obj = row["unit_id"].ToString();
                            int num = 0;
                            int.TryParse(obj, out num);
                            caseCards.Add(num);
                        }
                    }
                }
            }


            int selectUnitId = new Random().Next(0, caseCards.Count);
            string o = unitMTable.Rows[selectUnitId]["unit_number"].ToString();
            int unit_number = 0;
            int.TryParse(o, out unit_number);


            string usePropName = "";
            long useProp = 0, hasProp = 0;
            if(couponticket == true)
            {
                if(Sender.CouponTicket < 5)
                {
                    Sender.Send(Sender.AT + "你的辅助招募券不足!");
                    return 1;
                }
                Sender.CouponTicket = Sender.CouponTicket - 5;
                usePropName = "[CQ:image,file=UI/Items/辅助招募券.png]";
                useProp = 5;
                hasProp = Sender.CouponTicket;
            }
            else
            {
                if (Sender.ScoutTicket > 0)
                {
                    Sender.ScoutTicket--;
                    usePropName = "[CQ:image,file=UI/Items/优等生招募券.png]";
                    useProp = 1;
                    hasProp = Sender.ScoutTicket;
                }
                else
                {
                    if (Sender.Loveca < 5)
                    {
                        Sender.Send(Sender.AT + "你的Loveca不足！");
                        return 1;
                    }
                    Sender.Loveca = Sender.Loveca - 5;
                    usePropName = "[CQ:image,file=UI/Items/Loveca.png]";
                    useProp = 5;
                    hasProp = Sender.Loveca;
                }
            }
            if(!couponticket)
            {
                Sender.HonorStudentBonus++;
            }

            while(Sender.HonorStudentBonus >= 10)
            {
                Sender.HonorStudentBonus = Sender.HonorStudentBonus - 10;
                Sender.CouponTicket++;
                Sender.Send(Sender.AT + "优等生奖励已满，辅助招募券+1！");
            }

            Sender.Send(
                Sender.AT + "\n" +
                "[CQ:image,file=UI/CardHead/icon_" + unit_number + ".png]\n" +
                unitMTable.Rows[selectUnitId]["name"].ToString() + " " + unitMTable.Rows[selectUnitId]["eponym"].ToString() + "\n" +
                "消耗" + usePropName + " × " + useProp + "\n" +
                "剩余" + usePropName + " × " + hasProp);
            return 1;
        }
        /// <summary>
        /// CSharp中的UnitTypes enum转为数据库中的Unit Type Id
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<int> CsEnumToDbUnitTypeId(UnitTypes type)
        {
            switch(type)
            {
                case UnitTypes.KousakaHonoka:
                    return new List<int>() { 1 };
                case UnitTypes.AyaseEri:
                    return new List<int>() { 2 };
                case UnitTypes.MinamiKotori:
                    return new List<int>() { 3 };
                case UnitTypes.SonodaUmi:
                    return new List<int>() { 4 };
                case UnitTypes.HoshizoraRin:
                    return new List<int>() { 5 };
                case UnitTypes.NishikinoMaki:
                    return new List<int>() { 6 };
                case UnitTypes.ToujiyouNozomi:
                    return new List<int>() { 7 };
                case UnitTypes.KoizumiHanayo:
                    return new List<int>() { 8 };
                case UnitTypes.YazawaNiko:
                    return new List<int>() { 9 };
                case UnitTypes.TakamiChika:
                    return new List<int>() { 101 };
                case UnitTypes.SakurauchiRiko:
                    return new List<int>() { 102 };
                case UnitTypes.MatsuuraKanan:
                    return new List<int>() { 103 };
                case UnitTypes.KurosawaDiya:
                    return new List<int>() { 104 };
                case UnitTypes.WatanabeYou:
                    return new List<int>() { 105 };
                case UnitTypes.TsushimaYoshiko:
                    return new List<int>() { 106 };
                case UnitTypes.KunikidaHanamaru:
                    return new List<int>() { 107 };
                case UnitTypes.OharaMari:
                    return new List<int>() { 108 };
                case UnitTypes.KurosawaRubii:
                    return new List<int>() { 109 };
                case UnitTypes.Ms:
                    return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                case UnitTypes.Aqours:
                    return new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109 };
                case UnitTypes.CYaRon:
                    return new List<int>() { 101, 105, 109 };
                case UnitTypes.AZALEA:
                    return new List<int>() { 103, 104, 107 };
                case UnitTypes.GuityKiss:
                    return new List<int>() { 102, 106, 108 };
                case UnitTypes.BiBi:
                    return new List<int>() { 2, 6, 9 };
                case UnitTypes.Printemps:
                    return new List<int>() { 1, 3, 8 };
                case UnitTypes.LilyWhite:
                    return new List<int>() { 4, 5, 7 };
            }
            return new List<int>();
        }
        /// <summary>
        /// 获取某张卡的小头像
        /// </summary>
        /// <param name="UnitNumber">卡片UnitNumber</param>
        /// <param name="UnitDB">数据库</param>
        /// <returns></returns>
        static Bitmap GetMiniHeadWithAllBorder(int UnitNumber, DataTable table)
        {
            Bitmap bmp = new Bitmap(128, 128);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            Image cardH = Image.FromFile(MainPlugin.RunPath + "UI\\CardHead\\icon_" + UnitNumber + ".png");
            Image cardBg = Image.FromFile(MainPlugin.RunPath + "UI\\CardImage\\Icon\\" + GetCardAttribute(UnitNumber, table) + "_bg.png");
            Image cardC = Image.FromFile(MainPlugin.RunPath + "UI\\CardImage\\Icon\\" + GetCardRarity(UnitNumber, table) + "_" + GetCardAttribute(UnitNumber, table) + "_c.png");
            g.DrawImage(cardBg, 0, 0);
            g.DrawImage(cardH, 0, 0);
            g.DrawImage(cardC, 0, 0);
            GC.Collect();
            return bmp;
        }
        /// <summary>
        /// 获取某张卡的颜色
        /// </summary>
        /// <param name="UnitNumber">卡片UnitNumber</param>
        /// <param name="UnitDB">使用的数据库</param>
        /// <returns></returns>
        static int GetCardAttribute(int UnitNumber, DataTable table)
        {
            string numS = "";
            int num = 0;
            foreach(DataRow row in table.Rows)
            {
                if(row["unit_number"].ToString() == UnitNumber.ToString())
                {
                    numS = row["attribute_id"].ToString();
                }
            }
            int.TryParse(numS, out num);
            return num;
        }
        /// <summary>
        /// 获取某张卡的稀有度
        /// </summary>
        /// <param name="UnitNumber">卡片UnitNumber</param>
        /// <param name="UnitDB">数据库</param>
        /// <returns></returns>
        static int GetCardRarity(int UnitNumber, DataTable table)
        {
            string numS = "";
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                if (row["unit_number"].ToString() == UnitNumber.ToString())
                {
                    numS = row["rarity"].ToString();
                }
            }
            int.TryParse(numS, out num);
            return num;
        }
    }
}
