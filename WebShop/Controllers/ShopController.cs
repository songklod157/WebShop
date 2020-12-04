using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.ViewModel;
using System.IO;
using System.Threading.Tasks;


namespace WebShop.Controllers
{
    public class ShopController : Controller
    {
        private ECartDBEntities objECart;
        private List<ShoppingCartModel> ListShopcart;
        public ShopController()
        {
            objECart = new ECartDBEntities();
            ListShopcart = new List<ShoppingCartModel>();
        }
        // GET: Shop
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listofshoppingViewModels = (from objItem in objECart.Items
                                                                       join objCate in objECart.Categories
                                                                       on objItem.CategoryId equals objCate.CategoryId
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           Image = objItem.Image,
                                                                           Name = objItem.Name,
                                                                           Description = objItem.Description,
                                                                           Price = objItem.Price,
                                                                           ItemId = objItem.ItemId,
                                                                           Category = objCate.CategoryName,
                                                                           ItemCode = objItem.ItemCode

                                                                       }).ToList();

            return View(listofshoppingViewModels);

        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            ShoppingCartModel objShopCart = new ShoppingCartModel();
            Item objItem = objECart.Items.Single(model => model.ItemId.ToString() == ItemId);
            if (Session["CartCounter"] != null)
            {
                ListShopcart = Session["cartitem"] as List<ShoppingCartModel>;
            }
            if (ListShopcart.Any(Model => Model.ItemId == ItemId))
            {
                objShopCart = ListShopcart.Single(model => model.ItemId == ItemId);
                objShopCart.Quantity = objShopCart.Quantity + 1;
                objShopCart.Total = objShopCart.Quantity * objShopCart.UnitPrice;
            }
            else
            {
                objShopCart.ItemId = ItemId;
                objShopCart.Image = objItem.Image;
                objShopCart.ItemName = objItem.Name;
                objShopCart.Quantity = 1;
                objShopCart.Total = objItem.Price;
                objShopCart.UnitPrice = objItem.Price;
                ListShopcart.Add(objShopCart);

            }
            Session["CartCounter"] = ListShopcart.Count;
            Session["cartitem"] = ListShopcart;

            return Json(data: new { Success = true, Counter = ListShopcart.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order()
        {
                ListShopcart = Session["cartitem"] as List<ShoppingCartModel>; 
            
            return View(ListShopcart);
        }
        public ActionResult Delete(string id)
        {
            List<ShoppingCartModel> cart = (List<ShoppingCartModel>)Session["cartitem"];
            var del = cart.Find(m => m.ItemId == id);           
            cart.Remove(del);
            Session["CartCounter"] = cart.Count;
            Session["cartitem"] = cart;

            return View("Order",cart);
        }

    }
}