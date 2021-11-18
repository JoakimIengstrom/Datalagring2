using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Customer
    {
        public int ID { set; get; } //PK
        [Required] public string FullName { set; get; }
        [Required] public string PassWord { set; get; }
        public DateTime PassReg { set; get; }
        public string Email { set; get; }

        public virtual ICollection<Order> order { set; get; }
    }
}
