using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.DataAccess.Repositories
{
    public interface IUploadFileRepository : IRepository<UploadFile>
    {
        void UpdateProduectID(int mediaID, int productID);
        string GetThumbnailFromUploadFile(int productID);
        List<UploadFile> GetThumbnailsFromUploadFile(int productID);
    }
}
