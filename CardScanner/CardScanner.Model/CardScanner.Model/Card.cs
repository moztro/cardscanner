using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardScanner.Model
{
    public class Card
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }
        
        [Display(Name = "NIP (4 digits)")]
        public int Nip { get; set; }
        
        public ApplicationUser Owner { get; set; }
    }
}
