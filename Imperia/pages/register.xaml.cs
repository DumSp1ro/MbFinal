using Imperia.classes;
using Imperia.models;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для register.xaml
    /// </summary>
    public partial class register : Page
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        private bool flag = false;
        private users currentusers = new users { login = null, password = null, name = null };
        public register()
        {
            InitializeComponent();
            connect.modelbd = new imp();
            DataContext = currentusers;
        }
        private void Registraciya(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                using (var dbContextTransaction = imp.GetContext().Database.BeginTransaction())
                {
                    if (currentusers.id == 0)
                    {
                        currentusers.id_type = 3;
                        imp.GetContext().users.Add(currentusers);
                    }

                    imp.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    dbContextTransaction.Commit();
                }

                NavigateBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentusers.login))
            {
                errors.AppendLine("Укажите логин");
            }

            if (string.IsNullOrWhiteSpace(currentusers.password))
            {
                errors.AppendLine("Укажите пароль");
            }

            if (string.IsNullOrWhiteSpace(currentusers.name))
            {
                errors.AppendLine("Укажите имя");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void NavigateBack()
        {
            if (manager.MainFrame.CanGoBack)
            {
                manager.MainFrame.GoBack();
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
                string destinationPath = System.IO.Path.Combine(source, "res", System.IO.Path.GetFileName(selectedImagePath));
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(source, "res"));
                System.IO.File.Copy(selectedImagePath, destinationPath, true);
                if (System.IO.File.Exists(destinationPath))
                {
                    PreviewImage.Source = new BitmapImage(new Uri(destinationPath));
                    currentusers.photo = $"/res/{System.IO.Path.GetFileName(destinationPath)}";
                }
            }
        }

    }
}
