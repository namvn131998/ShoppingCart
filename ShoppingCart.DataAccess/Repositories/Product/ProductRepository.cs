using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.Models;
using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.DataAccess.Repositories
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
            var objPro = _context.Products.FirstOrDefault(c => c.Id == product.Id);
            if (objPro == null)
            {
                objPro.Name = product.Name;
                objPro.Description = product.Description;
                objPro.Price = product.Price;
            }
        }
    }
}
