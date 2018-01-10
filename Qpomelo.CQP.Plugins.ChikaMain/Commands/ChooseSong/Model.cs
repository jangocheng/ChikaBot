using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    [DataContract]
    class Model
    {
        [DataMember]
        public result result { get; set; }

        [DataMember]
        public int code { get; set; }
    }
}
