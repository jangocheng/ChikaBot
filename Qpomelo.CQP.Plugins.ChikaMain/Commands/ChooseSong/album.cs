using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    [DataContract]
    class album
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public artist artist { get; set; }

        [DataMember]
        public long publishTime { get; set; }

        [DataMember]
        public long size { get; set; }

        [DataMember]
        public long copyrightId { get; set; }

        [DataMember]
        public long status { get; set; }

        [DataMember]
        public long picId { get; set; }
    }
}
