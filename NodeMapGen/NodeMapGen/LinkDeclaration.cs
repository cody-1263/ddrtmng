using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeMapGen
{
    internal class LinkDeclaration
    {
        public NodeDeclaration Node1 { get; set; }

        public NodeDeclaration Node2 { get; set; }

        public LinkDeclaration(NodeDeclaration node1, NodeDeclaration node2)
        {
            Node1 = node1;
            Node2 = node2;
        }   
    }
}
