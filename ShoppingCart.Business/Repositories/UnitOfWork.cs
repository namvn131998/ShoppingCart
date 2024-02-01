using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Data;

namespace ShoppingCart.Business.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IRegistrationRepository RegistrationRepository { get; private set; }
        public IUploadFileRepository UploadFileRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
            RegistrationRepository = new RegistrationRepository(context);
            UploadFileRepository = new UploadFileRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
