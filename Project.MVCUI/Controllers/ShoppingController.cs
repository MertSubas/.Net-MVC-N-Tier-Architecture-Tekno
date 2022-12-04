using PagedList;
using Project.BLL.Designpattern.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;

        public ShoppingController()
        {
            _odRep = new OrderDetailRepository();
            _oRep = new OrderRepository();
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
        }
        public ActionResult ShoppingList(int? page, int? categoryID)
        {
            PaginationVM pavm = new PaginationVM
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                Categories = _cRep.GetActives()
            };
            if (categoryID != null) TempData["catID"] = categoryID;
            return View(pavm);
        }

        public ActionResult AddToCart(int id)
        {
            Card c = Session["scart"] == null ? new Card() : Session["scart"] as Card;
            Product eklenecekUrun = _pRep.Find(id);

            CardItem ci = new CardItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath
            };
            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");
        }
        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CardPageVM cpvm = new CardPageVM();
                Card c = Session["scart"] as Card;
                cpvm.Card = c;
                return View(cpvm);
            }
            TempData["bos"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("Shopping List");
        }
        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Card c = Session["scart"] as Card;
                c.SepettenCikar(id);
                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizdeki tüm ürünler cıkartılmıstır";
                    return RedirectToAction("Shopping List");
                }
                return RedirectToAction("Cartpage");
            }
            return RedirectToAction("ShoppingList");
        }
        public ActionResult ConfirmOrder()
        {
            AppUser currentUser;
            if (Session["member"] != null)
            {
                currentUser = Session["member"] as AppUser;

            }
            else TempData["anonim"] = "kullanıcı üye degil";
            return View();
        }

        //https://localhost:44384/api/Payment/ReceivePayment 
        [HttpPost]
        public ActionResult ConfirmOrder(OrderVM ovm)
        {
            bool result;
            Card sepet = Session["scart"] as Card;
            ovm.Order.TotalPrice = ovm.PaymentDTO.ShoppingPrice = sepet.TotalPrice;

            #region APISection

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44384/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment",ovm.PaymentDTO);
                HttpResponseMessage sonuc;


                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception)
                {

                    TempData["baglantiRed"] = "Banka baglantiyi reddetti";
                    return RedirectToAction("ShoppingList");
                }
                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    if (Session["member"] != null)
                    {
                        AppUser user = Session["member"] as AppUser;
                        ovm.Order.AppUserID = user.ID;
                        ovm.Order.UserName = user.UserName;
                    }
                    else
                    {
                        ovm.Order.AppUserID = null;
                        ovm.Order.UserName = TempData["anonim"].ToString();
                    }
                    _oRep.Add(ovm.Order); //OrderRepository bu noktada Order'i eklerken onun ID'sini olusturuyor

                    foreach (CardItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);

                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitsInStock -= item.Amount;
                        _pRep.Update(stokDus);
                    }
                    TempData["odeme"] = "Siparişiniz  bize ulasmıstır...Tesekkür ederiz";
                    MailService.Send(ovm.Order.Email, body: $"Siparişiniz basarıyla alındı {ovm.Order.TotalPrice}");
                    Session.Remove("scart");
                    return RedirectToAction("ShoppingList");

                }
                else
                {
                    Task<string> s = sonuc.Content.ReadAsStringAsync();
                    TempData["sorun"] = s.Result;
                    return RedirectToAction("ShoppingList");
                }












            }



            #endregion
        }






    }

}