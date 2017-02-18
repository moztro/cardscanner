using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardScanner.Model
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            this.Cards = new HashSet<Card>();
        }

        [Key]
        public string Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        public ICollection<Card> Cards { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }
}
