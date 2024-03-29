﻿using Imperia.classes;
using Imperia.models;
using Imperia.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using System.Windows.Threading;

namespace Imperia.pages
{
    /// <summary>
    /// Логика взаимодействия для authorize.xaml
    /// </summary>
    public partial class authorize : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan duration;
        Random random = new Random();
        string symbols = "";
        private int attempts = 0;
        public authorize()
        {
            connect.modelbd = new imp();
            InitializeComponent();
            if (App.IsGone == true)
            {
                duration = TimeSpan.FromMinutes(1);

                LoginTimerTB.Visibility = Visibility.Visible;
                LoginBlock.Visibility = Visibility.Collapsed;
                BlockedTB.Text = "Время сеанса истекло!";
                BtnInLogin.IsEnabled = false;
                StartTimer();
            }
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
                            manager.MainFrame.Navigate(new RootMerch(userObj));
                            break;;
                        case 3:
                            manager.MainFrame.Navigate(new merches(userObj));
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
                attempts++;
                CheckAttemps();
                AddLoginHistoryEntry(null, "Не успешно");
            }
            else
            {
                MessageBox.Show($"Вы вошли как: {userObj.type_user.role}", "Уведомление",
                MessageBoxButton.OK, MessageBoxImage.Information);
                LogIn();
            }
        }
        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (duration == TimeSpan.Zero)
            {
                timer.Stop();
                LoginTimerTB.Visibility = Visibility.Hidden;
                LoginBlock.Visibility = Visibility.Visible;
                BlockedTB.Text = "";
                BtnInLogin.IsEnabled = true;
                CaptchaBlock.Visibility = Visibility.Collapsed;
                CaptchaTbBlock.Visibility = Visibility.Collapsed;
                attempts = 0;
                duration = TimeSpan.FromSeconds(10);
            }
            else
            {
                duration = duration.Add(TimeSpan.FromSeconds(-1));
                LoginTimerTB.Text = duration.ToString("c");
            }
        }
        private void BtnUpdateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha();
        }

        private void UpdateCaptcha()
        {
            symbols = "";
            CaptchaTB.Text = "";
            SPanelSymbols.Children.Clear();
            CanvasNoise.Children.Clear();

            GenerateSymbols(4);
            GenerateNoise(32);
        }

        private void GenerateSymbols(int count)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol;
                lbl.FontSize = random.Next(24, 32);
                lbl.RenderTransform = new RotateTransform(random.Next(-24, 24));
                lbl.Margin = new Thickness(16, 0, 16, 0);

                SPanelSymbols.Children.Add(lbl);

                symbols = symbols + symbol;
            }
        }

        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                border.Height = random.Next(4, 8);
                border.Width = random.Next(256, 512);

                border.RenderTransform = new RotateTransform(random.Next(0, 360));

                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, random.Next(0, 200));
                Canvas.SetTop(border, random.Next(0, 75));


                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 64), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                ellipse.Height = ellipse.Width = random.Next(20, 40);

                CanvasNoise.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, random.Next(0, 400));
                Canvas.SetTop(ellipse, random.Next(0, 100));
            }
        }

        private void CheckAttemps()
        {
            if (attempts == 2)
            {
                CaptchaBlock.Visibility = Visibility.Visible;
                CaptchaTbBlock.Visibility = Visibility.Visible;
                UpdateCaptcha();
                AddLoginHistoryEntry(null, "Не успешно");
                MessageBox.Show("Пройдите капчу.", "Не удается войти!", MessageBoxButton.OK, MessageBoxImage.Warning);

                if (CaptchaTB.Text != symbols)
                {
                    BtnInLogin.Visibility = Visibility.Hidden;
                }
                else
                {
                    BtnInLogin.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (attempts == 3)
                {
                    duration = TimeSpan.FromSeconds(10);
                    LoginTimerTB.Visibility = Visibility.Visible;
                    LoginBlock.Visibility = Visibility.Collapsed;
                    BlockedTB.Text = "Превышено количество попыток авторизации.\nВозможность входа заблокирована.";
                    BtnInLogin.IsEnabled = false;
                    StartTimer();
                    AddLoginHistoryEntry(null, "Не успешно");
                }
            };
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTB.Text == symbols)
                BtnInLogin.Visibility = Visibility.Visible;
        }

        private void TbxReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            manager.MainFrame.Navigate(new register());
        }


        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (TxbPassword.Visibility == Visibility.Visible)
            {
                TxbPassword.Visibility = Visibility.Collapsed;
                PsbPassword.Visibility = Visibility.Visible;
                EyeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Imperia;component/icons/ppass.png"));
            }
            else
            {
                TxbPassword.Visibility = Visibility.Visible;
                PsbPassword.Visibility = Visibility.Collapsed;
                TxbPassword.Text = PsbPassword.Password;
                EyeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Imperia;component/icons/nepass.png"));
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new register());
        }
    }
}
