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
}
