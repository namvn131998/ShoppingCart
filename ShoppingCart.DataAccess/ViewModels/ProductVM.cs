using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.DataAccess.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
