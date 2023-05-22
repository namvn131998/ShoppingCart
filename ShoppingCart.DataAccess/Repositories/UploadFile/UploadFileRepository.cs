using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.Models;
using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.DataAccess.Repositories
{
    public class UploadFileRepository : Repository<UploadFile>, IUploadFileRepository
    {
        private ApplicationDbContext _context;
        public UploadFileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void UpdateProduectID (int mediaID, int productID)
        {
            var uploadfile = _context.UploadFiles.FirstOrDefault(u => u.MediaID == mediaID);
            if (uploadfile != null)
            {
                uploadfile.ProductID = productID;
            }
            _context.SaveChanges();
        }
        public string GetThumbnailFromUploadFile(int productID, string host)
        {
            var uploadfile = _context.UploadFiles.FirstOrDefault(u => u.ProductID == productID) ?? new UploadFile();
            return host + uploadfile.Thumbnail;
        }
        public List<UploadFile> GetThumbnailsFromUploadFile(int productID)
        {
            var uploadfile = _context.UploadFiles.Where(u => u.ProductID == productID).ToList();
            
            return uploadfile;
        }
    }
}
