using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class FoodBox
    {
        public int FoodBoxID { set; get; } //PK
        [Required] public string BoxName { set; get; }
        [Required] public string BoxCategory { set; get; }
        public decimal Price { set; get; }
        [Required] public DateTime BestBefore { set; get; }

        public Restaurant Restaurant { set; get; } //FK
        public Order Order { set; get; } //FK (Icollecting)
    }
}
