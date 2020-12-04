using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.ViewModel;
using System.IO;

namespace WebShop.Controllers
{
 
    public class ItemController : Controller
    {
        private ECartDBEntities objECart;
        public ItemController()
        {
            objECart = new ECartDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objitemview = new ItemViewModel();
            objitemview.CategorySelectListItem = (from objCate in objECart.Categories
                                                  select new SelectListItem()
                                                  {
                                                      Text = objCate.CategoryName,
                                                      Value = objCate.CategoryId.ToString(),
                                                      Selected = true
                                                  });

            return View(objitemview);
        }
        [HttpPost]
        public JsonResult Index(ItemViewModel objitemview)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objitemview.Image.FileName);
            objitemview.Image.SaveAs(filename: Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.Image = "~/Images/" + NewImage;
            objItem.CategoryId = objitemview.CategotyId;
            objItem.Description = objitemview.Description;
            objItem.ItemCode = objitemview.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.Name = objitemview.Name;
            objItem.Price = objitemview.Price;
            objECart.Items.Add(objItem);
            objECart.SaveChangesAsync();

            return Json(data: new { Success = true, Message = " Add Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}