using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qpomelo.CQP.Plugins.ChikaMain
{
    class SifIdHelper
    {
        /// <summary>
        /// Sif九位好友Id转为user id
        /// ※※不支持国服※※
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static long IdToUid(long id)
        {
            return ((long)id * 526850996) % 999999937;
        }
    }
}
