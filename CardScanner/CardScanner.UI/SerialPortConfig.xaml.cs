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
using System.IO.Ports;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CardScanner.Model;

namespace CardScanner.UI
{
    /// <summary>
    /// Interaction logic for SerialPortConfig.xaml
    /// </summary>
    public partial class SerialPortConfig : MetroWindow
    {
        

        public SerialPortConfig()
        {
            InitializeComponent();            
            InitializeSerialPorts();            
        }

        private void InitializeSerialPorts()
        {
            var ArrayComPortsNames = SerialPort.GetPortNames();
            var i = 0;
            foreach(string array in ArrayComPortsNames)
            {
                
                Model.COMPort port = new COMPort();
                port.Id = i;
                port.COM = array;
                Model.COMPort.COMPorts.Add(port);
                i++;
            }
            Ports.ItemsSource = Model.COMPort.COMPorts;
            Ports.DisplayMemberPath = "COM";
            BaudRates.ItemsSource = Model.BaudRate.BaudRates;
            BaudRates.DisplayMemberPath = "Value";

            HandShakes.ItemsSource = Enum.GetValues(typeof(EHandshake)).Cast<EHandshake>();
            Parities.ItemsSource = Enum.GetValues(typeof(EParity)).Cast<EParity>();
            StopBites.ItemsSource = Enum.GetValues(typeof(EStopBits)).Cast<EStopBits>();
            HandShakes.DisplayMemberPath = "DisplayName";
            Parities.DisplayMemberPath = "Name";
            StopBites.DisplayMemberPath = "Description";

            var settings = Settings.Instance;
            DataBits.Text = settings.DataBits.ToString();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Open())
            {
                await this.ShowMessageAsync("Config Finish", "Config has been done, ready for scan ;)");
                this.Close();
            }else
            {
                await this.ShowMessageAsync("Config Error", "An error ocurred :/");
            }

        }

        public bool Open()
        {
            try
            {
                var settings = Settings.Instance;
                settings.BaudRate = ((BaudRate)BaudRates.SelectedItem).Value;
                settings.DataBits = int.Parse(DataBits.Text);
                if(settings.DataBits < 5 || settings.DataBits > 8)
                {
                    settings.DataBits = 8;
                }
                settings.Handshake = HandShakes.SelectedItem != null ? (EHandshake)HandShakes.SelectedItem : EHandshake.None;
                settings.Parity = Parities.SelectedItem != null ? (EParity)Parities.SelectedItem : EParity.None;
                settings.PortName = Ports.SelectedItem != null ? ((COMPort)Ports.SelectedItem).COM : null;
                settings.StopBits = StopBites.SelectedItem != null ? (EStopBits)StopBites.SelectedItem: EStopBits.One;
                settings.UpdatePortSettings();
                //serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                //serialPort.Open();
            }
            catch(Exception ex) { return false; }
            return true;
        }
    }
}
