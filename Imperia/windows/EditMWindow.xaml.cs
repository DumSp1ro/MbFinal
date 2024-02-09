using Imperia.classes;
using Imperia.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
using System.Windows.Shapes;

namespace Imperia.windows
{
    /// <summary>
    /// Логика взаимодействия для EditMWindow.xaml
    /// </summary>
    public partial class EditMWindow : Window
    {
        public event EventHandler WindowClosed;

        private merch currentMerch;
        private string sourceDirectory = "photo";

        public EditMWindow(merch selectedMerch)
        {
            InitializeComponent();
            currentMerch = selectedMerch ?? new merch();
            DataContext = currentMerch;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentMerch.name))
                {
                    MessageBox.Show("Укажите название товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(currentMerch.size))
                {
                    MessageBox.Show("Укажите ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(currentMerch.brend))
                {
                    MessageBox.Show("Укажите производителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(currentMerch.discount?.ToString()) &&
                    (Convert.ToInt32(currentMerch.discount) < 0 || Convert.ToInt32(currentMerch.discount) > 99))
                {
                    MessageBox.Show("Скидка должна быть числом от 0 до 99", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(currentMerch.photo))
                {
                    MessageBox.Show("Выберите фотографию товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (currentMerch.id == 0)
                {
                    imp.GetContext().merch.Add(currentMerch);
                }

                imp.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                WindowClosed?.Invoke(this, EventArgs.Empty);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Ошибка валидации: {validationError.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ChoosePhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = sourceDirectory;
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Выберите изображение товара";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                currentMerch.photo = selectedImagePath;
                PreviewImage.Source = new BitmapImage(new Uri(selectedImagePath));
            }
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
