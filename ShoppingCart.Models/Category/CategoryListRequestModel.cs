using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models.Category
{
    public class CategoryListRequestModel : BaseListRequestModel
    {
        public CategoryListRequestModel() :base()
        {

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
