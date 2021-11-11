using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Order
    {
        public int OrderID { set; get; }
        public DateTime DeliveryDate { set; get; }
        public int CustomerID { set; get; }
        public virtual Customer customer { set; get; }

        public virtual ICollection<FoodPackage> FoodPackages { get; set; }
    }
}
