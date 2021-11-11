using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Restaurant
    {
        public int RestaurantID { set; get; }
        public string RestaurantName { set; get; }
        public string City { set; get; }
        public string Phonenumber { set; get; }

        public virtual ICollection<FoodPackage> foodPackages { get; set; }
    }
}
