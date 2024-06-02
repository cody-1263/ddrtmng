using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NodeMapGen
{
    internal class MapVisualizer
    {
        Dictionary<NodeDeclaration, (int x, int y)> _nodeVisualCenters = new();

        public void AddVisuals(ElementMap map, Grid layer1Grid, Grid layer2Grid)
        {
            foreach(var node in map._locations.Keys)
            {
                var mapLocation = map.GetNodeLocation(node);
                AddNode(node, mapLocation.x, mapLocation.y, layer2Grid);
            }

            foreach(var link in map._links)
            {
                AddLink(link, layer1Grid);
            }
        }

        int _multi = 64;

        

        void AddNode(NodeDeclaration nodeDeclaration, int mapX, int mapY, Grid grid)
        {
            var border = new Border();
            border.Width = 48;
            border.Height = 48;
            border.Background = Brushes.DarkSlateBlue;
            border.CornerRadius = new System.Windows.CornerRadius(8);
            border.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            var visualLocation = (x: mapX * _multi, y: mapY * _multi);
            var visualCenter = (x: (int)(visualLocation.x + border.Width / 2), y: (int)(visualLocation.y + border.Height / 2));

            border.Margin = new System.Windows.Thickness(visualLocation.x, visualLocation.y, 0, 0);

            var textBlock = new TextBlock();
            textBlock.Text = $"ND {nodeDeclaration.NodeId} \r\n{nodeDeclaration.Caption}";
            textBlock.Foreground = Brushes.WhiteSmoke;
            textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            border.Child = textBlock;

            _nodeVisualCenters[nodeDeclaration] = visualCenter;

            grid.Children.Add(border);
        }

        void AddLink(LinkDeclaration link, Grid grid)
        {
            var c1 = _nodeVisualCenters[link.Node1];
            var c2 = _nodeVisualCenters[link.Node2];

            var line = new Line();
            line.X1 = c1.x;
            line.Y1 = c1.y;
            line.X2 = c2.x;
            line.Y2 = c2.y;
            line.StrokeThickness = 4;
            line.Stroke = Brushes.CadetBlue;
            grid.Children.Add(line);
        }
    }
}
