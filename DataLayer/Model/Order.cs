using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Order
    {
        public int OrderID { set; get; } //PK
        public DateTime DeliveryDate { set; get; }
        public int CustomerID { set; get; } //FK

        public Customer Customer { set; get; }
        public ICollection<FoodBox> FoodBoxes { get; set; }
    }
}
