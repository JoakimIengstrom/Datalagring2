using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Customer
    {
        public int ID { set; get; }
        public string FullName { set; get; }
        public string PassWord { set; get; }
        public DateTime PassReg { set; get; }
        public string Email { set; get; }

        public virtual ICollection<Order> order { set; get; }
    }
}
