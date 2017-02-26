using CardScanner.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
        /// <summary> 
        /// Holds data received until we get a terminator. 
        /// </summary> 
        private string tString = string.Empty;
        /// <summary> 
        /// End of transmition byte in this case EOT (ASCII 4). 
        /// </summary> 
        private byte terminator = 0x4;

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
            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[Port.ReadBufferSize];

            //There is no accurate method for checking how many bytes are read 
            //unless you check the return from the Read method 
            int bytesRead = Port.Read(buffer, 0, buffer.Length);

            //For the example assume the data we are received is ASCII data. 
            tString += Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //Check if string contains the terminator  
            if (tString.IndexOf((char)terminator) > -1)
            {
                //If tString does contain terminator we cannot assume that it is the last character received 
                string workingString = tString.Substring(0, tString.IndexOf((char)terminator));
                //Remove the data up to the terminator from tString 
                tString = tString.Substring(tString.IndexOf((char)terminator));
                //Do something with workingString 
                Console.WriteLine(workingString);
            }
            else { Console.WriteLine(tString); }
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
            Settings.Instance.UpdatePortSettings();
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
