using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NodeMapGen
{
    internal class MapBuilder
    {
        ElementMap _elementMap = new(10,7);

        Dictionary<int, NodeDeclaration> _nodeDeclarations = new();

        Dictionary<DbNodeDescriptor, (IReadOnlyList<DbNodeDescriptor> incomingNodes, IReadOnlyList<DbNodeDescriptor> outcomingNodes)> _allIncomingOutcomingNodes = new();

        public ElementMap AddMap(IReadOnlyList<DbNodeDescriptor> dbNodes, IReadOnlyList<DbLinkDescriptor> dbLinks)
        {
            foreach(var node in dbNodes)
            {
                (var incomingNodes, var outcomingNodes) = FindIncomingOutcomingNodes(node, dbNodes, dbLinks);
                _allIncomingOutcomingNodes[node] = (incomingNodes, outcomingNodes);
            }

            foreach(var node in dbNodes)
            {
                (var incomingNodes, var outcomingNodes) = _allIncomingOutcomingNodes[node];
                if (incomingNodes.Count > 0 && outcomingNodes.Count == 0)
                    PlaceNodeRecursive(node, 9, 4);
            }

            foreach(var link in dbLinks)
                AddLinkDeclaration(link);

            return _elementMap;
        }

        (int x, int y) FindMapPlaceForNode(NodeDeclaration node, ElementMap map, int desiredX, int desiredY)
        {
            bool found = false;
            int iterationCounter = 0;
            int x = desiredX;
            int y = desiredY;
            while (!found)
            {
                var existingElement = _elementMap.GetElement(x, y);
                if (existingElement is null)
                {
                    return (x, y);
                }
                else
                {
                    y--;
                }

                if (iterationCounter++ == 5)
                    throw new Exception("Не удаётся найти место для ноды");
            }

            return (-1, -1);
        }

        (IReadOnlyList<DbNodeDescriptor> incomingNodes, IReadOnlyList<DbNodeDescriptor> outcomingNodes) FindIncomingOutcomingNodes(DbNodeDescriptor node, IReadOnlyList<DbNodeDescriptor> nodes, IReadOnlyList<DbLinkDescriptor> dbLinks)
        {
            List<DbNodeDescriptor> incomingNodes = new();
            List<DbNodeDescriptor> outcomingNodes = new();
            foreach (var dblink in dbLinks)
            {
                if (dblink.TargetNodeId == node.Id)
                {
                    var srcNode = nodes.Single(o => o.Id == dblink.SourceNodeId);
                    incomingNodes.Add(srcNode);
                }
                if (dblink.SourceNodeId == node.Id)
                {
                    var tgtNode = nodes.Single(o => o.Id == dblink.TargetNodeId);
                    outcomingNodes.Add(tgtNode);
                }
            }
            return (incomingNodes, outcomingNodes);
        }

        void PlaceNodeRecursive(DbNodeDescriptor dbNode, int prevX, int prevY)
        {
            var mapLocation = (x: -1, y: -1);

            if (_nodeDeclarations.ContainsKey(dbNode.Id))
            {
                var existingDeclaration = _nodeDeclarations[dbNode.Id];
                mapLocation = _elementMap.GetNodeLocation(existingDeclaration);
            }
            else
            {
                var newDeclaration = new NodeDeclaration() { NodeId = dbNode.Id, Caption = dbNode.Name };
                _nodeDeclarations[dbNode.Id] = newDeclaration;
                var desiredX = prevX - 1;
                var desiredY = prevY;
                mapLocation = FindMapPlaceForNode(newDeclaration, _elementMap, desiredX, desiredY);
                _elementMap.AddNode(newDeclaration, mapLocation.x, mapLocation.y);
            }

            var incomingNodes = _allIncomingOutcomingNodes[dbNode].incomingNodes;
            foreach(var incomingNode in incomingNodes)
                PlaceNodeRecursive(incomingNode, mapLocation.x, mapLocation.y);
        }

        void AddLinkDeclaration(DbLinkDescriptor dbLink)
        {
            var node1 = _nodeDeclarations[dbLink.SourceNodeId];
            var node2 = _nodeDeclarations[dbLink.TargetNodeId];
            var linkDeclaration = new LinkDeclaration(node1, node2);
            _elementMap.AddLinkDeclaration(linkDeclaration);
        }
    }
}
