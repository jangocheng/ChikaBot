using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    class Choose
    {
        public static int Auto(User Sender, string Msg)
        {

            try
            {

                string songName = "";

                if (Msg.Length == 4)
                {
                    MessageSendHelper.Send(Sender, Sender.AT + "请输入歌名！");
                    return 1;
                }

                songName = Msg.Replace("网易点歌","");


                string postString = "s=" + songName + "&limit=1&type=1";
                byte[] postData = Encoding.UTF8.GetBytes(postString);
                string url = "http://music.163.com/api/search/get/";//地址  
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  

               Model model = null;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(srcString)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(Model));
                    model = (Model)deseralizer.ReadObject(ms);
                }

                Sender.Send("[CQ:music,id=" + model.result.songs[0].id.ToString() + ",type=163]");
                return 1;
            }
            catch (Exception)
            {
                Sender.Send(Sender.AT + "点歌失败！\n请确认名称是否正确，如果有重名歌曲请加上 -歌手名称 再试试！");
            }

            return 1;
        }
    }
}
