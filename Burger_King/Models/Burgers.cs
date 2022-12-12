using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Burger_King.Models
{
    public class Burgers
    {
        [Key]
        public int burger_id { get; set; }

        [Required]
        public string burger_name { get; set; }

        [Required]
        public float burger_price { get; set; }

        [Required]
        public string burger_url { get; set; }

        [ForeignKey("burgerSauce")]
        public int sauce_fid { get; set; }
        public virtual Sauces burgerSauce { get; set; }

        [ForeignKey("burgerAddOn")]
        public int addon_fid { get; set; }
        public virtual Add_on burgerAddOn { get; set; }
    }
}
