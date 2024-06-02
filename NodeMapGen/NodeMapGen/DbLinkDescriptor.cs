using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeMapGen
{
    internal class DbLinkDescriptor
    {
        public int SourceNodeId { get; set; }

        public int TargetNodeId { get; set; }

        public DbLinkDescriptor(int srcId, int tgtId)
        {
            this.SourceNodeId = srcId;
            this.TargetNodeId = tgtId;
        }
    }
}
