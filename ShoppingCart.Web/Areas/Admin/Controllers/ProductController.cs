using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Repositories;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Models.Category;
using ShoppingCart.DataAccess.Constants.Enums;
using ShoppingCart.Business.Ultility;
using X.PagedList;


namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public ProductController(IUnitOfWork unitOfWork)
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
            var host = HttpContext.Request.Host.ToString();
            host = Commons.GetPathImage(host);
            ViewBag.Host = host;
            Product product = new Product();
            if (id == null || id == 0)
            {
                return View(product);
            }
            else
            {
                product = _unitOfWork.ProductRepository.GetT(x => x.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
        [HttpPost]
        public IActionResult CreateOrUpdate(Product product, string MediaIDs = "")
        {
            MediaIDs = MediaIDs.Trim(',');
            var ids = MediaIDs.Split(',').ToList();
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(product);
                }
                _unitOfWork.Save();
                int productID = product.Id;
                foreach (var item in ids)
                    _unitOfWork.UploadFileRepository.UpdateProduectID(Convert.ToInt32(item), productID);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.ProductRepository.GetT(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();
            return Json(new { result = "OK" });
        }
        [HttpGet]
        public IActionResult _List(ProductListRequestModel product)
        {
            ProductVM productVM = new ProductVM();
            if (string.IsNullOrEmpty(product.searchValue))
                productVM.Products = _unitOfWork.ProductRepository.GetAll();
            else
                productVM.Products = _unitOfWork.ProductRepository.GetAll(s => s.Name.Contains(product.searchValue));
            switch (product.sortBy)
            {
                case "Name":
                    if (product.sortDirection == "DESC")
                        productVM.Products = productVM.Products.OrderByDescending(x => x.Name);
                    else
                        productVM.Products = productVM.Products.OrderBy(x => x.Name);
                    break;
                default:
                    break;
            }
            return PartialView(productVM.Products.ToPagedList(product.Page, product.PageSize));
        }
        [HttpGet]
        public IActionResult ShowUploadFile()
        {
            return PartialView("_ShowUploadFile");
        }
        [HttpGet]
        public IActionResult ShowListMedia(int mediaid)
        {
            var host = HttpContext.Request.Host.ToString();
            host = Commons.GetPathImage(host);
            var Media = _unitOfWork.UploadFileRepository.GetT(x=> x.MediaID == mediaid);
            ViewBag.Media = Media;
            ViewBag.Host = host;
            return PartialView("_ShowListMedia");
        }
        [HttpPost]
        public IActionResult DeleteMedia(int mediaid)
        {
            var uploadfile = _unitOfWork.UploadFileRepository.GetT(x => x.MediaID == mediaid);
            if (uploadfile == null)
            {
                return NotFound();
            }
            _unitOfWork.UploadFileRepository.Delete(uploadfile);
            _unitOfWork.Save();
            return Json(new { result = "OK" });
        }
        [HttpPost]
        public async Task<IActionResult> AddListThumbnail (List<IFormFile> files, int userID)
        {
            long mediaID = 0;
            var folderHost = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string userFolder = $"UserID-{userID}";
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    var extensionFile = Path.GetExtension(file.FileName);
                    if (extensionFile == ".jpg" || extensionFile == ".png")
                    {
                        mediaID = await SaveThumbnail(file, folderHost, userID, userFolder);
                    }
                }
            }
            return Ok(mediaID);
        }
        private async Task<long> SaveThumbnail (IFormFile file, string folderHost, int userID, string userFolder)
        {
            var folderMedia = (MediaType)1;
            var pathFolderMedia = Path.Combine("upload",userFolder, folderMedia.ToString());
            var fullpathFolderMedia = Path.Combine(folderHost, pathFolderMedia);
            if (!Directory.Exists(fullpathFolderMedia))
            {
                Directory.CreateDirectory(fullpathFolderMedia);
            }
            var fullPathFile = Path.Combine(fullpathFolderMedia, file.FileName);
            using (var stream = new FileStream(fullPathFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var pathFile = Path.Combine(pathFolderMedia, file.FileName).Replace(@"\", "/");
            var uploadfile = new UploadFile
            {
                FileName = file.FileName,
                Thumbnail = pathFile,
                UploadDate = DateTime.Now,
                MediaTypeID = 1,
                UserID = userID
            };
            _unitOfWork.UploadFileRepository.Add(uploadfile);
            _unitOfWork.Save();
            int id = uploadfile.MediaID;
            return id;
        }
    }
}
