using Imperia.classes;
using Imperia.models;
using Imperia.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private ObservableCollection<orders> ordersCollection;

        public OrdersPage()
        {
            InitializeComponent();
            RefreshOrders();
        }

        private void RefreshOrders()
        {
            ordersCollection = new ObservableCollection<orders>(imp.GetContext().orders.ToList());
            dataGrid.ItemsSource = ordersCollection;
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            orders selectedOrder = ((FrameworkElement)sender).DataContext as orders;
            EditOWindow editOWindow = new EditOWindow(selectedOrder);
            editOWindow.WindowClosed += EditOWindow_WindowClosed;
            editOWindow.ShowDialog();
        }

        private void EditOWindow_WindowClosed(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;

                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                dialog.PrintVisual(dataGrid, "Печать отчета");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Печать отчета", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshPage()
        {
            RefreshOrders();
        }

        private void Ref(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }
    }
}
