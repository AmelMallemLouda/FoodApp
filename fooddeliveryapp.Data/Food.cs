using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddeliveryapp.Data
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }
        [Required]
        [Display(Name = "Food Price")]
        public int FoodPrice { get; set; }
        [Required]
        [Display(Name = "Food Ingridients")]
        public string FoodIngridients { get; set; }
    }
}
