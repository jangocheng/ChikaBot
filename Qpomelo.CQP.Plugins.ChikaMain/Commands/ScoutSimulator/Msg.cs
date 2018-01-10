using System.Collections.Generic;
using System.Data;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ScoutSimulator
{
    public enum ScoutMode { Eleven, Once, Coupon }

    public enum UnitTypes
    {
        TakamiChika, WatanabeYou, SakurauchiRiko, MatsuuraKanan, KurosawaDiya, OharaMari, KurosawaRubii, TsushimaYoshiko, KunikidaHanamaru, 
        KousakaHonoka, MinamiKotori, SonodaUmi, ToujiyouNozomi, AyaseEri, YazawaNiko, HoshizoraRin, NishikinoMaki, KoizumiHanayo,
        Ms, Aqours,
        CYaRon, AZALEA, GuityKiss,
        BiBi, Printemps, LilyWhite,
        Strange
    }

    class Msg
    {
        public static int Auto(User Sender,string Msg)
        {
            /*
             * 命令格式 <模式><卡池>
             * 
             * <模式>: 十一连、单抽、机票
             * <卡池>: 千歌梨子曜······CYaRonAZALEA······Aqoursμ's······
             * 
             */

            ScoutMode scoutMode;
            if (Msg.IndexOf("十一连") != 0)
                if (Msg.IndexOf("单抽") != 0)
                    if (Msg.IndexOf("机票") != 0)
                        return 0;
                    else
                        scoutMode = ScoutMode.Coupon;
                else
                    scoutMode = ScoutMode.Once;
            else
                scoutMode = ScoutMode.Eleven;

            List<UnitTypes> cardUnitTypes = new List<UnitTypes>();

            #region 判断UnitTypeId
            if (Msg.IndexOf("chika") > 0)
                cardUnitTypes.Add(UnitTypes.TakamiChika);
            if (Msg.IndexOf("you") > 0)
                cardUnitTypes.Add(UnitTypes.WatanabeYou);
            if (Msg.IndexOf("riko") > 0)
                cardUnitTypes.Add(UnitTypes.SakurauchiRiko);
            if (Msg.IndexOf("kanan") > 0)
                cardUnitTypes.Add(UnitTypes.MatsuuraKanan);
            if (Msg.IndexOf("dia") > 0)
                cardUnitTypes.Add(UnitTypes.KurosawaDiya);
            if (Msg.IndexOf("mari") > 0)
                cardUnitTypes.Add(UnitTypes.OharaMari);
            if (Msg.IndexOf("hanamaru") > 0)
                cardUnitTypes.Add(UnitTypes.KunikidaHanamaru);
            if (Msg.IndexOf("ruby") > 0)
                cardUnitTypes.Add(UnitTypes.KurosawaRubii);
            if (Msg.IndexOf("yoshiko") > 0)
                cardUnitTypes.Add(UnitTypes.TsushimaYoshiko);
            if (Msg.IndexOf("honoka") > 0)
                cardUnitTypes.Add(UnitTypes.KousakaHonoka);
            if (Msg.IndexOf("kotori") > 0)
                cardUnitTypes.Add(UnitTypes.MinamiKotori);
            if (Msg.IndexOf("umi") > 0)
                cardUnitTypes.Add(UnitTypes.SonodaUmi);
            if (Msg.IndexOf("niko") > 0)
                cardUnitTypes.Add(UnitTypes.YazawaNiko);
            if (Msg.IndexOf("eri") > 0)
                cardUnitTypes.Add(UnitTypes.AyaseEri);
            if (Msg.IndexOf("nozomi") > 0)
                cardUnitTypes.Add(UnitTypes.ToujiyouNozomi);
            if (Msg.IndexOf("maki") > 0)
                cardUnitTypes.Add(UnitTypes.NishikinoMaki);
            if (Msg.IndexOf("hanayo") > 0)
                cardUnitTypes.Add(UnitTypes.KoizumiHanayo);
            if (Msg.IndexOf("rin") > 0)
                cardUnitTypes.Add(UnitTypes.HoshizoraRin);
            if (Msg.IndexOf("ms") > 0)
                cardUnitTypes.Add(UnitTypes.Ms);
            if (Msg.IndexOf("aqours") > 0)
                cardUnitTypes.Add(UnitTypes.Aqours);
            if (Msg.IndexOf("bibi") > 0)
                cardUnitTypes.Add(UnitTypes.BiBi);
            if (Msg.IndexOf("lilywhite") > 0)
                cardUnitTypes.Add(UnitTypes.LilyWhite);
            if (Msg.IndexOf("printemps") > 0)
                cardUnitTypes.Add(UnitTypes.Printemps);
            if (Msg.IndexOf("cyaron") > 0)
                cardUnitTypes.Add(UnitTypes.CYaRon);
            if (Msg.IndexOf("azalea") > 0)
                cardUnitTypes.Add(UnitTypes.AZALEA);
            if (Msg.IndexOf("guitykiss") > 0)
                cardUnitTypes.Add(UnitTypes.GuityKiss);
            if (Msg.IndexOf("strange") > 0)
                cardUnitTypes = new List<UnitTypes>() { UnitTypes.Strange };
            #endregion

            string dbPath = "";
            foreach(UnitTypes tp in cardUnitTypes)
            {
                if(tp == UnitTypes.Strange)
                {
                    dbPath = MainPlugin.MainDatabase;
                    break;
                }
                else
                {
                    dbPath = MainPlugin.SifPathJp + "db/unit/unit.db_";
                }
            }

            return LaunchScout(cardUnitTypes, scoutMode, Sender, new DatabaseHelper(dbPath).Execute("select * from unit_m"));
        }

        static int LaunchScout(List<UnitTypes> cardType, ScoutMode mode, User Sender, DataTable table)
        {
            
            switch(mode)
            {
                case ScoutMode.Coupon:
                    return Scout.MakePictureOnceCoupon(Sender, cardType, table, false , true);
                case ScoutMode.Eleven:
                    return Scout.MakePictureEleven(Sender, cardType, table);
                case ScoutMode.Once:
                    return Scout.MakePictureOnceCoupon(Sender, cardType, table, false, false);
            }
            return 0;
        }
    }
}
