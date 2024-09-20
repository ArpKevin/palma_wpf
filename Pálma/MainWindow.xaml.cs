using System.IO;
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

namespace Pálma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var desserts = new List<Dessert>();

            string palmaSrc = @"..\..\..\src\palma.txt";

            foreach (var item in File.ReadAllLines(palmaSrc))
            {
                var x = palmaSrc.Split(";");
                desserts.Add(new(x[0], x[1], bool.Parse(x[2]), int.Parse(x[3]), x[4]));
            }
        }
    }
}