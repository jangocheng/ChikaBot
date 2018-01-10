using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    [DataContract]
    class result
    {
        [DataMember]
        public songs[] songs { get; set; }

        [DataMember]
        public long songCount { get; set; }
    }
}
