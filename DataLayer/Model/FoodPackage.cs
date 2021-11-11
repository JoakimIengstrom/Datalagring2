using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class FoodPackage
    {
        public int BoxID { set; get; }
        public string BoxName { set; get; }
        public string BoxCategory { set; get; }
        public decimal Price { set; get; }
        public DateTime Best_Before { set; get; }
        public int RestaurantID { set; get; }
        public int Order_ID { set; get; }
    }
}
