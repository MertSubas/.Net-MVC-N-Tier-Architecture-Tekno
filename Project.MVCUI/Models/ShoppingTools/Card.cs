using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ShoppingTools
{
    public class Card
    {
        Dictionary<int, CardItem> _sepetim;
        public Card()
        {
            _sepetim = new Dictionary<int, CardItem>();
        }

        public List<CardItem> Sepetim
        {
            get
            {
                return _sepetim.Values.ToList();
            }
        }
        public void SepeteEkle(CardItem item)
        {
            if(_sepetim.ContainsKey(item.ID))
            {
                _sepetim[item.ID].Amount++;
                return;
            }
            _sepetim.Add(item.ID, item);
        }
        public void SepettenCikar(int id)
        {
            if(_sepetim[id].Amount>1)
            {
                _sepetim[id].Amount--;
                return;
            }
            _sepetim.Remove(id);
        }
        public decimal TotalPrice
        {
            get
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
        }
    }
}