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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для EditMerch.xaml
    /// </summary>
    public partial class EditMerch : Page
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        private bool flag = false;
        private merch currentmerch = new merch();
        public EditMerch(merch sellectedMerch)
        {
            InitializeComponent();
            if (sellectedMerch != null)
            {
                currentmerch = sellectedMerch;
            }
            DataContext = currentmerch;
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            string[] propertiesToCheck = { currentmerch.name, currentmerch.description, currentmerch.manufacturer };

            string discountText = currentmerch.discount?.ToString() ?? string.Empty;
            string[] propertyNames = { "Укажите название услуги", "Укажите описание", "Укажите производителя" };

            for (int i = 0; i < propertiesToCheck.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(propertiesToCheck[i]))
                {
                    errors.AppendLine(propertyNames[i]);
                }
            }

            // Проверка на корректное значение скидки
            if (!string.IsNullOrWhiteSpace(discountText) && (!int.TryParse(discountText, out int discountValue) || discountValue < 0 || discountValue > 99))
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
                if (currentmerch.id == 0)
                {
                    imp.GetContext().merch.Add(currentmerch);
                }

                using (DbContextTransaction dbContextTransaction = imp.GetContext().Database.BeginTransaction())
                {
                    imp.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    dbContextTransaction.Commit();
                }

                manager.MainFrame.GoBack();
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
