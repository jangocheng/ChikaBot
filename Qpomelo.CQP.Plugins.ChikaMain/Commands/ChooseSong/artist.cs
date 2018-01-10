using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    [DataContract]
    class artist
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string picUrl { get; set; }

        [DataMember]
        public object[] alias { get; set; }

        [DataMember]
        public long albumSize { get; set; }

        [DataMember]
        public long picId { get; set; }

        [DataMember]
        public string img1v1Url { get; set; }

        [DataMember]
        public long img1v1 { get; set; }

        [DataMember]
        public object trans { get; set; }
    }
}
