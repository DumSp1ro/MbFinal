using Imperia.classes;
using Imperia.models;
using Imperia.pages;
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
using System.Windows.Shapes;

namespace Imperia.windows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        private bool flag = false;
        private users currentusers = new users { login = null, password = null, name = null };
        public class UserService
        {
            private readonly imp _context;

            public UserService()
            {
                _context = new imp(); // Инициализация контекста базы данных
            }

            public bool IsLoginExists(string login)
            {
                return _context.users.Any(u => u.login == login);
            }
        }

        private readonly UserService _userService;

        public RegisterWindow()
        {
            InitializeComponent();
            connect.modelbd = new imp();
            DataContext = currentusers;
            _userService = new UserService();
        }

        private void Registraciya(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                // Проверка логина
                if (_userService.IsLoginExists(currentusers.login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }

                // Логика сохранения пользователя
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


        private void NavigateBack()
        {
            AuthorizeWindow authorizeWindow = new AuthorizeWindow();
            authorizeWindow.Show();
            Close();
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void MinBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Authorize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AuthorizeWindow authWin = new AuthorizeWindow();
            Window currentWindow = GetWindow(this);
            currentWindow.Close();
            authWin.Show();
        }

        private void Foto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                // Проверка на null перед установкой изображения
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    // Отображаем выбранное изображение в предварительном просмотре
                    PreviewImage.Source = new BitmapImage(new Uri(selectedImagePath));
                    PreviewImage.Visibility = Visibility.Visible; // Показываем изображение
                                                                  // Сохраняем абсолютный путь к выбранному изображению
                    currentusers.photo = selectedImagePath;
                }
            }
        }
    }
}
