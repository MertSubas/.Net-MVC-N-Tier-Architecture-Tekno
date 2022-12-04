using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ShoppingTools
{
    public class CardItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public decimal SubTotal
        {
            get 
            {
                return Price * Amount; 
            }
        }
        public CardItem()
        {
            Amount++;
        }
    }
}