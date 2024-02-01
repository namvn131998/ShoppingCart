using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IRegistrationRepository RegistrationRepository { get; }
        IUploadFileRepository UploadFileRepository { get; }
        void Save();
    }
}
