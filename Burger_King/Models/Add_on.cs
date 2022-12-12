using System.ComponentModel.DataAnnotations;

namespace Burger_King.Models
{
    public class Add_on
    {
        [Key]
        public int addon_id { get; set; }

        [Required]
        public string addon_name { get; set; }
    }
}
