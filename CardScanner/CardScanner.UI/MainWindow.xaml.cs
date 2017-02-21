using CardScanner.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            Lunches.ItemsSource = Lunch.Lunches;
            Lunches.DisplayMemberPath = "Name";

            settings.Click += Settings_Click;
            settings.ToolTip = "Settings for serial ports";
                        
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SerialPortConfig settings = new SerialPortConfig();

            settings.Owner = this;
            settings.ShowDialog();
        }

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
    }
}
