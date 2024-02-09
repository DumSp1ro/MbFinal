using Imperia.classes;
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
using static Imperia.pages.merches;

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для RootMerch.xaml
    /// </summary>
    public partial class RootMerch : Page
    {
        private ObservableCollection<merch> merchCollection;
        public RootMerch(users user)
        {
            InitializeComponent();
            DataContext = user;
            merchCollection = new ObservableCollection<merch>(imp.GetContext().merch.ToList());
            MerchBD.ItemsSource = merchCollection;

            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            BrendTextBox.TextChanged += BrendTextBox_TextChanged;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            EditMWindow emw = new EditMWindow(null);
            emw.WindowClosed += EditMWindow_WindowClosed; // Подписываемся на событие WindowClosed
            emw.ShowDialog();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный элемент из DataGrid
            merch selectedMerch = (sender as Button).DataContext as merch;

            // Открываем окно EditMWindow для редактирования выбранного элемента
            EditMWindow emw = new EditMWindow(selectedMerch);
            emw.WindowClosed += EditMWindow_WindowClosed; // Подписываемся на событие WindowClosed
            emw.ShowDialog();
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            var MerchDell = MerchBD.SelectedItems.Cast<merch>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {MerchDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    imp.GetContext().merch.RemoveRange(MerchDell);
                    imp.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    // Обновление ObservableCollection, что автоматически обновит DataGrid
                    foreach (var merch in MerchDell)
                    {
                        merchCollection.Remove(merch);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Orders(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new OrdersPage());
        }


        private void RefreshPage()
        {
            merchCollection.Clear();
            var filteredMerch = imp.GetContext().merch.ToList();

            // Фильтрация поиском по названию
            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                filteredMerch = filteredMerch.Where(m => m.name.Contains(SearchTextBox.Text)).ToList();
            }

            // Фильтрация поиском по бренду
            if (!string.IsNullOrEmpty(BrendTextBox.Text))
            {
                filteredMerch = filteredMerch.Where(m => m.brend.Contains(BrendTextBox.Text)).ToList();
            }

            foreach (var merch in filteredMerch)
            {
                merchCollection.Add(merch);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshPage();
        }

        private void BrendTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshPage();
        }

        private void Ref(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private void History(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new HistoryPage());
        }

        private void Pods(object sender, RoutedEventArgs e)
        {
            PodskWindow podsk = new PodskWindow();
            podsk.ShowDialog();
        }
        private void EditMWindow_WindowClosed(object sender, EventArgs e)
        {
            // Выполняем обновление данных или перезагрузку страницы RootMerch
            RefreshPage();
        }
    }
}
