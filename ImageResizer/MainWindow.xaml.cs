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
            sizesDropdown.ItemsSource = new List<int> { 64, 128, 256, 512, 1024 };
            sizesDropdown.SelectedItem = 256;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DirectoryInfo di = new DirectoryInfo(InputFolderTB.Text);
            var files = di.GetFiles("*.jpg");

            int maxDim = (int)sizesDropdown.SelectedItem;
            
            int counter = 0;
            int errors = 0;
            StringBuilder messages = new StringBuilder();

            foreach (var filename in files)
            {
                counter++;
                try
                {
                    var input = System.Drawing.Image.FromFile(filename.FullName);

                    var output = Services.ImageTools.ResizeImage(input, maxDim, maxDim);
                    var outfile = System.IO.Path.Combine(OutputFolderTB.Text, "output-" + maxDim + "-" + counter + ".jpg");
                    using (var str = new FileStream(outfile, FileMode.Create))
                    {
                        output.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                catch (Exception ex)
                {
                    errors++;
                    messages.Append(Environment.NewLine + ex.Message);
                }
            }

            outputMessage.Text = String.Format("Converted {0} images with {1} errors. {2}", counter, errors, messages);
        }
    }
}
