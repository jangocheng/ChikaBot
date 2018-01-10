using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands
{
    class ToDoModelHelper
    {
        public static ToDoModel GetModel(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(ToDoModel));
                return (ToDoModel)deseralizer.ReadObject(ms);
            }
        }
    }

    [DataContract]
    class ToDoModel
    {
        [DataMember]
        public int loveca { get; set; }
        [DataMember]
        public int mardarin_point { get; set; }
        [DataMember]
        public int coupon_ticket { get; set; }
        [DataMember]
        public int at_ban_card { get; set; }
        [DataMember]
        public int random_ban_card { get; set; }
        [DataMember]
        public int no_normal_scout_ticket { get; set; }
        [DataMember]
        public int scout_ticket { get; set; }
        [DataMember]
        public int eleven_scout_ticket { get; set; }
    }
}
