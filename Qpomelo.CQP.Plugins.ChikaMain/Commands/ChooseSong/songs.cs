using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain.Commands.ChooseSong
{
    [DataContract]
    class songs
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public artists artists { get; set; }

        [DataMember]
        public album album { get; set; }

        [DataMember]
        public long duration { get; set; }

        [DataMember]
        public long copyrightId  { get; set; }

        [DataMember]
        public long status { get; set; }

        [DataMember]
        public object[] alias { get; set; }

        [DataMember]
        public long rtype { get; set; }

        [DataMember]
        public long ftype { get; set; }

        [DataMember]
        public long mvid { get; set; }

        [DataMember]
        public long fee { get; set; }

        [DataMember]
        public object rUrl { get; set; }
    }
}
