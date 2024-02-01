using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.DataAccess.Constants.Enums;
using ShoppingCart.Business.Utilities;
using ShoppingCart.DataAccess.Helper;
using ShoppingCart.Business.Repositories;

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
        public IActionResult Logout()
        {
            HttpContext.Session.Set(SessionUtilities.SessionCurrentUserkey, "");
            return RedirectToAction("AdminLogin", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            var model = new Registration();
            return View(model);
        }
        [HttpPost]
        public IActionResult Register(Registration reg)
        {
            if (ModelState.IsValid)
            {
                if (reg.UserID == 0)
                {
                    _unitOfWork.RegistrationRepository.Add(reg);
                }
                _unitOfWork.Save();
            }
            return Json(new {message="OK"});
        }
        [HttpGet]
        public IActionResult Profile(int UserID)
        {
            var model = _unitOfWork.RegistrationRepository.GetUserByID(UserID);
            if (model != null)
            {
                var uploadFile = _unitOfWork.UploadFileRepository.GetT(u => u.UserID == UserID && u.UploadTypeID == (int)UploadType.User);
                if (uploadFile != null)
                    model.Photo = GetHostName() + uploadFile.Thumbnail;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Changepassword(int UserID)
        {
            ViewBag.UserID = UserID;
            return View();
        }
        [HttpPost]
        public IActionResult Changepassword(int UserID = 0, string Password = "",string Newpassword = "", string Confirmpassword = "")
        {
            var user = _unitOfWork.RegistrationRepository.GetUserByID(UserID);
            if (user.Password == Password)
            {
                user.Password = Newpassword;
                _unitOfWork.Save();
                return Json(new { result = "OK" });
            }
            else
            {
                return Json(new { result = "Fail" });
            }
        }
        [HttpPost]
        public IActionResult UpdateUser(Registration reg)
        {
            _unitOfWork.RegistrationRepository.Update(reg);
            _unitOfWork.Save();
            var model = _unitOfWork.RegistrationRepository.GetUserByID(reg.UserID);
            return RedirectToAction("Profile", model);
        }
        public string GetHostName()
        {
            var host = HttpContext.Request.Host.ToString();
            host = Commons.GetPathImage(host);
            return host;
        }
    }
}
