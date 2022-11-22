using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Supplier:BaseEntity
    {
        public int OrderID { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }

        //Relational Properties

        public virtual List<Order> Orders { get; set; }
    }
}
