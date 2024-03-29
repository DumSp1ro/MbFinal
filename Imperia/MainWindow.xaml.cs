﻿using Imperia.classes;
using Imperia.pages;
using Imperia.windows;
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
using System.Windows.Threading;

namespace Imperia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int sessionTimeInMinutes = 3600;
        private int remainingTimeInSeconds;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            manager.MainFrame = MainFrame;
            InitializeTimer();
            MainFrame.Navigate(new merches(null));
        }
        private void autorizacia(object sender, RoutedEventArgs e)
        {
            //manager.MainFrame.Navigate(new authorize());
            AuthorizeWindow auth = new AuthorizeWindow();
            Window currentWindow = Window.GetWindow(this);
            currentWindow.Close();
            auth.Show();
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            StartSessionTimer();
        }

        private void StartSessionTimer()
        {
            remainingTimeInSeconds = sessionTimeInMinutes * 60;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTimeInSeconds--;
            if (remainingTimeInSeconds <= 0)
            {
                timer.Stop();
                Application.Current.Shutdown();
            }
            else
            {
                TimerTextBlock.Text = TimeSpan.FromSeconds(remainingTimeInSeconds).ToString(@"mm\:ss");

                if (remainingTimeInSeconds == 2 * 60) // Оповещение за 2 минуты до конца
                {
                    MessageBox.Show("До конца сессии осталось 2 минуты!");
                }
                else if (remainingTimeInSeconds == 60) // Блокировка кнопки за 1 минуту до конца
                {
                    autobnt.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
