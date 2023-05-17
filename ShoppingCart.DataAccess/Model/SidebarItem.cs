using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Model
{
    public class SidebarItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public int DisplayPriority { get; set; }

        public string ActiveUrls { get; set; }

        public bool IsActive { get; set; }
        public string Class { get; set; }
    }
}

