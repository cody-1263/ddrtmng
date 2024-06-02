using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NodeMapGen
{
    internal class ElementMap
    {

        public NodeDeclaration[,] _map;

        public Dictionary<NodeDeclaration, (int x, int y)> _locations;

        public List<LinkDeclaration> _links = new();

        public ElementMap(int width, int height)
        {
            _map = new NodeDeclaration[height,width];

            _locations = new Dictionary<NodeDeclaration, (int x, int y)>();
        }


        public void AddNode(NodeDeclaration node, int x, int y)
        {
            if (_locations.ContainsKey(node))
                throw new InvalidOperationException();

            _locations[node] = (x, y);
            _map[y,x] = node;
        }

        public (int x, int y) GetNodeLocation(NodeDeclaration node)
        {
            if (_locations.ContainsKey(node))
                return _locations[node];
            else
                return (-1, -1);
        }

        public bool HasNode(NodeDeclaration node)
        {
            return _locations.ContainsKey(node);
        }

        public NodeDeclaration? GetElement(int x, int y)
        {
            return _map[y,x];
        }

        public void AddLinkDeclaration(LinkDeclaration linkDeclaration)
        {
            _links.Add(linkDeclaration);
        }
    }
}
