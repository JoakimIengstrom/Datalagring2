using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class FoodContent
    {
        public string Content { set; get; }
        public int FoodContentID { set; get; } //PK

        public FoodBox Foodboxes { set; get; }
    }
}
