using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageResizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DirectoryInfo di = new DirectoryInfo(InputFolderTB.Text);
            var files = di.GetFiles("*.jpg");
                       
            int counter = 0;

            foreach (var filename in files)
            {
                var input = System.Drawing.Image.FromFile(filename.FullName);
                counter++;
                
                var output = Services.ImageTools.ResizeImage(input, 256, 256);
                var outfile = System.IO.Path.Combine(OutputFolderTB.Text, "output-" + counter + ".jpg");
                using (var str = new FileStream(outfile, FileMode.Create))
                {
                    output.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }
    }
}
