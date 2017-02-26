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
                settings.DataBits = 8;
                settings.Handshake = Handshake.None;
                settings.Parity = Parity.None;
                settings.PortName = ((COMPort)Ports.SelectedItem).COM;
                settings.StopBits = StopBits.One;
                //serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                //serialPort.Open();
            }
            catch { return false; }
            return true;
        }
    }
}
