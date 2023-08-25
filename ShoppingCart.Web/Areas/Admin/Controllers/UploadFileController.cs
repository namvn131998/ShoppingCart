using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Repositories;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Models.Category;
using ShoppingCart.DataAccess.Constants.Enums;
using ShoppingCart.Business.Utilities;
using ShoppingCart.DataAccess.Helper;
using X.PagedList;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UploadFileController : Controller
    {

        private IUnitOfWork _unitOfWork;
        private HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public UploadFileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string GetHostName()
        {
            var host = HttpContext.Request.Host.ToString();
            host = Commons.GetPathImage(host);
            return host;
        }
        [HttpGet]
        public IActionResult ShowUploadFile(string isProduct, int productID)
        {
            ViewBag.Host = GetHostName();
            ViewBag.isProduct = isProduct;
            ViewBag.productID = productID;
            return PartialView("_ShowUploadFile");
        }
        [HttpGet]
        public IActionResult ShowListMedia(int mediaid)
        {
            var host = HttpContext.Request.Host.ToString();
            host = Commons.GetPathImage(host);
            var Media = _unitOfWork.UploadFileRepository.GetT(x => x.MediaID == mediaid);
            ViewBag.Media = Media;
            ViewBag.Host = host;
            return PartialView("_ShowListMedia");
        }
        [HttpPost]
        public IActionResult DeleteMedia(int mediaid, int userID, string FileName = "", int productID = 0)
        {
            var uploadfile = _unitOfWork.UploadFileRepository.GetT(x => x.MediaID == mediaid);
            if (uploadfile == null)
            {
                return NotFound();
            }
            _unitOfWork.UploadFileRepository.Delete(uploadfile);
            _unitOfWork.Save();
            // get rootFolder of image
            var folderHost = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string userFolder = $"UserID-{userID}/ProductID-{productID}";
            var folderMedia = (MediaType)1;
            var pathFolderMedia = Path.Combine("Upload", userFolder, folderMedia.ToString());
            var rootFolder = Path.Combine(folderHost, pathFolderMedia);
            try
            {
                // Check if file exists with its full path
                if (System.IO.File.Exists((Path.Combine(rootFolder, FileName))))
                {
                    // If file found, delete it
                    System.IO.File.Delete(Path.Combine(rootFolder, FileName));
                }
            }
            catch (IOException ioExp)
            {
            }
            return Json(new { result = "OK" });
        }
        [HttpPost]
        public async Task<IActionResult> AddListThumbnail(List<IFormFile> files, int userID, int productID, int MediaTypeID , int UploadTypeID)
        {
            long mediaID = 0;
            string userFolder = "";
            var folderHost = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            switch (UploadTypeID)
            {
                case (int)UploadType.User:
                    userFolder = $"UserID-{userID}/Avatar";
                    
                    break;
                case (int)UploadType.Product:
                    userFolder = $"UserID-{userID}/ProductID-{productID}";
                    break;
            }
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    var extensionFile = Path.GetExtension(file.FileName);
                    if (extensionFile == ".jpg" || extensionFile == ".png")
                    {
                        mediaID = await SaveThumbnail(file, folderHost, userID, userFolder, MediaTypeID, UploadTypeID,productID);
                    }
                }
            }
            return Ok(mediaID);
        }
        private async Task<long> SaveThumbnail(IFormFile file, string folderHost, int userID, string userFolder, int MediaTypeID, int UploadTypeID, int productID)
        {
            var folderMedia = (MediaType)MediaTypeID;
            var pathFolderMedia = Path.Combine("Upload", userFolder, folderMedia.ToString());
            var fullpathFolderMedia = Path.Combine(folderHost, pathFolderMedia);
            int id = 0;
            if (!Directory.Exists(fullpathFolderMedia))
            {
                Directory.CreateDirectory(fullpathFolderMedia);
            }
            var fullPathFile = Path.Combine(fullpathFolderMedia, file.FileName);
            using (var stream = new FileStream(fullPathFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var isExist = CheckExistMedia(file.FileName, productID);
            if (!isExist)
            {
                var pathFile = Path.Combine(pathFolderMedia, file.FileName).Replace(@"\", "/");
                var uploadfile = new UploadFile
                {
                    FileName = file.FileName,
                    Thumbnail = pathFile,
                    UploadDate = DateTime.Now,
                    MediaTypeID = MediaTypeID,
                    UploadTypeID = UploadTypeID,
                    UserID = userID,
                    ProductID = productID
                };
                _unitOfWork.UploadFileRepository.Add(uploadfile);
                _unitOfWork.Save();
                id = uploadfile.MediaID;
            }
            return id;
        }
        public bool CheckExistMedia(string FileName, int ProductID)
        {
            var isExist = true;
            var uploadFile = _unitOfWork.UploadFileRepository.GetT(x => x.FileName == FileName && x.ProductID == ProductID);
            if (uploadFile == null) 
            {
                isExist = false;
            }
            return isExist;
        }
    }
}

