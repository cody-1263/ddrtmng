using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeMapGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var dbNodes = new DbNodeDescriptor[]
            {
                new DbNodeDescriptor(01, "Input 1"),
                new DbNodeDescriptor(02, "Input 2"),
                new DbNodeDescriptor(03, ""),
                new DbNodeDescriptor(04, ""),
                new DbNodeDescriptor(05, "SUM"),
                new DbNodeDescriptor(06, "MULTI"),

                new DbNodeDescriptor(10, ""),
                new DbNodeDescriptor(11, ""),
                new DbNodeDescriptor(12, "Output"),
            };
            var dbLinks = new DbLinkDescriptor[]
            {
                new DbLinkDescriptor(01, 03),
                new DbLinkDescriptor(02, 04),

                new DbLinkDescriptor(03, 05),
                new DbLinkDescriptor(04, 05),

                new DbLinkDescriptor(05, 06),

                new DbLinkDescriptor(06, 10),
                new DbLinkDescriptor(06, 11),
                new DbLinkDescriptor(10, 12),
                new DbLinkDescriptor(11, 12),
            };

            var mapBuilder = new MapBuilder();
            var map = mapBuilder.AddMap(dbNodes, dbLinks);

            var mapVisualizer = new MapVisualizer();
            mapVisualizer.AddVisuals(map, _layer1Grid, _layer2Grid);
        }
    }
}