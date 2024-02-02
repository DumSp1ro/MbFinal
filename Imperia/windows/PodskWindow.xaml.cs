using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Imperia.windows
{
    /// <summary>
    /// Логика взаимодействия для PodskWindow.xaml
    /// </summary>
    public partial class PodskWindow : Window
    {
        public PodskWindow()
        {
            InitializeComponent();
        }

        private void CloseWind_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
