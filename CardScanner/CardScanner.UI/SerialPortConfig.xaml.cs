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

namespace CardScanner.UI
{
    /// <summary>
    /// Interaction logic for SerialPortConfig.xaml
    /// </summary>
    public partial class SerialPortConfig : MetroWindow
    {
        SerialPort serialPort = new SerialPort();
        public SerialPortConfig()
        {
            InitializeComponent();            
            InitializeSerialPorts();
            //Init();
        }

        private void InitializeSerialPorts()
        {
            var ArrayComPortsNames = SerialPort.GetPortNames();
            
            foreach(string array in ArrayComPortsNames)
            {
                ports.Items.Add(array);
            }

            baudRate.Items.Add(300);
            baudRate.Items.Add(600);
            baudRate.Items.Add(1200);
            baudRate.Items.Add(2400);
            baudRate.Items.Add(9600);
            baudRate.Items.Add(14400);
            baudRate.Items.Add(19200);
            baudRate.Items.Add(38400);
            baudRate.Items.Add(57600);
            baudRate.Items.Add(115200);
        }

        private async void Init()
        {
            var task = await this.ShowProgressAsync("Serial Ports Configuration", "Reading your serial ports!!!");

            task.Maximum = 100;
            for (int i = 0; i <= task.Maximum; i += 10)
            {
                task.SetProgress(i);
                await Task.Delay(100);
            }

            if (task.IsOpen)
            {
                await task.CloseAsync();                
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Config Finish", "Config has been done, ready for scan ;)");
            this.Close();
        }
    }
}
