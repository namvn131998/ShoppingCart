using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.DataAccess.Repositories;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.DataAccess.Constants.Enums;
using ShoppingCart.Business.Utilities;
using ShoppingCart.DataAccess.Helper;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminLogin(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public  IActionResult AdminLogin(LoginViewModel log)
        {
            string message = "InvalidUserName";
            Registration registration = new Registration();
            LoginStatus loginStatus = _unitOfWork.RegistrationRepository.Login(log.Username, log.Password, out registration);
            if(registration != null)
            {
                switch (loginStatus)
                {
                    case LoginStatus.Pending:
                        message = "Your account is pending";
                        break;
                    case LoginStatus.Disabled:
                        message = "Your account is disabled";
                        break;
                    case LoginStatus.InvalidUserName:
                        message = "InvalidUserName";
                        ModelState.AddModelError("UserName", message);
                        break;
                    case LoginStatus.InvalidPassword:
                        message = "InvalidPassword";
                        ModelState.AddModelError("Password", message);
                        break;
                    default:
                        message = "LoginSuccess";
                        break;
                }
            }
            if(loginStatus == LoginStatus.Success)
            {
                var user = new LoggedUser(registration);
                HttpContext.Session.Set(SessionUtilities.SessionCurrentUserkey, user);
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }
    }
}
