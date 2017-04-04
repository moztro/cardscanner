using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public EParity Parity { get; set; }
        public int DataBits { get; set; }
        public EStopBits StopBits { get; set; }
        public EHandshake Handshake { get; set; }

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
            _port = new SerialPort(PortName ?? "COM1", BaudRate, (Parity)Parity, DataBits, (StopBits)StopBits);
            _port.Handshake = (Handshake)Handshake;
        }
    }

    public enum EParity
    {
        [Description("None")]
        None = 0,
        [Description("Odd")]
        Odd = 1,
        [Description("Even")]
        Even = 2,
        [Description("Mark")]
        Mark = 3,
        [Description("Space")]
        Space = 4
    }

    public enum EStopBits
    {
        [Description("One")]
        One = 1,
        [Description("Two")]
        Two = 2,
        [Description("One Point Five")]
        OnePointFive = 3
    }

    public enum EHandshake
    {
        [Description("None")]
        None = 0,
        [Description("XOnXOff")]
        XOnXOff = 1,
        [Description("RequestToSend")]
        RequestToSend = 2,
        [Description("RequestToSendXOnXOff")]
        RequestToSendXOnXOff = 3
    }
}
