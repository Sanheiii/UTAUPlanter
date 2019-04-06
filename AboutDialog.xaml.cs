using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Planter
{
    /// <summary>
    /// AboutDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
            version.Content ="Ver "+ Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            System.Diagnostics.Process.Start(link.NavigateUri.ToString());  
        }
    }
}
