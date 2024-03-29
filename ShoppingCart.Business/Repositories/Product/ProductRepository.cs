﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.Models;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Business.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Product product)
        {
            var objPro = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (objPro != null)
            {
                objPro.Name = product.Name;
                objPro.Description = product.Description;
                objPro.Price = product.Price;
            }
        }
        public void UpdateMediaID(int productID, string mediaIDs)
        {
            var objPro = _context.Products.FirstOrDefault(p => p.Id == productID);
            if (objPro != null)
            {
                objPro.MediaIds = mediaIDs;
            }
        }
    }
}
