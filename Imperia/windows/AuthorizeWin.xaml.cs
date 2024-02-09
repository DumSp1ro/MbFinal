using Imperia.classes;
using Imperia.models;
using Imperia.pages;
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
    /// Логика взаимодействия для AuthorizeWin.xaml
    /// </summary>
    public partial class AuthorizeWin : Window
    {
        public AuthorizeWin()
        {
            InitializeComponent();
            connect.modelbd = new imp();
        }

        private void LogIn()
        {
            try
            {
                var userObj = connect.modelbd.users.FirstOrDefault(x =>
                x.login == TxbLogin.Text && x.password == PsbPassword.Password);
                if (userObj != null)
                {
                    // Добавляем запись в историю входов
                    AddLoginHistoryEntry(userObj, "Успешно");

                    imp.CurrentUser = userObj;
                    currentuser.UserRole = userObj.type_user.role;
                    switch (userObj?.id_type)
                    {
                        case 1:
                            ;
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.MainFrame.Navigate(new RootMerch(userObj));
                            mainWindow.Show();
                            Close();
                            break;
                        case 3:
                            MainWindow mainWin = new MainWindow();
                            mainWin.MainFrame.Navigate(new merches(userObj));
                            mainWin.Show();
                            Close();
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Критическая работа приложения",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void AddLoginHistoryEntry(users user, string typeVxod)
        {
            try
            {
                // Создаем новую запись в истории входов
                LoginHistory loginHistoryEntry = new LoginHistory
                {
                    id_users = (user != null) ? user.id : (int?)null,
                    LoginDateTime = DateTime.Now,
                    TypeVxod = typeVxod
                };

                // Добавляем запись в базу данных
                connect.modelbd.LoginHistory.Add(loginHistoryEntry);
                connect.modelbd.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи в историю входов: " + ex.Message.ToString(),
                                "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnInLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxbLogin.Text) || string.IsNullOrEmpty(PsbPassword.Password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userObj = connect.modelbd.users.FirstOrDefault(x =>
                x.login == TxbLogin.Text && x.password == PsbPassword.Password);

            if (userObj == null)
            {
                MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                AddLoginHistoryEntry(null, "Не успешно");
            }
            else
            {
                MessageBox.Show($"Вы вошли как: {userObj.type_user.role}", "Уведомление",
                MessageBoxButton.OK, MessageBoxImage.Information);
                LogIn();
            }
        }


        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PsbPassword.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        private void Regist_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.MainFrame.Navigate(new register());
            //mainWindow.Show();
            //Close();

            RegisterWindow regWin = new RegisterWindow();
            Window currentWindow = GetWindow(this);
            regWin.Show();
            Close();
        }

        private void PokasPass(object sender, MouseButtonEventArgs e)
        {
            if (TxbPassword.Visibility == Visibility.Visible)
            {
                TxbPassword.Visibility = Visibility.Collapsed;
                PsbPassword.Visibility = Visibility.Visible;
            }
            else
            {
                TxbPassword.Visibility = Visibility.Visible;
                PsbPassword.Visibility = Visibility.Collapsed;
                TxbPassword.Text = PsbPassword.Password;
            }
        }
    }
}

