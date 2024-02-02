using Imperia.classes;
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
    /// Логика взаимодействия для EditMWindow.xaml
    /// </summary>
    public partial class EditMWindow : Window
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        private bool flag = false;
        private merch currentmerch = new merch();
        public event EventHandler WindowClosed;
        private merch currentMerch;
        public EditMWindow(merch selectedMerch)
        {
            InitializeComponent();
            currentMerch = selectedMerch ?? new merch();
            DataContext = currentMerch;
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
        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }
        private void MinBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentMerch.name))
            {
                errors.AppendLine("Укажите название товара");
            }

            if (string.IsNullOrWhiteSpace(currentMerch.description))
            {
                errors.AppendLine("Укажите описание");
            }

            if (string.IsNullOrWhiteSpace(currentMerch.manufacturer))
            {
                errors.AppendLine("Укажите производителя");
            }

            if (!string.IsNullOrWhiteSpace(currentMerch.discount?.ToString()) &&
    (Convert.ToInt32(currentMerch.discount) < 0 || Convert.ToInt32(currentMerch.discount) > 99))
            {
                errors.AppendLine("Скидка должна быть числом от 0 до 99.");
            }


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Проверка наличия элемента в БД по другому признаку, например, id
                if (currentMerch.id == 0)
                {
                    imp.GetContext().merch.Add(currentMerch);
                }

                imp.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");

                // Вызов события WindowClosed после сохранения изменений
                WindowClosed?.Invoke(this, EventArgs.Empty);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void Foto(object sender, RoutedEventArgs e)
        {
            string source = Environment.CurrentDirectory;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                flag = true;
                string selectedImagePath = openFileDialog.FileName;
                string destinationPath = System.IO.Path.Combine(source, "photo", System.IO.Path.GetFileName(selectedImagePath));
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(source, "photo"));
                System.IO.File.Copy(selectedImagePath, destinationPath, true);
                if (System.IO.File.Exists(destinationPath))
                {
                    PreviewImage.Source = new BitmapImage(new Uri(destinationPath));
                    currentmerch.photo = $"/photo/{System.IO.Path.GetFileName(destinationPath)}";
                }
            }
        }
    }
}
