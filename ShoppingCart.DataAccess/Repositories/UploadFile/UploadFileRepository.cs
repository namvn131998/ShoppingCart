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
        public string GetThumbnailFromUploadFile(int mediaID, string host)
        {
            var uploadfile = _context.UploadFiles.FirstOrDefault(u => u.MediaID == mediaID) ?? new UploadFile();
            return host + uploadfile.Thumbnail;
        }
        public List<UploadFile> GetThumbnailsFromUploadFile(int mediaID)
        {
            var uploadfile = _context.UploadFiles.Where(u => u.MediaID == mediaID).ToList();
            
            return uploadfile;
        }
    }
}
