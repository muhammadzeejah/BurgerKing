using System.ComponentModel.DataAnnotations;

namespace Burger_King.Models
{
    public class Sauces
    {
        [Key]
        public int sauce_id { get; set; }

        [Required]
        public string sauce_name { get; set; }
    }
}
