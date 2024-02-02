using Imperia.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для EditOWindow.xaml
    /// </summary>
    public partial class EditOWindow : Window
    {
        private orders currentOrder;
        public event EventHandler WindowClosed;

        public EditOWindow(orders selectedOrder)
        {
            InitializeComponent();
            currentOrder = selectedOrder != null ? imp.GetContext().orders.Find(selectedOrder.id) : new orders();
            DataContext = currentOrder;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentOrder.id_users == 0 || currentOrder.id_status == 0 || currentOrder.id_point == 0)
                {
                    MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (currentOrder.id == 0)
                {
                    imp.GetContext().orders.Add(currentOrder);
                }

                imp.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                WindowClosed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseDownHandler(sender, e);
        }

        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseDownHandler(sender, e);
        }

        private void MinBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }
    }

}

