using ShoppingCart.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Business.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        public IEnumerable<Category> GetListCategory();
    }
}
