using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork (ApplicationDbContext db)
        {
            _db = db;

            Category = new CategoryRespository(_db);

            CoverTypes = new CoverTypeRespository(_db);

            Products = new ProductRespository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverTypes { get; private set; }
        public IProductRepository Products { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
