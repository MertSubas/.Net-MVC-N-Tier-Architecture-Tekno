using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public short UnıtsInStock { get; set; }
        public decimal UnıtPrice { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }
        //Relational properties

        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
} 
