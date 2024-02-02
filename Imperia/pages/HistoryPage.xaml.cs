using Imperia.classes;
using Imperia.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private ObservableCollection<LoginHistory> _filteredLoginHistory;

        public HistoryPage()
        {
            InitializeComponent();
            LoadLoginHistory();
        }

        private async void LoadLoginHistory()
        {
            try
            {
                var loginHistory = await connect.modelbd.LoginHistory.ToListAsync();
                _filteredLoginHistory = new ObservableCollection<LoginHistory>(loginHistory);

                var uniqueLogins = loginHistory.Select(entry => entry.users?.login).Where(login => !string.IsNullOrEmpty(login)).Distinct().ToList();
                LoginFilterComboBox.ItemsSource = uniqueLogins;

                LoginHistoryDataGrid.ItemsSource = _filteredLoginHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке истории входов: " + ex.Message.ToString(),
                                "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NameFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void LoginFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var originalCollection = connect.modelbd.LoginHistory.ToList();
            var nameFilter = NameFilterTextBox.Text;

            var filteredCollection = originalCollection
                .Where(entry => (string.IsNullOrEmpty(nameFilter) || (entry.users != null && entry.users.name.Contains(nameFilter)))
                    && (LoginFilterComboBox.SelectedItem == null || (entry.users != null && entry.users.login == LoginFilterComboBox.SelectedItem.ToString())))
                .ToList();

            if (SuccessCheckBox.IsChecked == true)
            {
                filteredCollection = filteredCollection
                    .Where(entry => entry.TypeVxod == "Успешно")
                    .ToList();
            }

            _filteredLoginHistory.Clear();
            foreach (var item in filteredCollection)
            {
                _filteredLoginHistory.Add(item);
            }
        }

        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            LoadLoginHistory();
            NameFilterTextBox.Text = string.Empty;
            LoginFilterComboBox.SelectedItem = null;
            SuccessCheckBox.IsChecked = false;
        }
    }
}
