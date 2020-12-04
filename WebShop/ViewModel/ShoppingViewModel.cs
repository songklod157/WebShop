﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModel
{
    public class ShoppingViewModel
    {
        public Guid ItemId { get; set; }      
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }

    }
}