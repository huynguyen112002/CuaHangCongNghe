﻿using System;
using System.Collections.Generic;

namespace CuaHangCongNghe.Models.Shop
{
    public partial class Product
    {
        public Product()
        {
            Orderitems = new HashSet<Orderitem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int Stockquantity { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
    }
}
