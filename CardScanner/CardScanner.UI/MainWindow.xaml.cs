﻿using CardScanner.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CardScanner.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private SerialPort Port;

        public MainWindow()
        {
            InitializeComponent();          

            Lunches.ItemsSource = Lunch.Lunches;
            Lunches.DisplayMemberPath = "Name";

            settings.Click += Settings_Click;
            settings.ToolTip = "Settings for serial ports";
            
            Port = Settings.Instance.Port;
            Port.DataReceived += Port_DataReceived;
            Port.ErrorReceived += Port_ErrorReceived;
            Port.PinChanged += Port_PinChanged;
        }

        #region Serial port events
        private void Port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            string input = port.ReadExisting();

            string employeeIdentifier = string.Empty;
            string temp = Utils.Left(input, 1);

            if(input.Length > 38)
            {
                if (temp != "<")
                {
                    employeeIdentifier = Utils.Right(Utils.Left(input, 38), 5);
                }
                else
                {
                    string tempRight = "H&" + Utils.Right(Utils.Left(input, 11), 6);
                    int result = 0;
                    int.TryParse(tempRight.Substring(2),
                                NumberStyles.AllowHexSpecifier,
                                null,
                                out result);

                    employeeIdentifier = (result / 2).ToString();
                }
            }

            Code.Text = employeeIdentifier;
        }
        #endregion

        #region Settings window events
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SerialPortConfig settings = new SerialPortConfig();

            settings.Owner = this;
            settings.Closed += Settings_Closed;
            settings.ShowDialog();
        }

        private void Settings_Closed(object sender, EventArgs e)
        {
            ((SerialPortConfig)sender).Open();
            //Settings.Instance.UpdatePortSettings();
            Port = Settings.Instance.Port;
        }
        #endregion

        #region Behavior events
        private async void item2_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Config", "Just other menu config ;)");
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("HELP", "Just slide your card in the scanner. Let us take care of the rest ;)");
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            var task = await this.ShowProgressAsync(string.Empty, "Reading your card, please wait...");
           
            task.Maximum = 100;
            for(int i=0; i <= task.Maximum; i += 10)
            {
                task.SetProgress(i);
                await Task.Delay(100);
            }

            var port = Port;

            if (task.IsOpen)
            {
                await task.CloseAsync();
                Code.Text = "1234 5678 910";
                this.SetUserData();
                UserData.Visibility = Visibility.Visible;
            }   
        }

        private void SetUserData()
        {

            var user = new ApplicationUser();
            user.FirstName = "Daniel";
            user.LastName = "Uribe";
            user.Username = "daniel@crea-ti.com.mx";

            Username.Content = user.FullName;
            Email.Content = user.Username;
            BusinessArea.Content = "IS";
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)e.Source;

            if (string.IsNullOrEmpty(textbox.Text))
            {
                UserData.Visibility = Visibility.Hidden;
            }
        }

        private async void Assign_Click(object sender, RoutedEventArgs e)
        {
            var task = await this.ShowProgressAsync(string.Empty, "Assigning your lunch, please wait...");

            task.Maximum = 100;
            for (int i = 0; i <= task.Maximum; i += 10)
            {
                task.SetProgress(i);
                await Task.Delay(100);
            }

            if (task.IsOpen)
            {
                await task.CloseAsync();
                Code.Text = string.Empty;
                Code.Focus();
                UserData.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
