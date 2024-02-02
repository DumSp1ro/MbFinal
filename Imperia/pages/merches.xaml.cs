using Imperia.models;
using Imperia.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для merches.xaml
    /// </summary>
    public partial class merches : Page
    {
        private ObservableCollection<OrderItem> orderItems = new ObservableCollection<OrderItem>();
        private ObservableCollection<merch> merchCollection;
        private int _currentPage = 1;
        private int _countInPage = 2;
        public merches(users user)
        {
            InitializeComponent();
            DataContext = user;
            merchCollection = new ObservableCollection<merch>(imp.GetContext().merch.ToList());
            UpdateMerchCollection();
        }

        private void MerchBD_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MerchBD.SelectedItem == null)
            {
                e.Handled = true;
            }
            else
            {
                merch selectedMerch = MerchBD.SelectedItem as merch;
                MessageBox.Show($"Selected Merch:\nName: {selectedMerch.name}\nManufacturer: {selectedMerch.manufacturer}\nPrice: {selectedMerch.price}", "Merchandise Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateOrderViewButtonVisibility()
        {
            if (orderItems.Any())
            {
                ShowOrderButton.Visibility = Visibility.Visible;
            }
            else
            {
                ShowOrderButton.Visibility = Visibility.Collapsed;
            }
        }
        private void ShowOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OneOrder orderViewWindow = new OneOrder(orderItems);
            orderViewWindow.ShowDialog();
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MerchBD.SelectedItem != null)
            {
                merch selectedMerch = MerchBD.SelectedItem as merch;

                OrderItem orderItem = new OrderItem
                {
                    MerchId = selectedMerch.id,
                    MerchName = selectedMerch.name,
                    MerchPrice = selectedMerch.price,
                    Quantity = 1,
                    PointCollection = new ObservableCollection<point>()
                };

                if (selectedMerch.discount.HasValue)
                {
                    orderItem.Discount = selectedMerch.discount.Value;
                }
                else
                {
                    MessageBox.Show("У товара нет скидки");
                }

                orderItems.Add(orderItem);

                UpdateOrderViewButtonVisibility();
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateMerchCollection();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            int totalItems = merchCollection.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / _countInPage);

            if (_currentPage < totalPages)
            {
                _currentPage++;
                UpdateMerchCollection();
            }
        }

        private void UpdateMerchCollection()
        {
            int startIndex = (_currentPage - 1) * _countInPage;
            var itemsForPage = merchCollection.Skip(startIndex).Take(_countInPage).ToList();
            MerchBD.ItemsSource = itemsForPage;
        }

        public class OrderItem
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int StatusId { get; set; }
            public int PointId { get; set; }
            public DateTime OrderDate { get; set; }
            public int Code { get; set; }
            public int Cost { get; set; }
            public int Discount { get; set; }
            public int MerchId { get; set; }
            public string MerchName { get; set; }
            public decimal MerchPrice { get; set; }
            public int Quantity { get; set; }
            //public string SelectedPoint { get; set; }
            public ObservableCollection<point> PointCollection { get; set; }
            public point SelectedPoint { get; set; }
        }
    }
}
