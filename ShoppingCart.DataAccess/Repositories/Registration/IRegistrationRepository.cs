using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.DataAccess.Constants.Enums;

namespace ShoppingCart.DataAccess.Repositories
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        void Update(Registration registration);
        LoginStatus Login(string username, string password, out Registration registration);
        Registration GetUserName(string username);
    }
}
