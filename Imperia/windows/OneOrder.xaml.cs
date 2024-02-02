using Imperia.classes;
using Imperia.models;
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
using System.Windows.Shapes;
using static Imperia.pages.merches;

namespace Imperia.windows
{
    /// <summary>
    /// Логика взаимодействия для OneOrder.xaml
    /// </summary>
    public partial class OneOrder : Window
    {
        private ObservableCollection<OrderItem> orderItems;
        private int OrderItemCount => orderItems.Count;
        public OneOrder(ObservableCollection<OrderItem> orderItems)
        {
            InitializeComponent();

            foreach (var orderItem in orderItems)
            {
                orderItem.PointCollection = new ObservableCollection<point>(connect.modelbd.point);
            }

            this.orderItems = orderItems;
            OrderListView.ItemsSource = orderItems;
            UpdateTotalCost();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListView.SelectedItem != null)
            {
                OrderItem selectedOrderItem = OrderListView.SelectedItem as OrderItem;
                if (selectedOrderItem != null)
                {
                    orderItems.Remove(selectedOrderItem);
                    UpdateTotalCost();
                }
            }
        }
        private void UpdateTotalCost()
        {
            int totalCost = orderItems.Sum(item => (int)item.MerchPrice * item.Quantity);
            int totalDiscount = orderItems.Sum(item => item.Discount > 0 ? (int)item.MerchPrice * item.Quantity * item.Discount / 100 : 0);
            int totalCostWithDiscount = totalCost - totalDiscount;

            DiscountInfoTextBlock.Text = $"Сумма заказа: {totalCost} руб.\nСкидка: {totalDiscount} руб.\nИтоговая цена: {totalCostWithDiscount} руб.";
        }
        public void SaveOrderToDatabase()
        {
            try
            {
                using (var context = new imp())
                {
                    foreach (var orderItem in orderItems)
                    {
                        var order = new orders
                        {
                            id_users = 3,
                            id_status = 1,
                            id_point = orderItem.SelectedPoint?.id ?? 1,
                            order_date = DateTime.Now,
                            code = GenerateOrderCode(),
                            cost = (int)orderItem.MerchPrice * orderItem.Quantity,
                            discount = orderItem.Discount,
                            total_cost = (int)orderItem.MerchPrice * orderItem.Quantity - orderItem.Discount,
                            delivery_days = OrderItemCount > 3 ? 3 : 6
                        };

                        context.orders.Add(order);

                        var sostav = new sostav
                        {
                            id_merch = orderItem.MerchId,
                            count = orderItem.Quantity,
                            quantity = 1,
                            total_cost = (int)orderItem.MerchPrice * orderItem.Quantity,
                            discount = orderItem.Discount
                        };

                        order.sostav.Add(sostav);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении заказа в базу данных: {ex.Message}", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GetTotalCost()
        {
            return orderItems.Sum(item => (int)item.MerchPrice * item.Quantity);
        }

        private int GetTotalDiscount()
        {
            return orderItems.Sum(item => item.Discount > 0 ? (int)item.MerchPrice * item.Quantity * item.Discount / 100 : 0);
        }

        private int GetTotalCostWithDiscount()
        {
            return GetTotalCost() - GetTotalDiscount();
        }

        private int GenerateOrderCode()
        {
            Random random = new Random();
            int number1 = random.Next(100, 1000);

            return number1;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveOrderToDatabase();
            orderItems.Clear();
            Close();
            MessageBox.Show("Заказ сохранен в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
