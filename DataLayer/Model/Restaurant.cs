using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Restaurant
    {
        public int RestaurantID { set; get; } //PK
        [Required] public string RestaurantName { set; get; }
        [Required] public string City { set; get; }
        [Required] public string Phonenumber { set; get; }

        public virtual ICollection<FoodBox> foodBox { get; set; }
    }
}
