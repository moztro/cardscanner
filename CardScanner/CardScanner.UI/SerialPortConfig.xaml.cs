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
        internal SerialPort serialPort = new SerialPort();
        private int _dataBits = 8;
        private Handshake _handshake = Handshake.None;
        private Parity _parity = Parity.None;        
        private StopBits _stopBits = StopBits.One;

        /// <summary> 
        /// Holds data received until we get a terminator. 
        /// </summary> 
        private string tString = string.Empty;
        /// <summary> 
        /// End of transmition byte in this case EOT (ASCII 4). 
        /// </summary> 
        private byte terminator = 0x4;

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
                serialPort.BaudRate = ((BaudRate) this.BaudRates.SelectedItem).Value;
                serialPort.DataBits = _dataBits;
                serialPort.Handshake = _handshake;
                serialPort.Parity = _parity;
                serialPort.PortName = ((COMPort) this.Ports.SelectedItem).COM;
                serialPort.StopBits = _stopBits;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                serialPort.Open();
            }
            catch { return false; }
            return true;
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[serialPort.ReadBufferSize];

            //There is no accurate method for checking how many bytes are read 
            //unless you check the return from the Read method 
            int bytesRead = serialPort.Read(buffer, 0, buffer.Length);

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
    }
}
