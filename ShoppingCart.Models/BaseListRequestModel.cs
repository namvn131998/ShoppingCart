using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class BaseListRequestModel
    {
        public BaseListRequestModel()
        {
            searchKey = string.Empty;
            searchValue = string.Empty;
            Page = 1;
            PageSize = 10;
            sortBy = string.Empty;
            sortDirection = string.Empty;
        }
        public string searchKey { get; set; }
        public string searchValue { get; set; }
        public string sortBy { get; set; }
        public string sortDirection { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}

