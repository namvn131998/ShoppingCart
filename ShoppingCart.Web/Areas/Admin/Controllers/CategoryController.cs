using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Repositories;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.DataAccess;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Models.Category;
using X.PagedList;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateOrUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unitOfWork.CategoryRepository.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }
        [HttpPost]
        public IActionResult CreateOrUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.Category.Id == 0)
                {
                    _unitOfWork.CategoryRepository.Add(vm.Category);
                }
                else
                {
                    _unitOfWork.CategoryRepository.Update(vm.Category);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var category = _unitOfWork.CategoryRepository.GetT(x => x.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();
            return Json(new { result ="OK"});
        }
        [HttpGet]
        public IActionResult _List(CategoryListRequestModel cate)
        {
            ViewBag.SearchValue = cate.searchValue;
            CategoryVM categoryVM = new CategoryVM();
            if(string.IsNullOrEmpty(cate.searchValue))
                categoryVM.categories = _unitOfWork.CategoryRepository.GetAll();
            else
                categoryVM.categories = _unitOfWork.CategoryRepository.GetAll(s => s.Name == cate.searchValue);
            switch (cate.sortBy)
            {
                case "Name":
                    if (cate.sortDirection == "DESC")
                        categoryVM.categories = categoryVM.categories.OrderByDescending(x => x.Name);
                    else
                        categoryVM.categories = categoryVM.categories.OrderBy(x => x.Name);
                    break;
                default:
                    break;
            }
            return PartialView(categoryVM.categories.ToPagedList(cate.Page,cate.PageSize));
        }
    }
}
