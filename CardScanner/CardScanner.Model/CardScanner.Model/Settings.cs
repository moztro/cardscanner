using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardScanner.Model
{
    public class Settings
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public Handshake Handshake { get; set; }

        private static Settings instance = null;
        private SerialPort _port { get; set; }

        private Settings()
        {
            _port = new SerialPort();
        }

        public static Settings Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Settings();
                }

                return instance;
            }
        }

        public SerialPort Port
        {
            get { return _port; }
        }

        public void UpdatePortSettings()
        {
            _port = new SerialPort(PortName ?? "COM1", BaudRate, Parity, DataBits, StopBits);
            _port.Handshake = Handshake;
        }
    }
}
