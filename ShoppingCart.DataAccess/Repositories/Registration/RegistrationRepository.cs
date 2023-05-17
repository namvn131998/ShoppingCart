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
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        private ApplicationDbContext _context;
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Registration registration)
        {
            var objReg = _context.Registrations.FirstOrDefault(r => r.UserID == registration.UserID);
            if (objReg != null)
            {
                objReg.UserName = registration.UserName;
                objReg.Password = registration.Password;
            }
        }
        public LoginStatus Login(string username, string password, out Registration registration)
        {
            LoginStatus loginStatus = LoginStatus.InvalidUserName;
            registration = GetUserName(username);
            if(registration != null)
            {
                if(password == registration.Password)
                {
                    switch(registration.Status)
                    {
                        case ((int)RegistrationStatus.Pending):
                            loginStatus = LoginStatus.Pending;
                            break;
                        case ((int)RegistrationStatus.Disabled):
                            loginStatus = LoginStatus.Disabled;
                            break;
                        default :
                            loginStatus = LoginStatus.Success;
                            break;
                    }
                }
                else
                {
                    loginStatus = LoginStatus.InvalidPassword;
                }
            }
            return loginStatus;
        }
        public Registration GetUserName(string username="")
        {
            return _context.Registrations.FirstOrDefault(r => r.UserName == username);
        }
    }
}
