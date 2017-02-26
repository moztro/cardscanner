using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardScanner.Model
{
    public class Lunch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static IEnumerable<Lunch> Lunches
        {
            get
            {
                return new List<Lunch>
                {
                    new Lunch { Id = 1, Name = "Breakfast" },
                    new Lunch { Id = 2, Name = "Meal" }
                };
            }
        }
    }

    public class COMPort
    {
        public int Id { get; set; }

        public string COM { get; set; }

        public static ICollection<COMPort> COMPorts = new List<COMPort>();
    }

    public class BaudRate
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public static IEnumerable<BaudRate> BaudRates
        {
            get
            {
                return new List<BaudRate>
                {
                    new BaudRate { Id = 1, Value = 300 },
                    new BaudRate { Id = 2, Value = 1200 },
                    new BaudRate { Id = 2, Value = 2400 },
                    new BaudRate { Id = 2, Value = 4800 },
                    new BaudRate { Id = 2, Value = 9600 },
                    new BaudRate { Id = 2, Value = 14400 },
                    new BaudRate { Id = 2, Value = 19200 },
                    new BaudRate { Id = 2, Value = 28800 },
                    new BaudRate { Id = 2, Value = 38400 },
                    new BaudRate { Id = 2, Value = 57600 },
                    new BaudRate { Id = 2, Value = 115200 },
                    new BaudRate { Id = 2, Value = 230400 }  
                };
            }
        }
    }
}
