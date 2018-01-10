using Newbe.CQP.Framework;
using Newbe.CQP.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class Logger
    {
        /// <summary>
        /// 日志等级 - 日志(输出大量日志使用)
        /// </summary>
        public const int INFO = 1;
        /// <summary>
        /// 日志等级 - 完成(某件耗时操作完成使用)
        /// </summary>
        public const int DONE = 2;
        /// <summary>
        /// 日志等级 - 调试(仅供调试使用，没事别乱用，会输出敏感信息)
        /// </summary>
        public const int DEBUG = 3;
        /// <summary>
        /// 日志等级 - 警告(程序得到的数值不满足预期值，但没有引发异常使用)
        /// </summary>
        public const int WARN = 4;
        /// <summary>
        /// 日志等级 - 错误(程序发生异常使用)
        /// </summary>
        public const int ERROR = 5;
        /// <summary>
        /// 日志等级 - 特(B)性(UG) (程序严重异常，会引起酷Q崩溃)
        /// </summary>
        public const int FEATURE = 6;

        /// <summary>
        /// 打印日志到酷Q日志
        /// </summary>
        /// <param name="log">日志信息</param>
        /// <param name="level">日志等级</param>
        public static void Log(string log,int level)
        {
            CoolQLogLevel cqll = CoolQLogLevel.None;

            switch(level)
            {
                case INFO:
                    cqll = CoolQLogLevel.Info;
                    break;
                case DONE:
                    cqll = CoolQLogLevel.InfoSuccess;
                    break;
                case DEBUG:
                    cqll = CoolQLogLevel.Debug;
                    break;
                case WARN:
                    cqll = CoolQLogLevel.Warning;
                    break;
                case ERROR:
                    cqll = CoolQLogLevel.Error;
                    break;
                case FEATURE:
                    cqll = CoolQLogLevel.Fatal;
                    break;
            }

            CoolApiExtensions.AddLog(
                MainPlugin.CoolQApi,
                cqll,
                log
            );
        }

        /// <summary>
        /// 打印日志到酷Q日志，并向指定QQ通知
        /// </summary>
        /// <param name="log">日志信息</param>
        /// <param name="level">日志等级</param>
        /// <param name="noticeQqs">被通知的QQ(或更多人)</param>
        /// <param name="needMinimumLevel">多严重才会被通知</param>
        public static void Log(string log,int level,long[] noticeQqs,int needMinimumLevel)
        {
            CoolQLogLevel cqll = CoolQLogLevel.None;
            string levelName = "";

            switch (level)
            {
                case INFO:
                    cqll = CoolQLogLevel.Info;
                    levelName = "Chika.Info(日志)";
                    break;
                case DONE:
                    cqll = CoolQLogLevel.InfoSuccess;
                    levelName = "Chika.Done(完成)";
                    break;
                case DEBUG:
                    cqll = CoolQLogLevel.Debug;
                    levelName = "Chika.Debug(调试)";
                    break;
                case WARN:
                    cqll = CoolQLogLevel.Warning;
                    levelName = "Chika.Warn(警告)";
                    break;
                case ERROR:
                    cqll = CoolQLogLevel.Error;
                    levelName = "Chika.Error(错误)";
                    break;
                case FEATURE:
                    cqll = CoolQLogLevel.Fatal;
                    levelName = "Chika.Feature(特性（呸）严重BUG)";
                    break;
            }

            CoolApiExtensions.AddLog(
                MainPlugin.CoolQApi,
                cqll,
                log
            );

            if(level >= needMinimumLevel)
            {
                for (int i = 0; i < noticeQqs.Length; i++)
                {
                    MainPlugin.CoolQApi.SendPrivateMsg(noticeQqs[i], "【千歌日志提醒】\n[等级] " + levelName + "\n" + log);
                }
            }
        }
    }
}
