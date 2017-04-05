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
        public BaudRate BaudRate { get; set; }
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
            _port = new SerialPort(PortName ?? "COM1", BaudRate.Id, (Parity)Parity.Id, DataBits, (StopBits)StopBits.Id);
            _port.Handshake = (Handshake)Handshake.Id;
        }
    }

    public class EParity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static EParity Default { get { return new EParity { Id = 0, Name = "None" }; } }

        public static IEnumerable<EParity> Parities
        {
            get
            {
                return new List<EParity>
                {
                    new EParity { Id = 0, Name = "None" },
                    new EParity { Id = 1, Name = "Odd" },
                    new EParity { Id = 2, Name = "Even" },
                    new EParity { Id = 3, Name = "Mark" },
                    new EParity { Id = 4, Name = "Space" },
                };
            }
        }
    }

    public class EStopBits
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static EStopBits Default { get { return new EStopBits { Id = 1, Name = "One" }; } }

        public static IEnumerable<EStopBits> StopBites
        {
            get
            {
                return new List<EStopBits>
                    {
                        new EStopBits { Id = 1, Name = "One" },
                        new EStopBits { Id = 2, Name = "Two" },
                        new EStopBits { Id = 3, Name = "One Point Five" }
                    };
            }
        }
    }

    public class EHandshake
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static EHandshake Default { get { return new EHandshake { Id = 0, Name = "None" }; } }

        public static IEnumerable<EHandshake> Handshakes
        {
            get
            {
                return new List<EHandshake>
                    {
                        new EHandshake { Id = 0, Name = "None" },
                        new EHandshake { Id = 1, Name = "XOnXOff" },
                        new EHandshake { Id = 2, Name = "RequestToSend" },
                        new EHandshake { Id = 3, Name = "RequestToSendXOnXOff" }
                    };
            }
        }
    }
}
