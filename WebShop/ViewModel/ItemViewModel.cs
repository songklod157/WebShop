using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.ViewModel;
using System.IO;

namespace WebShop.ViewModel
{
    public class ItemViewModel
    {
        public Guid ItemId { get; set; }
        public int CategotyId { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<SelectListItem> CategorySelectListItem { get; set; }

    }
}