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
        public int ContentID { set; get; }

        public virtual FoodPackage foodpackage { set; get; }
    }
}
